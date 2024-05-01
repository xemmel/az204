- Create Logic App (Consumption!) (init name)
  - Logic App Designer (Legacy view????)
    Trigger Type (HTTP) "When a HTTP request"
   - Expand "Add new Param" -> Method -> GET
   -> SAVE -> Copy URL 

   -> Postman GET 202 Accepted
  -> Logic App -> Run history -> Success execution
  -> Requestbin.com  -> public bin
  -> Copy URL

  -> Logic App -> Designer -> New Step -> Response -> Hello in body -> SAVE
  -> POSTMAN GET   200 ok (text)
-> Designer -> in between + Add an action -> HTTP
    -> Method GET
    -> URI -> requestbin /
    -> SAVE
  POSTMAN -> An error -> Run history, examine the error

------------------
Logic App -> Identity -> Enable On Save
Designer -> HTTP -> Add new param -> Authentication -> Managed Identity
    Audience: https://storage.azure.com/
SAVE

POSTMAN -> requestbin -> headers bearer token

--------------------------
  HTTP -> change from requestbin to private blob
       HEADER: x-ms-version: 2023-08-03

    ERROR Postman
  run history  (permission denied)

Storage account (private blob) -> Access Control -> Role Assignment -> Blob reader -> Logic name 
5-6
:-)





RG1
  Function App = Web App (+ MS Code)
     ServerLess (consumption) (Y1)
     App Service Plan -> Existing/S1/P
     Premium
  System Storage Account   (1-1)

Web App (App Service) -> App Service Plan


Function App = web app
    - Function (Timer Trigger)
    - Function (Http Trigger)


---------------------


https://az204mlcfunc.azurewebsites.net/api/MyFirstHttpTrigger?code=AaIXhZzXV67PZZEyCivuQQgFzICVV-3sjj2BXpZPw6N_AzFuhWO-_g==



------------------------------


Create new Function App (new resource group az204-init-functionapp)
   -> .NET
   -> 6 (In-process)
   -> Review Create * 2

 Function App -> Overview -> Create in Portal -> Http Trigger
   Code + test
         log.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Hello from Function");

   Save
    Get Function URL
   GET postman :-)
    remove code from url
   HEADER: x-functions-key

----------------------
Storage Account: az204mlc01st

Container: mysecondcontainer

FilePath: c:\temp\order.txt

az storage blob upload `
	--container-name mysecondcontainer `
  --file  c:\temp\order.txt `
   --account-name az204mlc01st `
   --overwrite
;


-----------------

Storage Account from yesterday
  -> Overview -> Storage account key access -> Enabled

-> Queues -> new Queue

-> Function app (overview)
     -> Create Function (Azure queue storage trigger)
     -> Next Queue Name -> queue name
     -> New (Connections) -> Find your storage account
     -> Create

-> Queue Trigger
   -> Code+Test (Set to FileSystemLogs)

-> Storage Account -> Queue -> Add Message (< 1 min)
    -> The message disappears
    -> 

    