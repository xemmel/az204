targetScope = 'resourceGroup'

param appName string
param env string
param location string = resourceGroup().location


//Monitor workspace
var workspaceName = 'log-${appName}-${env}'
module workspace 'monitorworkspace.bicep' = {
  name: 'workspace'
  params: {
    location: location
    workspaceName: workspaceName
  }
}

//App Insight
var appInsightName = 'appi-${appName}-${env}'
module appInsight 'appInsight.bicep' = {
  dependsOn: [
    workspace
  ]
  name: 'appInsight'
  params: {
    appInsightName: appInsightName
    location: location
    workspaceId: workspace.outputs.id
  }
}
