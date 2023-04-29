Set-Location $PSScriptRoot\..

try {
    dotnet test --collect:"XPlat Code Coverage"

    dotnet tool run reportgenerator `
        -reports:"tests\**\TestResults\**\coverage.cobertura.xml" `
        -targetdir:"coveragereport" `
        -reporttypes:Html

    Invoke-Item '.\coveragereport\index.html'
}
catch {
    Write-Error "An error occurred: $_. Press any key to continue or Ctrl+C to exit."
    $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") | Out-Null

    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
}
