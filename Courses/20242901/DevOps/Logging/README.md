- Create new Resource group (sdulogdemo-init)

- Create log ana workspace

- Create app insight (point at log ana work)

- Create function app
    - .NET
    - 6 (In-process)

  Monitoring
    (new)
    choose your app insight

  Create


Function App Overview

   Create function
   (Http) -> Calculator

            string numberString = req.Query["number"];

            log.LogInformation($"The number entered is {numberString}");
            int number = int.Parse(numberString);
            string responseMessage = $"The number {number} squared is {number*number}";
            return new OkObjectResult(responseMessage);


Postman check both


### Queries

```

##View requests

AppRequests
| where TimeGenerated > ago(1h)
| project TimeGenerated, Name,Success, DurationMs, OperationId
| order by TimeGenerated desc

### Success by piechart

AppRequests
| where TimeGenerated > ago(1h)
| summarize count() by tostring(Success)
| render piechart  

### Timeline

AppRequests
| where TimeGenerated > ago(1h)
| summarize count() by bin(TimeGenerated,3m), tostring(Success)
| render barchart 


### Debug /Log

AppTraces
| where OperationId == '111'

### Summarize by custom colum

AppTraces
| extend prop__numberEntered_ = tostring(Properties.prop__numberEntered)
| where prop__numberEntered_ != ''
| summarize count() by prop__numberEntered_
| render piechart 


```