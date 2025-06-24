using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginForm.Models;

public partial class UsersCredential
{
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Password must be strong")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
