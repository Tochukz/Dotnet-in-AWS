# Basic BeanStalk

### Description
This example creates a Dotnet Core Web Application and deploys it to an Elastic BeanStalk Environment.


### Operation
__Setup__   
1. Create a Solution and a Dotnet Web Core Project
```bash
# Create the solution
$ mkdir BasicBeanStalkSln
$ cd BasicBeanStalkSln
$ dotnet new sln --name BasicBeanStalkSln
# Create the project and add it to the solution
$  dotnet new mvc --name BasicBeanStalk
$  dotnet sln add BasicBeanStalk
# Run and test the application
$ cd BasicBeanStalk
$ dotnet run
```
We have created a MVC Web Core Application project

2. Publish the Web Application  
```bash
$ cd BasicBeanStalkSln/BasicBeanStalk
$ dotnet publish -c Release -r linux-x64 --self-contained false -o publish
```

3. Package the published code and send to S3.
```bash
$ cd publish
$ zip -r ../basic-beanstalk.zip .
$ aws s3 cp ../basic-beanstalk.zip s3://chucks-workspace-storage/v0.0.1/basic-beanstalk.zip
```

__Deployment__  
Validate the template
```bash
$ aws cloudformation validate-template --template-body file://BasicBeanStalk.yaml
```

Deploy the stack
```bash
$ aws cloudformation deploy --template-file BasicBeanStalk.yaml --stack-name BasicBeanStalk --capabilities CAPABILITY_NAMED_IAM
```

__After Deployment__  
Get the `EnvironmentUrl` and `EnvironmentIp` from the stack outputs
```bash
$ aws cloudformation describe-stacks --stack-name BasicBeanStalk --query "Stacks[0].Outputs" --no-cli-pager
```

__Testing__  
Use the `EnvironmentUrl` output to test the Web Application over a Browser.  
You should see the default page of a Dotnet Core MVC Web Application.  

__Cleanup__  
Delete the stack
```bash
$ aws cloudformation delete-stack --stack-name BasicBeanStalk
```
