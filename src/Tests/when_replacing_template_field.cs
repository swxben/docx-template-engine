using NUnit.Framework;
using Shouldly;
using swxben.docxtemplateengine;

namespace Tests
{
    [TestFixture]
    class when_replacing_template_field
    {
        [Test]
        public void simple_value_is_replaced()
        {
            var template = "this is an " + DocXTemplateEngine.TOKEN_START + "tmpl" + DocXTemplateEngine.TOKEN_END + " template";

            var result = DocXTemplateEngine.ReplaceTemplateField(template, "tmpl", "example");

            result.ShouldBe("this is an example template");
        }
    }
}
