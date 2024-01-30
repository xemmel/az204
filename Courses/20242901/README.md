Authentication (UserName, Password)
     Entra

ROLE ASSIGNMENTS (AZURE)
     - Who (student user)
     - What (owner)
     - Where (Sub, RG, Resource)




SAS TOKEN:

?sv=2022-11-02&ss=b&srt=co&sp=riytfx&se=2024-01-30T19:38:40Z&st=2024-01-29T11:38:40Z&spr=https&sig=k2fmxwlf3Ayi3EGavc4k3wQYQ3NKLeDTkm5c0CQyKVs%3D


?sv=2022-11-02
&ss=b
&srt=co
&sp=riytfx
&se=2024-01-30T19:38:40Z
&st=2024-01-29T11:38:40Z
&spr=https

&sig=k2fmxwlf3Ayi3EGavc4k3wQYQ3NKLeDTkm5c0CQyKVs%3D


------
Management Plane Rest API:


GET 

https://management.azure.com/subscriptions/9bc198aa-089c-4698-a7ef-8af058b48d90/resourcegroups?api-version=2021-04-01



----------

OAUTH2 Flow (Already authenticated)

ENTRA    Request Token (Audience/Scope/Resource!!) (Color of the keycard)

Token

REAL API

CALL
  Header
     Authorization: Bearer {{token}}


------------------


Get-AzAccessToken -ResourceUrl https://storage.azure.com/ | Select-Object -ExpandProperty Token

jwt.ms


-------------------

POSTMAN

private container
    -> ResourceNotFound


HEADERS
      x-ms-version: 2023-08-03
      Authorization: Bearer {{token}}

    -> This request is not authorized to perform this operation

------


Storage Account
  -> Access Control (IAM)
     -> Add -> Add Role Assignment
           -> Role (Storage Blob Data Reader)
           -> Select Members (yourself)

            -> Review+Assign x 2

> 5-6 min

POST Get Content


---------


az204mlc.azurewebsites.net


-------


App Service Plan (windows,S1) (region!) (existing RG Or new initials)

Web App / App Service

Windows
.NET 8
Region

dropdown -> Existing App Service Plan
-> next-next create

   Overview -> Click on default domain -> Standard MS Page

App Service Editor (Preview)

   -> new file (right click left) index.html
      Hello



-------------


Scale:

Up/Down   -> Same server more/less Hardware (NO Automation)

Out/In    -> Same config -> More/less Servers (> 1 server LB)



----------------

Function App

Plan      Consumption

Fixed pri    Pay-as-you-go
Semi-auto	Full auto scale
VNET inte       No VNET inte    (http trigger)
No Cold starts       Cold starts


RG-Function App
Function App (Web App) (Container)
    Function  (Http Trigger)
    Function  (Timer trigger)
     BLOB TRIGGER
     SERVICE BUS TRIGGER
     SQL TRIGGER

System Storage Account (new unique sa)

RG2

SA


-------------
Function app task
Create new function app (new resource group)
   .NET (.NET 6)
Create

Overview
   -> Scroll down -> Create in Portal
              Http Trigger -> Scroll down -> Rename
                Code+test

    log.LogInformation("C# HTTP trigger function processed a request.");

        string responseMessage = "Hello SDU!!123";
            return new OkObjectResult(responseMessage);
GET FUNCTION URL -> POSTMAN GET


SA






1 SA -> 1 PB
=

2 SA -> 1/2 PB 















------------------


Az 204 æøå


https://sdudkappxstr4434fde3.blob.core.windows.net/myfirstcontainer/TestFile.txt


https://sdudkappxstr4434fde3.blob.core.windows.net/mysecondcontainer/TestFile.txt

azure:

portal.azure.com

entra/aad
entra.microsoft.com


portal.azure.com

sdustudent@integration-it.com


Create Resource Group  az204 OG JERES INITIALER

----

Create Storage Account
   (Advanced -> Allow enabling ano. access)

           Containers
              New Container    -> Blob Anonymous
           Create File 
              Upload file to Container

------------

VS CODE
AZURE CLI
POSTMAN

winget search postman

winget install --silent Microsoft.PowerShell

------------




Subscription
    Resource Group
       Resource 1
       Res 2



Resource Group

Resource Group I

/subscriptionId/ResourceGroupName


Resource (Vm, WebApp, disk,...)
/subid/rgName/type/name

/testsub/applicationx/vm/vm1
/testsub/applicationx/disk/vm1

/testsub/applicationx/vm/vm1  :-(

/prodsub/applicationx/vm/vm1


portal.azure.com



LRS (Local) -> 3 disks i et datacenter i een region (21$)
GRS (Region Pair) 3 disks i et datacenter i een region -> Copy to R2 (IR) (46$)

ZRS (Availability Zones) -> 1 disk i 3 datacentre i R1  (26$)
GZRS ()



Storage Account
     Containers/Blob (Http file system) (Default)
        (Container = Folder, blob = File)
     File (SMB  mount (windows/linux))
     Tables (DocumentDb, NoSql)
     Queues (MQ)

     


















   
    













