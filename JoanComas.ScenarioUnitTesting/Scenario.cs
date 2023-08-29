﻿using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture;

namespace JoanComas.ScenarioUnitTesting;

/// <summary>
/// This class provides all tools necessary to test a class, which include:
/// <list type="bullet">
///     <item>
///         <description>
///             Automatically mocks all the system under test's dependencies, creating substitutes for all injected parameters from all its constructors.
///         </description>
///     </item>
///     <item>
///         <description>
///             Automatically instantiates the system under test with the mocked dependencies via the <c>When()</c> method.
///         </description>
///     </item>
///     <item>
///         <description>
///             Single access point for all dependencies via the <c>Dependency&lt;T&gt;()</c> method.
///         </description>
///     </item>
/// </list>
/// </summary>
/// <typeparam name="TSut">The type of the system under test.</typeparam>
public class Scenario<TSut> where TSut : class
{
    private readonly HashSet<Type> _substitutedDependencies;
    private TSut _systemUnderTest;

    /// <summary>
    /// Use this fixture to add any and all additional customizations in subtypes.
    /// </summary>
    protected IFixture Fixture { get; }

    /// <summary>
    /// This is the default constructor. It will mock all the constructor parameters of your System Under Test.
    /// </summary>
    public Scenario() : this(t => true)
    {
    }

    /// <summary>
    /// If certain types used in your SUT's constructor are not to be instantiated, create a sub type and call this constructor,
    /// and use the lambda to return <c>false</c> for the types not to be mocked.
    /// </summary>
    /// <param name="fakeConstructorParameter"></param>
    public Scenario(Func<ParameterInfo, bool> fakeConstructorParameter)
    {
        _substitutedDependencies = new HashSet<Type>();
        Fixture = new Fixture();

        // The approach is to create the mocks for all parameters in advance
        // and inject them in the Fixture.
        // This way, a test may call the When() first and then Dependency<T>()
        // to assert without configuring and it will still work.
        GetAllParametersFromAllConstructors()
            .Select(parameter => new
            {
                parameter.ParameterType,
                TypeToInject = fakeConstructorParameter(parameter)
                ? Substitute.For(new[] { parameter.ParameterType }, Array.Empty<object>())
                : parameter.ParameterType,
                IsFaked = fakeConstructorParameter(parameter)
            })
            .ToList()
            .ForEach(info =>
            {
                if (info.IsFaked)
                {
                    Fixture.InjectByType(info.ParameterType, info.TypeToInject);
                }
                _substitutedDependencies.Add(info.ParameterType);
            });
    }

    private static IEnumerable<ParameterInfo> GetAllParametersFromAllConstructors()
    {
        var parameters1 = typeof(TSut)
            .GetConstructors()
            .SelectMany(c => c.GetParameters())
            .Distinct()
            .ToArray();
        return parameters1;
    }

    /// <summary>
    /// Returns the dependency of the specified type.
    /// </summary>
    /// <typeparam name="TDependency">The type of dependency to be returned.</typeparam>
    /// <returns>The existing instance of the substitute for the specified dependency type.</returns>
    public TDependency Dependency<TDependency>() where TDependency : class
    {
        if (!_substitutedDependencies.Contains(typeof(TDependency)))
        {
            throw new DependencyTypeNotFoundInSutDependenciesException(typeof(TSut), typeof(TDependency));
        }

        var substitute = Fixture.Create<TDependency>();
        return substitute;
    }

    /// <summary>
    /// Returns the system under test.
    /// </summary>
    /// <returns>The instance of the system under test with all its dependencies mocked.</returns>
    public TSut When()
    {
        return _systemUnderTest ??= Fixture.Create<TSut>();
    }
}
