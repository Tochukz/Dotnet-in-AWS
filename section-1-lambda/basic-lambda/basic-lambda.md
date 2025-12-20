# Basic Lambda

### Description
This creates a Basic Lambda handler code using Dotnet Class Library Project.  

### Operations
__Setup__  
1. Create the class library project
```bash
$ dotnet new classlib -n BasicLambda
```

2. Install the `Amazon.Lambda.Core` and `Amazon.Lambda.Serialization.SystemTextJson` Nuget packages
```
$ cd BasicLambda
$ dotnet add package Amazon.Lambda.Core
$ dotnet add package Amazon.Lambda.Serialization.SystemTextJson
```

3. Create the `AssembyInfo.cs` file. See the content in the project.

4. Rename the `Class1.cs` file to `Function.cs` and update the code according. See content in the project.  

5. Create `Program.cs` file.   
  Lambda does not need the `Program.cs` file but the compiler needs it to build the project. You may have an empty `void Main` method in Program, see the content.  

6. Make the Dotnet Core Lib project to be runnable by editing the `.csproj` file
```xml
# BasicLambda.csproj
<PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <OutputType>Exe</OutputType>
    ...
</PropertyGroup>
```
The `<OutputType>Exe</OutputType>` directive makes the Lib a runnable code.

7. Publish the project
```bash
$ cd BasicLambda
$ dotnet publish -c Release -r linux-x64  --self-contained false -o publish
```
After publishing you will find the `<ProjectName>.runtimeconfig.json` file in the `publish` folder.

8. Package the code and send to S3
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

__After Deployment__  
Get the `FunctionUrl` from the stack output
```bash
$ aws cloudformation describe-stacks --stack-name BasicLambda --query "Stacks[0].Outputs" --no-cli-pager
```

__Testing__  
1. Invoke the Lamba function
```bash
$ aws lambda invoke --function-name BasicLambda --cli-binary-format raw-in-base64-out --payload '{"name": "Tochi"}' output.json
```
The `output.json` file should contain the text "Hello Tochi".  

2. Use the `FunctionUrl` to try the Lambda function on a Browser.  
You should see the text "Hello" on your browser page.

__Cleanup__  
Delete the stack
```bash
$ aws cloudformation delete-stack --stack-name BasicLambda
```
