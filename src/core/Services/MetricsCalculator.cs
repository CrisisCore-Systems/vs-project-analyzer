using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

public class MetricsCalculator
{
    public int CalculateLinesOfCode(SyntaxNode root)
    {
        var text = root.GetText();
        return text.Lines.Count;
    }

    public int CalculateCyclomaticComplexity(SyntaxNode root)
    {
        var complexity = 1; // Base complexity

        complexity += root.DescendantNodes().OfType<IfStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<SwitchStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<ForStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<ForEachStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<WhileStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<DoStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<CatchClauseSyntax>().Count();
        complexity += root.DescendantNodes().OfType<ConditionalExpressionSyntax>().Count();

        return complexity;
    }

    public IEnumerable<CodeIssue> DetectIssues(SyntaxNode root, string filePath)
    {
        var issues = new List<CodeIssue>();

        // Detect long methods
        var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
        foreach (var method in methodDeclarations)
        {
            var methodLines = method.GetText().Lines.Count;
            if (methodLines > 50)
            {
                issues.Add(new CodeIssue
                {
                    FilePath = filePath,
                    LineNumber = method.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                    Message = $"Method '{method.Identifier}' is too long ({methodLines} lines)",
                    Severity = IssueSeverity.Medium
                });
            }
        }

        // Detect high cyclomatic complexity
        var complexityIssues = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Select(method => new
            {
                Method = method,
                Complexity = CalculateCyclomaticComplexity(method)
            })
            .Where(x => x.Complexity > 10)
            .Select(x => new CodeIssue
            {
                FilePath = filePath,
                LineNumber = x.Method.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                Message = $"Method '{x.Method.Identifier}' has high cyclomatic complexity ({x.Complexity})",
                Severity = IssueSeverity.High
            });

        issues.AddRange(complexityIssues);

        return issues;
    }
}
