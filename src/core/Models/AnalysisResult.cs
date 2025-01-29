using System.Collections.Generic;

public class AnalysisResult
{
    public Dictionary<string, FileMetrics> FileMetrics { get; }
    public ProjectMetrics ProjectMetrics { get; }
    public List<CodeIssue> Issues { get; }

    public AnalysisResult(Dictionary<string, FileMetrics> fileMetrics, ProjectMetrics projectMetrics, List<CodeIssue> issues)
    {
        FileMetrics = fileMetrics;
        ProjectMetrics = projectMetrics;
        Issues = issues;
    }
}
