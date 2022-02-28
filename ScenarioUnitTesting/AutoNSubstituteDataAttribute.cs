using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace ScenarioUnitTesting
{
    /// <summary>
    /// Provides mocking functionality with NSubstitue for AutoFixture in XUnit.
    /// <para>
    /// Use this attribute instead of AutoData in your unit test methods marked with [Theory] in order to
    /// user NSubstitute to automatically generate mocks of the parameters.
    /// </para>
    /// </summary>
    /// <seealso cref="AutoDataAttribute" />
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        public AutoNSubstituteDataAttribute(bool configureMembers)
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization() { ConfigureMembers = configureMembers }))
        {
        }
    }
}