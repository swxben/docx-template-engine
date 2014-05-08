using System.IO;

namespace swxben.docxtemplateengine
{
    public interface IDocXTemplateEngine
    {
        void Process(string source, string destination, object data);
        void Process(string source, Stream destination, object data);
        void Process(Stream source, Stream destination, object data);
    }
}