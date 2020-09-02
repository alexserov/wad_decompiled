// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IAccessible
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient
{
  [Guid("618736E0-3C3D-11CF-810C-00AA00389B71")]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public interface IAccessible
  {
    [DispId(-5000)]
    object accParent { [DispId(-5000), TypeLibFunc(TypeLibFuncFlags.FHidden), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

    [DispId(-5001)]
    int accChildCount { [TypeLibFunc(TypeLibFuncFlags.FHidden), DispId(-5001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5002)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object get_accChild([MarshalAs(UnmanagedType.Struct), In] Variant varChild);

    [DispId(-5003)]
    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accName([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5004)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accValue([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5005)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accDescription([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [DispId(-5006)]
    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    Variant get_accRole([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5007)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    Variant get_accState([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5008)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accHelp([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5009)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string pszHelpFile, [MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [DispId(-5010)]
    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accKeyboardShortcut([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [DispId(-5011)]
    Variant accFocus { [TypeLibFunc(TypeLibFuncFlags.FHidden), DispId(-5011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

    [DispId(-5012)]
    Variant accSelection { [TypeLibFunc(TypeLibFuncFlags.FHidden), DispId(-5012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5013)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accDefaultAction([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5014)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void accSelect([In] int flagsSelect, [MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5015)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void accLocation(
      out int pxLeft,
      out int pyTop,
      out int pcxWidth,
      out int pcyHeight,
      [MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [DispId(-5016)]
    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    Variant accNavigate([In] int navDir, [MarshalAs(UnmanagedType.Struct), In, Optional] Variant varStart);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5017)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Struct)]
    Variant accHitTest([In] int xLeft, [In] int yTop);

    [DispId(-5018)]
    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void accDoDefaultAction([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild);

    [DispId(-5003)]
    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void set_accName([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild, [MarshalAs(UnmanagedType.BStr), In] string pszName);

    [TypeLibFunc(TypeLibFuncFlags.FHidden)]
    [DispId(-5004)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void set_accValue([MarshalAs(UnmanagedType.Struct), In, Optional] Variant varChild, [MarshalAs(UnmanagedType.BStr), In] string pszValue);
  }
}
