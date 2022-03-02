namespace ScenarioUnitTesting.SourceGenerator;

internal struct ScenarioSourceInfo
{
    public string ClassName { get; }
    public string ScenarioCode { get; }

    public ScenarioSourceInfo(string className, string scenarioCode)
    {
        ClassName = className;
        ScenarioCode = scenarioCode;
    }
}