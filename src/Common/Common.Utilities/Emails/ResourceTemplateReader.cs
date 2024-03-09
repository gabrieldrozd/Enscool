using System.Resources;
using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Common.Utilities.Resources.EmailTemplates;
using Scriban;

namespace Common.Utilities.Emails;

public static class ResourceTemplateReader
{
    /// <summary>
    /// The name of the template that wraps the actual template.
    /// </summary>
    private const string WrapperTemplateName = "WrapperTemplate";

    /// <summary>
    /// The resource manager that contains all the templates.
    /// </summary>
    private static readonly ResourceManager ResourceManager = FileResource.ResourceManager;

    /// <summary>
    /// Returns the template with the given name.
    /// </summary>
    /// <param name="templateName">Name of the template.</param>
    /// <returns>The subject and wrapped html body of the template.</returns>
    public static (string Subject, string HtmlBody)? GetTemplate(string templateName)
    {
        var fullTemplate = ResourceManager.GetString(templateName);
        Ensure.Not.NullOrEmpty(fullTemplate);

        var index = fullTemplate.IndexOf('\n');
        var subject = fullTemplate[..index].Trim();
        var innerTemplate = fullTemplate[(index + 1)..].Trim();

        var wrapperTemplate = ResourceManager.GetString(WrapperTemplateName);
        Ensure.Not.NullOrEmpty(wrapperTemplate);

        var template = Template.Parse(wrapperTemplate);
        var htmlBody = template.Render(new { template = innerTemplate });
        return (subject, htmlBody);
    }
}