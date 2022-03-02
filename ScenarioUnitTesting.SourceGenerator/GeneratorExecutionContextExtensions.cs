using Microsoft.CodeAnalysis;

namespace ScenarioUnitTesting.SourceGenerator;

internal static class GeneratorExecutionContextExtensions
{
    public static string GetCurrentProjectRootNamespace(this GeneratorExecutionContext context)
    {
        var compilationSourceModule = context.Compilation.SourceModule;
        var currentProjectName = compilationSourceModule.Name
            .Replace(".dll", string.Empty);
        return currentProjectName;
    }
}