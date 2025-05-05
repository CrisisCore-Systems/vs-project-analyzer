using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

public class MetricsCalculator
{
    private const int LongMethodThreshold = 50;
    private const int HighComplexityThreshold = 10;

    // Helper to count specific nodes
    private int CountNodesOfType<T>(SyntaxNode root) where T : SyntaxNode
    {
        return root.DescendantNodes().OfType<T>().Count();
    }

    public int CalculateLinesOfCode(SyntaxNode root)
    {
        return root.GetText().Lines.Count;
    }

    public int CalculateCyclomaticComplexity(SyntaxNode root)
    {
        int complexity = 1; // Base complexity
        var nodeTypes = new[]
        {
            typeof(IfStatementSyntax),
            typeof(SwitchStatementSyntax),
            typeof(ForStatementSyntax),
            typeof(ForEachStatementSyntax),
            typeof(WhileStatementSyntax),
            typeof(DoStatementSyntax),
            typeof(CatchClauseSyntax),
            typeof(ConditionalExpressionSyntax)
        };

        complexity += nodeTypes.Sum(nodeType => CountNodesOfType<SyntaxNode>(root));
        return complexity;
    }

    public IEnumerable<CodeIssue> DetectIssues(SyntaxNode root, string filePath)
    {
        var issues = new List<CodeIssue>();

        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            var methodLines = method.GetText().Lines.Count;
            if (methodLines > LongMethodThreshold)
            {
                issues.Add(new CodeIssue
                {
                    FilePath = filePath,
                    LineNumber = method.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                    Message = $"Method '{method.Identifier}' is too long ({methodLines} lines)",
                    Severity = IssueSeverity.Medium
                });
            }

            var complexity = CalculateCyclomaticComplexity(method);
            if (complexity > HighComplexityThreshold)
            {
                issues.Add(new CodeIssue
                {
                    FilePath = filePath,
                    LineNumber = method.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                    Message = $"Method '{method.Identifier}' has high cyclomatic complexity ({complexity})",
                    Severity = IssueSeverity.High
                });
            }
        }

        return issues;
    }
}
