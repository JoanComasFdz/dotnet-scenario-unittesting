using Microsoft.CodeAnalysis;

namespace JoanComas.ScenarioUnitTesting.SourceGenerator;

[Generator]
public class ScenarioGenerator : ISourceGenerator
{
    private readonly ReferencedProjectClassesFinder _referencedProjectClassesFinder;
    private readonly ScenariosBuilder _scenariosBuilder;

    public ScenarioGenerator()
    {
        this._referencedProjectClassesFinder = new ReferencedProjectClassesFinder();
        this._scenariosBuilder = new ScenariosBuilder();
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var currentProjectRootNamespace = context.GetCurrentProjectRootNamespace();
        var classesToCreateScenariosFor = _referencedProjectClassesFinder.FindClasses(context, currentProjectRootNamespace);
        var scenarios = this._scenariosBuilder
            .WithCurrentProjectName(currentProjectRootNamespace)
            .WithClasses(classesToCreateScenariosFor)
            .Build(); 
        foreach (var scenario in scenarios)
        {
            var hintName = $"{scenario.ClassName}.g.cs";
            context.AddSource(hintName, scenario.ScenarioCode);
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}