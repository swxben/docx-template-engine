@echo off
if /i "%1" == "gui" goto :gui

:console
	packages\NUnit.Runners.2.6.2\tools\nunit-console.exe src\Tests\bin\Debug\Tests.dll
	goto :done

:gui
	packages\NUnit.Runners.2.6.2\tools\nunit.exe src\Tests\bin\Debug\Tests.dll
	goto :done

:done
del /Q TestResult.xml