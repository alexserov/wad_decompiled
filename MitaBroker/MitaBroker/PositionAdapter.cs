// Decompiled with JetBrains decompiler
// Type: MitaBroker.PositionAdapter
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal static class PositionAdapter {
        private const int E_HANDLE = -2147024890;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ClientToScreen(IntPtr hwnd, ref POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ScreenToClient(IntPtr hwnd, ref POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClientRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int MapWindowPoints(
            IntPtr hwndFrom,
            IntPtr hwndTo,
            ref RECT rect,
            int cPoints);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(
            IntPtr hwnd,
            DWMWINDOWATTRIBUTE dwAttribute,
            out RECT lpRect,
            int cbAttribute);

        private static void Convert(
            ConvertMode mode,
            IntPtr hwnd,
            int xSource,
            int ySource,
            out int xTarget,
            out int yTarget) {
            if (hwnd == UIObject.Root.NativeWindowHandle) {
                xTarget = xSource;
                yTarget = ySource;
            } else {
                POINT lpPoint = new POINT(xSource, ySource);
                RECT lpRect = new RECT();
                if (!GetClientRect(hwnd, out lpRect))
                    throw new ExternalException("GetClientRect failed.", Marshal.GetHRForLastWin32Error());
                if (MapWindowPoints(hwnd, IntPtr.Zero, ref lpRect, 2) == 0) {
                    int lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 0)
                        throw new ExternalException("MapWindowPoints failed.", lastWin32Error);
                }

                RECT extendedFrameBoundsRect = new RECT();
                int dwmRectangle = GetDwmRectangle(hwnd, ref extendedFrameBoundsRect);
                switch (dwmRectangle) {
                    case -2147024890:
                    case 0:
                        POINT point = new POINT();
                        point.X = lpRect.Left - extendedFrameBoundsRect.Left;
                        point.Y = lpRect.Top - extendedFrameBoundsRect.Top;
                        switch (mode) {
                            case ConvertMode.ClientToScreen:
                                if (!ClientToScreen(hwnd, ref lpPoint))
                                    throw new ExternalException("ClientToScreen failed.", Marshal.GetHRForLastWin32Error());
                                lpPoint.X -= point.X;
                                lpPoint.Y -= point.Y;
                                break;
                            case ConvertMode.ScreenToClient:
                                if (!ScreenToClient(hwnd, ref lpPoint))
                                    throw new ExternalException("ScreenToClient failed.", Marshal.GetHRForLastWin32Error());
                                lpPoint.X += point.X;
                                lpPoint.Y += point.Y;
                                break;
                        }

                        xTarget = lpPoint.X;
                        yTarget = lpPoint.Y;
                        break;
                    default:
                        throw new ExternalException("DwmGetWindowAttribute failed.", dwmRectangle);
                }
            }
        }

        public static void ConvertClientToScreen(
            IntPtr hwnd,
            int xClient,
            int yClient,
            out int xScreen,
            out int yScreen) => Convert(ConvertMode.ClientToScreen, hwnd, xClient, yClient, out xScreen, out yScreen);

        public static void ConvertScreenToClient(
            IntPtr hwnd,
            int xScreen,
            int yScreen,
            out int xClient,
            out int yClient) => Convert(ConvertMode.ScreenToClient, hwnd, xScreen, yScreen, out xClient, out yClient);

        public static PointI GetAbsolutePoint(PointI relativePoint, PointI referencePoint) => new PointI(referencePoint.X + relativePoint.X, referencePoint.Y + relativePoint.Y);

        public static PointI GetRelativePoint(PointI absolutePoint, PointI referencePoint) => new PointI(absolutePoint.X - referencePoint.X, absolutePoint.Y - referencePoint.Y);

        public static PointI GetBoundingRectangleTopLeft(
            this UIObject obj,
            IntPtr appRootWindowHandle) => obj.BoundingRectangle.GetWindowTopLeft(appRootWindowHandle);

        public static PointI GetWindowTopLeft(this RectangleI rect, IntPtr nativeWindowHandle) {
            int xClient;
            int yClient;
            ConvertScreenToClient(nativeWindowHandle, rect.Left, rect.Top, out xClient, out yClient);
            return new PointI(xClient, yClient);
        }

        public static RectangleI GetAdjustedBoundingRectangle(this UIObject obj) {
            RectangleI rectangleI = obj.BoundingRectangle;
            if (obj != UIObject.Root) {
                IntPtr nativeWindowHandle = obj.NativeWindowHandle;
                if (nativeWindowHandle != IntPtr.Zero) {
                    RECT extendedFrameBoundsRect = new RECT();
                    int dwmRectangle = GetDwmRectangle(nativeWindowHandle, ref extendedFrameBoundsRect);
                    switch (dwmRectangle) {
                        case -2147024890:
                            break;
                        case 0:
                            rectangleI = new RectangleI(extendedFrameBoundsRect.Left, extendedFrameBoundsRect.Top, extendedFrameBoundsRect.Right - extendedFrameBoundsRect.Left, extendedFrameBoundsRect.Bottom - extendedFrameBoundsRect.Top);
                            break;
                        default:
                            throw new ExternalException("DwmGetWindowAttribute failed.", dwmRectangle);
                    }
                }
            }

            return rectangleI;
        }

        public static PointI GetAdjustedBoundingRectangleCenterPosition(this UIObject obj) {
            RectangleI boundingRectangle = obj.GetAdjustedBoundingRectangle();
            return new PointI(boundingRectangle.Left + boundingRectangle.Width / 2, boundingRectangle.Top + boundingRectangle.Height / 2);
        }

        public static PointI GetAdjustedBoundingRectangleOffsetPosition(this UIObject obj) {
            RectangleI boundingRectangle1 = obj.GetAdjustedBoundingRectangle();
            RectangleI boundingRectangle2 = obj.BoundingRectangle;
            return new PointI(boundingRectangle2.Left - boundingRectangle1.Left, boundingRectangle2.Top - boundingRectangle1.Top);
        }

        public static SizeI GetAdjustedBoundingRectangleOffsetSize(this UIObject obj) {
            RectangleI boundingRectangle1 = obj.GetAdjustedBoundingRectangle();
            RectangleI boundingRectangle2 = obj.BoundingRectangle;
            return new SizeI(boundingRectangle2.Width - boundingRectangle1.Width, boundingRectangle2.Height - boundingRectangle1.Height);
        }

        private static int GetDwmRectangle(
            IntPtr hwnd,
            ref RECT extendedFrameBoundsRect) => DwmGetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out extendedFrameBoundsRect, Marshal.SizeOf<RECT>(extendedFrameBoundsRect));

        private struct POINT {
            public int X;
            public int Y;

            public POINT(PointI mitaPoint) {
                this.X = mitaPoint.X;
                this.Y = mitaPoint.Y;
            }

            public POINT(int x, int y) {
                this.X = x;
                this.Y = y;
            }
        }

        private struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom) {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        private enum DWMWINDOWATTRIBUTE : uint {
            DWMWA_EXTENDED_FRAME_BOUNDS = 9
        }

        private enum ConvertMode {
            ClientToScreen,
            ScreenToClient
        }
    }
}