// Decompiled with JetBrains decompiler
// Type: Variant
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct Variant
{
  [FieldOffset(0)]
  public Variant.TypeUnion _typeUnion;
  [FieldOffset(0)]
  public Decimal _decimal;

  [StructLayout(LayoutKind.Explicit)]
  public struct TypeUnion
  {
    [FieldOffset(0)]
    public ushort _vt;
    [FieldOffset(2)]
    public ushort _wReserved1;
    [FieldOffset(4)]
    public ushort _wReserved2;
    [FieldOffset(6)]
    public ushort _wReserved3;
    [FieldOffset(8)]
    public Variant.UnionTypes _unionTypes;
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct UnionTypes
  {
    [FieldOffset(0)]
    public sbyte _i1;
    [FieldOffset(0)]
    public short _i2;
    [FieldOffset(0)]
    public int _i4;
    [FieldOffset(0)]
    public long _i8;
    [FieldOffset(0)]
    public byte _ui1;
    [FieldOffset(0)]
    public ushort _ui2;
    [FieldOffset(0)]
    public uint _ui4;
    [FieldOffset(0)]
    public ulong _ui8;
    [FieldOffset(0)]
    public int _int;
    [FieldOffset(0)]
    public uint _uint;
    [FieldOffset(0)]
    public short _bool;
    [FieldOffset(0)]
    public int _error;
    [FieldOffset(0)]
    public float _r4;
    [FieldOffset(0)]
    public double _r8;
    [FieldOffset(0)]
    public long _cy;
    [FieldOffset(0)]
    public double _date;
    [FieldOffset(0)]
    public IntPtr _bstr;
    [FieldOffset(0)]
    public IntPtr _unknown;
    [FieldOffset(0)]
    public IntPtr _dispatch;
    [FieldOffset(0)]
    public IntPtr _pvarVal;
    [FieldOffset(0)]
    public IntPtr _byref;
    [FieldOffset(0)]
    public Variant.Record _record;
  }

  public struct Record
  {
    public IntPtr _record;
    public IntPtr _recordInfo;
  }
}
