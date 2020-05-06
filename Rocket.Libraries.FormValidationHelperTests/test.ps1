dotnet clean
rm -r ./TestResults
mkdir ./TestResults
dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=cobertura
cd ./TestResults
reportgenerator -reports:coverage.cobertura.xml -targetdir:reports
cd ..