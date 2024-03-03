using Microsoft.Extensions.Configuration;
using System.Globalization;
using Tools.Interfaces.Configuration;

namespace Tools.Helpers.Configuration;

/// <summary>
/// Class that contains the system's general configurations.
/// Binded from the appsettings "General" section.
/// </summary>
public class GeneralConfiguration : IGeneralConfiguration
{
    public string SystemName { get; set; }
    public string SystemUrl { get; set; }
    public string BackofficeUrl { get; set; }
    public string FrontUrl { get; set; }
    public bool EnableAntiforgeryToken { get; set; }
    public string CookiePolicyUrl { get; set; }
    public string PrivacyPolicyUrl { get; set; }
    public string TermsAndConditionsURL { get; set; }
    public string TermsAndConditionsDate { get; set; }
    public string SystemLogoUrl { get; set; }

    public GeneralConfiguration(IConfiguration configuration)
    {
        Bind(configuration);
    }

    public GeneralConfiguration Bind(IConfiguration configuration)
    {
        configuration.GetSection("General").Bind(this);
        return this;
    }

    public string GetFileViewUrl(Guid fileId, string language = null, int width = 110, int height = 110) =>
        $"{SystemUrl}/file/display/public/{fileId}?culture={language ?? CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}&Width={width}&Height={height}&Mode=stretch&useCache=false";
}