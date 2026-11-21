using Microsoft.AspNetCore.Mvc;

namespace labASP.Models;

public class BookDeleteViewModel
{
    [HiddenInput]
    public int Id { get; set; }
}