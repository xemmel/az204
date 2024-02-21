
## Create two App Registrations

In entra

A Client App with Secret (Get tenantId, clientId and secret)

A Api Client
  - Expose an API -> Application ID URI -> Add  (Keep default values)
  - Get the clientId and tenantId

Use Script ServicePrincipals/Get_Token (with the client App tenantId, clientId and secret and the API Audience (The *Applcation ID URI*))
tenantId and audience is inserted into the script *clientId* and *secret* are
passed as parameters

Get the token and examine the audience etc. with the "Decode_token.ps1" script
remember to run the "Get_Token" script with the extra dot

```powershell

. .\Get_Token.ps1 -clientId .. -clientSecret ...

```


## Create API Project

```powershell


dotnet new webapi -o secureapi
cd secureapi
dotnet add package Microsoft.Identity.Web


```

### Ensure Https in Properties->launchsettings
See *WebApp.md* for details

### Call

In Postman GET {url}/weatherforecast

### Add Code to Program.cs

```csharp

//Add at top
using Microsoft.Identity.Web;


var builder = WebApplication.CreateBuilder(args);

//Add
builder
    .Services
    .AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
    
  


app.UseHttpsRedirection();
//Add
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/weatherforecast",[Authorize] () =>

```

```powershell

dotnet run

```

## Call

In postman call again this time you should get an error

Get a token as described before

Headers:

Authorization: Bearer **TOKEN**

Call should succeed now

### Roles

In the API App Reg make yourself owner
  -> Owners -> **+ Add owners**

Under **App Roles** add two roles: reader, writer


In the Client App
 -> API permissions -> **+ Add a permission**
 -> **My APIs**
 -> Choose your API APP
 -> Choose *reader*
 -> *Add permissions*

 Wait a while and then get a new token examine and verify that you now have role

 Change the code to:

 ```csharp

app.MapGet("/weatherforecast",[Authorize(Roles = "writer")] () =>

 ```

 use the new token and verify that you CANNOT execute the method

 now in the code change *writer* to *reader* and now you should be able to execute since you have the correct role
 