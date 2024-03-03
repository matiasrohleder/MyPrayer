using Microsoft.Extensions.Configuration;
using Tools.Interfaces.Configuration;

namespace Tools.Helpers.Configuration;
// sarasa revisar properties
public class RealmConfiguration : IRealmConfiguration
{
    public string Name { get; set; }
    public bool AllowSelfSignUp { get; set; }
    public bool UseSelfSignUpCaptcha { get; set; }
    public bool Allow2FA { get; set; }
    public bool AllowApiKeyAuthorization { get; set; }
    public bool AllowPasswordlessLogin { get; set; }
    public bool AllowDeleteMyAccount { get; set; }
    public int IdentityTokenExpirationMinutes { get; set; }
    public int ApiKeyCacheExpirationInMinutes { get; set; }
    public bool RequireAdminUserConfirmation { get; set; }
    public bool RequireEmailConfirmation { get; set; }

    private string supportedDomains;

    public string SupportedDomains
    {
        get { return supportedDomains?.Replace(" ", ""); }
        set { supportedDomains = value; }
    }

    public RealmConfiguration(IConfiguration configuration)
    {
        Bind(configuration);
    }

    public RealmConfiguration Bind(IConfiguration configuration)
    {
        configuration.GetSection("Realm").Bind(this);
        return this;
    }
}