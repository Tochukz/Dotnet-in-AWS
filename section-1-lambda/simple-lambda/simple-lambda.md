# Simple Lambda

Create a Basic Lambda Project

```bash
$ dotnet new lambda.EmptyFunction --name SimpleFunc
cd SimpleFunc
```

Invoke your Lambda Code locally
```bash
$ dotnet lambda invoke-function SimpleFunc --payload "hello world"
```

Build and Package you Dotnet Code
```bash
$ dotnet lambda package --output-package bin/Release/net8.0/deploy-package.zip
```
