# Rebuilds only the local estate_dev schema in this repository's Compose PostgreSQL service.
# All rows in that schema are discarded. Schema generation must succeed before any data is removed.
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$composeArgs = @('compose', '--project-directory', $repoRoot, '--file', (Join-Path $repoRoot 'compose.yaml'))
$sqlPath = Join-Path ([System.IO.Path]::GetTempPath()) ("estate-schema-$([Guid]::NewGuid().ToString('N')).sql")
$originalConnectionString = $env:ConnectionStrings__PostgreSql
$originalEnvironment = $env:ASPNETCORE_ENVIRONMENT
$originalOutputEncoding = $OutputEncoding

Push-Location $repoRoot
try {
    # Use the checked-in local development database, independently of user secrets or shell settings.
    $env:ConnectionStrings__PostgreSql = 'Host=localhost;Database=estate_dev;Username=estate;Password=estate_dev_password'
    $env:ASPNETCORE_ENVIRONMENT = 'Development'

    dotnet tool restore
    if ($LASTEXITCODE -ne 0) { throw 'Could not restore the EF Core tool.' }

    dotnet ef dbcontext script --project src/EState.Infrastructure --startup-project src/EState.Api --output $sqlPath
    if ($LASTEXITCODE -ne 0) { throw 'Could not generate the database schema.' }

    & docker @composeArgs up --detach --wait --wait-timeout 60 postgres
    if ($LASTEXITCODE -ne 0) { throw 'Could not start the local PostgreSQL service.' }

    Write-Host 'Recreating the local estate_dev schema. All existing rows in this schema will be discarded.'
    $resetSql = "DROP SCHEMA public CASCADE;`nCREATE SCHEMA public;`n" + [System.IO.File]::ReadAllText($sqlPath)
    $OutputEncoding = [System.Text.UTF8Encoding]::new($false)

    # One transaction restores the old schema if any statement fails.
    $resetSql | & docker @composeArgs exec -T postgres psql --username estate --dbname estate_dev --set ON_ERROR_STOP=1 --single-transaction
    if ($LASTEXITCODE -ne 0) { throw 'Database reset failed; the transaction was rolled back.' }

    Write-Host 'Local database schema recreated from the current EF model.'
}
finally {
    $env:ConnectionStrings__PostgreSql = $originalConnectionString
    $env:ASPNETCORE_ENVIRONMENT = $originalEnvironment
    $OutputEncoding = $originalOutputEncoding
    if (Test-Path -LiteralPath $sqlPath) { Remove-Item -LiteralPath $sqlPath }
    Pop-Location
}
