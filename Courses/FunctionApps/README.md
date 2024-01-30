

### Create function App Project

```powershell

func init myisolatedfunctions --worker-runtime dotnetIsolated --target-framework net8.0


```

### Create Http Trigger Function

In Project Folder

```powershell

func new -n myfirsthttptrigger -t HttpTrigger

```

### Run Function App Locally

```powershell

func start

```