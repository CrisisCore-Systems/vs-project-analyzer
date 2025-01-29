using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading.Tasks;

[TestClass]
public class MetricsCalculatorTests
{
    [TestMethod]
    public async Task CalculateLinesOfCode_ShouldReturnCorrectCount() 
    {
        var calculator = new MetricsCalculator();
        var syntaxTree = CSharpSyntaxTree.ParseText("class Test { }");
        var result = calculator.CalculateLinesOfCode(await syntaxTree.GetRootAsync());
        Assert.AreEqual(1, result);
    }
}
