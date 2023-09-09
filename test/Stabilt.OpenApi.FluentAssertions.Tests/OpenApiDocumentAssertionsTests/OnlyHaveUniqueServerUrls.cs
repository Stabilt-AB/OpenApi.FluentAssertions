namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class OnlyHaveUniqueServerUrls
{
    [Fact]
    public void When_only_unique_server_urls_exists()
    {
        var document = OpenApiDocumentHelper.Create()
            .WithServer(OpenApiServerHelper.Create("https://foo.bar"))
            .WithServer(OpenApiServerHelper.Create("https://bar.foo"));

        var act = () => document.Should().OnlyHaveUniqueServerUrls();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_duplicate_server_urls_exists()
    {
        var openApiDocument = OpenApiDocumentHelper.Create()
            .WithServer(OpenApiServerHelper.Create())
            .WithServer(OpenApiServerHelper.Create("https://foo.bar"))
            .WithServer(OpenApiServerHelper.Create("https://foo.bar"));

        var act = () => openApiDocument.Should().OnlyHaveUniqueServerUrls("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected openApiDocument to only contain unique server URLs because reasons, but found multiple of {"https://foo.bar"}.""");
    }
}
