using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JoanComas.ScenarioUnitTesting.AspNetCore;

/// <summary>
/// Extends the <see cref="Scenario{TSut}"/> class so that it can handle Asp .Net Core's <see cref="ControllerBase"/> classes
/// and provides tools too access particular properties of a <see cref="ControllerBase"/>.
/// 
/// <para>
/// This is necessary because when AutoFixture creates an instance of a <c>Controller</c>, it will not be able to create a
/// substitute for the property <see cref="BindingInfo"/>. This class provides a customization to avoid the issue.
/// </para>
///
/// <para>
/// Source: https://github.com/AutoFixture/AutoFixture/issues/1141
/// </para>
/// </summary>
/// <typeparam name="TController"></typeparam>
public class ControllerScenario<TController> : Scenario<TController> where TController : ControllerBase
{
    public ControllerScenario()
    {
        Fixture.Customize(new AutoNSubstituteCustomization());
        Fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
    }

    /// <summary>
    /// Returns the <see cref="ControllerContext"/> property.
    /// This way, the tester does not have to call <c>When()</c> in order to access it.
    /// </summary>
    /// <returns>The substitute used in the controller under test's.</returns>
    public ControllerContext ControllerContext()
    {
        return Fixture.Create<ControllerContext>();
    }
}