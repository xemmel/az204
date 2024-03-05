targetScope = 'resourceGroup'

param appName string
param env string
param location string = 'swedencentral'

var storageAccountName = '${appName}${env}st'
resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}

var serviceBusName = '${appName}${env}-seb'
resource serviceBus 'Microsoft.ServiceBus/namespaces@2021-11-01' = {
  name: serviceBusName
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}

