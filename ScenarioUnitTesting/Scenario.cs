using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScenarioUnitTesting
{
    public class Scenario<TSut> where TSut : class
    {
        private readonly Dictionary<Type, object> _dictionaryOfDependencies = new();

        public Scenario()
        {
            // Preemptively generate all the mocks.
            foreach (var parameter in GetConstructorDependencies())
            {
                var substitute = Substitute.For(new[] { parameter.ParameterType }, Array.Empty<object>());
                _dictionaryOfDependencies.Add(parameter.ParameterType, substitute);
            }
        }

        private static ParameterInfo[] GetConstructorDependencies()
        {
            var sutConstructors = typeof(TSut).GetConstructors();
            if (sutConstructors.Length != 1)
            {
                throw new Exception($"The Scenario class can only work with SUTs that have only one constructor, but the sut {typeof(TSut).Name} contains {sutConstructors.Length} constructors");
            }

            var constructor = sutConstructors.Single();
            return constructor.GetParameters();
        }

        public TDependency Dependency<TDependency>()
        {
            if (!_dictionaryOfDependencies.ContainsKey(typeof(TDependency)))
            {
                throw new Exception($"The {typeof(TSut).Name}'s constructor does not contain a parameter of type {typeof(TDependency).FullName}.");
            }

            return (TDependency)_dictionaryOfDependencies[typeof(TDependency)];
        }

        public TSut When()
        {
            var constructorInfo = typeof(TSut).GetConstructors().Single();

            var instance = (TSut)constructorInfo.Invoke(_dictionaryOfDependencies.Values.ToArray());

            return instance;
        }
    }
}