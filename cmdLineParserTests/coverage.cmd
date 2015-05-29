cd bin\Debug\
openCover.console.exe  -target:"xunit.console.x86.exe" -targetargs:"cmdLineParserTests.dll" -filter:+[*]cmdLineParser* -output:coverage.xml
cd ..\..\
reportGenerator bin\Debug\coverage.xml .\coverageReport