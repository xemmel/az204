targetScope = 'resourceGroup'

param workspaceName string
param location string = resourceGroup().location


resource workspace 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: workspaceName
  location: location
}

output workspaceId string = workspace.id

