
Create a azure-pipelines.yaml file in your repo

```yaml

trigger:
  - master

pool:
  vmImage: ubuntu-latest


steps:
  - script: echo 'Hello again world'
    displayName: 'Echoing Hello world'

```

Remove your master repo policy

push the new file to master

Create Pipeline, point to the .yaml file, RUN -> Check run

Change to the pipeline code -> Push to master -> Pipeline starts automatically