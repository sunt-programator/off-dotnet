Set-StrictMode -Version Latest

try {
    Set-Location $PSScriptRoot\..\benchmarks\Off.Net.Pdf.Core.Benchmarks

    dotnet run -c Release2 -- --job short --runtimes net6.0 --filter *
}
catch {
    Write-Error "An error occurred: $_. Press any key to continue or Ctrl+C to exit."
    $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") | Out-Null

    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
}
