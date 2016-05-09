call "%~dp0SetupTools.bat"

:: get git log
set logName=RepoLog.log
git log --pretty=format:"[%%h] %%an %%ad %%s" --date=short --numstat > %logName% 

:: Perform Summary Analysis
set effortFilename=RevisionsByFile.csv
call maat -c git -l %logName% -a revisions > %effortFilename%

:: Complexity by file
set complexityFilename=ComplexityByFile.csv
cloc .\ --by-file --csv --quiet --report-file=%complexityFilename%

:: Combine Effort And Complexity
python .\Tools\merge_comp_freqs.py %effortFilename% %complexityFilename% > EffortAndComplexityByFile.csv
type EffortAndComplexityByFile.csv 