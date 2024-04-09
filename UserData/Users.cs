using System.ComponentModel.DataAnnotations;

namespace Intex2Backend.UserData;

public class User
{
    [Key] public int UserId { get; set; }

    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    public bool IsAdmin { get; set; } = false;
}