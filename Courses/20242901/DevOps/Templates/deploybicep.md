## 

```yaml

trigger:
  - master

pool:
  vmImage: ubuntu-latest

variables:
  - name: serviceConnection
    value: themanualserviceconnectionteachingtest
  - name: rgName 
    value: sdumlcdevops04-rg
  - name: location
    value: swedencentral
  - name: appName
    value: sduafternoon
  - name: env
    value: test
  

steps:
  - script: echo 'Hello again world'
    displayName: 'Echoing Hello world'
  - task: AzureResourceManagerTemplateDeployment@3
    displayName: 'Publish template'
    inputs: 
      azureResourceManagerConnection: $(serviceConnection)
      resourceGroupName: $(rgName)
      location: $(location)
      csmFile: ./Templates/logging.bicep
      overrideParameters: >
        -appName $(appName)
        -env $(env)


```