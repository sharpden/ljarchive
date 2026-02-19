# Run from Developer Command Prompt for VS 2019
all:
	msbuild ljArchive.2005.sln /t:Build /p:Configuration=Release
	"c:\Program Files (x86)\NSIS\Bin\makensis.exe" makesetup.nsi
