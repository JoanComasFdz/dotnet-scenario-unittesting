using AutoFixture;

namespace JoanComas.ScenarioUnitTesting;

/// <summary>
/// This class provides an extension methods for the <see cref="IFixture"/> interface.
/// </summary>
internal static class FixtureExtensions
{
    /// <summary>
    ///
    /// <para>
    /// Injects the specified instance for the specified type, so that <see cref="IFixture"/>
    /// will return the specified instance when the <c>Create&lt;T&gt;()</c> method is called.
    /// </para>
    ///
    /// <para>
    /// This is useful when gathering types and creating instances during runtime (via Reflection),
    /// since usually the instance will be stored in a variable of type <see cref="object"/>.
    /// </para>
    ///
    /// </summary>
    /// <param name="fixture">The fixture instance to be extended.</param>
    /// <param name="type">The type for which the instance will be injected for.</param>
    /// <param name="instance">The actual instance to be injected.</param>
    /// <returns>The same instance of the class <see cref="Fixture"/> that was extended,
    /// to allow fluent declarations.</returns>
    internal static IFixture InjectByType(this IFixture fixture, Type type, object instance)
    {
        var genericInjector = typeof(GenericInjector<>).MakeGenericType(type);
        var genericInjectorInstance = Activator.CreateInstance(genericInjector);
        var mi = genericInjectorInstance?.GetType().GetMethod("Inject");
        mi?.Invoke(genericInjectorInstance, [fixture, instance]);

        return fixture;
    }
}