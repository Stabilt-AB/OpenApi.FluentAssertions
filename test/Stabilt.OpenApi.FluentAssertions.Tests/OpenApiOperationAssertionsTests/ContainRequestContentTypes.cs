using System.Net.Mime;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainRequestContentTypes
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithRequestBodyContent(MediaTypeNames.Application.Pdf, OpenApiMediaTypeHelper.Create())
        .WithRequestBodyContent(MediaTypeNames.Application.Xml, OpenApiMediaTypeHelper.Create())
        .WithRequestBodyContent(MediaTypeNames.Application.Json, OpenApiMediaTypeHelper.Create());

    [Theory]
    [InlineData("application/pdf")]
    [InlineData("application/pdf", "application/xml")]
    [InlineData("application/pdf", "application/xml", "application/json")]
    [InlineData("application/pdf", "application/json")]
    [InlineData("application/pdf", "application/json", "application/xml")]
    [InlineData("application/xml")]
    [InlineData("application/xml", "application/pdf")]
    [InlineData("application/xml", "application/pdf", "application/json")]
    [InlineData("application/xml", "application/json")]
    [InlineData("application/xml", "application/json", "application/pdf")]
    [InlineData("application/json")]
    [InlineData("application/json", "application/pdf")]
    [InlineData("application/json", "application/pdf", "application/xml")]
    [InlineData("application/json", "application/xml")]
    [InlineData("application/json", "application/xml", "application/pdf")]
    public void When_contains_all_Does_not_throw_exception(params string[] expectedValues)
    {
        var act = () => _operation.Should().ContainRequestContentTypes(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _operation.Should().ContainRequestContentTypes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain request body content types {"foo", "bar"} because reasons, but found {"application/pdf", "application/xml", "application/json"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _operation.Should().ContainRequestContentTypes(["foo", "application/pdf"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain request body content types {"foo", "application/pdf"} because reasons, but found {"application/pdf", "application/xml", "application/json"}.""");
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainRequestContentTypes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_request_body_is_null_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainRequestContentTypes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain request body content types {"foo", "bar"} because reasons, but found no request body.""");
    }

    [Fact]
    public void When_empty_and_asserting_for_none_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create()
            .WithRequestBody();

        var act = () => operation.Should().ContainRequestContentTypes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain request body content types {"foo", "bar"} because reasons, but found no request body.""");
    }
}
