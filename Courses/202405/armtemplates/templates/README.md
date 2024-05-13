```powershell

## inside the templates folder

$rgName = "rg-az204-init-04"

az group create --name $rgName --location germanywestcentral


az deployment group create --resource-group $rgName --template-file .\storeaccount.bicep
## You will be prompted for storage account name (small letters only)



```

