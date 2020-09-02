// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.IUIAutomationWindowPattern
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [Guid(guid: "0FAEF453-9208-43EF-BBB2-3B485177864F"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsDual), ComImport]
    public interface IUIAutomationWindowPattern {
        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Close();

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int WaitForInputIdle([In] int milliseconds);

        [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetWindowVisualState([In] WindowVisualState state);

        [DispId(dispId: 1610678275)]
        int CurrentCanMaximize {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678276)]
        int CurrentCanMinimize {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678277)]
        int CurrentIsModal {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678278)]
        int CurrentIsTopmost {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678279)]
        WindowVisualState CurrentWindowVisualState {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678280)]
        WindowInteractionState CurrentWindowInteractionState {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678281)]
        int CachedCanMaximize {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678282)]
        int CachedCanMinimize {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678283)]
        int CachedIsModal {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678284)]
        int CachedIsTopmost {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678285)]
        WindowVisualState CachedWindowVisualState {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(dispId: 1610678286)]
        WindowInteractionState CachedWindowInteractionState {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}