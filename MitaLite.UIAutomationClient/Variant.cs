// Decompiled with JetBrains decompiler
// Type: Variant
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.InteropServices;

[StructLayout(layoutKind: LayoutKind.Explicit)]
public struct Variant {
    [FieldOffset(offset: 0)]
    public TypeUnion _typeUnion;

    [FieldOffset(offset: 0)]
    public decimal _decimal;

    [StructLayout(layoutKind: LayoutKind.Explicit)]
    public struct TypeUnion {
        [FieldOffset(offset: 0)]
        public ushort _vt;

        [FieldOffset(offset: 2)]
        public ushort _wReserved1;

        [FieldOffset(offset: 4)]
        public ushort _wReserved2;

        [FieldOffset(offset: 6)]
        public ushort _wReserved3;

        [FieldOffset(offset: 8)]
        public UnionTypes _unionTypes;
    }

    [StructLayout(layoutKind: LayoutKind.Explicit)]
    public struct UnionTypes {
        [FieldOffset(offset: 0)]
        public sbyte _i1;

        [FieldOffset(offset: 0)]
        public short _i2;

        [FieldOffset(offset: 0)]
        public int _i4;

        [FieldOffset(offset: 0)]
        public long _i8;

        [FieldOffset(offset: 0)]
        public byte _ui1;

        [FieldOffset(offset: 0)]
        public ushort _ui2;

        [FieldOffset(offset: 0)]
        public uint _ui4;

        [FieldOffset(offset: 0)]
        public ulong _ui8;

        [FieldOffset(offset: 0)]
        public int _int;

        [FieldOffset(offset: 0)]
        public uint _uint;

        [FieldOffset(offset: 0)]
        public short _bool;

        [FieldOffset(offset: 0)]
        public int _error;

        [FieldOffset(offset: 0)]
        public float _r4;

        [FieldOffset(offset: 0)]
        public double _r8;

        [FieldOffset(offset: 0)]
        public long _cy;

        [FieldOffset(offset: 0)]
        public double _date;

        [FieldOffset(offset: 0)]
        public IntPtr _bstr;

        [FieldOffset(offset: 0)]
        public IntPtr _unknown;

        [FieldOffset(offset: 0)]
        public IntPtr _dispatch;

        [FieldOffset(offset: 0)]
        public IntPtr _pvarVal;

        [FieldOffset(offset: 0)]
        public IntPtr _byref;

        [FieldOffset(offset: 0)]
        public Record _record;
    }

    public struct Record {
        public IntPtr _record;
        public IntPtr _recordInfo;
    }
}