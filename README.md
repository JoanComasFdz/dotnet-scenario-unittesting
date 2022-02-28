# dotnet-scenario-unittesting
A very easy way to write Unit Tests in [xUnit](https://xunit.net/docs/getting-started/netcore/cmdline), leveraging [NSubstitute](https://github.com/nsubstitute/NSubstitute) and [AutoFixture](https://github.com/AutoFixture/AutoFixture).

I recommend to complement it with [FluentAssertions](https://fluentassertions.com/).

## How to Use
Explore the `ScenarioUnitTesting.Tests` folder for a couple of examples.

### Step by step
1. Decorate your test with `[Theory, AutoNSubstituteData]`
2. Add a Scenario parameter for the class you want to test:
3. Use the `When()` method to gethe instance of the System Under Test.
4. Use the `Dependency<T>` method to get a Mock, then configure it or assert it.

Example:

```chsarp
[Theory, AutoNSubstituteData]
public void ExampleTest(Scenario<NotificationService> scenario)
{
    scenario.Dependency<IMyInterface>().GetSomething(true).Returns(123);

    scenario.When().DoSomething();

    scenario.Dependency<IMyInterface>()
        .Received()
        .GetSomething(true);
}
```