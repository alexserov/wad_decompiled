// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.IRawElementProviderFragment
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using System.Windows.Types;
using UIAutomationClient;

namespace System.Windows.Automation.Provider
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("f7063da8-8359-439c-9297-bbc5299a7d87")]
  [ComImport]
  public interface IRawElementProviderFragment : IRawElementProviderSimple
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    IRawElementProviderFragment Navigate(NavigateDirection direction);

    int[] GetRuntimeId();

    Rect BoundingRectangle { get; }

    IRawElementProviderSimple[] GetEmbeddedFragmentRoots();

    void SetFocus();

    IRawElementProviderFragmentRoot FragmentRoot { [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
