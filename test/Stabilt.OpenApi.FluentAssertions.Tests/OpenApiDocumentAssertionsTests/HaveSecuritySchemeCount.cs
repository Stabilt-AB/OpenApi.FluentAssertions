namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveSecuritySchemeCount
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSecuritySchemeComponent("bearer-1")
        .WithSecuritySchemeComponent("bearer-2");

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveSecuritySchemeCount(2);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveSecuritySchemeCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have 3 security schemes because reasons, but found 2: {"bearer-1", "bearer-2"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveSecuritySchemeCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveSecuritySchemeCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected document to have 3 security schemes because reasons, but found no security schemes.");
    }
}
