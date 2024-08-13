using System.Diagnostics.CodeAnalysis;

namespace JoanComas.ScenarioUnitTesting;

/// <summary>
/// Is thrown when there is a call to <c>scenario.Dependency&lt;MyDependency&gt;()</c>, but the type is not passed
/// in the system under test constructor, meaning this type is not a dependency of the system under test.
///
/// <para>
/// Basically you wrote the wrong type in your test.
/// </para>
/// </summary>
public class DependencyTypeNotFoundInSutDependenciesException : Exception
{
    internal DependencyTypeNotFoundInSutDependenciesException([NotNull] Type sutType, [NotNull] Type dependencyType)
        : base($"The system under test of type {sutType.FullName} does not have a dependency of type {dependencyType.FullName}.")
    {
    }
}