Set-Location $PSScriptRoot\..

try {
    dotnet test --collect:"XPlat Code Coverage"

    $test_projects = ("tests\Off.Net.Pdf.Core.Tests\")

    foreach ($test_project in $test_projects) {
        $test_report_folder_object = Get-ChildItem "$test_project\TestResults" | Where-Object { $_.PSIsContainer } | Sort-Object CreationTime -desc | Select-Object -f 1
        $test_report_folder_name = $test_report_folder_object.PSChildName
        dotnet tool run reportgenerator `
        -reports:"$test_project\TestResults\$test_report_folder_name\coverage.cobertura.xml" `
        -targetdir:"coveragereport" `
        -reporttypes:Html
    }

    Invoke-Item '.\coveragereport\index.html'
}
catch {
    Write-Error "An error occurred: $_. Press any key to continue or Ctrl+C to exit."
    $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") | Out-Null

    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
}
