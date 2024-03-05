targetScope = 'resourceGroup'

param workspaceName string
param location string


resource monitorWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: workspaceName
  location: location
}

output id string = monitorWorkspace.id
