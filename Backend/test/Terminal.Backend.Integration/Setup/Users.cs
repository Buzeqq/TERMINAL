﻿using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Integration.Setup;

public static class Users
{
    public const string Password = "1qaz@WSX3edc$RFV";

    private const string AdminEmail = "admin@terminal.com";
    public static ApplicationUser Admin = new()
    {
        Email = AdminEmail,
        UserName = AdminEmail,
        Role = ApplicationRole.Administrator,
        EmailConfirmed = true,
        TwoFactorEnabled = false
    };

    private const string ModeratorEmail = "moderator@terminal.com";
    public static ApplicationUser Moderator = new()
    {
        Email = ModeratorEmail,
        UserName = ModeratorEmail,
        Role = ApplicationRole.Moderator,
        EmailConfirmed = true,
        TwoFactorEnabled = false
    };

    private const string UserEmail = "user@terminal.com";
    public static ApplicationUser User = new()
    {
        Email = UserEmail,
        UserName = UserEmail,
        Role = ApplicationRole.User,
        EmailConfirmed = true,
        TwoFactorEnabled = false
    };

    private const string GuestEmail = "guest@terminal.com";
    public static ApplicationUser Guest = new()
    {
        Email = GuestEmail,
        UserName = GuestEmail,
        Role = ApplicationRole.Guest,
        EmailConfirmed = false,
        TwoFactorEnabled = false
    };
}
