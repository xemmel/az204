### Install dev cert

```powershell

dotnet dev-certs https -t

```

### Create WebApp 

```powershell

dotnet new webapp -o [thename]

```

### Alter Properties->launchsettings

Copy line 25 to line 16

```json

  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7245;http://localhost:5212",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7245;http://localhost:5212",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },

```

### Inside project folder run


```powershell

dotnet run

```

Browse with https and verify it works

### Install nuget packages

```powershell

dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect
dotnet add package Microsoft.Identity.Web;
dotnet add package Microsoft.Identity.Web.UI;

```

### In entra

Create App Registration in Entra (entra.microsoft.com)

Under Redirect URI (optional)

 - select a platform -> Web
 - url: https://localhost:xxxx/signin-oidc

Save

Authentication (in App Reg) -> Implicit -> ID Tokens

Save

Get ClientId and TenantId


### Alter program

```csharp


using Microsoft.Identity.Web;

builder.Services.AddRazorPages();

builder
    .Services
    .AddMicrosoftIdentityWebAppAuthentication(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});


....


app.UseAuthentication(); //Insert this
app.UseAuthorization();


```

Create Shared/_LoginPartial.cshtml

```csharp


@using System.Security.Principal

<ul class="navbar-nav">
@if (User.Identity?.IsAuthenticated == true)
{
        <span class="navbar-text text-dark">Hello @User.Identity?.Name!</span>
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="MicrosoftIdentity" asp-controller="Account" asp-action="SignOut">Sign out</a>
        </li>
}
else
{
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="MicrosoftIdentity" asp-controller="Account" asp-action="SignIn">Sign in</a>
        </li>
}


```

Alter Shared/_Layout.cshtml (line 28)

```html

                    </ul>
                    <partial name="_LoginPartial" />
                </div>

```


dotnet run