### Install and login CLI

```powershell

winget install Microsoft.AzureCLI

winget install Git.Git

az login --use-device-code

```

### Create resource group and apply arm template

```powershell

## inside the bicep folder
## az account show
## Change init and change storage acocunt name in the parameter files

$rgName  = "invixo-init-armdemo-01-rg"
$env = "Test"
az deployment group create --resource-group $rgName --template-file .\Templates\azurestorageaccount.bicep --parameters .\Parameters\$env\storageaccount.json

```