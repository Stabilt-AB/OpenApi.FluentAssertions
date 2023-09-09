using System.Net.Mime;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainRequestContentType
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithRequestBodyContent(MediaTypeNames.Application.Xml, OpenApiMediaTypeHelper.Create())
        .WithRequestBodyContent(MediaTypeNames.Application.Json, OpenApiMediaTypeHelper.Create());

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _operation.Should().ContainRequestContentType("application/json");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _operation.Should().ContainRequestContentType("image/jpeg", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain a request body with content type "image/jpeg" because reasons, but found {"application/xml", "application/json"}.""");
    }

    [Fact]
    public void When_request_body_is_null_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainRequestContentType("image/jpeg", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain a request body with content type "image/jpeg" because reasons, but found no request body.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create()
            .WithRequestBody();

        var act = () => operation.Should().ContainRequestContentType("image/jpeg", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain a request body with content type "image/jpeg" because reasons, but found no request body.""");
    }
}
