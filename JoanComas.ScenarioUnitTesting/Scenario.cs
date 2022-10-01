using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture;

namespace JoanComas.ScenarioUnitTesting
{
    public class Scenario<TSut> where TSut : class
    {
        protected IFixture Fixture { get; }

        public Scenario()
        {
            Fixture = new Fixture();

            // The trick is to create the mocks for all parameters in advance
            // and register them in the Fixture.
            // This way, a test may call the When() first and then Dependency<T>() to assert without configuring
            // and it will still work.
            GetAllParametersFromAllConstructors()
                .Select(parameter => new
                {
                    parameter.ParameterType,
                    ParameterSubstitute = Substitute.For(
                        new[] { parameter.ParameterType },
                        Array.Empty<object>())
                })
                .ToList()
                .ForEach(pair => Fixture.InjectByType(pair.ParameterType, pair.ParameterSubstitute));
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

        public TDependency Dependency<TDependency>() where TDependency : class
        {
            var substitute = Fixture.Create<TDependency>();
            return substitute;
        }

        public TSut When()
        {
            var instance= Fixture.Create<TSut>();
            return instance;
        }
    }
}