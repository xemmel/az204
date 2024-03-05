
### Create Service Principal

In entra create new App Registration
  - Create secret
  - Get *App Name*, *ClientId*, *ClientSecret (remember value NOT id), tenantId and **Azure Subscription Id**

Get the token examine it in *jwt.ms*

```powershell

Clear-Host;

$clientId = "...";
$tenantId = "551c586d-a82d-4526-b186-d061ceaa589e";
$secret = "....";

$scope = "https://management.azure.com/.default";

$url = "https://login.microsoftonline.com/$($tenantId)/oauth2/v2.0/token";


$body = "";
$body += "client_id=$($clientId)";
$body += "&client_secret=$($secret)";
$body += "&grant_type=client_credentials";
$body += "&scope=$($scope)";

$response = $null;
$response = Invoke-WebRequest `
    -Uri $url `
    -Method Post `
    -Body $body;

$token = $response.Content | ConvertFrom-Json | Select-Object -ExpandProperty access_token;
$token | Set-Clipboard;
$token;



```


Create a new *Service Connection* in *DevOps* in your Project (Project settings)

   - Azure Resource Manager
   - Service Principal (Manual)
  
Fill in all values until the **Verify** button

Your verify will fail because the new service principal does not have permissions on the Azure Subsription

Give the new App Reg **Owner** permissions on the subscription (Remember under Conditions you need to set *Allow users to assign all roles*)

- Subscription 
- Access Control (IAM)
- Add Role Assignment

After created verify that verification now succeeds.

Give your Service Connection a name and allow it on all Pipelines.


