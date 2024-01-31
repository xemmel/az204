Clear-Host;

$resource = "https://storage.azure.com/";
$url = "http://169.254.169.254/metadata/identity/oauth2/token?resource=$resource&api-version=2018-02-01";


$response = $null;
$response = Invoke-WebRequest -Method Get -Uri $url -Headers @{ "metadata" = "true" };
$response;

$token = $response.Content | ConvertFrom-Json | Select-Object -ExpandProperty access_token;
$expires = $response.Content | ConvertFrom-Json | Select-Object -ExpandProperty expires_on;

$token;
$token | Set-Clipboard;
$expires;
