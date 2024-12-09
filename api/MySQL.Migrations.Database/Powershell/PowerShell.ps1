Write-Host "Power Shell - Fluent Migrator";
$Env:DOTNET_ROLL_FORWARD = "LatestMajor"
$webConfig = ".\web.config"
$xml = [xml](get-content $webConfig)
$dbInfo = $xml.SelectNodes("/configuration/connectionStrings/add") | Select connectionString
$connection = $dbInfo.connectionString.ToString()

  Write-Host "Power Shell - Compilando Proyecto";
  dotnet build ..\MySQL.Migrations.Database.csproj

Do {
  $opUpDown = '';
  $assembly = '../bin/Debug/net8.0/MySQL.Migrations.Database.dll';
  $opDetail = '';
  $opDetailCommand = '';

  $op = Read-Host "Write options: migrate or rollback";

  if ($op -eq "migrate") {
    $opUpDown = Read-Host "Write options: up or down";
    $opDetail = Read-Host "Write options: all or to";
    dotnet fm $op -p MySql -c $connection -a $assembly -o="migration_up.sql" --allowDirtyAssemblies $opUpDown;
  }
  if ($op -eq "rollback") {
    $opDetail = Read-Host "Write options: last or to";
    dotnet fm $op -p MySql -c $connection -a $assembly -o="migration_down.sql" --allowDirtyAssemblies;
  }

  if ($opDetail -eq "to") {
    $version = Read-Host "Write Versions: ";
    $opDetailCommand = '-t ' + $version;
    dotnet fm $op -p MySql -c $connection -a $assembly -o="migration.sql" --allowDirtyAssemblies $opUpDown $opDetailCommand;
  }


  $i++
}
While ($i -le 9999)
