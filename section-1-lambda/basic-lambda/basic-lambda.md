# Basic LambdaEventSource

### Description
This creates a Basic Lambda handler code using Dotnet Class Library Project template

### Operations
__Setup__  
1. Create the class library project
```bash
$ dotnet new classlib -n BasicLambda
```
2. Install the` Amazon.Lambda.Core` Nuget package
```
$ cd BasicLambda
$ dotnet add package Amazon.Lambda.Core
```

3. Rename the `Class1.cs`file to `Function.cs` and update the code according.

4. Publish the project
```bash
$ cd BasicLambda
$ dotnet publish -c Release -r linux-x64  --self-contained false -o publish
```

5. Package the code and send to S3
```bash
$ cd publish
$ zip -r ../basic-lambda.zip .
$ aws s3 cp ../basic-lambda.zip s3://chucks-workspace-storage/v0.0.1/basic-lambda.zip
```

__Deployment__  
Lint the template
```bash
$ cfn-lint BasicLambda.yaml
```

Deploy the Stack
```bash
$ aws cloudformation deploy --template-file BasicLambda.yaml --stack-name BasicLambda --capabilities CAPABILITY_NAMED_IAM
```
