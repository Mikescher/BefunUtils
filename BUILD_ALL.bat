set VCTargetsPath=C:\Program Files (x86)\MSBuild\Microsoft.Cpp\v4.0\V140

msbuild BefunUtils.sln /nologo /t:Clean,Build /p:Platform="Any CPU" /p:Configuration=Release /verbosity:m /p:VisualStudioVersion=14.0

@PAUSE
