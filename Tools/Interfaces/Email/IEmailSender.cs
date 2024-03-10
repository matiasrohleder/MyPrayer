using Tools.Helpers.Email;

namespace Tools.Interfaces.Email;

/// <summary>
/// Email sender helper.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email that was previously built.
    /// </summary>
    Task SendEmailAsync();

    #region Builder methods
    /// <summary>
    /// Sets the adress of the mail.
    /// </summary>
    EmailSender To(string email);

    /// <summary>
    /// Sets the addressees of the mail.
    /// </summary>
    EmailSender To(IEnumerable<string> emails);

    /// <summary>
    /// Set the provider of the subject.
    /// </summary>
    EmailSender SetSubjectProvider(Func<string> subjectProvider);

    /// <summary>
    /// Set the provider of the body template.
    /// </summary>
    EmailSender SetBodyTemplateModelProvider(Func<IEmailDTO> bodyTemplateModelProvider);

    /// <summary>
    /// Set the provider of the html body.
    /// </summary>
    EmailSender SetHTMLBodyProvider(Func<string> htmlBodyProvider);
    #endregion
}