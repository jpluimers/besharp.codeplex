{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit RecordTypeInformationUnit;

// see also: http://wwwswt.informatik.uni-rostock.de/deutsch/Mitarbeiter/michael/lehre/Delphi_WS2000/rtti.pdf

interface

uses
  TypInfo;

{$if CompilerVersion >= 23.0}
  {$define d15up} // Delphi XE and up
{$ifend CompilerVersion >= 23.0}

type
  ///	<summary>
  ///	  For a field in a record points to the
  ///	  <see cref="System.TypInfo|PPTypeInfo" /> for the type information and
  ///	  the memory <see cref="Offset" /> relative to the start of the record.
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    <see cref="TypeInfo" /> can be nil, but only if the RTL has the
  ///	    <c>WEAKREF</c> conditional define. This seems to be a feature that
  ///	    might get introduced in a future compiler and RTL. See for instance
  ///	    these methods:
  ///	  </para>
  ///	  <list type="bullet">
  ///	    <item>
  ///	      <see cref="System.TypInfo|HasWeakRef" />
  ///	    </item>
  ///	    <item>
  ///
  ///	      <see cref="System.Generics.Collections|TEnumerable&lt;T&gt;.HasWeakRef" />
  ///	       ,<br />
  ///	      <see cref="System.Generics.Collections|TManualArrayManager&lt;T&gt;" />
  ///	       ,<br />
  ///	      <see cref="System.Generics.Collections|TList&lt;T&gt;.Create" />,
  ///	      <br />
  ///	      <see cref="System.Generics.Collections|TQueue&lt;T&gt;.Create" />
  ///	    </item>
  ///	    <item>
  ///	      <see cref="System|_CleanupInstance" />,<br />
  ///	      <see cref="System|_InitializeRecord" />,<br />
  ///	      <see cref="System|_InitializeArray" />,<br />
  ///	      <see cref="System|_FinalizeRecord" />,<br />
  ///	      <see cref="System|_FinalizeArray" />,<br />
  ///	      <see cref="System|_AddRefRecord" />,<br />
  ///	      <see cref="System|_AddRefArray" />,<br />
  ///	      <see cref="System|_CopyRecord" /> ,<br />
  ///	      <see cref="System|_CopyArray" />,<br />
  ///	      <see cref="System|DynArraySetLength" />,<br />
  ///	      <see cref="System|RegisterWeakRef" />,<br />
  ///	      <see cref="System|UnregisterWeakRef" />,<br />
  ///	      <see cref="System|RegisterWeakMethodRef" />,<br />
  ///	      <see cref="System|UnregisterWeakMethodRef" />,<br />
  ///	      <see cref="System|_CleanupInstance" />,<br />
  ///	      <see cref="System|_ClosureRemoveWeakRef" />,<br />
  ///	      <see cref="System|_ClosureAddWeakRef" />,<br />
  ///	      <see cref="System|_ClosureArrayRemoveWeakRef" />,<br />
  ///	      <see cref="System|_CopyClosure" />,<br />
  ///	      <see cref="System|_AsgClosureObj" />,<br />
  ///	      <see cref="System|RegisterWeakRef" />,<br />
  ///	      <see cref="System|RegisterWeakRef" />
  ///	    </item>
  ///	    <item>
  ///	      <c>WEAKINSTREF</c> conditional define, see<br />
  ///	      <see cref="System|_InstWeakClear" />,<br />
  ///	      <see cref="System|_InstWeakArrayClear" />,<br />
  ///	      <see cref="System|_InstWeakCopy" />
  ///	    </item>
  ///	    <item>
  ///	      <c>WEAKINTFREF</c> conditional define, see<br />
  ///	      <see cref="System|_IntfWeakClear" />,<br />
  ///	      <see cref="System|_IntfWeakArrayClear" />,<br />
  ///	      <see cref="System|_IntfWeakCopy" />
  ///	    </item>
  ///	  </list>
  ///	  <para>
  ///	    The size of this structure will vary with the bitness of the
  ///	    compiler: 16 bytes for the 64-bit compiler or 8 bytes for the 32-bit
  ///	    compiler.
  ///	  </para>
  ///	  <para>
  ///	    Copied from the <c>implementation</c> section of the
  ///	    <see cref="System" /> and <see cref="System.TypInfo" /> units in
  ///	    Delphi 6 and up.
  ///	  </para>
  ///	</remarks>
  TFieldInfo = packed record
    TypeInfo: PPTypeInfo;
{$ifdef d15up}
  // 32 bit = 8 bytes, 64bit = 16 bytes
    case Integer of
    0: ( Offset: Cardinal );
    1: ( _Dummy: NativeUInt );
{$else}
    Offset: Cardinal;
{$endif d15up}
  end;

  ///	<summary>
  ///	  <para>
  ///	    Points to a <see cref="TFieldTable" /> structure.
  ///	  </para>
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    Copied from the <c>implementation</c> section of the
  ///	    <see cref="System" /> and <see cref="System.TypInfo" /> units in
  ///	    Delphi 6 and up.
  ///	  </para>
  ///	</remarks>
  PFieldTable = ^TFieldTable;

  ///	<summary>
  ///	  <para>
  ///	    Even though Delphi XE2 introduced the <see cref="System.Rtti" /> unit
  ///	    with the <see cref="System.Rtti|TValue" /> type, there is still very
  ///	    limited RTTI on record types.
  ///	  </para>
  ///	  <para>
  ///	    This structure contains that RTTI.
  ///	  </para>
  ///	  <para>
  ///	    Only managed fields are included in the RTTI. Those fields can have
  ///	    <see cref="System.TypInfo|TTypeKind">Info.Kind</see> values of
  ///	    <c>tkClass</c>, <c>tkArray</c>, <c>tkRecord</c>, <c>tkInterface</c>,
  ///	    <c>tkDynArray</c>, <c>tkPointer</c>
  ///	  </para>
  ///	  <para>
  ///	    Delphi XE3 introduced a method
  ///	    <see cref="System.Classes|CheckForCycles" /> in the
  ///	    <see cref="System.Classes" /> unit, that shows the use of this RTTI.
  ///	  </para>
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    The size of this structure will vary with the bitness of the
  ///	    compiler: 16 bytes for the 64-bit compiler or 8 bytes for the 32-bit
  ///	    compiler.
  ///	  </para>
  ///	  <para>
  ///	    Delphi XE and up made the RTL support x64, but Delphi XE2 added the
  ///	    x64 compiler.
  ///	  </para>
  ///	  <para>
  ///	    Copied from the <c>implementation</c> section of the
  ///	    <see cref="System" /> and <see cref="System.TypInfo" /> units in
  ///	    Delphi 6 and up.
  ///	  </para>
  ///	</remarks>
  TFieldTable = packed record
    X: Word;
    Size: Cardinal;
    Count: Cardinal;
    Fields: array [0..0] of TFieldInfo;
  end;

///	<summary>
///	  <para>
///	    Gets the <see cref="PFieldTable" /> from the
///	    <see cref="System.TypInfo|PTypeInfo" />.
///	  </para>
///	  <para>
///	    Algorithm from many places in the System unit, like the
///	    <see cref="System|_InitializeRecord" />.
///	  </para>
///	</summary>
function GetFieldTable(TypeTypeInfo: PTypeInfo): PFieldTable;

implementation

function GetFieldTable(TypeTypeInfo: PTypeInfo): PFieldTable;
begin
  Result := PFieldTable(Integer(TypeTypeInfo) + Byte(PTypeInfo(TypeTypeInfo).Name[0]));
end;

end.

