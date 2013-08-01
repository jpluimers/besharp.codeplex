:: parse the output of `svn status` to see which files/directories have been 'switched' to a different branch
:: then get information about those switches
@echo off

echo If you do not see any output below this line, then the repository root could not be found

:: trick: set delimiter to a character disallowed in Windows filenames: \ / : * ? " < > |
:: see http://support.microsoft.com/kb/177506/en-us
:: that is also not part of the `svn status` output
:: see http://www.visualsvn.com/support/svnbook/ref/svn/c/status/
:: use back quotes and a circonflex + pipe to do piping in a for loop
:: see http://stackoverflow.com/questions/6413097/how-to-make-pipe-in-for-cycle-in-second-section
:: redirect stderr to stdout 2 greater than ampersand 1
:: see http://stackoverflow.com/questions/8842782/how-can-i-redirect-the-output-of-a-command-running-in-a-batch-loop-to-a-file
FOR /F "delims=\ USEBACKQ" %%l IN (`2^>^&1 svn info %* ^| find "Repository Root: "`) DO call :line "%%l"
goto :eof

:line
setlocal ENABLEDELAYEDEXPANSION
set line=%1
:: substring http://stackoverflow.com/questions/636381/what-is-the-best-way-to-do-a-substring-in-a-batch-file
:: %1 is quoted; first quote is at position 1, last at position -1
:: echo %line%
set URL=%line:~18,-1%
call :switched "%URL%"
endlocal
goto :eof

:switched
echo Repository Root: "%URL%"

goto :eof

