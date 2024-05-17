```powershell

## inside the templates folder

$rgName = "rg-az204-init-04"

az group create --name $rgName --location germanywestcentral


az deployment group create --resource-group $rgName --template-file .\storeaccount.bicep
## You will be prompted for storage account name (small letters only)

## Deploy with parameter files

az deployment group create --resource-group $rgName --template-file .\storeaccount.bicep --parameters ..\parameters\$env\storageaccount.json


### Deploy all monitoring

C:\code\az204\Courses\202405\armtemplates\templates> az deployment group create --resource-group $rgName --template-file .\monitor.bicep --parameters appName=az204mlc05 --parameters env=test



```

