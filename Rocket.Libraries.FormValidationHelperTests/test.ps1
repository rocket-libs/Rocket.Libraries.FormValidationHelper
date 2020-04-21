dotnet clean
Remove-Item ./TestResults -Recurse -ErrorAction Ignore
mkdir ./TestResults -ErrorAction Ignore
dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=cobertura
Set-Location ./TestResults -ErrorAction Stop
reportgenerator -reports:coverage.cobertura.xml -targetdir:reports
Set-Location ..