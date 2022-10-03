using System;
using System.Diagnostics.CodeAnalysis;

namespace JoanComas.ScenarioUnitTesting;

public class DependencyTypeNotFoundInSutDependenciesException : Exception
{
    public DependencyTypeNotFoundInSutDependenciesException([NotNull] Type sutType, [NotNull] Type dependencyType)
        : base($"The system under test of type {sutType.FullName} does not have a dependency of type {dependencyType.FullName}.")
    {
    }
}