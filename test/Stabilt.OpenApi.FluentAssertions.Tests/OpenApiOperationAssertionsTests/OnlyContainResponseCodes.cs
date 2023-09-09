using System.Net;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class OnlyContainResponseCodes
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithResponseCode(200, OpenApiResponseHelper.Create())
        .WithResponseCode(401, OpenApiResponseHelper.Create())
        .WithResponseCode(404, OpenApiResponseHelper.Create());

    [Theory]
    [InlineData(HttpStatusCode.NotFound, HttpStatusCode.OK, HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.NotFound, HttpStatusCode.Unauthorized, HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.OK, HttpStatusCode.Unauthorized, HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.NotFound, HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.OK, HttpStatusCode.NotFound)]
    public void With_enum_When_contains_only_expected_Does_not_throw_exception(params HttpStatusCode[] responseCodes)
    {
        var act = () => _operation.Should().OnlyContainResponseCodes(responseCodes);

        act.Should().NotThrow();
    }

    [Fact]
    public void With_enum_When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainResponseCodes([HttpStatusCode.IMUsed, HttpStatusCode.OK], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain responses with statuses {"226", "200"} because reasons, but found {"200", "401", "404"}.""");
    }

    [Fact]
    public void With_enum_When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainResponseCodes([HttpStatusCode.OK, HttpStatusCode.NotFound], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain responses with statuses {"200", "404"} because reasons, but found no responses.""");
    }

    [Fact]
    public void With_enum_When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainResponseCodes(Array.Empty<HttpStatusCode>(), "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(200, 401, 404)]
    [InlineData(200, 404, 401)]
    [InlineData(401, 200, 404)]
    [InlineData(401, 404, 200)]
    [InlineData(404, 200, 401)]
    [InlineData(404, 401, 200)]
    public void With_integer(params Int32[] responseCodes)
    {
        var act = () => _operation.Should().OnlyContainResponseCodes(responseCodes);

        act.Should().NotThrow();
    }

    [Fact]
    public void With_integer_When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainResponseCodes([418, 200], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain responses with statuses {"418", "200"} because reasons, but found {"200", "401", "404"}.""");
    }

    [Fact]
    public void With_integer_When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainResponseCodes([200, 404], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain responses with statuses {"200", "404"} because reasons, but found no responses.""");
    }

    [Fact]
    public void With_integer_When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainResponseCodes(Array.Empty<Int32>(), "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData("200", "401", "404")]
    [InlineData("200", "404", "401")]
    [InlineData("401", "200", "404")]
    [InlineData("401", "404", "200")]
    [InlineData("404", "200", "401")]
    [InlineData("404", "401", "200")]
    public void With_string(params String[] responseCodes)
    {
        var act = () => _operation.Should().OnlyContainResponseCodes(responseCodes);

        act.Should().NotThrow();
    }

    [Fact]
    public void With_string_When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainResponseCodes(["418", "200"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain responses with statuses {"418", "200"} because reasons, but found {"200", "401", "404"}.""");
    }

    [Fact]
    public void With_string_When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainResponseCodes(["200", "404"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain responses with statuses {"200", "404"} because reasons, but found no responses.""");
    }

    [Fact]
    public void With_string_When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainResponseCodes(Array.Empty<String>(), "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
