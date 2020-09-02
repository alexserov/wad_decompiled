// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.ScreenCapture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.System.Profile;

namespace MS.Internal.Mita.Foundation.Utilities {
    public static class ScreenCapture {
        static long XboxLastCaptureTicks;
        internal static INativeMethods nativeMethods = new NativeMethods();

        public static void SaveImage(string fileName, ImageFormat format = ImageFormat.Png) {
            SaveImage(rectangle: nativeMethods.GetClientArea(), fileName: fileName, format: format);
        }

        public static void SaveImage(RectangleI rectangle, string fileName, ImageFormat format = ImageFormat.Png) {
            SaveImage(bitmap: GetImage(rectangle: rectangle) ?? throw new Exception(message: "Unable to capture screen. GetImage returned null."), filename: fileName, format: format);
        }

        static ImageHelpers.IWICBitmap GetImage(RectangleI rectangle) {
            Log.Out(msg: "Dimensions: Left: {0} Top: {1} Right: {2} Bottom {3}", (object) rectangle.Left, (object) rectangle.Top, (object) rectangle.Right, (object) rectangle.Bottom);
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            if (deviceFamily.Equals(value: "Windows.Desktop", comparisonType: StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals(value: "Windows.Server", comparisonType: StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals(value: "Windows.Team", comparisonType: StringComparison.OrdinalIgnoreCase))
                return GetImageDesktop(rectangle: rectangle);
            if (deviceFamily.Equals(value: "Windows.Mobile", comparisonType: StringComparison.OrdinalIgnoreCase))
                return GetImageMobile(rectangle: rectangle);
            if (deviceFamily.Equals(value: "Windows.Xbox", comparisonType: StringComparison.OrdinalIgnoreCase))
                return GetImageXbox(rectangle: rectangle);
            throw new NotImplementedException(message: string.Format(format: "Screen Capture not implemented on this platform: {0}", arg0: deviceFamily));
        }

        static ImageHelpers.IWICBitmap GetImageDesktop(RectangleI rectangle) {
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            if (!deviceFamily.Equals(value: "Windows.Desktop", comparisonType: StringComparison.OrdinalIgnoreCase) && !deviceFamily.Equals(value: "Windows.Server", comparisonType: StringComparison.OrdinalIgnoreCase) && !deviceFamily.Equals(value: "Windows.Team", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(message: string.Format(format: "GetImageDesktop not supported on your platform: {0}", arg0: deviceFamily));
            var num1 = IntPtr.Zero;
            var num2 = IntPtr.Zero;
            var num3 = IntPtr.Zero;
            if (rectangle.Width <= 0)
                throw new ArgumentOutOfRangeException(paramName: "width", actualValue: rectangle.Width, message: StringResource.Get(id: "RectangleMustBeNonEmpty"));
            if (rectangle.Height <= 0)
                throw new ArgumentOutOfRangeException(paramName: "height", actualValue: rectangle.Height, message: StringResource.Get(id: "RectangleMustBeNonEmpty"));
            try {
                var zero = IntPtr.Zero;
                num1 = ImageHelpers.GDI.CreateDC(driver: "DISPLAY", device: IntPtr.Zero, output: IntPtr.Zero, devMode: IntPtr.Zero);
                if (num1 == IntPtr.Zero) {
                    var lastWin32Error = Marshal.GetLastWin32Error();
                    throw new DisplayException(message: StringResource.Get(id: "UnableToGetDC"), windowsError: lastWin32Error);
                }

                num2 = ImageHelpers.GDI.CreateCompatibleDC(dc: num1);
                if (num2 == IntPtr.Zero) {
                    var lastWin32Error = Marshal.GetLastWin32Error();
                    throw new DisplayException(message: StringResource.Get(id: "UnableToCreateDC"), windowsError: lastWin32Error);
                }

                num3 = ImageHelpers.GDI.CreateCompatibleBitmap(dc: num1, width: rectangle.Width, height: rectangle.Height);
                if (num3 == IntPtr.Zero) {
                    var lastWin32Error = Marshal.GetLastWin32Error();
                    throw new DisplayException(message: StringResource.Get(id: "UnableToCreateBitmap"), windowsError: lastWin32Error);
                }

                if (ImageHelpers.GDI.SelectObject(dc: num2, gdi: num3) == IntPtr.Zero) {
                    var lastWin32Error = Marshal.GetLastWin32Error();
                    throw new DisplayException(message: StringResource.Get(id: "CannotSelectBitmapIntoDC"), windowsError: lastWin32Error);
                }

                if (ImageHelpers.GDI.BitBlt(destDC: num2, xDest: 0, yDest: 0, width: rectangle.Width, height: rectangle.Height, sourceDC: num1, xSource: rectangle.Left, ySource: rectangle.Top, rasterOperation: 13369376U) == 0) {
                    var lastWin32Error = Marshal.GetLastWin32Error();
                    throw new DisplayException(message: StringResource.Get(id: "BitBlitFailed"), windowsError: lastWin32Error);
                }

                var imagingFactory = ImageHelpers.WIC.CreateImagingFactory();
                var num4 = new IntPtr();
                var hBitmap = num3;
                var hPalette = num4;
                return imagingFactory.CreateBitmapFromHBITMAP(hBitmap: hBitmap, hPalette: hPalette, options: ImageHelpers.WICBitmapAlphaChannelOption.WICBitmapIgnoreAlpha) ?? throw new DisplayException(message: StringResource.Get(id: "CreateBitmapFailed"));
            } finally {
                ImageHelpers.GDI.DeleteDC(dc: num1);
                ImageHelpers.GDI.DeleteDC(dc: num2);
                ImageHelpers.GDI.DeleteObject(gdiObject: num3);
            }
        }

        static ImageHelpers.IWICBitmap GetImageMobile(RectangleI rectangle) {
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            if (!deviceFamily.Equals(value: "Windows.Mobile", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(message: string.Format(format: "GetImageMobile not supported on your platform: {0}", arg0: deviceFamily));
            if (rectangle.Width <= 0)
                throw new ArgumentOutOfRangeException(paramName: "width", actualValue: rectangle.Width, message: StringResource.Get(id: "RectangleMustBeNonEmpty"));
            if (rectangle.Height <= 0)
                throw new ArgumentOutOfRangeException(paramName: "height", actualValue: rectangle.Height, message: StringResource.Get(id: "RectangleMustBeNonEmpty"));
            var bitmap = ImageHelpers.CaptureMobileScreenshot();
            if (bitmap == null)
                throw new ScreenCaptureException(message: "CaptureMobileScreenshot failed to capture screen. Returned bitmap is null.");
            uint puiWidth = 0;
            uint puiHeight = 0;
            bitmap.GetSize(puiWidth: out puiWidth, puiHeight: out puiHeight);
            if (rectangle.Width == puiWidth && rectangle.Height == puiHeight)
                return bitmap;
            Log.Out(msg: "Requested capture area is different than screen size:");
            Log.Out(msg: "    Requested: Width: {0} Height: {1} Left: {2} Top {3}", (object) rectangle.Width, (object) rectangle.Height, (object) rectangle.Left, (object) rectangle.Top);
            Log.Out(msg: "    Screen: Width: {0} Height: {1}", (object) puiWidth, (object) puiHeight);
            return ClipImage(bitmap: bitmap, rectangle: rectangle);
        }

        static ImageHelpers.IWICBitmap GetImageXbox(RectangleI rectangle) {
            var ticks = TimeSpan.FromMilliseconds(value: 1000.0).Ticks;
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            if (!deviceFamily.Equals(value: "Windows.Xbox", comparisonType: StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(message: string.Format(format: "GetImageXbox not supported on your platform: {0}", arg0: deviceFamily));
            if (rectangle.Width <= 0)
                throw new ArgumentOutOfRangeException(paramName: "width", actualValue: rectangle.Width, message: StringResource.Get(id: "RectangleMustBeNonEmpty"));
            if (rectangle.Height <= 0)
                throw new ArgumentOutOfRangeException(paramName: "height", actualValue: rectangle.Height, message: StringResource.Get(id: "RectangleMustBeNonEmpty"));
            var now = DateTime.Now;
            var num = now.Ticks - XboxLastCaptureTicks;
            if (num < ticks) {
                var timeSpan1 = TimeSpan.FromTicks(value: ticks);
                var timeSpan2 = TimeSpan.FromTicks(value: ticks - num);
                Log.Out(msg: "Cannot capture Xbox screenshots more than once every {0} milliseconds. Waiting {1} milliseconds before proceeding.", (object) timeSpan1.Milliseconds, (object) timeSpan2.Milliseconds);
                Thread.Sleep(millisecondsTimeout: timeSpan2.Milliseconds);
            }

            now = DateTime.Now;
            XboxLastCaptureTicks = now.Ticks;
            var bitmap = ImageHelpers.CaptureXboxScreenshot();
            if (bitmap == null)
                throw new ScreenCaptureException(message: "CaptureXboxScreenshot failed to capture screen. Returned bitmap is null.");
            uint puiWidth = 0;
            uint puiHeight = 0;
            bitmap.GetSize(puiWidth: out puiWidth, puiHeight: out puiHeight);
            if (rectangle.Width == puiWidth && rectangle.Height == puiHeight)
                return bitmap;
            Log.Out(msg: "Requested capture area is different than screen size:");
            Log.Out(msg: "    Requested: Width: {0} Height: {1} Left: {2} Top {3}", (object) rectangle.Width, (object) rectangle.Height, (object) rectangle.Left, (object) rectangle.Top);
            Log.Out(msg: "    Screen: Width: {0} Height: {1}", (object) puiWidth, (object) puiHeight);
            return ClipImage(bitmap: bitmap, rectangle: rectangle);
        }

        static void SaveImage(
            ImageHelpers.IWICBitmap bitmap,
            string filename,
            ImageFormat format) {
            if (bitmap == null)
                throw new ArgumentNullException(paramName: nameof(bitmap), message: "bitmap cannot be null.");
            if (filename == null)
                throw new ArgumentNullException(paramName: nameof(filename), message: "filename cannot be null.");
            var imagingFactory = ImageHelpers.WIC.CreateImagingFactory();
            var stream = imagingFactory.CreateStream();
            stream.InitializeFromFilename(wzFileName: filename, dwDesiredAccess: ImageHelpers.GenericAccessRights.GENERIC_WRITE);
            var encoder = imagingFactory.CreateEncoder(guidContainerFormat: ImageHelpers.GetContainerFormat(format: format), pguidVendor: null);
            encoder.Initialize(pIStream: stream, cacheOption: ImageHelpers.WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);
            ImageHelpers.IWICBitmapFrameEncode ppIFrameEncode = null;
            var encoderOptions = new ImageHelpers.IPropertyBag2[1];
            encoder.CreateNewFrame(ppIFrameEncode: out ppIFrameEncode, encoderOptions: encoderOptions);
            ppIFrameEncode.Initialize(pIEncoderOptions: encoderOptions[0]);
            ppIFrameEncode.WriteSource(pIBitmapSource: bitmap, prc: null);
            ppIFrameEncode.Commit();
            encoder.Commit();
            Marshal.ReleaseComObject(o: stream);
            Marshal.ReleaseComObject(o: encoder);
            Marshal.ReleaseComObject(o: ppIFrameEncode);
        }

        static ImageHelpers.IWICBitmap ClipImage(
            ImageHelpers.IWICBitmap bitmap,
            RectangleI rectangle) {
            var prc = new ImageHelpers.WICRect();
            prc.X = rectangle.X;
            prc.Y = rectangle.Y;
            prc.Width = rectangle.Width;
            prc.Height = rectangle.Height;
            var imagingFactory = ImageHelpers.WIC.CreateImagingFactory();
            var bitmapClipper = imagingFactory.CreateBitmapClipper();
            bitmapClipper.Initialize(pISource: bitmap, prc: prc);
            var bitmapFromSource = imagingFactory.CreateBitmapFromSource(pIBitmapSource: bitmapClipper, option: ImageHelpers.WICBitmapCreateCacheOption.WICBitmapNoCache);
            Marshal.ReleaseComObject(o: bitmapClipper);
            return bitmapFromSource;
        }

        [StructLayout(layoutKind: LayoutKind.Sequential, Size = 1)]
        struct RasterOperation {
            public const uint SRCCOPY = 13369376;
        }
    }
}