# Minmal API  

### Description
This example shows how a Donet Core Minimal API can be deployed to a Lambda Function.  

### Operation

__Setup__  
1. Create a new ASP.NET Core Web API Project.  
2. Install the AWS Lambda Adaptor Nuget Package
```bash
$ cd minimal-api/MinimalAPI/MinimalAPI
$ dotnet add package Amazon.Lambda.AspNetCoreServer.Hosting
```
This package:
- Translates API Gateway / ALB events into HTTP requests
- Boots ASP.NET Core once per cold start
- Reuses the app for warm invocations
3. Update the `Program.cs` file
```CS
using Amazon.Lambda.AspNetCoreServer.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Lambda hosting for HTTP API or Lambda URL
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
// Alternatives:
// LambdaEventSource.RestApi   (API Gateway REST API)
// LambdaEventSource.ApplicationLoadBalancer

var app = builder.Build();
```

4. Publish the application
```bash
$ dotnet publish -c Release -r linux-x64 --self-contained false -o publish
```

5. Zip the `publish` folder content and push to S3 bucket  
```
$ cd publish
$ zip -r ../minimal-api.zip .
$ aws s3 cp ../minimal-api.zip s3://chucks-workspace-storage/v0.0.1/minimal-api.zip
```
__Deployment__   
Validate the template
```bash
$ aws cloudformation validate-template --template-body file://MinimalApi.yaml
```

Deploy the stack
```bash
$ aws cloudformation deploy --template-file MinimalApi.yaml --stack-name MinimalApi --capabilities CAPABILITY_NAMED_IAM
```
