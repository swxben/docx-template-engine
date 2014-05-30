DocX Template Engine
====================

A template engine for the .NET platform which takes a DOCX file and applies a data object to generate reports, do mail merges, etc. All without introducing a dependency on MS Word.


## Installation

Install the engine via [NuGet](http://nuget.org/packages/swxben.docxtemplateengine), either in Visual Studio (right-click project, Manage NuGet Packages, search for docxtemplateengine) or via the package manager console using `Install-Package DocXTemplateEngine`.


## Usage

In `template.docx` - use a field (Insert / Quick Parts / Field), _Field type_ is `MergeField` and enter the field or property name in _Field_:

	Brought to you by <<Name>>!

In code:

	var templateEngine = new swxben.docxtemplateengine.DocXTemplateEngine();
	templateEngine.Process(
		source = "template.docx",
		destination = "dest.docx",
		data = new {
			Name = "SWXBEN"
		});

`data` can be just about any data type. Fields and properties are iterated, and if it can be cast to `IDictionary<object, string>` (such as a dynamic `ExpandoObject`) the dictionary entries are iterated.

`dest.docx` is created:

	Brought to you by SWXBEN!

A very simple use case is given in the `docxtemplateenginetest` project, which is a command line application that takes an input filename,
an output filename, and a JSON string. Eg:

	docxtemplateenginetest.exe template.docx output.docx "{ Name: 'Ben', Website: 'swxben.com' }"


## Limitations

The templating language in the first milestone is a simple search and replace using the value `.ToString()`. No escaping of the 
template sequence, loops, formatting in document etc. The template _probably_ won't be recognised if there is any formatting within
the template string such as an errant bold character like {{th**i**s}}. There are also issues with UTF8 encoding and characters such as em dash which are currently solved just by replacing the errant characters which isn't ideal.


## Versions

### 0.1.4

- Read from stream ([@MarkKGreenway](https://github.com/MarkKGreenway))


## Roadmap

### First milestone - completed 20121204

- Simple search and replace as given in the first example.
- Originally I wanted plain text templates such as `[[Name]]` or `{{Name}}` but Word likes to insert runs for things like grammar warnings, so
sticking to merge fields for now

### Second milestone - completed 20121205

- NuGet package

### Third milestone

- Nested objects, eg `{{Address.Line1}} {{Address.Line2}}`
- Formatting in the template, eg `{{DateOfBirth.ToString("dd-MM-yyyy")}}`

### Fourth milestone

- Flow control in the template, eg `{{foreach (var name in Names)}} {{name}}, {{endforeach}}`


## Contribute

If you want to contribute to this project, start by forking the repo: <https://github.com/swxben/docx-template-engine>. Create an issue if applicable, create a branch in your fork, and create a pull request when it's ready. Thanks!

Note that the template engine implements the `IDocXTemplateEngine` interface, so any additions to the engine need to be reflected in the interface.

**Please don't include any new versions of the NuGet package in your PR!** I'll upload a new package as soon as I'm happy with the changes in the new version. But thanks for your enthusisasm ;-)


## Building, running tests and the NuGet package

THe VS2013 solution is in the root folder. Unit tests (src\Tests\bin\Debug\Tests.dll) can be run in a console using `tests.bat`. The NuGet package can be built by running `build-nuget-package.cmd`. The NuGet build script assumes VS 2013 (VS110).


## Licenses

All files [CC BY-SA 3.0](http://creativecommons.org/licenses/by-sa/3.0/) unless otherwise specified.

### Third party licenses

Third party libraries or resources have been included in this project under their respective licenses.

- The engine uses SharpZipLib [1](http://www.icsharpcode.net/opensource/sharpziplib/) [2](https://github.com/icsharpcode/SharpZipLib) which is GPL with an exception meaning it can be used in commercial closed-source applications.
- The example project uses [JSON.NET](http://json.codeplex.com/) which uses the [MIT License and is &copy; James Newton-King](http://json.codeplex.com/license).


## Contributors & Maintainers

- [@bendetat]() (contributor)
- [@deltasem](https://github.com/deltasem)
- [@MarkKGreenway](https://github.com/MarkKGreenway)
