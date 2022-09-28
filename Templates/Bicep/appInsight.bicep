targetScope = 'resourceGroup'

param appName string
param location string = resourceGroup().location
param workspaceId string


var kind = 'Web'

resource appInsight 'Microsoft.Insights/components@2020-02-02' = {
  name: appName
  location: location
  kind: kind
  properties: {
    Application_Type: kind
    WorkspaceResourceId: workspaceId
  }
}



