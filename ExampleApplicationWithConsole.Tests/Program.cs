// See https://aka.ms/new-console-template for more information

using ExampleApplicationWithConsole;
using ExampleApplicationWithConsole.Domain;
using ExampleApplicationWithConsole.Tests;
using JoanComas.ScenarioUnitTesting;

Console.WriteLine("Hello, THIS IS A UNIT TEST EMULATOR because can't debug a Source Generator with an xUnit project!");

var t1 = new WebScrapperTest();
t1.WhenScrapIsCalled_HttpClientGetsUrl(new Scenario<WebScrapper>(), "http://www.roche.com");

var t2 = new NotificationServiceTest();
t2.WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(new Scenario<NotificationService>(), "joan.comas@lol.net", new Alert("My alert"));