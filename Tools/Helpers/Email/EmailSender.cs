using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Tools.Helpers.Configuration;
using Tools.Interfaces.Email;

namespace Tools.Helpers.Email;

/// <summary>
/// Email sender helper.
/// </summary>
public class EmailSender(
    IConfiguration configuration,
    IFluentEmailFactory mail
        ) : IEmailSender
{
    #region Builder Properties
    private List<string> Emails { get; set; } = new();
    private Func<string> SubjectProvider { get; set; } = () => "";
    private Func<IEmailDTO> BodyTemplateModelProvider { get; set; } = () => null!;
    private Func<string> HtmlBodyProvider { get; set; } = () => "";
    #endregion

    private readonly IFluentEmailFactory mail = mail;
    private readonly SmtpConfiguration smtpConfiguration = new SmtpConfiguration().Bind(configuration);

    #region Public methods
    public async Task SendEmailAsync()
    {
        EmailConfiguration emailConfig = Build();

        IFluentEmail mail = this.mail
            .Create()
            .To(GetAddresses(emailConfig.Emails))
            .SetFrom(smtpConfiguration.Address)
            .Subject(emailConfig.Subject);

        if (emailConfig.Body is not null)
            mail.Body(emailConfig.Body);
        else
            mail.UsingTemplateFromEmbedded(
                path: emailConfig.BodyTemplateModel.EmailTemplateNamespace,
                model: emailConfig.BodyTemplateModel,
                assembly: emailConfig.BodyTemplateModel.GetType().GetTypeInfo().Assembly);

        await mail.SendAsync();
    }

    #region Builder Methods
    public EmailSender To(string email)
    {
        Emails = new() {
            email
        };

        return this;
    }

    public EmailSender To(IEnumerable<string> emails)
    {
        Emails = emails.ToList();
        return this;
    }

    public EmailSender SetSubjectProvider(Func<string> subjectProvider)
    {
        SubjectProvider = subjectProvider;
        return this;
    }

    public EmailSender SetBodyTemplateModelProvider(Func<IEmailDTO> bodyTemplateModelProvider)
    {
        HtmlBodyProvider = () => "";
        BodyTemplateModelProvider = bodyTemplateModelProvider;
        return this;
    }

    public EmailSender SetHTMLBodyProvider(Func<string> htmlBodyProvider)
    {
        BodyTemplateModelProvider = null!;
        HtmlBodyProvider = htmlBodyProvider;
        return this;
    }
    #endregion
    #endregion

    #region Private Methods

    private static List<Address> GetAddresses(IEnumerable<string> emails) => emails.Select(email => new Address(email)).ToList();

    private EmailConfiguration Build() =>
        new()
        {
            Emails = Emails,
            Subject = SubjectProvider?.Invoke(),
            BodyTemplateModel = BodyTemplateModelProvider.Invoke(),
            Body = HtmlBodyProvider?.Invoke()
        };
    #endregion
}