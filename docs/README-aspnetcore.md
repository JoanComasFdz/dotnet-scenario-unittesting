# Asp .NET Core Scenario Unit Testing ![JoanComas.ScenarioUnitTesting.AspNetCore](https://buildstats.info/nuget/JoanComas.ScenarioUnitTesting.AspNeTCore)


Based on the ScenarioUnitTesting, allows to instantiate a `Controller` (which won't be possilble with the `Scenario` class because of [BindingInfo](https://github.com/AutoFixture/AutoFixture/issues/1141])), just use the `ControllerScenario` instead.

Additionally, it has a `ControllerContext` property exposed to arrange it.

Finally, it makes sure that any `DbContext` uses an in-memory database, so that you don't have to arrange nor fake it to avoid a real database connection.

## Usage

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
 If your Controller gets a `DbContext` injected, make sure that the types used in the `DbSets` have a primary key.