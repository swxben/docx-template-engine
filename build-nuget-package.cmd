@echo off
call "%VS110COMNTOOLS%vsvars32.bat"
mkdir log\
mkdir lib\net40\

msbuild.exe /ToolsVersion:4.0 "src\swxben.docxtemplateengine\swxben.docxtemplateengine.csproj" /p:configuration=Release
utilities\nuget.exe pack swxben.docxtemplateengine.nuspec
pause