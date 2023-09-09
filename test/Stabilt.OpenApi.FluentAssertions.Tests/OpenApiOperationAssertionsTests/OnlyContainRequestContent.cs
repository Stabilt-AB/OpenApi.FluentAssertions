using System.Net.Mime;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class OnlyContainRequestContent
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithRequestBodyContent(MediaTypeNames.Application.Xml, OpenApiMediaTypeHelper.Create())
        .WithRequestBodyContent(MediaTypeNames.Application.Json, OpenApiMediaTypeHelper.Create());

    [Theory]
    [InlineData("application/xml", "application/json")]
    [InlineData("application/json", "application/xml")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] parameters)
    {
        var act = () => _operation.Should().OnlyContainRequestContentTypes(parameters);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainRequestContentTypes(["text/html"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain request body content types {"text/html"} because reasons, but found {"application/xml", "application/json"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainRequestContentTypes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainRequestContentTypes(["text/html"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain request body content types {"text/html"} because reasons, but found no request body.""");
    }
}
