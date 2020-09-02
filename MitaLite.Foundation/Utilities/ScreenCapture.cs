// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.ScreenCapture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using Windows.System.Profile;

namespace MS.Internal.Mita.Foundation.Utilities
{
  public static class ScreenCapture
  {
    private static long XboxLastCaptureTicks = 0;
    internal static INativeMethods nativeMethods = (INativeMethods) new MS.Internal.Mita.Foundation.NativeMethods();

    public static void SaveImage(string fileName, ImageFormat format = ImageFormat.Png) => ScreenCapture.SaveImage(ScreenCapture.nativeMethods.GetClientArea(), fileName, format);

    public static void SaveImage(RectangleI rectangle, string fileName, ImageFormat format = ImageFormat.Png) => ScreenCapture.SaveImage(ScreenCapture.GetImage(rectangle) ?? throw new Exception("Unable to capture screen. GetImage returned null."), fileName, format);

    private static ImageHelpers.IWICBitmap GetImage(RectangleI rectangle)
    {
      Log.Out("Dimensions: Left: {0} Top: {1} Right: {2} Bottom {3}", (object) rectangle.Left, (object) rectangle.Top, (object) rectangle.Right, (object) rectangle.Bottom);
      string deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
      if (deviceFamily.Equals("Windows.Desktop", StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals("Windows.Server", StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals("Windows.Team", StringComparison.OrdinalIgnoreCase))
        return ScreenCapture.GetImageDesktop(rectangle);
      if (deviceFamily.Equals("Windows.Mobile", StringComparison.OrdinalIgnoreCase))
        return ScreenCapture.GetImageMobile(rectangle);
      if (deviceFamily.Equals("Windows.Xbox", StringComparison.OrdinalIgnoreCase))
        return ScreenCapture.GetImageXbox(rectangle);
      throw new NotImplementedException(string.Format("Screen Capture not implemented on this platform: {0}", (object) deviceFamily));
    }

    private static ImageHelpers.IWICBitmap GetImageDesktop(RectangleI rectangle)
    {
      string deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
      if (!deviceFamily.Equals("Windows.Desktop", StringComparison.OrdinalIgnoreCase) && !deviceFamily.Equals("Windows.Server", StringComparison.OrdinalIgnoreCase) && !deviceFamily.Equals("Windows.Team", StringComparison.OrdinalIgnoreCase))
        throw new InvalidOperationException(string.Format("GetImageDesktop not supported on your platform: {0}", (object) deviceFamily));
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      IntPtr num3 = IntPtr.Zero;
      if (rectangle.Width <= 0)
        throw new ArgumentOutOfRangeException("width", (object) rectangle.Width, StringResource.Get("RectangleMustBeNonEmpty"));
      if (rectangle.Height <= 0)
        throw new ArgumentOutOfRangeException("height", (object) rectangle.Height, StringResource.Get("RectangleMustBeNonEmpty"));
      try
      {
        IntPtr zero = IntPtr.Zero;
        num1 = ImageHelpers.GDI.CreateDC("DISPLAY", IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
        if (num1 == IntPtr.Zero)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw new DisplayException(StringResource.Get("UnableToGetDC"), lastWin32Error);
        }
        num2 = ImageHelpers.GDI.CreateCompatibleDC(num1);
        if (num2 == IntPtr.Zero)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw new DisplayException(StringResource.Get("UnableToCreateDC"), lastWin32Error);
        }
        num3 = ImageHelpers.GDI.CreateCompatibleBitmap(num1, rectangle.Width, rectangle.Height);
        if (num3 == IntPtr.Zero)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw new DisplayException(StringResource.Get("UnableToCreateBitmap"), lastWin32Error);
        }
        if (ImageHelpers.GDI.SelectObject(num2, num3) == IntPtr.Zero)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw new DisplayException(StringResource.Get("CannotSelectBitmapIntoDC"), lastWin32Error);
        }
        if (ImageHelpers.GDI.BitBlt(num2, 0, 0, rectangle.Width, rectangle.Height, num1, rectangle.Left, rectangle.Top, 13369376U) == 0)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw new DisplayException(StringResource.Get("BitBlitFailed"), lastWin32Error);
        }
        ImageHelpers.IWICImagingFactory imagingFactory = ImageHelpers.WIC.CreateImagingFactory();
        IntPtr num4 = new IntPtr();
        IntPtr hBitmap = num3;
        IntPtr hPalette = num4;
        return imagingFactory.CreateBitmapFromHBITMAP(hBitmap, hPalette, ImageHelpers.WICBitmapAlphaChannelOption.WICBitmapIgnoreAlpha) ?? throw new DisplayException(StringResource.Get("CreateBitmapFailed"));
      }
      finally
      {
        ImageHelpers.GDI.DeleteDC(num1);
        ImageHelpers.GDI.DeleteDC(num2);
        ImageHelpers.GDI.DeleteObject(num3);
      }
    }

    private static ImageHelpers.IWICBitmap GetImageMobile(RectangleI rectangle)
    {
      string deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
      if (!deviceFamily.Equals("Windows.Mobile", StringComparison.OrdinalIgnoreCase))
        throw new InvalidOperationException(string.Format("GetImageMobile not supported on your platform: {0}", (object) deviceFamily));
      if (rectangle.Width <= 0)
        throw new ArgumentOutOfRangeException("width", (object) rectangle.Width, StringResource.Get("RectangleMustBeNonEmpty"));
      if (rectangle.Height <= 0)
        throw new ArgumentOutOfRangeException("height", (object) rectangle.Height, StringResource.Get("RectangleMustBeNonEmpty"));
      ImageHelpers.IWICBitmap bitmap = ImageHelpers.CaptureMobileScreenshot();
      if (bitmap == null)
        throw new ScreenCaptureException("CaptureMobileScreenshot failed to capture screen. Returned bitmap is null.");
      uint puiWidth = 0;
      uint puiHeight = 0;
      bitmap.GetSize(out puiWidth, out puiHeight);
      if ((long) rectangle.Width == (long) puiWidth && (long) rectangle.Height == (long) puiHeight)
        return bitmap;
      Log.Out("Requested capture area is different than screen size:");
      Log.Out("    Requested: Width: {0} Height: {1} Left: {2} Top {3}", (object) rectangle.Width, (object) rectangle.Height, (object) rectangle.Left, (object) rectangle.Top);
      Log.Out("    Screen: Width: {0} Height: {1}", (object) puiWidth, (object) puiHeight);
      return ScreenCapture.ClipImage(bitmap, rectangle);
    }

    private static ImageHelpers.IWICBitmap GetImageXbox(RectangleI rectangle)
    {
      long ticks = TimeSpan.FromMilliseconds(1000.0).Ticks;
      string deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
      if (!deviceFamily.Equals("Windows.Xbox", StringComparison.OrdinalIgnoreCase))
        throw new InvalidOperationException(string.Format("GetImageXbox not supported on your platform: {0}", (object) deviceFamily));
      if (rectangle.Width <= 0)
        throw new ArgumentOutOfRangeException("width", (object) rectangle.Width, StringResource.Get("RectangleMustBeNonEmpty"));
      if (rectangle.Height <= 0)
        throw new ArgumentOutOfRangeException("height", (object) rectangle.Height, StringResource.Get("RectangleMustBeNonEmpty"));
      DateTime now = DateTime.Now;
      long num = now.Ticks - ScreenCapture.XboxLastCaptureTicks;
      if (num < ticks)
      {
        TimeSpan timeSpan1 = TimeSpan.FromTicks(ticks);
        TimeSpan timeSpan2 = TimeSpan.FromTicks(ticks - num);
        Log.Out("Cannot capture Xbox screenshots more than once every {0} milliseconds. Waiting {1} milliseconds before proceeding.", (object) timeSpan1.Milliseconds, (object) timeSpan2.Milliseconds);
        Thread.Sleep(timeSpan2.Milliseconds);
      }
      now = DateTime.Now;
      ScreenCapture.XboxLastCaptureTicks = now.Ticks;
      ImageHelpers.IWICBitmap bitmap = ImageHelpers.CaptureXboxScreenshot();
      if (bitmap == null)
        throw new ScreenCaptureException("CaptureXboxScreenshot failed to capture screen. Returned bitmap is null.");
      uint puiWidth = 0;
      uint puiHeight = 0;
      bitmap.GetSize(out puiWidth, out puiHeight);
      if ((long) rectangle.Width == (long) puiWidth && (long) rectangle.Height == (long) puiHeight)
        return bitmap;
      Log.Out("Requested capture area is different than screen size:");
      Log.Out("    Requested: Width: {0} Height: {1} Left: {2} Top {3}", (object) rectangle.Width, (object) rectangle.Height, (object) rectangle.Left, (object) rectangle.Top);
      Log.Out("    Screen: Width: {0} Height: {1}", (object) puiWidth, (object) puiHeight);
      return ScreenCapture.ClipImage(bitmap, rectangle);
    }

    private static void SaveImage(
      ImageHelpers.IWICBitmap bitmap,
      string filename,
      ImageFormat format)
    {
      if (bitmap == null)
        throw new ArgumentNullException(nameof (bitmap), "bitmap cannot be null.");
      if (filename == null)
        throw new ArgumentNullException(nameof (filename), "filename cannot be null.");
      ImageHelpers.IWICImagingFactory imagingFactory = ImageHelpers.WIC.CreateImagingFactory();
      ImageHelpers.IWICStream stream = imagingFactory.CreateStream();
      stream.InitializeFromFilename(filename, ImageHelpers.GenericAccessRights.GENERIC_WRITE);
      ImageHelpers.IWICBitmapEncoder encoder = imagingFactory.CreateEncoder(ImageHelpers.GetContainerFormat(format), (Guid[]) null);
      encoder.Initialize((IStream) stream, ImageHelpers.WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);
      ImageHelpers.IWICBitmapFrameEncode ppIFrameEncode = (ImageHelpers.IWICBitmapFrameEncode) null;
      ImageHelpers.IPropertyBag2[] encoderOptions = new ImageHelpers.IPropertyBag2[1];
      encoder.CreateNewFrame(out ppIFrameEncode, encoderOptions);
      ppIFrameEncode.Initialize(encoderOptions[0]);
      ppIFrameEncode.WriteSource(bitmap, (ImageHelpers.WICRect) null);
      ppIFrameEncode.Commit();
      encoder.Commit();
      Marshal.ReleaseComObject((object) stream);
      Marshal.ReleaseComObject((object) encoder);
      Marshal.ReleaseComObject((object) ppIFrameEncode);
    }

    private static ImageHelpers.IWICBitmap ClipImage(
      ImageHelpers.IWICBitmap bitmap,
      RectangleI rectangle)
    {
      ImageHelpers.WICRect prc = new ImageHelpers.WICRect();
      prc.X = rectangle.X;
      prc.Y = rectangle.Y;
      prc.Width = rectangle.Width;
      prc.Height = rectangle.Height;
      ImageHelpers.IWICImagingFactory imagingFactory = ImageHelpers.WIC.CreateImagingFactory();
      ImageHelpers.IWICBitmapClipper bitmapClipper = imagingFactory.CreateBitmapClipper();
      bitmapClipper.Initialize((ImageHelpers.IWICBitmapSource) bitmap, prc);
      ImageHelpers.IWICBitmap bitmapFromSource = imagingFactory.CreateBitmapFromSource((ImageHelpers.IWICBitmapSource) bitmapClipper, ImageHelpers.WICBitmapCreateCacheOption.WICBitmapNoCache);
      Marshal.ReleaseComObject((object) bitmapClipper);
      return bitmapFromSource;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct RasterOperation
    {
      public const uint SRCCOPY = 13369376;
    }
  }
}
