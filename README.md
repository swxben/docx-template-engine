DocX Template Engine
====================

A template engine for the .NET platform which takes a DOCX file and applies a data object to generate reports, do mail merges, etc. All without introducing a dependency on MS Word.


## Installation

At the moment there isn't a reasonable install or upgrade path. Maybe a NuGet package will be in the pipeline some time in the distant future.


## Usage

In `template.docx`:

	Brought to you by {{Name}}!

In code:

	var templateEngine = new swxben.docxtemplateengine.DocXTemplateEngine();
	templateEngine.Process(
		source = "template.docx",
		destination = "dest.docx",
		data = new {
			Name = "SWXBEN"
		});

`dest.docx` is created:

	Brought to you by SWXBEN!

A very simple use case is given in the `docxtemplateenginetest` project, which is a command line application that takes an input filename,
an output filename, and a JSON string.


## Limitations

The templating language in the first milestone is a simple search and replace using the value `.ToString()`. No escaping of the 
template sequence, loops, formatting in document etc. The template _probably_ won't be recognised if there is any formatting within
the template string such as an errant bold character like {{th**i**s}}.


## Roadmap

### First milestone

- Simple search and replace as given in the first example.

### Second milestone

- NuGet package

### Third milestone

- Escaping in the templating language
- Nested objects, eg `{{Address.Line1}} {{Address.Line2}}`
- Formatting in the template, eg `{{DateOfBirth.ToString("dd-MM-yyyy")}}`

### Fourth milestone

- Flow control in the template, eg `{{foreach (var name in Names)}} {{name}}, {{endforeach}}`


## Contribute

If you want to contribute to this project, start by forking the repo: <https://github.com/swxben/docx-template-engine>. Create an issue if applicable, create a branch in your fork, and create a pull request when it's ready. Thanks!


## Licenses

All files [CC BY-SA 3.0](http://creativecommons.org/licenses/by-sa/3.0/) unless otherwise specified.

### Third party licenses

Third party libraries or resources have been included in this project under their respective licenses.

- The example project uses [JSON.NET](http://json.codeplex.com/) which uses the [MIT License and is &copy; James Newton-King](http://json.codeplex.com/license).

