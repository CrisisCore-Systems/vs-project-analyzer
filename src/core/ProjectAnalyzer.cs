using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ProjectAnalyzer.Core
{
    public class CodeAnalyzer
    {
        public async Task<AnalysisResult> AnalyzeProjectAsync(string projectPath)
        {
            // Implementation coming soon
            throw new NotImplementedException();
        }
    }

    public record AnalysisResult(
        Dictionary<string, FileMetrics> FileMetrics,
        ProjectMetrics ProjectMetrics,
        List<CodeIssue> Issues
    );
}
