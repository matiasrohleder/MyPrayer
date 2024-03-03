namespace Tools.Interfaces.Configuration;

public interface IRealmConfiguration
{
    string Name { get; set; }
    bool AllowSelfSignUp { get; set; }
    bool AllowPasswordlessLogin { get; set; }
    int IdentityTokenExpirationMinutes { get; set; }
    bool UseSelfSignUpCaptcha { get; set; }
    bool Allow2FA { get; set; }
    bool AllowApiKeyAuthorization { get; set; }
    bool AllowDeleteMyAccount { get; set; }
    int ApiKeyCacheExpirationInMinutes { get; set; }
    string SupportedDomains { get; set; }
    bool RequireAdminUserConfirmation { get; set; }
    bool RequireEmailConfirmation { get; set; }
}