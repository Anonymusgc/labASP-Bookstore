using System.ComponentModel.DataAnnotations;

namespace labASP.Models;

public class Author
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane.")]
    [Display(Name = "Imię")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko jest wymagane.")]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Kraj")]
    public string? Country { get; set; }
}