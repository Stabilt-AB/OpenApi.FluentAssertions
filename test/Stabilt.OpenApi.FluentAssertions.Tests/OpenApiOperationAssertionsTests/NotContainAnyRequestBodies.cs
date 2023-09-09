using System.Net.Mime;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class NotContainAnyRequestBodies
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _operation.Should().NotContainAnyRequestBodies();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _operation
            .WithRequestBodyContent(MediaTypeNames.Application.Json, OpenApiMediaTypeHelper.Create());

        var act = () => _operation.Should().NotContainAnyRequestBodies("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to not contain any request bodies because reasons, but found {"application/json"}.""");
    }
}
