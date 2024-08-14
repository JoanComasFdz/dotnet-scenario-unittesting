# .NET Scenario Unit Testing ![JoanComas.ScenarioUnitTesting](https://buildstats.info/nuget/JoanComas.ScenarioUnitTesting)
A very easy way to write Unit Tests in [xUnit](https://xunit.net/docs/getting-started/netcore/cmdline), leveraging [NSubstitute](https://github.com/nsubstitute/NSubstitute) and [AutoFixture](https://github.com/AutoFixture/AutoFixture), complemented with [FluentAssertions](https://fluentassertions.com/).

## Usage
1. Decorate your test with `[Theory, AutoData]`.
2. Add a `Scenario<MyClass>` parameter to the test method.
3. Use the `Dependency<T>` method to get a Mock, then configure it or assert it.
4. Use the `When()` method to get the instance of the System Under Test.

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