using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace swxben.docxtemplateengine
{
    public interface IDocXTemplateEngine
    {
        void Process(string source, string destination, object data);
        void Process(string source, Stream destination, object data);
    }

    public class DocXTemplateEngine : IDocXTemplateEngine
    {
        const string DOCUMENT_XML_PATH = @"word/document.xml";
        public const string TOKEN_START = "«";
        public const string TOKEN_END = "»";

        public void Process(string source, Stream destination, object data)
        {
            var sourceData = File.ReadAllBytes(source);
            destination.Write(sourceData, 0, sourceData.Length);
            destination.Seek(0, SeekOrigin.Begin);
            using (var zipFile = new ZipFile(destination))
            {
                ProcessZip(data, zipFile);
            }
        }

        public void Process(string source, string destination, object data)
        {
            if (File.Exists(destination)) File.Delete(destination);

            File.Copy(source, destination);

            using (var zipFile = new ZipFile(destination))
            {
                ProcessZip(data, zipFile);
            }
        }


        public void Process(Stream sourceStream, Stream destinationStream, object data)
        {
            sourceStream.CopyTo(destinationStream);
            destinationStream.Seek(0, SeekOrigin.Begin);
            using (var zipFile = new ZipFile(destinationStream))
            {
                ProcessZip(data, zipFile);
            }
        }
        private static void ProcessZip(object data, ZipFile zipFile)
        {
            zipFile.BeginUpdate();

            var document = "";
            var entry = zipFile.GetEntry(DOCUMENT_XML_PATH);
            if (entry == null)
            {
                throw new Exception(string.Format("Can't find {0} in template zip", DOCUMENT_XML_PATH));
            }

            using (var s = zipFile.GetInputStream(entry))
            using (var reader = new StreamReader(s, Encoding.UTF8))
            {
                document = reader.ReadToEnd();
            }

            var newDocument = ParseTemplate(document, data);

            zipFile.Add(new StringStaticDataSource(newDocument), DOCUMENT_XML_PATH, CompressionMethod.Deflated, true);

            zipFile.CommitUpdate();

        }

        public class StringStaticDataSource : IStaticDataSource
        {
            string _source;

            public StringStaticDataSource(string source)
            {
                _source = source;
            }

            public Stream GetSource()
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(_source));
            }
        }

        public static string ParseTemplate(string document, object data)
        {
            foreach (var field in data.GetType().GetFields())
            {
                document = ReplaceTemplateField(document, field.Name, field.GetValue(data).ToString());
            }
            foreach (var property in data.GetType().GetProperties())
            {
                document = ReplaceTemplateField(document, property.Name, property.GetValue(data, null).ToString());
            }
            if (data is IDictionary<string, object>)
            {
                var dict = (IDictionary<string, object>)data;
                foreach (var key in dict.Keys)
                {
                    document = ReplaceTemplateField(document, key, dict[key].ToString());
                }
            }

            return document;
        }

        public static string ReplaceTemplateField(string document, string fieldName, string fieldValue)
        {
            return document.Replace(TOKEN_START + fieldName + TOKEN_END, fieldValue);
        }
    }
}
