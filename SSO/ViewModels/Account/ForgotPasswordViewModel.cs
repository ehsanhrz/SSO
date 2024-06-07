﻿using System.ComponentModel.DataAnnotations;

namespace SSO.ViewModels.Account;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
