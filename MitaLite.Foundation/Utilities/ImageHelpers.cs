// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.ImageHelpers
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace MS.Internal.Mita.Foundation.Utilities {
    internal static class ImageHelpers {
        public enum CLIPFORMAT : short {
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
            CF_DSPENHMETAFILE = 142 // 0x008E
        }

        [Flags]
        public enum GenericAccessRights : uint {
            GENERIC_READ = 2147483648, // 0x80000000
            GENERIC_WRITE = 1073741824, // 0x40000000
            GENERIC_EXECUTE = 536870912, // 0x20000000
            GENERIC_ALL = 268435456, // 0x10000000
            GENERIC_READ_WRITE = GENERIC_WRITE | GENERIC_READ // 0xC0000000
        }

        public enum WICBitmapAlphaChannelOption : uint {
            WICBitmapUseAlpha,
            WICBitmapUsePremultipliedAlpha,
            WICBitmapIgnoreAlpha
        }

        public enum WICBitmapCreateCacheOption : uint {
            WICBitmapNoCache,
            WICBitmapCacheOnDemand,
            WICBitmapCacheOnLoad
        }

        public enum WICBitmapEncoderCacheOption : uint {
            WICBitmapEncoderCacheInMemory,
            WICBitmapEncoderCacheTempFile,
            WICBitmapEncoderNoCache
        }

        [Flags]
        public enum WICBitmapLockFlags : uint {
            WICBitmapLockRead = 1,
            WICBitmapLockWrite = 2
        }

        public enum WICBitmapPaletteType : uint {
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
            WICBitmapPaletteTypeFixedGray256 = 12 // 0x0000000C
        }

        const int WINCODEC_SDK_VERSION = 566;
        const int MaxScreenshotWidth = 3840;
        const int MaxScreenshotHeight = 2160;
        const int RGBPixelSize = 3;
        static readonly Guid GUID_ContainerFormatBmp = new Guid(a: 183621758U, b: 64766, c: 16776, d: 189, e: 235, f: 167, g: 144, h: 100, i: 113, j: 203, k: 227);
        static readonly Guid GUID_ContainerFormatPng = new Guid(a: 461175540, b: 28991, c: 18236, d: 187, e: 205, f: 97, g: 55, h: 66, i: 95, j: 174, k: 175);
        static readonly Guid GUID_ContainerFormatIco = new Guid(a: 2745721028U, b: 13199, c: 19479, d: 145, e: 154, f: 251, g: 164, h: 181, i: 98, j: 143, k: 33);
        static readonly Guid GUID_ContainerFormatJpeg = new Guid(a: 434415018, b: 22114, c: 20421, d: 160, e: 192, f: 23, g: 88, h: 2, i: 142, j: 16, k: 87);
        static readonly Guid GUID_ContainerFormatTiff = new Guid(a: 373017648U, b: 58089, c: 20235, d: 150, e: 29, f: 163, g: 233, h: 253, i: 183, j: 136, k: 163);
        static readonly Guid GUID_ContainerFormatGif = new Guid(a: 529159681, b: 32077, c: 19645, d: 156, e: 130, f: 27, g: 200, h: 212, i: 238, j: 185, k: 165);
        static readonly Guid GUID_ContainerFormatWmp = new Guid(a: 1470332074, b: 13946, c: 17728, d: 145, e: 107, f: 241, g: 131, h: 197, i: 9, j: 58, k: 75);
        static readonly Guid GUID_WICPixelFormat24bppRGB = new Guid(a: 1876804388, b: 19971, c: 19454, d: 177, e: 133, f: 61, g: 119, h: 118, i: 141, j: 201, k: 13);

        public static Guid GetContainerFormat(ImageFormat format) {
            switch (format) {
                case ImageFormat.Bmp:
                    return GUID_ContainerFormatBmp;
                case ImageFormat.Gif:
                    return GUID_ContainerFormatGif;
                case ImageFormat.Ico:
                    return GUID_ContainerFormatIco;
                case ImageFormat.Jpeg:
                    return GUID_ContainerFormatJpeg;
                case ImageFormat.Png:
                    return GUID_ContainerFormatPng;
                case ImageFormat.Tiff:
                    return GUID_ContainerFormatTiff;
                case ImageFormat.Wmp:
                    return GUID_ContainerFormatWmp;
                default:
                    return GUID_ContainerFormatPng;
            }
        }

        public static IWICBitmap CaptureMobileScreenshot() {
            var ppBitmap = new IWICBitmap[1];
            var num = NativeMethods.CaptureScreen(dwFlags: 0U, ppBitmap: ppBitmap);
            if (num != 0) {
                var lastWin32Error = Marshal.GetLastWin32Error();
                throw new DisplayException(message: string.Format(format: "CaptureScreen failed with result: {0} and error: {1}", arg0: num, arg1: lastWin32Error));
            }

            Log.Out(msg: "CaptureScreen succeeded with result: {0}", (object) num);
            return ppBitmap[0];
        }

        public static IWICBitmap CaptureXboxScreenshot() {
            var cb = 24883200;
            var num = Marshal.AllocCoTaskMem(cb: cb);
            ulong size = (uint) cb;
            long width = 0;
            long height = 0;
            long pitch = 0;
            long bitsPerChannel = 0;
            try {
                var windowsError = NativeMethods.CaptureScreenshot(buffer: num, size: size, width: out width, height: out height, pitch: out pitch, bitsPerChannel: out bitsPerChannel);
                if (windowsError != 0)
                    throw new ScreenCaptureException(message: string.Format(format: "CaptureXboxScreenshot failed with result: {0}", arg0: windowsError), windowsError: windowsError);
            } catch (Exception ex) {
                throw new ScreenCaptureException(message: string.Format(format: "CaptureXboxScreenshot threw '{0}' with message \"{1}'\"", arg0: ((object) ex).GetType(), arg1: ex.Message), innerException: ex);
            }

            if (height == 0L || width == 0L || pitch == 0L || bitsPerChannel == 0L)
                throw new ScreenCaptureException(message: string.Format(format: "CaptureXboxScreenshot returned success but a returned value is 0. H: {0} W: {1} P: {2} BPC: {3}", (object) height, (object) width, (object) pitch, (object) bitsPerChannel));
            Log.Out(msg: "CaptureXboxScreenshot call succeeded. H: {0} W: {1} P: {2} BPC: {3}", (object) height, (object) width, (object) pitch, (object) bitsPerChannel);
            var length = (int) (height * width * 3L);
            var numArray = length <= cb ? new byte[length] : throw new ScreenCaptureException(message: string.Format(format: "CaptureXboxScreenshot returned a larger size screenshot than the allocated buffer. H: {0} W: {1} P: {2} BPC: {3}", (object) height, (object) width, (object) pitch, (object) bitsPerChannel));
            Marshal.Copy(source: num, destination: numArray, startIndex: 0, length: length);
            Marshal.FreeCoTaskMem(ptr: num);
            return WIC.CreateImagingFactory().CreateBitmapFromMemory(uiWidth: (uint) width, uiHeight: (uint) height, pixelFormat: GUID_WICPixelFormat24bppRGB, cbStride: (uint) pitch, cbBufferSize: (uint) numArray.Length, pbBuffer: numArray) ?? throw new ScreenCaptureException(message: "CreateBitmapFromMemory returned null.");
        }

        public static class GDI {
            public static IntPtr CreateCompatibleBitmap(IntPtr dc, int width, int height) {
                return NativeMethods.CreateCompatibleBitmap(dc: dc, width: width, height: height);
            }

            public static int BitBlt(
                IntPtr destDC,
                int xDest,
                int yDest,
                int width,
                int height,
                IntPtr sourceDC,
                int xSource,
                int ySource,
                uint rasterOperation) {
                return NativeMethods.BitBlt(destDC: destDC, xDest: xDest, yDest: yDest, width: width, height: height, sourceDC: sourceDC, xSource: xSource, ySource: ySource, rasterOperation: rasterOperation);
            }

            public static IntPtr CreateDC(
                string driver,
                IntPtr device,
                IntPtr output,
                IntPtr devMode) {
                return NativeMethods.CreateDC(driver: driver, device: device, output: output, devMode: devMode);
            }

            public static IntPtr CreateCompatibleDC(IntPtr dc) {
                return NativeMethods.CreateCompatibleDC(dc: dc);
            }

            public static int DeleteDC(IntPtr dc) {
                return NativeMethods.DeleteDC(dc: dc);
            }

            public static IntPtr CreatePalette(byte[] lplgpl) {
                return NativeMethods.CreatePalette(lplgpl: lplgpl);
            }

            public static IntPtr SelectObject(IntPtr dc, IntPtr gdi) {
                return NativeMethods.SelectObject(dc: dc, gdi: gdi);
            }

            public static int DeleteObject(IntPtr gdiObject) {
                return NativeMethods.DeleteObject(gdiObject: gdiObject);
            }
        }

        public static class WIC {
            static IWICImagingFactory factory;

            public static IWICImagingFactory CreateImagingFactory() {
                if (factory == null)
                    factory = NativeMethods.WICCreateImagingFactory_Proxy(SDKVersion: 566U);
                return factory;
            }
        }

        static class NativeMethods {
            const string WINCODECS_DLL = "windowscodecs.dll";
            const string GDIDRAW_DLL = "ext-ms-win-gdi-draw-l1-1-1";
            const string GDIDCCREATE_DLL = "ext-ms-win-gdi-dc-create-l1-1-1";
            const string GDIDC_DLL = "ext-ms-win-gdi-dc-l1-2-0";
            const string GDIOBJECT_DLL = "ext-ms-win-rtcore-gdi-object-l1-1-0";
            const string DWMMOBILE_DLL = "dwmmobile.dll";
            const string CONSOLECONTROL_DLL = "xtfconsolecontrol.dll";

            [DllImport(dllName: "windowscodecs.dll", PreserveSig = false)]
            internal static extern IWICImagingFactory WICCreateImagingFactory_Proxy(
                uint SDKVersion);

            [DllImport(dllName: "ext-ms-win-gdi-draw-l1-1-1", SetLastError = true)]
            internal static extern IntPtr CreateCompatibleBitmap(IntPtr dc, int width, int height);

            [DllImport(dllName: "ext-ms-win-gdi-draw-l1-1-1", SetLastError = true)]
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

            [DllImport(dllName: "ext-ms-win-gdi-dc-create-l1-1-1", EntryPoint = "CreateDCW", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr CreateDC(
                string driver,
                IntPtr device,
                IntPtr output,
                IntPtr devMode);

            [DllImport(dllName: "ext-ms-win-gdi-dc-create-l1-1-1", SetLastError = true)]
            internal static extern IntPtr CreateCompatibleDC(IntPtr dc);

            [DllImport(dllName: "ext-ms-win-gdi-dc-create-l1-1-1")]
            internal static extern int DeleteDC(IntPtr dc);

            [DllImport(dllName: "ext-ms-win-gdi-dc-l1-2-0")]
            internal static extern IntPtr CreatePalette([MarshalAs(unmanagedType: UnmanagedType.LPArray)]
                                                        byte[] lplgpl);

            [DllImport(dllName: "ext-ms-win-gdi-dc-l1-2-0", SetLastError = true)]
            internal static extern IntPtr SelectObject(IntPtr dc, IntPtr gdi);

            [DllImport(dllName: "ext-ms-win-rtcore-gdi-object-l1-1-0")]
            internal static extern int DeleteObject(IntPtr gdiObject);

            [DllImport(dllName: "dwmmobile.dll")]
            internal static extern int CaptureScreen(
                uint dwFlags,
                [Out] IWICBitmap[] ppBitmap,
                [MarshalAs(unmanagedType: UnmanagedType.LPWStr), In, Optional]
                string pwszCaptureEventName);

            [DllImport(dllName: "xtfconsolecontrol.dll")]
            internal static extern int CaptureScreenshot(
                [In, Out] IntPtr buffer,
                [In] ulong size,
                out long width,
                out long height,
                out long pitch,
                out long bitsPerChannel);
        }

        [Guid(guid: "ec5ec8a9-c395-4314-9c77-54d7a935ff70"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICImagingFactory {
            IWICBitmapDecoder CreateDecoderFromFilename(
                [MarshalAs(unmanagedType: UnmanagedType.LPWStr)]
                string wzFilename,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1)]
                Guid[] pguidVendor,
                uint dwDesiredAccess,
                uint metadataOptions);

            IWICBitmapDecoder CreateDecoderFromStream(
                IStream pIStream,
                IntPtr pguidVendor,
                uint metadataOptions);

            IWICBitmapDecoder CreateDecoderFromFileHandle(
                IntPtr hFile,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1)]
                Guid[] pguidVendor,
                uint metadataOptions);

            IWICComponentInfo CreateComponentInfo([MarshalAs(unmanagedType: UnmanagedType.LPStruct)]
                                                  Guid clsidComponent);

            IWICBitmapDecoder CreateDecoder(
                [MarshalAs(unmanagedType: UnmanagedType.LPStruct)]
                Guid guidContainerFormat,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1)]
                Guid[] pguidVendor);

            IWICBitmapEncoder CreateEncoder(
                [MarshalAs(unmanagedType: UnmanagedType.LPStruct)]
                Guid guidContainerFormat,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1)]
                Guid[] pguidVendor);

            IWICPalette CreatePalette();

            IWICFormatConverter CreateFormatConverter();

            IWICBitmapScaler CreateBitmapScaler();

            IWICBitmapClipper CreateBitmapClipper();

            IWICBitmapFlipRotator CreateBitmapFlipRotator();

            IWICStream CreateStream();

            IWICColorContext CreateColorContext();

            IWICColorTransform CreateColorTransform();

            IWICBitmap CreateBitmap(
                uint uiWidth,
                uint uiHeight,
                [MarshalAs(unmanagedType: UnmanagedType.LPStruct)]
                Guid pixelFormat,
                uint option);

            IWICBitmap CreateBitmapFromSource(
                IWICBitmapSource pIBitmapSource,
                WICBitmapCreateCacheOption option);

            IWICBitmap CreateBitmapFromSourceRect(
                IWICBitmapSource pIBitmapSource,
                uint x,
                uint y,
                uint width,
                uint height);

            IWICBitmap CreateBitmapFromMemory(
                uint uiWidth,
                uint uiHeight,
                [MarshalAs(unmanagedType: UnmanagedType.LPStruct)]
                Guid pixelFormat,
                uint cbStride,
                uint cbBufferSize,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 4)]
                byte[] pbBuffer);

            IWICBitmap CreateBitmapFromHBITMAP(
                IntPtr hBitmap,
                IntPtr hPalette,
                WICBitmapAlphaChannelOption options);

            IWICBitmap CreateBitmapFromHICON(IntPtr hIcon);

            IEnumUnknown CreateComponentEnumerator(
                uint componentTypes,
                uint options);

            IWICFastMetadataEncoder CreateFastMetadataEncoderFromDecoder(
                IWICBitmapDecoder pIDecoder);

            IWICFastMetadataEncoder CreateFastMetadataEncoderFromFrameDecode(
                IWICBitmapFrameDecode pIFrameDecoder);

            IWICMetadataQueryWriter CreateQueryWriter(
                [MarshalAs(unmanagedType: UnmanagedType.LPStruct)]
                Guid guidMetadataFormat,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1)]
                Guid[] pguidVendor);

            IWICMetadataQueryWriter CreateQueryWriterFromReader(
                IWICMetadataQueryReader pIQueryReader,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1)]
                Guid[] pguidVendor);
        }

        [Guid(guid: "00000121-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmap : IWICBitmapSource {
            new void GetSize(out uint puiWidth, out uint puiHeight);

            new void GetPixelFormat(out Guid pPixelFormat);

            new void GetResolution(out double pDpiX, out double pDpiY);

            new void CopyPalette(IWICPalette pIPalette);

            new void CopyPixels(
                WICRect prc,
                uint cbStride,
                uint cbBufferSize,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 2), Out]
                byte[] pbBuffer);

            IWICBitmapLock Lock(
                WICRect prcLock,
                WICBitmapLockFlags flags);

            void SetPalette(IWICPalette pIPalette);

            void SetResolution(double dpiX, double dpiY);
        }

        [Guid(guid: "00000120-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapSource {
            void GetSize(out uint puiWidth, out uint puiHeight);

            void GetPixelFormat(out Guid pPixelFormat);

            void GetResolution(out double pDpiX, out double pDpiY);

            void CopyPalette(IWICPalette pIPalette);

            void CopyPixels(WICRect prc, uint cbStride, uint cbBufferSize, [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 2), Out]
                            byte[] pbBuffer);
        }

        [Guid(guid: "E4FBCF03-223D-4e81-9333-D635556DD1B5"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapClipper : IWICBitmapSource {
            new void GetSize(out uint puiWidth, out uint puiHeight);

            new void GetPixelFormat(out Guid pPixelFormat);

            new void GetResolution(out double pDpiX, out double pDpiY);

            new void CopyPalette(IWICPalette pIPalette);

            new void CopyPixels(
                WICRect prc,
                uint cbStride,
                uint cbBufferSize,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 2), Out]
                byte[] pbBuffer);

            void Initialize(IWICBitmapSource pISource, WICRect prc);
        }

        [Guid(guid: "00000040-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICPalette {
            void InitializePredefined(
                WICBitmapPaletteType ePaletteType,
                [MarshalAs(unmanagedType: UnmanagedType.Bool)]
                bool fAddTransparentColor);

            void InitializeCustom([MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 1)]
                                  uint[] pColors, uint cCount);

            void InitializeFromBitmap(
                IWICBitmapSource pISurface,
                uint cCount,
                [MarshalAs(unmanagedType: UnmanagedType.Bool)]
                bool fAddTransparentColor);

            void InitializeFromPalette(IWICPalette pIPalette);

            WICBitmapPaletteType GetType();

            uint GetColorCount();

            uint GetColors(uint cCount, [MarshalAs(unmanagedType: UnmanagedType.LPArray), Out]
                           uint[] pColors);

            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            bool IsBlackWhite();

            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            bool IsGrayscale();

            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            bool HasAlpha();
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "135FF860-22B7-4ddf-B0F6-218F4F299A43"), ComImport]
        public interface IWICStream : IStream {
            new void Read([MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 2), Out]
                          byte[] pv, int cb, IntPtr pcbRead);

            new void Write([MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 2)]
                           byte[] pv, int cb, IntPtr pcbWritten);

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
                [MarshalAs(unmanagedType: UnmanagedType.LPWStr)]
                string wzFileName,
                GenericAccessRights dwDesiredAccess);

            void InitializeFromMemory([MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 1)]
                                      byte[] pbBuffer, uint cbBufferSize);

            void InitializeFromIStreamRegion(IStream pIStream, ulong ulOffset, ulong ulMaxSize);
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "00000105-a8f2-4877-ba0a-fd2b6645fb94"), ComImport]
        public interface IWICBitmapFrameEncode {
            void Initialize(IPropertyBag2 pIEncoderOptions);

            void SetSize(uint uiWidth, uint uiHeight);

            void SetResolution(double dpiX, double dpiY);

            void SetPixelFormat(ref Guid pPixelFormat);

            void SetColorContexts(uint cCount, [MarshalAs(unmanagedType: UnmanagedType.LPArray)]
                                  IWICColorContext[] ppIColorContext);

            void SetPalette(IWICPalette pIPalette);

            void SetThumbnail(IWICBitmapSource pIThumbnail);

            void WritePixels(uint lineCount, uint cbStride, uint cbBufferSize, [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeParamIndex = 2)]
                             byte[] pbPixels);

            void WriteSource(IWICBitmap pIBitmapSource, WICRect prc);

            void Commit();

            IWICMetadataQueryWriter GetMetadataQueryWriter();
        }

        [Guid(guid: "00000103-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapEncoder {
            void Initialize(
                IStream pIStream,
                WICBitmapEncoderCacheOption cacheOption);

            void GetContainerFormat(out Guid pguidContainerFormat);

            IWICBitmapEncoderInfo GetEncoderInfo();

            void SetColorContexts(uint cCount, [MarshalAs(unmanagedType: UnmanagedType.LPArray)]
                                  IWICColorContext[] ppIColorContext);

            void SetPalette(IWICPalette pIPalette);

            void SetThumbnail(IWICBitmapSource pIThumbnail);

            void SetPreview(IWICBitmapSource pIPreview);

            void CreateNewFrame(
                out IWICBitmapFrameEncode ppIFrameEncode,
                [MarshalAs(unmanagedType: UnmanagedType.LPArray, SizeConst = 1), In, Out]
                IPropertyBag2[] encoderOptions);

            void Commit();

            IWICMetadataQueryWriter GetMetadataQueryWriter();
        }

        [Guid(guid: "00000123-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapLock {
        }

        [Guid(guid: "9EDDE9E7-8DEE-47ea-99DF-E6FAF2ED44BF"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapDecoder {
        }

        [Guid(guid: "23BC3F0A-698B-4357-886B-F24D50671334"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICComponentInfo {
        }

        [Guid(guid: "00000301-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICFormatConverter : IWICBitmapSource {
        }

        [Guid(guid: "00000302-a8f2-4877-ba0a-fd2b6645fb94"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapScaler : IWICBitmapSource {
        }

        [Guid(guid: "5009834F-2D6A-41ce-9E1B-17C5AFF7A782"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICBitmapFlipRotator : IWICBitmapSource {
        }

        [Guid(guid: "3C613A02-34B2-44ea-9A7C-45AEA9C6FD6D"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICColorContext {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "B66F034F-D0E2-40ab-B436-6DE39E321A94"), ComImport]
        public interface IWICColorTransform : IWICBitmapSource {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "3B16811B-6A43-4ec9-A813-3D930C13B940"), ComImport]
        public interface IWICBitmapFrameDecode : IWICBitmapSource {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "94C9B4EE-A09F-4f92-8A1E-4A9BCE7E76FB"), ComImport]
        public interface IWICBitmapEncoderInfo : IWICBitmapCodecInfo, IWICComponentInfo {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "E87A44C4-B76E-4c47-8B09-298EB12A2714"), ComImport]
        public interface IWICBitmapCodecInfo : IWICComponentInfo {
        }

        [Guid(guid: "22F55882-280B-11d0-A8A9-00A0C90C2004"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IPropertyBag2 {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "30989668-E1C9-4597-B395-458EEDB808DF"), ComImport]
        public interface IWICMetadataQueryReader {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "A721791A-0DEF-4d06-BD91-2118BF1DB10B"), ComImport]
        public interface IWICMetadataQueryWriter : IWICMetadataQueryReader {
        }

        [Guid(guid: "B84E2C09-78C9-4AC4-8BD3-524AE1663A2F"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        public interface IWICFastMetadataEncoder {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "00000100-0000-0000-C000-000000000046"), ComImport]
        public interface IEnumUnknown {
        }

        [StructLayout(layoutKind: LayoutKind.Sequential)]
        public class WICRect {
            public int Height;
            public int Width;
            public int X;
            public int Y;
        }
    }
}