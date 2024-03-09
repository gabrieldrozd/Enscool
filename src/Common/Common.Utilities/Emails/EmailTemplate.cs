using Scriban;

namespace Common.Utilities.Emails;

public abstract class EmailTemplate
{
    public bool Render(out string subject, out string htmlBody, out string textBody)
    {
        var template = ResourceTemplateReader.GetTemplate(GetType().Name);
        if (template is null)
        {
            subject = string.Empty;
            htmlBody = string.Empty;
            textBody = string.Empty;
            return false;
        }

        var parsedTemplate = Template.Parse(template.Value.HtmlBody);
        if (parsedTemplate is null || parsedTemplate.HasErrors)
        {
            subject = string.Empty;
            htmlBody = string.Empty;
            textBody = string.Empty;
            return false;
        }

        subject = template.Value.Subject;
        htmlBody = parsedTemplate.Render(this);
        textBody = parsedTemplate.ToText();
        return true;
    }
}