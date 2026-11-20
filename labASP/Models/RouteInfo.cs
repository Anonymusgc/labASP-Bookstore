namespace labASP.Models;

public class RouteInfo
{
   public string Pattern { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Methods { get; set; } = string.Empty;
    public string? Area { get; set; }
}