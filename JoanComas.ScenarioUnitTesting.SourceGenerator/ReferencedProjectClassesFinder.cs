using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ScenarioUnitTesting.SourceGenerator;

internal class ReferencedProjectClassesFinder
{
    /// <summary>
    /// <para>
    /// Analyzes the referenced assemblies, discarding the system ones, and looks for classes
    /// </para>
    /// <para>
    /// Source: https://stackoverflow.com/questions/68732040/how-to-access-project-referenced-assemblies-in-source-generator
    /// </para>
    /// </summary>
    /// <param name="context">The context instance coming from the Execute method of a source generator.</param>
    /// <returns>The set of classes for which scenarios will be created.</returns>
    internal IReadOnlyCollection<ITypeSymbol> FindClasses(GeneratorExecutionContext context, string currenTesttProjectName)
    {
        var referencedProject = currenTesttProjectName.Replace(".Tests", string.Empty);
        var assemblySymbols = context.Compilation.SourceModule.ReferencedAssemblySymbols
            //.Where(ass => !ass.Name.ToLower().StartsWith("system"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("microsoft"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("xunit"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("windows"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("nuget"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("scenariounittesting"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("autofixture"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("nsubstitute"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("mscorlib"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("fare"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("fluentassertions"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("newtonsoft"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("testhost"))
            //.Where(ass => !ass.Name.ToLower().StartsWith("castle."))
            //.Where(ass => !ass.Name.ToLower().StartsWith("netstandard"))
            .Where(ass => ass.Name.StartsWith(referencedProject))
            .ToArray();

        var types = assemblySymbols
            .SelectMany(a =>
            {
                try
                {
                    var strings = a.Identity.Name.Split('.');
                    var main = strings
                        .Aggregate(a.GlobalNamespace, (s, c) => s.GetNamespaceMembers().Single(m => m.Name.Equals(c)));

                    return GetAllTypesRecursively(main);
                }
                catch
                {
                    return Enumerable.Empty<ITypeSymbol>();
                }
            })
            .ToArray();

        var classTypes = types.Where(t => t.TypeKind == TypeKind.Class)
            .Where(t => t.IsRecord == false)
            .ToArray();

        return classTypes;
    }

    private static IEnumerable<ITypeSymbol> GetAllTypesRecursively(INamespaceSymbol root)
    {
        foreach (var namespaceOrTypeSymbol in root.GetMembers())
        {
            if (namespaceOrTypeSymbol is INamespaceSymbol @namespace)
            {
                foreach (var nested in GetAllTypesRecursively(@namespace))
                {
                    yield return nested;
                }
            }
            else if (namespaceOrTypeSymbol is ITypeSymbol type)
            {
                yield return type;
            }
        }
    }
}