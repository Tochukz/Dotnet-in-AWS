# Simple Func

### Description
The example uses the _Lambda EmptyProject_ template from the `Amazon.Lambda.Templates` collection to build a Dotnet Core Lambda handler.  

__Note:__  The current default template targets `Dotnet 8`, therefore the Runtime property of the Lambda function is set to `dotnet8` for compatibility.

### operations
__Setup__
1. Create a Basic Lambda Project using the `lambda.EmptyFunction` template
```bash
$  dotnet new lambda.EmptyFunction --name SimpleFunc --output SimpleFuncSln
```

2. Publish the project
```bash
$ cd SimpleFuncSln/src/SimpleFunc
$ dotnet publish -c Release -r linux-x64 --self-contained false -o publish
```

3. Package the published code and send to S3
```bash
$ cd publish
$ zip -r ../simple-func.zip .
$ aws s3 cp ../simple-func.zip s3://chucks-workspace-storage/v0.0.1/simple-func.zip
```

__Deployemnt__  
Validate the template
```bash
$ aws cloudformation validate-template --template-body file://SimpleFunc.yaml
```

Deploy the stack
```bash
$ aws cloudformation deploy --template-file SimpleFunc.yaml --stack-name SimpleFunc --capabilities CAPABILITY_NAMED_IAM
```

__After Deployment__  


__Testing__  
Invoke the Lambda function
```bash
# use gitbash to run this because of the nested quotes in the payload string
 $  aws lambda invoke --function-name SimpleFunc --cli-binary-format raw-in-base64-out --payload '"Hello DotnetCore Lambda!"' output.txt
```
The `output.txt` file should content a uppercase version of the input test - `"HELLO DOTNETCORE LAMBDA!"`

__Cleanup__  
Delete the stack
```bash
$ aws cloudformation delete-stack --stack-name SimpleFunc
```

### Learn More
__Dotnet Lambda CLI Tool__  
The `dotnet lambda` CLI tool is a wrapper over the `aws lambda`command.    
You can use it to deploy your lambda function, invoke the function, list all deployed functions and more.
Use it's help option command to check out all the available commands:
```bash
$ dotnet lambda --help
```
For example, to list all deployed Lambda function:
```bash
$ dotnet lambda list-functions --region eu-west-2
```

//@todo: Fixed the broken installation of the `dotnet lambda` utility.
