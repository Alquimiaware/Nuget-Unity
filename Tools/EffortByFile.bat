call "%~dp0SetupTools.bat"

:: get git log
set logName=RepoLog.log
git log --pretty=format:"[%%h] %%an %%ad %%s" --date=short --numstat > %logName% 

:: Perform Summary Analysis
set outputName=RevisionsByFile.csv
call maat -c git -l %logName% -a revisions > %outputName%
type %outputName%