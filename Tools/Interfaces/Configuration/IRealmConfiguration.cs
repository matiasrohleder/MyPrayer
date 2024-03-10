namespace Tools.Interfaces.Configuration;

public interface IRealmConfiguration
{
    string Name { get; set; }
    bool AllowSelfSignUp { get; set; }
    bool AllowApiKeyAuthorization { get; set; }
    int IdentityTokenExpirationMinutes { get; set; }
    bool RequireEmailConfirmation { get; set; }
}