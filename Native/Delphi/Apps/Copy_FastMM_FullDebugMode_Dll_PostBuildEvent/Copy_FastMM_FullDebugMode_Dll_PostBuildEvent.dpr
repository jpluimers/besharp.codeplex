program Copy_FastMM_FullDebugMode_Dll_PostBuildEvent;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  FastMM4 in '..\..\..\..\..\fastmm.sourceforge.net\FastMM4.pas',
  FastMM4Messages in '..\..\..\..\..\fastmm.sourceforge.net\FastMM4Messages.pas',
  System.SysUtils;

{$ifndef FullDebugMode}
  {$message error 'FullDebugMode needs to be defined to load FastMM_FullDebugMode.dll or FastMM_FullDebugMode64.dll' }
// $message without quotes gives you a funny error:
// [DCC Error] E1030 Invalid compiler directive: 'message'
//  {$message error FullDebugMode needs to be defined to load FastMM_FullDebugMode.dll or FastMM_FullDebugMode64.dll }
{$endif}

{ Note there is a lot of stuff you cannot do in build events:
:: double colons as remarks
setlocal
!expansion!
%expansion%

If you do any of these, the build event will not fire, but it will also not fail.

>> %temp%\prebuild.txt echo %DATE%_%TIME%
:: http://stackoverflow.com/questions/12524909/printing-current-date-and-time-in-dos-script
setlocal enableDelayedExpansion
>> %temp%\prebuild.txt echo !DATE!_!TIME!
endlocal

Sample to show all info we can get:

>> %temp%\prebuild.txt echo BDS: $(BDS)
>> %temp%\prebuild.txt echo Config: $(Config)
>> %temp%\prebuild.txt echo DEFINES: $(DEFINES)
>> %temp%\prebuild.txt echo DIR: $(DIR)
>> %temp%\prebuild.txt echo INCLUDEPATH: $(INCLUDEPATH)
>> %temp%\prebuild.txt echo INPUTDIR: $(INPUTDIR)
>> %temp%\prebuild.txt echo INPUTEXT: $(INPUTEXT)
>> %temp%\prebuild.txt echo INPUTFILENAME: $(INPUTFILENAME)
>> %temp%\prebuild.txt echo INPUTNAME: $(INPUTNAME)
>> %temp%\prebuild.txt echo INPUTPATH: $(INPUTPATH)
>> %temp%\prebuild.txt echo LOCALCOMMAND: $(LOCALCOMMAND)
>> %temp%\prebuild.txt echo OUTPUTDIR: $(OUTPUTDIR)
>> %temp%\prebuild.txt echo OUTPUTEXT: $(OUTPUTEXT)
>> %temp%\prebuild.txt echo OUTPUTFILENAME: $(OUTPUTFILENAME)
>> %temp%\prebuild.txt echo OUTPUTNAME: $(OUTPUTNAME)
>> %temp%\prebuild.txt echo OUTPUTPATH: $(OUTPUTPATH)
>> %temp%\prebuild.txt echo Path: $(Path)
>> %temp%\prebuild.txt echo Platform: $(Platform)
>> %temp%\prebuild.txt echo PROJECTDIR: $(PROJECTDIR)
>> %temp%\prebuild.txt echo PROJECTEXT: $(PROJECTEXT)
>> %temp%\prebuild.txt echo PROJECTFILENAME: $(PROJECTFILENAME)
>> %temp%\prebuild.txt echo PROJECTNAME: $(PROJECTNAME)
>> %temp%\prebuild.txt echo PROJECTPATH: $(PROJECTPATH)
>> %temp%\prebuild.txt echo SAVE: $(SAVE)
>> %temp%\prebuild.txt echo SystemRoot: $(SystemRoot)
>> %temp%\prebuild.txt echo WINDIR: $(WINDIR)

Output notes:

INPUTDIR and OUTPUTDIR contain a trailing backslash; PROJECTDIR does not.
BDS path is lowercase, though the actual directory names are upper case (so you need case insensitive string compares)

BDS: c:\program files (x86)\embarcadero\rad studio\9.0
Config: Debug
DEFINES: DEBUG;FullDebugMode;
DIR:
INCLUDEPATH: c:\program files (x86)\embarcadero\rad studio\9.0\lib\Win32\release;C:\Users\developer\Documents\RAD Studio\9.0\Imports;c:\program files (x86)\embarcadero\rad studio\9.0\Imports;C:\Users\Public\Documents\RAD Studio\9.0\Dcp;c:\program files (x86)\embarcadero\rad studio\9.0\include;C:\Program Files (x86)\FastReports\LibD16;c:\program files (x86)\embarcadero\rad studio\9.0\RaveReports\Lib;C:\Program Files (x86)\Raize\CS5\Lib\RS-XE2\Win32;c:\program files (x86)\embarcadero\rad studio\9.0\RaveReports\Lib;
INPUTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\
INPUTEXT: .dproj
INPUTFILENAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj
INPUTNAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent
INPUTPATH: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj
LOCALCOMMAND:
OUTPUTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\
OUTPUTEXT: .exe
OUTPUTFILENAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.exe
OUTPUTNAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent
OUTPUTPATH: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.exe
Path: C:\Program Files (x86)\CollabNet;C:\Program Files (x86)\Embarcadero\RAD Studio\9.0\bin;C:\Users\Public\Documents\RAD Studio\9.0\Bpl;C:\Program Files (x86)\Embarcadero\RAD Studio\9.0\bin64;C:\Users\Public\Documents\RAD Studio\9.0\Bpl\Win64;C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Program Files (x86)\Embarcadero\Prism\bin;C:\Program Files (x86)\Embarcadero\Prism\bin;C:\Program Files\TortoiseSVN\bin
Platform: Win32
PROJECTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent
PROJECTEXT: .dproj
PROJECTFILENAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj
PROJECTNAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent
PROJECTPATH: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj
SAVE:
SystemRoot: C:\Windows
WINDIR: C:\Windows


"$(INPUTDIR)Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.bat" "$(Platform)" "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\" "$(OUTPUTDIR)"



if "$(Platform)"=="Win32" copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "$(OUTPUTDIR)"
if "$(Platform)"=="Win64" copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "$(OUTPUTDIR)"

INPUTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\



if !$(Platform)!==!Win32! echo Win32 A >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo Win64 A >> %temp%\prebuild.txt
if !$(Platform)!==!Win32! copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "$(OUTPUTDIR)"
if !$(Platform)!==!Win32! echo Win32 B >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo Win64 B >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "$(OUTPUTDIR)"
if !$(Platform)!==!Win32! echo Win32 C >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo Win64 C >> %temp%\prebuild.txt

if !$(Platform)!==!Win32! echo Win32 A >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo Win64 A >> %temp%\prebuild.txt
if !$(Platform)!==!Win32! echo copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "$(OUTPUTDIR)" >> %temp%\prebuild.txt
if !$(Platform)!==!Win32! copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "$(OUTPUTDIR)"
if "$(Platform)"=="Win32" copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "$(OUTPUTDIR)"
if !$(Platform)!==!Win32! echo Win32 B >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo Win64 B >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "$(OUTPUTDIR)"  >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "$(OUTPUTDIR)"
if "$(Platform)"=="Win64" copy "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "$(OUTPUTDIR)"
if !$(Platform)!==!Win32! echo Win32 C >> %temp%\prebuild.txt
if !$(Platform)!==!Win64! echo Win64 C >> %temp%\prebuild.txt

They are not batch files, see this error message:

[Exec Error] The command ">> %temp%\prebuild.txt echo BDS: c:\program files (x86)\embarcadero\rad studio\9.0&&
>> %temp%\prebuild.txt echo Config: Debug&&
>> %temp%\prebuild.txt echo DEFINES: DEBUG;FullDebugMode;&&
>> %temp%\prebuild.txt echo DIR: &&
>> %temp%\prebuild.txt echo INCLUDEPATH: C:\LMD2013\lib\d16\Win32;c:\program files (x86)\embarcadero\rad studio\9.0\lib\Win32\release;C:\Users\developer\Documents\RAD Studio\9.0\Imports;c:\program files (x86)\embarcadero\rad studio\9.0\Imports;C:\Users\Public\Documents\RAD Studio\9.0\Dcp;c:\program files (x86)\embarcadero\rad studio\9.0\include;C:\Program Files (x86)\FastReports\LibD16;c:\program files (x86)\embarcadero\rad studio\9.0\RaveReports\Lib;C:\Program Files (x86)\Raize\CS5\Lib\RS-XE2\Win32;c:\users\developer\dropbox\shared-webspheremq\cas400mq-sources\cas400wmq7xe2-sources\bin;c:\users\developer\dropbox\shared-webspheremq\cas400mq-sources\cas400wmq7xe2-sources\cas400\cas400components\bin;c:\program files (x86)\embarcadero\rad studio\9.0\RaveReports\Lib;C:\Program Files (x86)\Gurock Software\SmartInspect Professional\lib\delphi\delphiXE2\;C:\Program Files (x86)\Developer Express.VCL\Library\RS16;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Dcu\D16\win32;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Source;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Source\CodeGen;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Source\DataSnap;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Source\ZLib;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Source\RODEC;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Source\Synapse;C:\Program Files (x86)\RemObjects Software\Everwood\Bin&&
>> %temp%\prebuild.txt echo INPUTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\&&
>> %temp%\prebuild.txt echo INPUTEXT: .dproj&&
>> %temp%\prebuild.txt echo INPUTFILENAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj&&
>> %temp%\prebuild.txt echo INPUTNAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent&&
>> %temp%\prebuild.txt echo INPUTPATH: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj&&
>> %temp%\prebuild.txt echo LOCALCOMMAND: &&
>> %temp%\prebuild.txt echo OUTPUTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\&&
>> %temp%\prebuild.txt echo OUTPUTEXT: .exe&&
>> %temp%\prebuild.txt echo OUTPUTFILENAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.exe&&
>> %temp%\prebuild.txt echo OUTPUTNAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent&&
>> %temp%\prebuild.txt echo OUTPUTPATH: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.exe&&
>> %temp%\prebuild.txt echo Path: C:\Program Files (x86)\CollabNet;C:\Program Files (x86)\Embarcadero\RAD Studio\9.0\bin;C:\Users\Public\Documents\RAD Studio\9.0\Bpl;C:\Program Files (x86)\Embarcadero\RAD Studio\9.0\bin64;C:\Users\Public\Documents\RAD Studio\9.0\Bpl\Win64;C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Program Files (x86)\Embarcadero\Prism\bin;C:\Program Files (x86)\Embarcadero\Prism\bin;C:\Program Files (x86)\GtkSharp\2.12\bin;C:\Program Files (x86)\IBM\WebSphere MQ\bin64;C:\Program Files (x86)\IBM\WebSphere MQ\bin;C:\Program Files (x86)\IBM\WebSphere MQ\tools\c\samples\bin;C:\Program Files (x86)\Microsoft Team Foundation Server Integration Tools\;C:\Program Files\TortoiseSVN\bin;C:\Program Files (x86)\Developer Express.VCL\Library\RS16;C:\Program Files (x86)\Developer Express.VCL\Library\RS16\Win64;C:\Program Files (x86)\RemObjects Software\Everwood\Bin;C:\Program Files (x86)\RemObjects Software\RemObjects SDK for Delphi\Dcu\D16\win32;c:\bin;c:\bin\BC3Portable&&
>> %temp%\prebuild.txt echo Platform: Win32&&
>> %temp%\prebuild.txt echo PROJECTDIR: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent&&
>> %temp%\prebuild.txt echo PROJECTEXT: .dproj&&
>> %temp%\prebuild.txt echo PROJECTFILENAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj&&
>> %temp%\prebuild.txt echo PROJECTNAME: Copy_FastMM_FullDebugMode_Dll_PostBuildEvent&&
>> %temp%\prebuild.txt echo PROJECTPATH: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.dproj&&
>> %temp%\prebuild.txt echo SAVE: &&
>> %temp%\prebuild.txt echo SystemRoot: C:\Windows&&
>> %temp%\prebuild.txt echo WINDIR: C:\Windows&&
if !Win32!==!Win32! echo Win32 A >> %temp%\prebuild.txt&&
if !Win32!==!Win64! echo Win64 A >> %temp%\prebuild.txt&&
if !Win32!==!Win32! echo copy "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\" >> %temp%\prebuild.txt&&
if !Win32!==!Win32! copy "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\"&&
if "Win32"=="Win32" copy "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode.dll" "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\"&&
if !Win32!==!Win32! echo Win32 B >> %temp%\prebuild.txt&&
if !Win32!==!Win64! echo Win64 B >> %temp%\prebuild.txt&&
if !Win32!==!Win64! echo copy "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\"  >> %temp%\prebuild.txt&&
if !Win32!==!Win64! copy "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\"&&
if "Win32"=="Win64" copy "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\FastMM_FullDebugMode64.dll" "C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug\"&&
if !Win32!==!Win32! echo Win32 C >> %temp%\prebuild.txt&&
if !Win32!==!Win64! echo Win64 C >> %temp%\prebuild.txt"
 exited with code 255.
 }

{
Copy_FastMM_FullDebugMode_Dll_PostBuildEvent

C:\Users\developer\Documents\SVN\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled
C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent

Win32:
From: FastMM_FullDebugMode.dll
To: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win32\Debug
Win64:
From: FastMM_FullDebugMode64.dll
To: C:\Users\developer\Documents\SVN\BeSharp.codeplex.com\Native\Delphi\Apps\Copy_FastMM_FullDebugMode_Dll_PostBuildEvent\Win64\Debug
}

(* Note: FulDebugMode is not available on OS X and Linux:

{Some features not currently supported under Kylix / OS X}
{$ifdef POSIX}
  {$undef FullDebugMode}
  {$undef LogErrorsToFile}
  {$undef LogMemoryLeakDetailToFile}
  {$undef ShareMM}
  {$undef AttemptToUseSharedMM}
  {$undef RequireIDEPresenceForLeakReporting}
  {$undef UseOutputDebugString}
  {$ifdef PIC}
    {BASM version does not support position independent code}
    {$undef ASMVersion}
  {$endif}
{$endif}
*)

begin
  try
    TObject.Create(); // artificial memory leak
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.
