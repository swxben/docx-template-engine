using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;
using swxben.docxtemplateengine;

namespace Tests
{
    public class when_reading_to_and_from_a_stream
    {
        readonly IDocXTemplateEngine _templateEngine = new DocXTemplateEngine();
        private readonly string _pathToTemplateDoc = Path.Combine(TestHelpers.GetRootPath(), "template1.docx");

        [Test]
        public void the_output_stream_is_the_same_length_as_the_output_when_reading_from_a_file()
        {
            byte[] bufferFromFile, bufferFromStream;
            GetTestBuffers(out bufferFromFile, out bufferFromStream);

            bufferFromStream.Length.ShouldBe(bufferFromFile.Length);
        }

        [Test]
        public void the_output_stream_matches_the_output_when_reading_from_a_file()
        {
            byte[] bufferFromFile, bufferFromStream;
            GetTestBuffers(out bufferFromFile, out bufferFromStream);

            for (var i = 0; i < bufferFromFile.Length; i++)
            {
                bufferFromStream[i].ShouldBe(bufferFromFile[i]);
            }
        }

        private void GetTestBuffers(out byte[] bufferFromFile, out byte[] bufferFromStream)
        {
            var data = new {Name = "Bar"};
            using (var streamFromFile = new MemoryStream())
            using (var streamFromStream = new MemoryStream())
            {
                _templateEngine.Process(_pathToTemplateDoc, streamFromFile, data);
                streamFromFile.Close();

                _templateEngine.Process(File.OpenRead(_pathToTemplateDoc), streamFromStream, data);
                streamFromStream.Close();

                bufferFromFile = streamFromFile.GetBuffer();
                bufferFromStream = streamFromStream.GetBuffer();
            }
        }
    }
}
