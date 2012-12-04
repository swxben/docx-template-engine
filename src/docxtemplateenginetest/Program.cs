using System;
using System.Linq;
using Newtonsoft.Json;
using swxben.docxtemplateengine;

namespace docxtemplateenginetest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("docxtemplateenginetest, CC BY-SA 3.0, swxben.com");

            if (args.Count() != 3)
            {
                Console.WriteLine("Usage: docxtemplateenginetest <input.docx> <output.docx> \"<json input>\"");
                Console.WriteLine("Eg: docxtemplateenginetest input.docx output.docx { name: 'Software by Ben' }");
                return;
            }

            var source = args[0];
            var destination = args[1];
            var json = args[2];

            Console.WriteLine();
            Console.WriteLine("Data:");
            var data = JsonConvert.DeserializeObject(json);
            Console.WriteLine(data.ToString());
            Console.WriteLine();

            var templateEngine = new DocXTemplateEngine();

            Console.WriteLine("Processing...");

            templateEngine.Process(source, destination, data);

            Console.WriteLine("Complete");
            Console.WriteLine();
        }
    }
}
