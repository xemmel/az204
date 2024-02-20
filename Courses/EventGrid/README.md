- Create Storage Account (with Resource Group)
  - Create Container

## Upload File

Azure CLI


```powershell

az storage blob upload `
    --file [fileName] `
    --account-name [accountName] `
    --container-name [containerName] `
    --overwrite
;

```

Azure Powershell

```powershell

$storageAccount = Get-AzStorageAccount `
    -Name [accountName] `
    -ResourceGroupName [resourceGroupName]
;

$storageAccount | 
    Set-AzStorageBlobContent `
        -File [file] `
        -Container [containerName] `
        -Force
;

```

### Create Logic App

Create Logic App (REMEMBER CONSUMPTION)

Choose HTTP Trigger (When an Http is..)

Save and get the URL


### Setup Storage Account Event

In Storage Account -> Events
  - *+ Event Subscription*
  - Give it a **Name** and **System Topic Name**
  - Select *Blob Created* Event Type
  - Under *Endpoint Details* set *Endpoint Type* -> Web Hook
    - Paste URL from Logic App
  - *Create*

Upload file again and verify that Logic App was run
