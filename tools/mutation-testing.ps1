Set-Location $PSScriptRoot\..

dotnet tool run dotnet-stryker --config-file "stryker-config.json" --reporter "html" --open-report
