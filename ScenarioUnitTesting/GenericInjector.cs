using System;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;

namespace ScenarioUnitTesting;

/// <summary>
/// This class was created after inspecting the source code of the Inject extension methods.
/// It adds the generic type to the class definition, so th at the correct generic type
/// can be made and instantiated via reflection.
/// </summary>
/// <typeparam name="T">The type of the parameter to be injected</typeparam>
internal class GenericInjector<T>
{
    // ReSharper disable once UnusedMember.Global Reason: Called via reflection.
#pragma warning disable CA1822 // Mark members as static
    public void Inject(IFixture fixture, T instance)
#pragma warning restore CA1822 // Mark members as static
    {
        fixture.Customize<T>((Func<ICustomizationComposer<T>, ISpecimenBuilder>)
            (c => (ISpecimenBuilder)
                c.FromFactory(() => instance).OmitAutoProperties()));
    }
}