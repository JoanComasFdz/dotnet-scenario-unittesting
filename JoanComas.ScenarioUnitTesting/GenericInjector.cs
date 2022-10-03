using AutoFixture;

namespace JoanComas.ScenarioUnitTesting;

/// <summary>
/// <para>
/// This class was created after inspecting the source code of the <c>Inject&lt;T&gt;()</c>
/// extension methods for the <see cref="IFixture"/> interface.
/// </para>
/// 
/// It adds the generic type to the class definition, so th at the correct generic type
/// can be made and instantiated via reflection.
/// </summary>
/// <typeparam name="T">The type of the parameter to be injected</typeparam>
// ReSharper disable once UnusedMember.Global Reason: Called via reflection.
#pragma warning disable CA1812 // Called via reflection
internal class GenericInjector<T>
#pragma warning restore CA1812
{
    /// <summary>
    /// It customizes the specified <see cref="IFixture"/> to return the specified instance of type <see cref="T"/>.
    /// </summary>
    /// <param name="fixture"></param>
    /// <param name="instance"></param>
    // ReSharper disable once UnusedMember.Global Reason: Called via reflection.
#pragma warning disable CA1822 // Mark members as static
    public void Inject(IFixture fixture, T instance)
#pragma warning restore CA1822 // Mark members as static
    {
        fixture.Customize<T>(c => c.FromFactory(() => instance).OmitAutoProperties());
    }
}