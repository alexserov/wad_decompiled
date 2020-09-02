// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.ImageHelpers
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace MS.Internal.Mita.Foundation.Utilities
{
  internal static class ImageHelpers
  {
    private const int WINCODEC_SDK_VERSION = 566;
    private const int MaxScreenshotWidth = 3840;
    private const int MaxScreenshotHeight = 2160;
    private const int RGBPixelSize = 3;
    private static readonly Guid GUID_ContainerFormatBmp = new Guid(183621758U, (ushort) 64766, (ushort) 16776, (byte) 189, (byte) 235, (byte) 167, (byte) 144, (byte) 100, (byte) 113, (byte) 203, (byte) 227);
    private static readonly Guid GUID_ContainerFormatPng = new Guid(461175540, (short) 28991, (short) 18236, (byte) 187, (byte) 205, (byte) 97, (byte) 55, (byte) 66, (byte) 95, (byte) 174, (byte) 175);
    private static readonly Guid GUID_ContainerFormatIco = new Guid(2745721028U, (ushort) 13199, (ushort) 19479, (byte) 145, (byte) 154, (byte) 251, (byte) 164, (byte) 181, (byte) 98, (byte) 143, (byte) 33);
    private static readonly Guid GUID_ContainerFormatJpeg = new Guid(434415018, (short) 22114, (short) 20421, (byte) 160, (byte) 192, (byte) 23, (byte) 88, (byte) 2, (byte) 142, (byte) 16, (byte) 87);
    private static readonly Guid GUID_ContainerFormatTiff = new Guid(373017648U, (ushort) 58089, (ushort) 20235, (byte) 150, (byte) 29, (byte) 163, (byte) 233, (byte) 253, (byte) 183, (byte) 136, (byte) 163);
    private static readonly Guid GUID_ContainerFormatGif = new Guid(529159681, (short) 32077, (short) 19645, (byte) 156, (byte) 130, (byte) 27, (byte) 200, (byte) 212, (byte) 238, (byte) 185, (byte) 165);
    private static readonly Guid GUID_ContainerFormatWmp = new Guid(1470332074, (short) 13946, (short) 17728, (byte) 145, (byte) 107, (byte) 241, (byte) 131, (byte) 197, (byte) 9, (byte) 58, (byte) 75);
    private static readonly Guid GUID_WICPixelFormat24bppRGB = new Guid(1876804388, (short) 19971, (short) 19454, (byte) 177, (byte) 133, (byte) 61, (byte) 119, (byte) 118, (byte) 141, (byte) 201, (byte) 13);

    public static Guid GetContainerFormat(ImageFormat format)
    {
      switch (format)
      {
        case ImageFormat.Bmp:
          return ImageHelpers.GUID_ContainerFormatBmp;
        case ImageFormat.Gif:
          return ImageHelpers.GUID_ContainerFormatGif;
        case ImageFormat.Ico:
          return ImageHelpers.GUID_ContainerFormatIco;
        case ImageFormat.Jpeg:
          return ImageHelpers.GUID_ContainerFormatJpeg;
        case ImageFormat.Png:
          return ImageHelpers.GUID_ContainerFormatPng;
        case ImageFormat.Tiff:
          return ImageHelpers.GUID_ContainerFormatTiff;
        case ImageFormat.Wmp:
          return ImageHelpers.GUID_ContainerFormatWmp;
        default:
          return ImageHelpers.GUID_ContainerFormatPng;
      }
    }

    public static ImageHelpers.IWICBitmap CaptureMobileScreenshot()
    {
      ImageHelpers.IWICBitmap[] ppBitmap = new ImageHelpers.IWICBitmap[1];
      int num = ImageHelpers.NativeMethods.CaptureScreen(0U, ppBitmap);
      if (num != 0)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        throw new DisplayException(string.Format("CaptureScreen failed with result: {0} and error: {1}", (object) num, (object) lastWin32Error));
      }
      Log.Out("CaptureScreen succeeded with result: {0}", (object) num);
      return ppBitmap[0];
    }

    public static ImageHelpers.IWICBitmap CaptureXboxScreenshot()
    {
      int cb = 24883200;
      IntPtr num = Marshal.AllocCoTaskMem(cb);
      ulong size = (ulong) (uint) cb;
      long width = 0;
      long height = 0;
      long pitch = 0;
      long bitsPerChannel = 0;
      try
      {
        int windowsError = ImageHelpers.NativeMethods.CaptureScreenshot(num, size, out width, out height, out pitch, out bitsPerChannel);
        if (windowsError != 0)
          throw new ScreenCaptureException(string.Format("CaptureXboxScreenshot failed with result: {0}", (object) windowsError), windowsError);
      }
      catch (Exception ex)
      {
        throw new ScreenCaptureException(string.Format("CaptureXboxScreenshot threw '{0}' with message \"{1}'\"", (object) ((object) ex).GetType().ToString(), (object) ex.Message), ex);
      }
      if (height == 0L || width == 0L || (pitch == 0L || bitsPerChannel == 0L))
        throw new ScreenCaptureException(string.Format("CaptureXboxScreenshot returned success but a returned value is 0. H: {0} W: {1} P: {2} BPC: {3}", (object) height, (object) width, (object) pitch, (object) bitsPerChannel));
      Log.Out("CaptureXboxScreenshot call succeeded. H: {0} W: {1} P: {2} BPC: {3}", (object) height, (object) width, (object) pitch, (object) bitsPerChannel);
      int length = (int) (height * width * 3L);
      byte[] numArray = length <= cb ? new byte[length] : throw new ScreenCaptureException(string.Format("CaptureXboxScreenshot returned a larger size screenshot than the allocated buffer. H: {0} W: {1} P: {2} BPC: {3}", (object) height, (object) width, (object) pitch, (object) bitsPerChannel));
      Marshal.Copy(num, numArray, 0, length);
      Marshal.FreeCoTaskMem(num);
      return ImageHelpers.WIC.CreateImagingFactory().CreateBitmapFromMemory((uint) width, (uint) height, ImageHelpers.GUID_WICPixelFormat24bppRGB, (uint) pitch, (uint) numArray.Length, numArray) ?? throw new ScreenCaptureException("CreateBitmapFromMemory returned null.");
    }

    public static class GDI
    {
      public static IntPtr CreateCompatibleBitmap(IntPtr dc, int width, int height) => ImageHelpers.NativeMethods.CreateCompatibleBitmap(dc, width, height);

      public static int BitBlt(
        IntPtr destDC,
        int xDest,
        int yDest,
        int width,
        int height,
        IntPtr sourceDC,
        int xSource,
        int ySource,
        uint rasterOperation) => ImageHelpers.NativeMethods.BitBlt(destDC, xDest, yDest, width, height, sourceDC, xSource, ySource, rasterOperation);

      public static IntPtr CreateDC(
        string driver,
        IntPtr device,
        IntPtr output,
        IntPtr devMode) => ImageHelpers.NativeMethods.CreateDC(driver, device, output, devMode);

      public static IntPtr CreateCompatibleDC(IntPtr dc) => ImageHelpers.NativeMethods.CreateCompatibleDC(dc);

      public static int DeleteDC(IntPtr dc) => ImageHelpers.NativeMethods.DeleteDC(dc);

      public static IntPtr CreatePalette(byte[] lplgpl) => ImageHelpers.NativeMethods.CreatePalette(lplgpl);

      public static IntPtr SelectObject(IntPtr dc, IntPtr gdi) => ImageHelpers.NativeMethods.SelectObject(dc, gdi);

      public static int DeleteObject(IntPtr gdiObject) => ImageHelpers.NativeMethods.DeleteObject(gdiObject);
    }

    public static class WIC
    {
      private static ImageHelpers.IWICImagingFactory factory;

      public static ImageHelpers.IWICImagingFactory CreateImagingFactory()
      {
        if (ImageHelpers.WIC.factory == null)
          ImageHelpers.WIC.factory = ImageHelpers.NativeMethods.WICCreateImagingFactory_Proxy(566U);
        return ImageHelpers.WIC.factory;
      }
    }

    private static class NativeMethods
    {
      private const string WINCODECS_DLL = "windowscodecs.dll";
      private const string GDIDRAW_DLL = "ext-ms-win-gdi-draw-l1-1-1";
      private const string GDIDCCREATE_DLL = "ext-ms-win-gdi-dc-create-l1-1-1";
      private const string GDIDC_DLL = "ext-ms-win-gdi-dc-l1-2-0";
      private const string GDIOBJECT_DLL = "ext-ms-win-rtcore-gdi-object-l1-1-0";
      private const string DWMMOBILE_DLL = "dwmmobile.dll";
      private const string CONSOLECONTROL_DLL = "xtfconsolecontrol.dll";

      [DllImport("windowscodecs.dll", PreserveSig = false)]
      internal static extern ImageHelpers.IWICImagingFactory WICCreateImagingFactory_Proxy(
        uint SDKVersion);

      [DllImport("ext-ms-win-gdi-draw-l1-1-1", SetLastError = true)]
      internal static extern IntPtr CreateCompatibleBitmap(IntPtr dc, int width, int height);

      [DllImport("ext-ms-win-gdi-draw-l1-1-1", SetLastError = true)]
      internal static extern int BitBlt(
        IntPtr destDC,
        int xDest,
        int yDest,
        int width,
        int height,
        IntPtr sourceDC,
        int xSource,
        int ySource,
        uint rasterOperation);

      [DllImport("ext-ms-win-gdi-dc-create-l1-1-1", EntryPoint = "CreateDCW", CharSet = CharSet.Unicode, SetLastError = true)]
      internal static extern IntPtr CreateDC(
        string driver,
        IntPtr device,
        IntPtr output,
        IntPtr devMode);

      [DllImport("ext-ms-win-gdi-dc-create-l1-1-1", SetLastError = true)]
      internal static extern IntPtr CreateCompatibleDC(IntPtr dc);

      [DllImport("ext-ms-win-gdi-dc-create-l1-1-1")]
      internal static extern int DeleteDC(IntPtr dc);

      [DllImport("ext-ms-win-gdi-dc-l1-2-0")]
      internal static extern IntPtr CreatePalette([MarshalAs(UnmanagedType.LPArray)] byte[] lplgpl);

      [DllImport("ext-ms-win-gdi-dc-l1-2-0", SetLastError = true)]
      internal static extern IntPtr SelectObject(IntPtr dc, IntPtr gdi);

      [DllImport("ext-ms-win-rtcore-gdi-object-l1-1-0")]
      internal static extern int DeleteObject(IntPtr gdiObject);

      [DllImport("dwmmobile.dll")]
      internal static extern int CaptureScreen(
        uint dwFlags,
        [Out] ImageHelpers.IWICBitmap[] ppBitmap,
        [MarshalAs(UnmanagedType.LPWStr), In, Optional] string pwszCaptureEventName);

      [DllImport("xtfconsolecontrol.dll")]
      internal static extern int CaptureScreenshot(
        [In, Out] IntPtr buffer,
        [In] ulong size,
        out long width,
        out long height,
        out long pitch,
        out long bitsPerChannel);
    }

    [Guid("ec5ec8a9-c395-4314-9c77-54d7a935ff70")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICImagingFactory
    {
      ImageHelpers.IWICBitmapDecoder CreateDecoderFromFilename(
        [MarshalAs(UnmanagedType.LPWStr)] string wzFilename,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Guid[] pguidVendor,
        uint dwDesiredAccess,
        uint metadataOptions);

      ImageHelpers.IWICBitmapDecoder CreateDecoderFromStream(
        IStream pIStream,
        IntPtr pguidVendor,
        uint metadataOptions);

      ImageHelpers.IWICBitmapDecoder CreateDecoderFromFileHandle(
        IntPtr hFile,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Guid[] pguidVendor,
        uint metadataOptions);

      ImageHelpers.IWICComponentInfo CreateComponentInfo([MarshalAs(UnmanagedType.LPStruct)] Guid clsidComponent);

      ImageHelpers.IWICBitmapDecoder CreateDecoder(
        [MarshalAs(UnmanagedType.LPStruct)] Guid guidContainerFormat,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Guid[] pguidVendor);

      ImageHelpers.IWICBitmapEncoder CreateEncoder(
        [MarshalAs(UnmanagedType.LPStruct)] Guid guidContainerFormat,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Guid[] pguidVendor);

      ImageHelpers.IWICPalette CreatePalette();

      ImageHelpers.IWICFormatConverter CreateFormatConverter();

      ImageHelpers.IWICBitmapScaler CreateBitmapScaler();

      ImageHelpers.IWICBitmapClipper CreateBitmapClipper();

      ImageHelpers.IWICBitmapFlipRotator CreateBitmapFlipRotator();

      ImageHelpers.IWICStream CreateStream();

      ImageHelpers.IWICColorContext CreateColorContext();

      ImageHelpers.IWICColorTransform CreateColorTransform();

      ImageHelpers.IWICBitmap CreateBitmap(
        uint uiWidth,
        uint uiHeight,
        [MarshalAs(UnmanagedType.LPStruct)] Guid pixelFormat,
        uint option);

      ImageHelpers.IWICBitmap CreateBitmapFromSource(
        ImageHelpers.IWICBitmapSource pIBitmapSource,
        ImageHelpers.WICBitmapCreateCacheOption option);

      ImageHelpers.IWICBitmap CreateBitmapFromSourceRect(
        ImageHelpers.IWICBitmapSource pIBitmapSource,
        uint x,
        uint y,
        uint width,
        uint height);

      ImageHelpers.IWICBitmap CreateBitmapFromMemory(
        uint uiWidth,
        uint uiHeight,
        [MarshalAs(UnmanagedType.LPStruct)] Guid pixelFormat,
        uint cbStride,
        uint cbBufferSize,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] pbBuffer);

      ImageHelpers.IWICBitmap CreateBitmapFromHBITMAP(
        IntPtr hBitmap,
        IntPtr hPalette,
        ImageHelpers.WICBitmapAlphaChannelOption options);

      ImageHelpers.IWICBitmap CreateBitmapFromHICON(IntPtr hIcon);

      ImageHelpers.IEnumUnknown CreateComponentEnumerator(
        uint componentTypes,
        uint options);

      ImageHelpers.IWICFastMetadataEncoder CreateFastMetadataEncoderFromDecoder(
        ImageHelpers.IWICBitmapDecoder pIDecoder);

      ImageHelpers.IWICFastMetadataEncoder CreateFastMetadataEncoderFromFrameDecode(
        ImageHelpers.IWICBitmapFrameDecode pIFrameDecoder);

      ImageHelpers.IWICMetadataQueryWriter CreateQueryWriter(
        [MarshalAs(UnmanagedType.LPStruct)] Guid guidMetadataFormat,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Guid[] pguidVendor);

      ImageHelpers.IWICMetadataQueryWriter CreateQueryWriterFromReader(
        ImageHelpers.IWICMetadataQueryReader pIQueryReader,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] Guid[] pguidVendor);
    }

    [Guid("00000121-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmap : ImageHelpers.IWICBitmapSource
    {
      new void GetSize(out uint puiWidth, out uint puiHeight);

      new void GetPixelFormat(out Guid pPixelFormat);

      new void GetResolution(out double pDpiX, out double pDpiY);

      new void CopyPalette(ImageHelpers.IWICPalette pIPalette);

      new void CopyPixels(
        ImageHelpers.WICRect prc,
        uint cbStride,
        uint cbBufferSize,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] byte[] pbBuffer);

      ImageHelpers.IWICBitmapLock Lock(
        ImageHelpers.WICRect prcLock,
        ImageHelpers.WICBitmapLockFlags flags);

      void SetPalette(ImageHelpers.IWICPalette pIPalette);

      void SetResolution(double dpiX, double dpiY);
    }

    [Guid("00000120-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapSource
    {
      void GetSize(out uint puiWidth, out uint puiHeight);

      void GetPixelFormat(out Guid pPixelFormat);

      void GetResolution(out double pDpiX, out double pDpiY);

      void CopyPalette(ImageHelpers.IWICPalette pIPalette);

      void CopyPixels(ImageHelpers.WICRect prc, uint cbStride, uint cbBufferSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] byte[] pbBuffer);
    }

    [Guid("E4FBCF03-223D-4e81-9333-D635556DD1B5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapClipper : ImageHelpers.IWICBitmapSource
    {
      new void GetSize(out uint puiWidth, out uint puiHeight);

      new void GetPixelFormat(out Guid pPixelFormat);

      new void GetResolution(out double pDpiX, out double pDpiY);

      new void CopyPalette(ImageHelpers.IWICPalette pIPalette);

      new void CopyPixels(
        ImageHelpers.WICRect prc,
        uint cbStride,
        uint cbBufferSize,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] byte[] pbBuffer);

      void Initialize(ImageHelpers.IWICBitmapSource pISource, ImageHelpers.WICRect prc);
    }

    [Guid("00000040-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICPalette
    {
      void InitializePredefined(
        ImageHelpers.WICBitmapPaletteType ePaletteType,
        [MarshalAs(UnmanagedType.Bool)] bool fAddTransparentColor);

      void InitializeCustom([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] uint[] pColors, uint cCount);

      void InitializeFromBitmap(
        ImageHelpers.IWICBitmapSource pISurface,
        uint cCount,
        [MarshalAs(UnmanagedType.Bool)] bool fAddTransparentColor);

      void InitializeFromPalette(ImageHelpers.IWICPalette pIPalette);

      ImageHelpers.WICBitmapPaletteType GetType();

      uint GetColorCount();

      uint GetColors(uint cCount, [MarshalAs(UnmanagedType.LPArray), Out] uint[] pColors);

      [return: MarshalAs(UnmanagedType.Bool)]
      bool IsBlackWhite();

      [return: MarshalAs(UnmanagedType.Bool)]
      bool IsGrayscale();

      [return: MarshalAs(UnmanagedType.Bool)]
      bool HasAlpha();
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("135FF860-22B7-4ddf-B0F6-218F4F299A43")]
    [ComImport]
    public interface IWICStream : IStream
    {
      new void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] byte[] pv, int cb, IntPtr pcbRead);

      new void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, IntPtr pcbWritten);

      new void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

      new void SetSize(long libNewSize);

      new void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

      new void Commit(int grfCommitFlags);

      new void Revert();

      new void LockRegion(long libOffset, long cb, int dwLockType);

      new void UnlockRegion(long libOffset, long cb, int dwLockType);

      new void Stat(out STATSTG pstatstg, int grfStatFlag);

      new void Clone(out IStream ppstm);

      void InitializeFromIStream(IStream pIStream);

      void InitializeFromFilename(
        [MarshalAs(UnmanagedType.LPWStr)] string wzFileName,
        ImageHelpers.GenericAccessRights dwDesiredAccess);

      void InitializeFromMemory([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pbBuffer, uint cbBufferSize);

      void InitializeFromIStreamRegion(IStream pIStream, ulong ulOffset, ulong ulMaxSize);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000105-a8f2-4877-ba0a-fd2b6645fb94")]
    [ComImport]
    public interface IWICBitmapFrameEncode
    {
      void Initialize(ImageHelpers.IPropertyBag2 pIEncoderOptions);

      void SetSize(uint uiWidth, uint uiHeight);

      void SetResolution(double dpiX, double dpiY);

      void SetPixelFormat(ref Guid pPixelFormat);

      void SetColorContexts(uint cCount, [MarshalAs(UnmanagedType.LPArray)] ImageHelpers.IWICColorContext[] ppIColorContext);

      void SetPalette(ImageHelpers.IWICPalette pIPalette);

      void SetThumbnail(ImageHelpers.IWICBitmapSource pIThumbnail);

      void WritePixels(uint lineCount, uint cbStride, uint cbBufferSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pbPixels);

      void WriteSource(ImageHelpers.IWICBitmap pIBitmapSource, ImageHelpers.WICRect prc);

      void Commit();

      ImageHelpers.IWICMetadataQueryWriter GetMetadataQueryWriter();
    }

    [Guid("00000103-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapEncoder
    {
      void Initialize(
        IStream pIStream,
        ImageHelpers.WICBitmapEncoderCacheOption cacheOption);

      void GetContainerFormat(out Guid pguidContainerFormat);

      ImageHelpers.IWICBitmapEncoderInfo GetEncoderInfo();

      void SetColorContexts(uint cCount, [MarshalAs(UnmanagedType.LPArray)] ImageHelpers.IWICColorContext[] ppIColorContext);

      void SetPalette(ImageHelpers.IWICPalette pIPalette);

      void SetThumbnail(ImageHelpers.IWICBitmapSource pIThumbnail);

      void SetPreview(ImageHelpers.IWICBitmapSource pIPreview);

      void CreateNewFrame(
        out ImageHelpers.IWICBitmapFrameEncode ppIFrameEncode,
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 1), In, Out] ImageHelpers.IPropertyBag2[] encoderOptions);

      void Commit();

      ImageHelpers.IWICMetadataQueryWriter GetMetadataQueryWriter();
    }

    [Guid("00000123-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapLock
    {
    }

    [Guid("9EDDE9E7-8DEE-47ea-99DF-E6FAF2ED44BF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapDecoder
    {
    }

    [Guid("23BC3F0A-698B-4357-886B-F24D50671334")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICComponentInfo
    {
    }

    [Guid("00000301-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICFormatConverter : ImageHelpers.IWICBitmapSource
    {
    }

    [Guid("00000302-a8f2-4877-ba0a-fd2b6645fb94")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapScaler : ImageHelpers.IWICBitmapSource
    {
    }

    [Guid("5009834F-2D6A-41ce-9E1B-17C5AFF7A782")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICBitmapFlipRotator : ImageHelpers.IWICBitmapSource
    {
    }

    [Guid("3C613A02-34B2-44ea-9A7C-45AEA9C6FD6D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICColorContext
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("B66F034F-D0E2-40ab-B436-6DE39E321A94")]
    [ComImport]
    public interface IWICColorTransform : ImageHelpers.IWICBitmapSource
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3B16811B-6A43-4ec9-A813-3D930C13B940")]
    [ComImport]
    public interface IWICBitmapFrameDecode : ImageHelpers.IWICBitmapSource
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("94C9B4EE-A09F-4f92-8A1E-4A9BCE7E76FB")]
    [ComImport]
    public interface IWICBitmapEncoderInfo : ImageHelpers.IWICBitmapCodecInfo, ImageHelpers.IWICComponentInfo
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("E87A44C4-B76E-4c47-8B09-298EB12A2714")]
    [ComImport]
    public interface IWICBitmapCodecInfo : ImageHelpers.IWICComponentInfo
    {
    }

    [Guid("22F55882-280B-11d0-A8A9-00A0C90C2004")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IPropertyBag2
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("30989668-E1C9-4597-B395-458EEDB808DF")]
    [ComImport]
    public interface IWICMetadataQueryReader
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("A721791A-0DEF-4d06-BD91-2118BF1DB10B")]
    [ComImport]
    public interface IWICMetadataQueryWriter : ImageHelpers.IWICMetadataQueryReader
    {
    }

    [Guid("B84E2C09-78C9-4AC4-8BD3-524AE1663A2F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IWICFastMetadataEncoder
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000100-0000-0000-C000-000000000046")]
    [ComImport]
    public interface IEnumUnknown
    {
    }

    public enum WICBitmapAlphaChannelOption : uint
    {
      WICBitmapUseAlpha,
      WICBitmapUsePremultipliedAlpha,
      WICBitmapIgnoreAlpha,
    }

    public enum WICBitmapCreateCacheOption : uint
    {
      WICBitmapNoCache,
      WICBitmapCacheOnDemand,
      WICBitmapCacheOnLoad,
    }

    public enum WICBitmapEncoderCacheOption : uint
    {
      WICBitmapEncoderCacheInMemory,
      WICBitmapEncoderCacheTempFile,
      WICBitmapEncoderNoCache,
    }

    public enum WICBitmapPaletteType : uint
    {
      WICBitmapPaletteTypeCustom = 0,
      WICBitmapPaletteTypeMedianCut = 1,
      WICBitmapPaletteTypeFixedBW = 2,
      WICBitmapPaletteTypeFixedHalftone8 = 3,
      WICBitmapPaletteTypeFixedHalftone27 = 4,
      WICBitmapPaletteTypeFixedHalftone64 = 5,
      WICBitmapPaletteTypeFixedHalftone125 = 6,
      WICBitmapPaletteTypeFixedHalftone216 = 7,
      WICBitmapPaletteTypeFixedWebPalette = 7,
      WICBitmapPaletteTypeFixedHalftone252 = 8,
      WICBitmapPaletteTypeFixedHalftone256 = 9,
      WICBitmapPaletteTypeFixedGray4 = 10, // 0x0000000A
      WICBitmapPaletteTypeFixedGray16 = 11, // 0x0000000B
      WICBitmapPaletteTypeFixedGray256 = 12, // 0x0000000C
    }

    [Flags]
    public enum WICBitmapLockFlags : uint
    {
      WICBitmapLockRead = 1,
      WICBitmapLockWrite = 2,
    }

    [Flags]
    public enum GenericAccessRights : uint
    {
      GENERIC_READ = 2147483648, // 0x80000000
      GENERIC_WRITE = 1073741824, // 0x40000000
      GENERIC_EXECUTE = 536870912, // 0x20000000
      GENERIC_ALL = 268435456, // 0x10000000
      GENERIC_READ_WRITE = GENERIC_WRITE | GENERIC_READ, // 0xC0000000
    }

    public enum CLIPFORMAT : short
    {
      CF_TEXT = 1,
      CF_BITMAP = 2,
      CF_METAFILEPICT = 3,
      CF_SYLK = 4,
      CF_DIF = 5,
      CF_TIFF = 6,
      CF_OEMTEXT = 7,
      CF_DIB = 8,
      CF_PALETTE = 9,
      CF_PENDATA = 10, // 0x000A
      CF_RIFF = 11, // 0x000B
      CF_WAVE = 12, // 0x000C
      CF_UNICODETEXT = 13, // 0x000D
      CF_ENHMETAFILE = 14, // 0x000E
      CF_HDROP = 15, // 0x000F
      CF_LOCALE = 16, // 0x0010
      CF_MAX = 17, // 0x0011
      CF_OWNERDISPLAY = 128, // 0x0080
      CF_DSPTEXT = 129, // 0x0081
      CF_DSPBITMAP = 130, // 0x0082
      CF_DSPMETAFILEPICT = 131, // 0x0083
      CF_DSPENHMETAFILE = 142, // 0x008E
    }

    [StructLayout(LayoutKind.Sequential)]
    public class WICRect
    {
      public int X;
      public int Y;
      public int Width;
      public int Height;
    }
  }
}
