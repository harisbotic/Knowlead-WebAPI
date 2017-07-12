namespace Knowlead.Common.Email.Interfaces
{
    public interface IEmail
    {
        string TemplateFilename { get; }
        string FillTemplate(string template);
    }
}