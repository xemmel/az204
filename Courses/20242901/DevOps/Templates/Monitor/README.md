```powershell

Login-AzAccount

Get-AzContext

winget install Microsoft.bicep

```


```powershell



$rgName = "sdumlctemplate02-rg"

New-AzResourceGroup -Name $rgName -Location swedencentral -Force

## Courses\20242901\DevOps\Templates

New-AzResourceGroupDeployment -ResourceGroupName $rgName -TemplateFile .\Monitor\logging.bicep -TemplateParameterObject @{"appName" = "thesdumonitor";"env" = "test"}

```

