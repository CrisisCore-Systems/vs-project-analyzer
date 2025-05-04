using System;
using System.ComponentModel;

/// <summary>
/// Represents a single issue detected during code analysis.
/// Provides insights into the file, location, and severity of the issue.
/// </summary>
public class CodeIssue
{
    private string _filePath;

    /// <summary>
    /// The path to the file where the issue was detected.
    /// </summary>
    public string FilePath
    {
        get => _filePath;
        set => _filePath = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("FilePath cannot be null or empty.")
            : value;
    }

    /// <summary>
    /// The line number in the file where the issue was detected.
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// The message describing the issue.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// The severity level of the issue.
    /// </summary>
    public IssueSeverity Severity { get; set; }

    /// <summary>
    /// The identifier for the rule that caused the issue.
    /// </summary>
    public string RuleId { get; set; }

    /// <summary>
    /// The category of the issue (e.g., "CodeStyle", "Performance").
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Automated suggestions for resolving the issue.
    /// </summary>
    public string Suggestions { get; set; }

    /// <summary>
    /// Factory method for creating a high-severity CodeIssue.
    /// </summary>
    public static CodeIssue CreateHighSeverityIssue(string filePath, int lineNumber, string message) =>
        new CodeIssue
        {
            FilePath = filePath,
            LineNumber = lineNumber,
            Message = message,
            Severity = IssueSeverity.High
        };
}

/// <summary>
/// Defines the severity levels of a code issue.
/// </summary>
public enum IssueSeverity
{
    [Description("Low impact issue.")]
    Low,
    [Description("Medium impact issue.")]
    Medium,
    [Description("High impact issue.")]
    High
}
