public class CodeIssue
{
    public string FilePath { get; set; }
    public int LineNumber { get; set; }
    public string Message { get; set; }
    public IssueSeverity Severity { get; set; }
}

public enum IssueSeverity
{
    Low,
    Medium,
    High
}
