using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

/// <summary>
/// Represents the culmination of an analysis, weaving together metrics, issues, and insights.
/// </summary>
public class AnalysisResult
{
    /// <summary>
    /// A map of file paths to their respective metrics.
    /// </summary>
    public IReadOnlyDictionary<string, FileMetrics> FileMetrics { get; }

    /// <summary>
    /// Metrics summarizing the entire project.
    /// </summary>
    public ProjectMetrics ProjectMetrics { get; }

    /// <summary>
    /// A collection of issues detected during the analysis.
    /// </summary>
    public IReadOnlyList<CodeIssue> Issues { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnalysisResult"/> class.
    /// </summary>
    /// <param name="fileMetrics">Metrics associated with individual files.</param>
    /// <param name="projectMetrics">The overarching project metrics.</param>
    /// <param name="issues">A list of detected code issues.</param>
    /// <exception cref="ArgumentNullException">Thrown when any input is null.</exception>
    public AnalysisResult(
        IDictionary<string, FileMetrics> fileMetrics,
        ProjectMetrics projectMetrics,
        IList<CodeIssue> issues)
    {
        FileMetrics = new Dictionary<string, FileMetrics>(
            fileMetrics ?? throw new ArgumentNullException(nameof(fileMetrics))
        );

        ProjectMetrics = projectMetrics ?? 
            throw new ArgumentNullException(nameof(projectMetrics));

        Issues = new List<CodeIssue>(
            issues ?? throw new ArgumentNullException(nameof(issues))
        );
    }

    /// <summary>
    /// Generates a narrative summary of the analysis metrics and issues.
    /// </summary>
    /// <returns>A string summarizing the analysis.</returns>
    public string SummarizeMetrics()
    {
        return $"Project Analysis:\n" +
               $"- Total Files Analyzed: {FileMetrics.Count}\n" +
               $"- Total Issues Detected: {Issues.Count}\n";
    }

    /// <summary>
    /// Adds a new issue to the collection. This method mirrors the ever-expanding nature of analysis.
    /// </summary>
    /// <param name="issue">The code issue to add.</param>
    public void AddIssue(CodeIssue issue)
    {
        ((List<CodeIssue>)Issues).Add(issue);
    }

    /// <summary>
    /// Retrieves metrics for a specific file, if it exists.
    /// </summary>
    /// <param name="filePath">The path of the file to retrieve metrics for.</param>
    /// <returns>The file metrics if found; otherwise, null.</returns>
    public FileMetrics? GetFileMetrics(string filePath)
    {
        return FileMetrics.TryGetValue(filePath, out var metrics) ? metrics : null;
    }
}
