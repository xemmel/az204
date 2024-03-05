

### Create Resource Group (Powershell)

```yaml

  - task: AzurePowerShell@5
    displayName: 'Creating resource group'
    inputs:
      azureSubscription: theautomaticserviceconnection
      azurePowerShellVersion: LatestVersion
      ScriptType: 'InlineScript'
      Inline: |
        New-AzResourceGroup -Name sdumlc02-rg -Location swedencentral -Force 

```