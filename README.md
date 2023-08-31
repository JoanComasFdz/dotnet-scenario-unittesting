# Dotnet Scenario Unit Testing ![JoanComas.ScenarioUnitTesting](https://buildstats.info/nuget/JoanComas.ScenarioUnitTesting)
A very easy way to write Unit Tests in [xUnit](https://xunit.net/docs/getting-started/netcore/cmdline), leveraging [NSubstitute](https://github.com/nsubstitute/NSubstitute) and [AutoFixture](https://github.com/AutoFixture/AutoFixture), complemented with [FluentAssertions](https://fluentassertions.com/).

# Installation
Install the [JoanComas.ScenarioUnitTesting Nuget Package](https://www.nuget.org/packages/JoanComas.ScenarioUnitTesting)

## Package Manager Console
```
Install-Package JoanComas.ScenarioUnitTesting
```

## .NET Core CLI
```
dotnet add package JoanComas.ScenarioUnitTesting
```

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

## Asp .NET Core Scenario Unit Testing ![JoanComas.ScenarioUnitTesting.AspNetCore](https://buildstats.info/nuget/JoanComas.ScenarioUnitTesting.AspNeTCore)


Based on the ScenarioUnitTesting, allows to instantiate a `Controller` (which won't be possilble with the `Scenario` class because of [BindingInfo](https://github.com/AutoFixture/AutoFixture/issues/1141])), just use the `ControllerScenario` instead.

Additionally, it has a `ControllerContext` property exposed to arrange it.

Finally, it makes sure that any `DbContext` uses an in-memory database, so that you don't have to arrange nor fake it to avoid a real database connection.

# Installation
Install the [JoanComas.ScenarioUnitTesting.AspNetCore Nuget Package](https://www.nuget.org/packages/JoanComas.ScenarioUnitTesting.AspNetCore)


## Package Manager Console
```
Install-Package JoanComas.ScenarioUnitTesting.AspNetCore
```

## .NET Core CLI
```
dotnet add package JoanComas.ScenarioUnitTesting.AspNetCore
```

# Usage

Example:

```chsarp
[Theory, AutoData]
public void ExampleTest(ControllerScenario<MyControllerClass> scenario)
{
    scenario.Dependency<IMyInterface>().GetSomething(true).Returns(123);
    scenario.ControllerContext().HttpContext.User.Identity?.Name.Returns("User1");

    scenario.When().DoSomething();

    scenario.Dependency<IMyInterface>()
        .Received()
        .GetSomething(true);
}
```

**Important**: 
 If your Controller gets a `DbContext` injected, make sure that:
 - It has a parameterless constructor.
 - The types used in the `DbSets` have a primary key.