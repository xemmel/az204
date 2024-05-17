
### HTTP Trigger Calculator

```csharp

#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string numberString = req.Query["number"];
    log.LogInformation($"The number entered is: {numberString}");

    int number = int.Parse(numberString);


    string responseMessage = $"the number: {number} squared is: {number*number}!";

            return new OkObjectResult(responseMessage);
}

```

......&number=33


### Queries

```
### Get requests

AppRequests
| where TimeGenerated > ago(5m)
| where Name == 'Calculator'
| project TimeGenerated, Success, OperationId
| where Success == false
| order by TimeGenerated desc


### Show piechart

AppRequests
| where TimeGenerated > ago(5h)
| where Name == 'Calculator'
| summarize count() by tostring(Success)
| render piechart 

### Show barchart


AppRequests
| where TimeGenerated > ago(5h)
| where Name == 'Calculator'
| summarize count() by bin(TimeGenerated,5m), tostring(Success)
| render barchart 


```