:: parameters
:: 1="$(Platform)"
:: 2="$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\"
:: 3="$(OUTPUTDIR)"
:: from a pre-build or post-build event, call it like this:
:: "$(INPUTDIR)Copy_FastMM_FullDebugMode_Dll_PostBuildEvent.bat" "$(Platform)" "$(INPUTDIR)..\..\..\..\..\fastmm.sourceforge.net\FullDebugMode DLL\Precompiled\" "$(OUTPUTDIR)"
>> %temp%\prebuild.txt echo 1=%1
>> %temp%\prebuild.txt echo 2=%2
>> %temp%\prebuild.txt echo 3=%3
if %1=="Win32" copy %2\FastMM_FullDebugMode.dll   %3 >> %temp%\prebuild.txt
if %1=="Win64" copy %2\FastMM_FullDebugMode64.dll %3 >> %temp%\prebuild.txt
