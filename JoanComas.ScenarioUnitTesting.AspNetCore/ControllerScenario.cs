using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JoanComas.ScenarioUnitTesting.AspNetCore
{
    public class ControllerScenario<TSut> : Scenario<TSut> where TSut : class
    {
        public ControllerScenario()
        {
            Fixture.Customize(new AutoNSubstituteCustomization());
            Fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
        }

        public ControllerContext ControllerContext()
        {
            return Fixture.Create<ControllerContext>();
        }
    }
}