call %~dp0SetupTools.bat

:: get git log
set logName=RepoLog.log
git log --pretty=format:"[%%h] %%an %%ad %%s" --date=short --numstat > %logName% 

:: Perform Summary Analysis
call maat -c git -l %logName% -a summary > RepoSummary.txt
type RepoSummary.txt
pause