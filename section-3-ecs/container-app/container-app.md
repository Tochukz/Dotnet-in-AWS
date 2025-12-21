# Container App
### Description
The example deploys a Dotnet Core Web Application to an ECS Container.  

### Operation
__Before Setup__  
If you don't already have an _ECR repository_, create one by deploying the `WebAppRepo` stack
```bash
# Validate the template
$ aws cloudformation validate-template --template-body file://WebAppRepo.yaml
# Deploy
$ aws cloudformation deploy --template-file WebAppRepo.yaml --stack-name WebAppRepo
# Get the RepositoryUrl from the stack output
$ aws cloudformation describe-stacks  --stack-name WebAppRepo --query "Stacks[0].Outputs" --no-cli-pager
```

__Setup__  
1. Create a new Solution and add an MVC Web App project to the solution
```bash
# Create the solution
$ mkdir ContainerAppSln
$ cd ContainerAppSln
$ dotnet new sln -n ContainerAppSln
# Create the mvc project and add it to the solution
$ dotnet new mvc -n ContainerApp
$ dotnet sln add ContainerApp
# Add a gitignore file
$ dotnet new gitignore
# Run and test the new application
$ cd ContainerApp
$ dotnet run
```
2. Build the Docker image
```bash
$ ContainerAppSln/ContainerApp
$ docker build -t dotnet-webapp .
# Run the docker container
$  docker run -p 9000:8000 dotnet-webapp
```
Go the http://localhost:9000 to test the running docker container.  

3. Push the docker image to ECR
```bash
# Login Docker Client to  ECR
$ aws ecr get-login-password | docker login --username AWS --password-stdin <account-id>.dkr.ecr.eu-west-2.amazonaws.com
# Tag the image
$ docker tag dotnet-webapp:latest <account-id>.dkr.ecr.eu-west-2.amazonaws.com/dotnet-webapp:latest
# Push image to ECR repository
$ docker push <account-id>.dkr.ecr.eu-west-2.amazonaws.com/dotnet-webapp:latest
```

__Deployment__  
