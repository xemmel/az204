# Azure Developer
## Morten la Cour


### Function App Local

1. Create new Function app 

```powershell

func init theisolatedfunctions --worker-runtime dotnetIsolated

```

2. Create HttpTrigger Function

```powershell

func new --name MyFirstHttpTrigger --template HttpTrigger

```

3. Func start -> Verify in Postman

4. DI

```csharp

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => 
            s
            .AddScoped<IGreeter,DummyGreeter>()
            )
    .Build();

host.Run();



public interface IGreeter
{
    Task<string> GetGreetingAsync(CancellationToken cancellationToken = default);
}



internal class DummyGreeter : IGreeter
{
    public async Task<string> GetGreetingAsync(CancellationToken cancellationToken = default)
    {
        return "Hello from DI function";
    }
}


namespace theisolatedfunctions
{
    public class MyFirstHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IGreeter _greeter;

        public MyFirstHttpTrigger(ILoggerFactory loggerFactory, IGreeter greeter)
        {
            _logger = loggerFactory.CreateLogger<MyFirstHttpTrigger>();
            _greeter = greeter;
        }

        [Function("MyFirstHttpTrigger")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "something/new")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            string greeting = await _greeter.GetGreetingAsync();
            response.WriteString(greeting);

            return response;
        }
    }
}

```


### Queue Triggers

1. Insert a *Storage Account* Connection-String in local.settings "AzureWebJobsStorage"
2. New QueueTrigger

```powershell
func new -n MyQueueTrigger -t QueueTrigger

```

```csharp

    public class MyQueueTrigger
    {
        private readonly ILogger _logger;

        public MyQueueTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyQueueTrigger>();
        }

        [Function("MyQueueTrigger")]
        [QueueOutput("onpremoutput", Connection = "ExternalStorage")]
        public Family Run([QueueTrigger("onpreminput", Connection = "ExternalStorage")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            Person person = new Person
            {
                Name = myQueueItem,
                Age = 30
            };
            Person person2 = new Person
            {
                Name = myQueueItem,
                Age = 50
            };
            var people = new Person[] { person, person2 };
            var family = new Family {People = people};
            return family;
        }
    }

    public record Family
    {
        public ICollection<Person> People { get; set; }
    }

    public record Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}

```

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "...",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "ExternalStorage" : "...."
    }
}


```


#### Get Blob Content from Private Container using Role Assignments

1. Storage Account -> Private Container -> Blob (non accessable)
2. Post Man -> Blob Url (GET) x-ms-version 2020-04-08
3. Get Access Token:

```powershell

az account get-access-token --resource https://storage.azure.com/
```

4. Copy Access Token

5. PostMan : HEADER Authorization: Bearer [token]
6. Verify -> Not allowed error
7. Storage -> Access Control -> New Role Assignment -> Yourself!! Storage Blob data reader
8. Wait up to 6 min.
9. Postman -> Verify get content


### Access Storage Account in .NET using ManId/Client Secret (Password-less/key-less)

1. In AAD (entra.microsoft.com)
2. Create *App Registration*
3. Secrets -> Create new Secret (Saved: Secret **VALUE** ) (ClientId, TenantId, Secret)


4. Create Virtual Machine
4. **Windows Server 2022 Datacenter - x64 Gen2**
5. UserName / Password

6. Login (Remote deskop) (Ip address) (UserName .\[username])
7. Make VM System Managed Identity (Identity)
8. Restart
9. Powershell

```powershell

Clear-Host;

$scope = "https://servicebus.azure.net/";
$scope = "https://storage.azure.com/";

## $scope = "api://integrationit.com/danskindustriapi";


$url = "http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=$scope";
$token = $null;
$token = Invoke-WebRequest -Method Get -Uri $url -Headers @{ "metadata" = "true" } | Select-Object -ExpandProperty Content | ConvertFrom-Json | Select-Object -ExpandProperty access_token;
$token;
$token | Set-Clipboard;

```

```powershell

Clear-Host;
$token = Get-Clipboard;
$p = $token.Split('.');
$parts = $p[1];
if (($parts.Length % 4) -ne 0) {
    if (($parts.Length % 4) -eq 1) {
        $parts += "===";
    }
        if (($parts.Length % 4) -eq 2) {
        $parts += "==";
    }
            if (($parts.Length % 4) -eq 3) {
        $parts += "=";
    }
}



[System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($parts)) | ConvertFrom-Json | ConvertTo-Json;




```


10. Make a new Console Application

```powershell

dotnet new console -o theconsole
cd theconsole
dotnet add package Azure.Identity
dotnet add package Azure.Storage.Blobs


```csharp

// See https://aka.ms/new-console-template for more information
using Azure.Core;


Console.WriteLine("Hello, World!");

//TokenCredential
//var token = new Azure.Identity.ManagedIdentityCredential();
bool man = false;
TokenCredential token;
if (man)
{
    token = new Azure.Identity.ManagedIdentityCredential();
}
else
{
    token = new Azure.Identity.ClientSecretCredential(
        tenantId: "--",
        clientId: "--",
        clientSecret: "---");

}

 
//var tokenRequestContext = new TokenRequestContext(scopes: new string[] { "https://storage.azure.com/" });
//var accessToken = await token.GetTokenAsync(requestContext: tokenRequestContext);
//Console.WriteLine(accessToken.Token);


BlobServiceClient serviceClient = new(serviceUri: new Uri("https://[storageAccountName].blob.core.windows.net"), credential: token);

var containerClient = serviceClient.GetBlobContainerClient("FOLDERNAME");
var blobClient = containerClient.GetBlobClient("BLOB_NAME");
var content = await blobClient.DownloadContentAsync();
var result = content.Value;
Console.WriteLine(result.Content);





```

