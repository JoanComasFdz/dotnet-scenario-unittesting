# dotnet-scenario-unittesting
A very easy way to write Unit Tests in [xUnit](https://xunit.net/docs/getting-started/netcore/cmdline), leveraging [NSubstitute](https://github.com/nsubstitute/NSubstitute) and [AutoFixture](https://github.com/AutoFixture/AutoFixture).

I recommend to complement it with [FluentAssertions](https://fluentassertions.com/).

[![NuGet Badge](https://buildstats.info/nuget/JoanComas.ScenarioUnitTesting)](https://www.nuget.org/packages/JoanComas.ScenarioUnitTesting)

## How to Use
1. Decorate your test with `[Theory, AutoData]`.
2. Add a `Scenario<MyClass>` parameter to the test test.
3. Use the `When()` method to get the instance of the System Under Test.
4. Use the `Dependency<T>` method to get a Mock, then configure it or assert it.

Example:

```chsarp
[Theory, AutoData]
public void ExampleTest(Scenario<MyClass> scenario)
{
    scenario.Dependency<IMyInterface>().GetSomething(true).Returns(123);

    scenario.When().DoSomething();

    scenario.Dependency<IMyInterface>()
        .Received()
        .GetSomething(true);
}
```

## What problem does it solve?
The idea is to have a very accessible "automock container", that can abstract all the nitty gritty of using AutoFixture, by providing a single access point to the system under test and its dependencies.

Tradicionally, you have a class and its constructor has parameters of type interface. The **injected dependencies**.

The `Scenario<T>` class takes care of creating all the mocks with NSubstitue and registering them in a Fixture, then using the Fixture to instantiate the system under test (thus receiving the mocks).

The system under test can be accessed via the `When()` method.

The injected depenencies can be accessed via the `Dependency<T>()` method. It will always return the very same instance, which is the one injected in the system under test. This way, the mock can be set up, injected and asserted over. And there is no need to [deal with Rejister, Inject and Freeze](https://stackoverflow.com/questions/18161127/cant-grasp-the-difference-between-freeze-inject-register).

## Coding guidelines
* Always use `scenario.Dependency<T>()` to setup or assert. Do not add interfaces as parameters in your unit test. 
* Do not store the interfaces in a local variable. Alyways use `scenario.Dependency<T>()` to obtain it.
* You already have NSubstitute available, so familiarize yourselve with `.Returns()` and `.Received()` methods.
* Yoy already have FluentAssertions available, so familiarize yourself with `.Should()` method.

## Future work
I am currently working on adding a [.NET Source Generator]() that will automatically generate a custom Scenario class, replacing the `When()` method by a property of the type and name of the system under test, so it will look like:

```csharp
public void MyTest(MyClassScenario scenario)
{
    // Arrange...

    scenario.MyClass.DoSomething();

    // Assert...
}

```

Unfortunately, as of now, Source Generators [do not support xUnit projects](https://github.com/dotnet/roslyn-sdk/issues/972). Most of the code is already available in this repo, it just doesnt really work.

### Useful link for Source Generators
* [dominikjeske | Source Generators - real world example](https://dominikjeske.github.io/source-generators/)
* [andrewlock | Series: Creating a source generator](https://andrewlock.net/series/creating-a-source-generator/)
* [Github | Source Generators Cookbook](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#breaking-changes)
* [Github | C# Source Generators](https://github.com/amis92/csharp-source-generators)
* [Microsoft Docs | Source Generators](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)
* [InfoQ | Building a Source Generator for C#](https://www.infoq.com/articles/CSharp-Source-Generator/)