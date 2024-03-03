namespace Tools.Interfaces.Configuration;

/// <summary>
/// Class that contains the system's general configurations.
/// </summary>
public interface IGeneralConfiguration
{
    string SystemName { get; set; }
    string SystemUrl { get; set; }
    string BackofficeUrl { get; set; }
    string FrontUrl { get; set; }
    bool EnableAntiforgeryToken { get; set; }
    string CookiePolicyUrl { get; set; }
    string PrivacyPolicyUrl { get; set; }
    string TermsAndConditionsURL { get; set; }
    string TermsAndConditionsDate { get; set; }
    string SystemLogoUrl { get; set; }
    string GetFileViewUrl(Guid fileId, string language = null, int width = 110, int height = 110);
}