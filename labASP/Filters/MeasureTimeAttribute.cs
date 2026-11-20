using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace labASP.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MeasureTimeAttribute : ActionFilterAttribute
{
    private const string Key = "__ActionSw";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Items[Key] = Stopwatch.StartNew();
        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.HttpContext.Items[Key] is Stopwatch sw)
        {
            sw.Stop();
            context.HttpContext.Response.Headers["X-Action-Ms"] = sw.ElapsedMilliseconds.ToString();
        }
        base.OnActionExecuted(context);
    }
}