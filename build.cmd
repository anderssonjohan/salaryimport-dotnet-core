@echo off
dotnet restore
pushd .\src\salaryimport
dotnet build -r win10-x64
dotnet publish -c release -r win10-x64
popd