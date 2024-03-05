targetScope = 'resourceGroup'

param appName string
param env string
param location string = resourceGroup().location


//Monitor workspace
var workspaceName = '${appName}-${env}-log'
module workspace 'MonitorWorkspace.bicep' = {
  name: 'workspace'
  params: {
    location: location
    workspaceName: workspaceName
  }
}

//Application Insight
var appInsightName = '${appName}-${env}-appi'
module appInsight 'ApplicationInsight.bicep' = {
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


