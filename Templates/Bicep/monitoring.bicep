targetScope = 'resourceGroup'

param appName string
param location string = resourceGroup().location


var workspaceName = 'log-${appName}'
module workspace 'workspace.bicep' = {
  name: 'workspace'
  params: {
    workspaceName: workspaceName
    location: location
  }
}

var appInsightName = 'appi-${appName}'
module appInsight 'appInsight.bicep' = {
  name: 'appInsight'
  params: {
    appName: appInsightName
    workspaceId: workspace.outputs.workspaceId
    location: location
  }
}
