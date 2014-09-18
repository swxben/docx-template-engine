using System.IO;

namespace swxben.docxtemplateengine
{
    public interface IDocXTemplateEngine
    {
        void Process(string source, string destination, object data, DocxXmlHandling docxXmlHandling = DocxXmlHandling.Ignore);
        void Process(string source, Stream destination, object data, DocxXmlHandling docxXmlHandling = DocxXmlHandling.Ignore);
        void Process(Stream source, Stream destination, object data, DocxXmlHandling docxXmlHandling = DocxXmlHandling.Ignore);
    }
}