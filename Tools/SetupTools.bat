:: Setting Up All Required Tools
:: Project Root
set ProjectRoot=%~dp0..\
cd %ProjectRoot%
:: Git
set GIT_PAGER=cat
set PATH=%PATH%;%PROGRAMFILES(X86)%\Git\cmd
:: Code Maat
set PATH=%PATH%;%PROGRAMFILES(X86)%\winmaat0.8.5
:: Cloc
set PATH=%PATH%;%PROGRAMFILES(X86)%\cloc
:: Python27
set PATH=%PATH%;C:\Python27