@echo off
@echo COPYING STATIC FILES
REM 1st argument should be Project folder
REM 2nd argument should be project configuration (Debug, Release)
xcopy "%1\bin\%2\netstandard2.0\dist\**" "wwwroot" /y /s /i
