dotnet restore
pushd ./src/salaryimport
dotnet build -r osx.10.12-x64
dotnet publish -c release -r osx.10.12-x64
popd
