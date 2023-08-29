using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EntityFrameworkCore.AutoFixture.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
/// 
/// <para>
/// Additionally, it makes sure that any <see cref="DbContext"/> uses an in-memory databases,
/// so that you do not have to arrange all the calls to the context and you make sure no connection
/// to an actual DB is made. In future work, an IntegrationScenario will use your actual DbContext
/// to connect to an actual DB. Stay tuned!
/// </para>
/// </summary>
/// <typeparam name="TController"></typeparam>
public class ControllerScenario<TController> : Scenario<TController> where TController : ControllerBase
{
    /// <summary>
    /// This constructor makes sure that the <see cref="Scenario"/> does not mock any DbContext passed
    /// in the constructor of the SUT.
    /// </summary>
    public ControllerScenario() : base(t => !typeof(DbContext).IsAssignableFrom(t.ParameterType))
    {
        Fixture.Customize(new AutoNSubstituteCustomization());
        Fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

        // Make sure all DbContext instances use an in-memory database.
        var customization = new EntityFrameworkCore.AutoFixture.InMemory.InMemoryCustomization
        {
            DatabaseName = $"InMemory-{Guid.NewGuid()}",
            UseUniqueNames = false, // No suffix for store names. All contexts will connect to same store
        };
        Fixture.Customize(customization);
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