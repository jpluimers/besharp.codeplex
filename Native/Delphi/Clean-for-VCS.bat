:: clean files for a clean build of everything, and before checkin to VCS
del /s /q %~dp0*.dcu
del /s /q %~dp0*.exe
del /s /q %~dp0.dcp
del /s /q %~dp0.bpl
::del /s /q %~dp0.~*
::del /s /q %~dp0.ddp
