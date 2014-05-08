using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;
using swxben.docxtemplateengine;
using Newtonsoft.Json;
using System.Dynamic;

namespace Tests
{
    [TestFixture]
    class when_parsing_simple_template
    {
        [Test]
        public void single_object_property_is_replaced()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END;
            var data = new { Name = "John" };

            var result = DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: John");
        }

        [Test]
        public void multiple_object_properties_are_replaced()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END + ", Age: " + DocXTemplateEngine.TOKEN_START + "Age" + DocXTemplateEngine.TOKEN_END;
            var data = new
            {
                Name = "Ben",
                Age = 32
            };

            var result = DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: Ben, Age: 32");
        }

        class PersonWithFields
        {
            public string Name;
            public int Age;
        }
        [Test]
        public void data_fields_are_used()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END + ", Age: " + DocXTemplateEngine.TOKEN_START + "Age" + DocXTemplateEngine.TOKEN_END;
            var data = new PersonWithFields
            {
                Name = "Sam",
                Age = 29
            };

            var result = DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: Sam, Age: 29");
        }

        class PersonWithProperties
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        [Test]
        public void data_properties_are_used()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END + ", Age: " + DocXTemplateEngine.TOKEN_START + "Age" + DocXTemplateEngine.TOKEN_END;
            var data = new PersonWithProperties
            {
                Name = "Sam",
                Age = 29
            };

            var result = DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: Sam, Age: 29");
        }

        [Test]
        public void typed_field_object_from_json_is_used()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END + ", Age: " + DocXTemplateEngine.TOKEN_START + "Age" + DocXTemplateEngine.TOKEN_END;
            var data = JsonConvert.DeserializeObject<PersonWithFields>("{ Name: 'Bob', Age: 26 }");

            var result = DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: Bob, Age: 26");
        }

        [Test]
        public void typed_property_object_from_json_is_used()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END + ", Age: " + DocXTemplateEngine.TOKEN_START + "Age" + DocXTemplateEngine.TOKEN_END;
            var data = JsonConvert.DeserializeObject<PersonWithProperties>("{ Name: 'Bob', Age: 26 }");

            var result = DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: Bob, Age: 26");
        }

        [Test]
        public void dynamic_object_is_used()
        {
            var template = "Name: " + DocXTemplateEngine.TOKEN_START + "Name" + DocXTemplateEngine.TOKEN_END + ", Age: " + DocXTemplateEngine.TOKEN_START + "Age" + DocXTemplateEngine.TOKEN_END;
            dynamic data = new ExpandoObject();
            data.Name = "Maxi";
            data.Age = 25;

            var result = (string)DocXTemplateEngine.ParseTemplate(template, data);

            result.ShouldBe("Name: Maxi, Age: 25");
        }
    }
}
