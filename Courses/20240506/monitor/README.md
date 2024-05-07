```csharp

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string numberString = req.Query["number"];
    log.LogInformation("The number entered is: {number}",numberString);
    int number = int.Parse(numberString); // :-(
   
        string responseMessage = $"The number {number} squared is: {number*number}";
            return new OkObjectResult(responseMessage);
}

```


### Queries

AppRequests
| project TimeGenerated, Name, Success, OperationId, DurationMs
| where Name == 'Calculator'
| order by TimeGenerated desc



AppRequests
| where Name == 'Calculator'
| summarize count() by tostring(Success)
| render piechart 


AppRequests
| where Name == 'Calculator'
| summarize count() by tostring(Success)
| render piechart 



AppTraces
| extend prop__number_ = tostring(Properties.prop__number)
//| where OperationId == '5e88a84fc7e4afe02e4fb4214b2e58a6'
//| order by TimeGenerated asc
| summarize count() by prop__number_
| render piechart 