$DirectoryName = Read-Host "Por favor, ingrese el nombre del directorio"

$basePath = "src/app/modules/$DirectoryName"
$paths = @(
  "$basePath/application",
  "$basePath/infrastructure",
  "$basePath/domain",
  "$basePath/ui"
)

foreach ($path in $paths) {
  New-Item -ItemType Directory -Path $path -Force
}

Write-Host "Se han creado las carpetas en: $basePath"
