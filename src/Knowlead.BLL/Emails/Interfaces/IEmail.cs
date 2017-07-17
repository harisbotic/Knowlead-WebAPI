namespace Knowlead.BLL.Emails.Interfaces
{
    public interface IEmail
    {
        string TemplateFilename { get; }
        string FillTemplate(string template);
    }
}