namespace BasicLambda;

using Amazon.Lambda.Core;

public class Function
{
  public string FunctionHandler(Input input, ILambdaContext context)
  {
        context.Logger.LogLine($"Name: {input.Name}");
        return $"Hello {input.Name}";
  }

    public class Input
    {
        public string Name { get; set; } = "";
    }
}
