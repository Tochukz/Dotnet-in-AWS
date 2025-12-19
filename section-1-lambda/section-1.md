# Section 1: Lambda
### Setup
Install Lambda Templates Package
```bash
$ dotnet new install Amazon.Lambda.Templates
```

To List all installed Lambda Templates
```bash
$  dotnet new list | grep lambda
```

Install Lambda Tools for deployment
```bash
$ dotnet tool install -g Amazon.Lambda.Tools
```

Deploy a Function
```bash
$ dotnet lambda deploy-function SimpleFunc
```
This will:
* Package your code
* Upload to AWS Lambda
* Configure execution role  

This is useful if you want the tool to manage the AWS Lambda resources for you. For example, the Lambda Function, IAM Roles and associated resources.
