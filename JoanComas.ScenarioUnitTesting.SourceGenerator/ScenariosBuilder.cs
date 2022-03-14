using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace JoanComas.ScenarioUnitTesting.SourceGenerator;

internal class ScenariosBuilder
{
    private readonly List<ITypeSymbol> _classes;
    private string _currentProjectName;

    public ScenariosBuilder()
    {
        _classes = new List<ITypeSymbol>();
        _currentProjectName = string.Empty;
    }

    public ScenariosBuilder WithCurrentProjectName(string currentProjectName)
    {
        this._currentProjectName = currentProjectName;
        return this;
    }

    public ScenariosBuilder WithClasses(IReadOnlyCollection<ITypeSymbol> classes)
    {
        this._classes.AddRange(classes);
        return this;
    }

    public IReadOnlyCollection<ScenarioSourceInfo> Build()
    {
        var scenarios = this._classes
            .Select(sut => CreateScenarioSource(this._currentProjectName, sut))
            .ToArray();
        return scenarios;
    }

    private static ScenarioSourceInfo CreateScenarioSource(string currentProjectName, ISymbol sut)
    {
        var sutName = sut.Name;
        var sutNamespace = GetFullNamespace(sut.ContainingSymbol);
        var scenarioSource = $@"// Auto-generated code with ScenarioUnitTesting.SourceGenerator at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}

using JoanComas.ScenarioUnitTesting;

namespace {currentProjectName}
{{
    public class {sutName}Scenario : Scenario<{sutNamespace}.{sutName}>
    {{
        public {sutNamespace}.{sutName} {sutName} {{ get {{ return base.When(); }} }}
    }}
}}
";
        return new ScenarioSourceInfo($"{sutName}Scenario", scenarioSource);
    }

    /// <summary>
    /// Source: https://stackoverflow.com/questions/27105909/get-fully-qualified-metadata-name-in-roslyn
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private static string GetFullNamespace(ISymbol? s)
    {
        if (s == null || IsRootNamespace(s))
        {
            return string.Empty;
        }

        var sb = new StringBuilder(s.MetadataName);
        var last = s;

        s = s.ContainingSymbol;

        while (!IsRootNamespace(s))
        {
            if (s is ITypeSymbol && last is ITypeSymbol)
            {
                sb.Insert(0, '+');
            }
            else
            {
                sb.Insert(0, '.');
            }

            sb.Insert(0, s.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
            s = s.ContainingSymbol;
        }

        return sb.ToString();
    }

    private static bool IsRootNamespace(ISymbol? symbol)
    {
        return symbol is INamespaceSymbol {IsGlobalNamespace : true};
    }
}