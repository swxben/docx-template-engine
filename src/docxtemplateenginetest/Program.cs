using System;
using System.Linq;
using Newtonsoft.Json;
using swxben.docxtemplateengine;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Collections.Generic;

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

            var data = JsonToDynamic((JToken)JsonConvert.DeserializeObject(json));

            var templateEngine = new DocXTemplateEngine();

            Console.WriteLine("Processing...");

            templateEngine.Process(source, destination, data);

            Console.WriteLine("Complete");
            Console.WriteLine();
        }

        static dynamic JsonToDynamic(JToken token)
        {
            if (token is JValue) return ((JValue)token).Value;

            if (token is JObject)
            {
                var expando = new ExpandoObject();
                foreach (var childToken in token.OfType<JProperty>())
                {
                    ((IDictionary<string, object>)expando).Add(childToken.Name, JsonToDynamic(childToken.Value));
                }
                return expando;
            }

            if (token is JArray)
            {
                var items = new List<ExpandoObject>();
                foreach (var arrayItem in ((JArray)token))
                {
                    items.Add(JsonToDynamic(arrayItem));
                }
                return items;
            }

            throw new ArgumentException(string.Format("Unknown token type {0}", token.GetType()), "token");
        }
    }
}
