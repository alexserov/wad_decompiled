// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IAccessible
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "618736E0-3C3D-11CF-810C-00AA00389B71"), TypeLibType(flags: TypeLibTypeFlags.FHidden), ComImport]
    public interface IAccessible {
        [DispId(dispId: -5000)]
        object accParent {
            [DispId(dispId: -5000), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.IDispatch)]
            get;
        }

        [DispId(dispId: -5001)]
        int accChildCount {
            [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5001), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5002), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.IDispatch)]
        object get_accChild([MarshalAs(unmanagedType: UnmanagedType.Struct), In]
                            Variant varChild);

        [DispId(dispId: -5003), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string get_accName([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                           Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5004), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string get_accValue([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                            Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5005), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string get_accDescription([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                                  Variant varChild);

        [DispId(dispId: -5006), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        Variant get_accRole([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                            Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5007), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        Variant get_accState([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                             Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5008), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string get_accHelp([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                           Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5009), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int get_accHelpTopic([MarshalAs(unmanagedType: UnmanagedType.BStr)]
                             out string pszHelpFile, [MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                             Variant varChild);

        [DispId(dispId: -5010), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string get_accKeyboardShortcut([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                                       Variant varChild);

        [DispId(dispId: -5011)]
        Variant accFocus {
            [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5011), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
            get;
        }

        [DispId(dispId: -5012)]
        Variant accSelection {
            [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5012), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
            get;
        }

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5013), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.BStr)]
        string get_accDefaultAction([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                                    Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5014), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void accSelect([In] int flagsSelect, [MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                       Variant varChild);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5015), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void accLocation(
            out int pxLeft,
            out int pyTop,
            out int pcxWidth,
            out int pcyHeight,
            [MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
            Variant varChild);

        [DispId(dispId: -5016), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        Variant accNavigate([In] int navDir, [MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                            Variant varStart);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5017), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(unmanagedType: UnmanagedType.Struct)]
        Variant accHitTest([In] int xLeft, [In] int yTop);

        [DispId(dispId: -5018), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void accDoDefaultAction([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                                Variant varChild);

        [DispId(dispId: -5003), TypeLibFunc(flags: TypeLibFuncFlags.FHidden), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void set_accName([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                         Variant varChild, [MarshalAs(unmanagedType: UnmanagedType.BStr), In]
                         string pszName);

        [TypeLibFunc(flags: TypeLibFuncFlags.FHidden), DispId(dispId: -5004), MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void set_accValue([MarshalAs(unmanagedType: UnmanagedType.Struct), In, Optional]
                          Variant varChild, [MarshalAs(unmanagedType: UnmanagedType.BStr), In]
                          string pszValue);
    }
}