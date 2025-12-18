namespace BasicLambda;

using Amazon.Lambda.Core;

public class Function
{
  public string FunctionHandler(string input, ILambdaContext context)
  {
        context.Logger.LogLine($"Input: {input}");
        return $"Hello {input}";
   }
}
