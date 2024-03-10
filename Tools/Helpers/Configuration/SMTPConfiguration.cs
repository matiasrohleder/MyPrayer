using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Tools.Helpers.Configuration;

public class SmtpConfiguration
{
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public string Address { get; set; } = "";

    public SmtpClient GenerateSmtpClient()
    {
        SmtpClient client = new(Host, Port)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(Name, Password)
        };
        return client;
    }

    public SmtpConfiguration Bind(IConfiguration configuration)
    {
        IConfigurationSection smtpConfig = configuration.GetSection(nameof(SmtpConfiguration));

        if (smtpConfig.Exists())
            smtpConfig.Bind(this);
        else
            SetDefaultConfig();

        return this;
    }

    #region Private methods
    private void SetDefaultConfig()
    {
        Host = "configure-me-host.myprayer.dev";
        Port = 9025;
        Name = "apikey";
        Password = "";
        Address = "configure-me-address@myprayer.dev";
    }
    #endregion
}