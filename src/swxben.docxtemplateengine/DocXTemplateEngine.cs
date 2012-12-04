using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace swxben.docxtemplateengine
{
    public interface IDocXTemplateEngine
    {
        void Process(string source, string destination, object data);
    }

    public class DocXTemplateEngine : IDocXTemplateEngine
    {
        public void Process(string source, string destination, object data)
        {
        }
    }
}
