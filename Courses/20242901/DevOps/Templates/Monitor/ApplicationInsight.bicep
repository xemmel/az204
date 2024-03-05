targetScope = 'resourceGroup'

param appInsightName string
param location string
param workspaceId string


var kind = 'web'

resource appInsight 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightName
  location: location
  kind: kind
  properties: {
    Application_Type: kind
    WorkspaceResourceId: workspaceId
  }
}
