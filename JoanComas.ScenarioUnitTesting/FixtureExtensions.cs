using System;
using AutoFixture;

namespace JoanComas.ScenarioUnitTesting;

/// <summary>
/// The inject methods available in the Fixture are actually extension methods
/// so it is not easy to create a generic version of them.
/// The substitutes have to be injected by the correct type, otherwise they will
/// be registered just for object.
/// The approach therefore is to create a class that simulates the Inject extension
/// methods so it can be created with the correct type. This way the Injection will
/// be done as if an Inject<T>() extension method was called.
/// </summary>
internal static class FixtureExtensions
{
    internal static IFixture InjectByType(this IFixture fixture, Type type, object instance)
    {
        var genericInjector = typeof(GenericInjector<>).MakeGenericType(type);
        var genericInjectorInstance = Activator.CreateInstance(genericInjector);
        var mi = genericInjectorInstance.GetType().GetMethod("Inject");
        mi.Invoke(genericInjectorInstance, new [] {fixture, instance});
                
        return fixture;
    }
}