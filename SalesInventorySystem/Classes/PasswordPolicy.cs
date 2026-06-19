using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class PasswordPolicy
{
    public const int MinLength = 8;
    public const int MaxLength = 20;

    public static bool Validate(string password, string userId, out string errorMessage)
    {
        List<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errorMessage = "Password is required.";
            return false;
        }

        if (password.Length < MinLength || password.Length > MaxLength)
            errors.Add("Password must be between 8 and 20 characters.");

        if (!Regex.IsMatch(password, "[A-Z]"))
            errors.Add("Password must contain at least one uppercase letter.");

        if (!Regex.IsMatch(password, "[a-z]"))
            errors.Add("Password must contain at least one lowercase letter.");

        if (!Regex.IsMatch(password, "[0-9]"))
            errors.Add("Password must contain at least one number.");

        // Optional rule: require special character
        // Uncomment if you want stronger passwords
        /*
        if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            errors.Add("Password must contain at least one special character.");
        */

        if (!string.IsNullOrWhiteSpace(userId) &&
            password.IndexOf(userId, StringComparison.OrdinalIgnoreCase) >= 0)
        {
            errors.Add("Password must not contain your User ID.");
        }

        string lower = password.ToLowerInvariant();
        string[] banned =
        {
            "123456",
            "12345678",
            "password",
            "qwerty",
            "admin",
            "abc123",
            "111111",
            "123123"
        };

        if (banned.Contains(lower))
            errors.Add("Password is too common. Please choose a stronger password.");

        // Optional: reject repeated same character e.g. AAAAAAAA or 11111111
        if (Regex.IsMatch(password, @"^(.)\1+$"))
            errors.Add("Password must not contain only repeated characters.");

        if (errors.Count > 0)
        {
            errorMessage = string.Join(Environment.NewLine, errors);
            return false;
        }

        errorMessage = "";
        return true;
    }
}