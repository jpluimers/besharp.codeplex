:: check if user is administrator

  "C:\Windows\system32\cacls.exe" "C:\Windows\system32\config\system" 1>nul 2>&1  && (goto :isAdmin)
:isNoAdmin
  echo you need to be Administrator, and (when under Vista or higher) run this using using UAC
  goto :exit

:isAdmin

:: Gets the right link for x86 (32-bit) program files
IF /I %PROCESSOR_ARCHITECTURE% == amd64 goto :x64
IF /I %PROCESSOR_ARCHITEW6432% == amd64 goto :x64
goto :x86
:x64
   :: OS is 64bit
   set ProgramFilesX86=%ProgramFiles(x86)%
  goto :continue
:x86
   :: OS is 32bit
  set ProgramFilesX86=%ProgramFiles%
  goto :continue
 
:continue

:: create the right directory and copy the files

  setlocal

  set TargetDirectory=%ProgramFilesX86%\GExperts for RAD Studio XE4

  mkdir "%TargetDirectory%"
::  set FilesToCopy=DbugIntf.pas ExpertManager.exe GExperts.chm GExpertsDebugWindow.exe GExpertsGrep.exe regularexpert\GExpertsRSXE4.dll Readme.txt preview.pas
  set FilesToCopy=ExpertManager.exe GExperts.chm regularexpert\GExpertsRSXE4.dll preview.pas

  for %%f in (%FilesToCopy%) do copy /y %%f "%TargetDirectory%\"
  set RegFile="%TargetDirectory%\ExpertsXE4.reg" 
::  explorer /select,"%TargetDirectory%\GExpertsRSXE4.dll"

:: expand backslash into double backslash for .REG file
  set ExpertTarget="%TargetDirectory%\GExpertsRSXE4.dll"
  set ExpertTarget=%ExpertTarget:\=\\%

::Windows Registry Editor Version 5.00
::
::[HKEY_CURRENT_USER\Software\Embarcadero\BDS\11.0\Experts]
::"GExperts"="C:\\Program Files (x86)\\GExperts for RAD Studio XE4\\GExpertsRSXE4.dll"
::
  echo Windows Registry Editor Version 5.00 >%RegFile%
  echo. >>%RegFile%
  echo [HKEY_CURRENT_USER\Software\Embarcadero\BDS\11.0\Experts] >>%RegFile%
  echo "GExperts"=%ExpertTarget% >>%RegFile%
  echo. >>%RegFile%
::  explorer /select,%RegFile%
  regedit /S %RegFile%

  endlocal

  goto :exit


:exit