:: parse the output of `svn status` to see which files/directories have been 'switched' to a different branch
:: then get information about those switches
@echo off

echo If you do not see any output below this line, then none of your files/directories have been switched.

:: trick: set delimiter to a character disallowed in Windows filenames: \ / : * ? " < > |
:: see http://support.microsoft.com/kb/177506/en-us
:: that is also not part of the `svn status` output
:: see http://www.visualsvn.com/support/svnbook/ref/svn/c/status/
FOR /F "delims=:" %%l IN ('svn status --depth=infinity %*') DO call :line "%%l"
goto :eof

:line
setlocal ENABLEDELAYEDEXPANSION
set line=%1
:: substring http://stackoverflow.com/questions/636381/what-is-the-best-way-to-do-a-substring-in-a-batch-file
:: %1 is quoted; first quote is at position 1, last at position -1
set switch=%line:~5,1%
set subdirectory=%line:~9,-1%
if "%switch%"=="S" call :switched "%subdirectory%"
endlocal
goto :eof

:switched
echo subdirectory: "%subdirectory%" is switched
svn info %1

goto :eof

