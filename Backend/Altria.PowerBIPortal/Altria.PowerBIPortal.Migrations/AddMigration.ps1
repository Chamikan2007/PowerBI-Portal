$Migration = Read-Host -Prompt 'Enter Migration Name'

$LastMigration =''
$MigrationsPath = ".\Migrations"
$ExistingMigrations = Get-ChildItem -Path $MigrationsPath -Filter *.Designer.cs -Recurse -File
$ExistingMigrationCount = ($ExistingMigrations | Measure-Object).Count
if($ExistingMigrationCount -gt 0){
    $Temp = $ExistingMigrations[$ExistingMigrationCount - 1].Name.Substring(15)
    $LastMigration = $Temp.Substring(0, $Temp.Length - 12)
}

Write-Host "Generating migrations." -ForegroundColor Yellow

dotnet ef migrations add $Migration --output-dir $MigrationsPath

Write-Host "Migration, '${Migration}' generated successfully." -ForegroundColor Yellow

$ScriptPath = ".\Scripts"
if(!(Test-Path -Path $ScriptPath)){
    $n = New-Item $ScriptPath -ItemType Directory
}

$Scripts = Get-ChildItem -Path $ScriptPath -Filter *.sql -Recurse -File | Measure-Object
$Count = $Scripts.Count + 1
$FileName = $Count.ToString("00") + "_" + $Migration + ".sql"

Write-Host "Generating scripts." -ForegroundColor Yellow

$OutputPath = "Scripts/" + $FileName

if ($LastMigration) {
    dotnet-ef migrations script $LastMigration --output $OutputPath
}
else {
    dotnet-ef migrations script --output $OutputPath
}

Write-Host "Script, '${fileName}' generated successfully." -ForegroundColor Yellow