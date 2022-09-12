Set-Location $PSScriptRoot\..

dotnet tool run dotnet-stryker --config-file "stryker-config.json" --reporter "html"

$stryker_path = ".\StrykerOutput"
$test_report_folder_object = Get-ChildItem $stryker_path | Where-Object { $_.PSIsContainer } | Sort-Object CreationTime -desc | Select-Object -f 1 | foreach {$_.Name}

if ($test_report_folder_object) {
    Invoke-Item "$stryker_path\$test_report_folder_object\reports\mutation-report.html"
}