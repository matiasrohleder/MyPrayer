using Tools.Interfaces.Email;

namespace Tools.Helpers.Email;

public class EmailConfiguration
{
    public EmailConfiguration()
    {
        Emails = [];
    }

    public List<string> Emails { get; set; }
    public string? Subject { get; set; }
    public IEmailDTO? BodyTemplateModel { get; set; }
    public string? Body { get; set; }
}