11. ERROR!!!
12. Storage account -> Blob Reader -> Virtual Machine Identity + App Registration Identity


#### Access Key Vault

1. Create new KeyVault
2. Create a secret with a value
3. Verify that you can access the secret
4. Change Access Configuration -> RBAC
5. Verify that you now cannot access the secret
6. Added The *Key Vault Administrator* role to you on the Key Vault
7. Wait and verify that you can access
8. Find your HttpTrigger Function from the Function App
9. In Postman verify that it still works
10. Add this code:

```csharp


    string password = Environment.GetEnvironmentVariable("password");
    string responseMessage = $"Hello from Function!!!! the password is {password}";

            return new OkObjectResult(responseMessage);

```

11. Verify that the value is empty
12. In Configuration insert password: somevalue
13. Save and restart 
14. Verify that you now get your hardcoded value



15. Now replace the Configuration Value with

```

@Microsoft.KeyVault(VaultName=....;SecretName=...)

```

16. Make sure your Function App has a **System Managed Identity**
17. Give the identity the appropriate permissions on the Key Vault *Key Vault Secrets User*

18. Restart Function App and wait
19. Verify that you now get the value from within the *Key Vault*



### Arm

1. Clone the repo to your local drive
2. In the folder run the following (verify that your *azure cli* is connected "az account show")
3. Change storage account names in parameter files

```powershell

$rgName = "rg-tekno-arm";

az group create -n $rgName -l westeurope;

az deployment group create --name thename --resource-group $rgName --template-file .\Templates\storageAccount.json --parameters .\Parameters\Test\storageAccount.json
az deployment group create --name thename --resource-group $rgName --template-file .\Templates\storageAccount.json --parameters .\Parameters\Prod\storageAccount.json

```


### Publish Function App through CLI

1. Create new Function App
2. In the project folder

```powershell
func azure functionapp publish [function app name]

```

### Secure API

#### Create project

```powershell

dotnet new webapi -o the204api

```
#### Test the App

In the project folder

```powershell

dotnet run


```

In another powershell

```powershell
Invoke-WebRequest "https://localhost:7068/weatherforecast" |
    Select-Object -ExpandProperty Content | 
    ConvertFrom-Json | 
    ConvertTo-Json -Depth 10;
```


#### Create an App Registration in AAD for the API

Create App Reg. Fetch *TenantId* *ClientId*

```powershell

dotnet add package Microsoft.Identity.Web

```

appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AzureAD" : {
    "Instance": "https://login.microsoftonline.com/",
    "ClientId" : "aaa",
    "TenantId" : "bbb"
  }
}

```

```csharp
using Microsoft.Identity.Web;

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

...

app.UseAuthentication();
app.UseAuthorization();

```

#### Call again and test (should still work)

In controller
```csharp

[Authorize]
[ApiController]
[Route("[controller]")]

```

#### Call again -> 401

#### Make token call (default audience)

```powershell

$token = az account get-access-token | ConvertFrom-Json | Select-Object -ExpandProperty accessToken;

Invoke-WebRequest "https://localhost:7068/weatherforecast" -Headers @{ "Authorization" = "Bearer $token" } | Select-Object -ExpandProperty Content | ConvertFrom-Json | ConvertTo-Json -Depth 10;
```

#### Create webapi Audience

##### App Registration (API)

##### Expose an API

> Application ID URI -> Set

Copy the api uri and Save


#### Create token with new Audience

```powershell

$audience = "api://5d23ab39-b2eb-4aa2-b7e1-53510f1d92e0";

$token = az account get-access-token --resource $audience | ConvertFrom-Json | Select-Object -ExpandProperty accessToken;

Invoke-WebRequest "https://localhost:7068/weatherforecast" -Headers @{ "Authorization" = "Bearer $token" } | Select-Object -ExpandProperty Content | ConvertFrom-Json | ConvertTo-Json -Depth 10;

```

### Kusto Queries

```

AppTraces
| where OperationId == '56d55fa5d627b8a810f7ba54a29803a8'
| order by TimeGenerated asc


AppRequests
| where TimeGenerated > ago(1h)
| where Name == 'Calculate'
| project TimeGenerated, Success, DurationMs, OperationId
| order by TimeGenerated desc


AppRequests
| where TimeGenerated > ago(1h)
| where Name == 'Calculate'
| summarize count() by tostring(Success)
| render piechart 



``` 


### DockerFile


```

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

EXPOSE 80

WORKDIR /app
COPY ./theapp/*.csproj ./
RUN dotnet restore

COPY /theapp/* ./
RUN dotnet publish -c release -o out



FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "thedockerapp.dll"]


```


### API Management


```xml

        <validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized" require-expiration-time="true" require-scheme="Bearer" require-signed-tokens="true">
            <openid-config url="https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration" />
            <issuers>
                <issuer>https://sts.windows.net/551c586d-a82d-4526-b186-d061ceaa589e/</issuer>
            </issuers>
        </validate-jwt>




        <validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized" require-expiration-time="true" require-scheme="Bearer" require-signed-tokens="true">
            <openid-config url="https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration" />
                     <issuers>
                <issuer>https://sts.windows.net/551c586d-a82d-4526-b186-d061ceaa589e/</issuer>
            </issuers>
		<required-claims>
		<claim name="aud">
                <value>api://05389de2-92fc-4176-aa9b-1990e03a85a0</value>
		</claim>
            </required-claims>
        </validate-jwt>



```