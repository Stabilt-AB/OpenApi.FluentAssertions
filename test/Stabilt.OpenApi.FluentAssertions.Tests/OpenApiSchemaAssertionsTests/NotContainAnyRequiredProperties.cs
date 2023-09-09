namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class NotContainAnyRequiredProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _schema.Should().NotContainAnyRequiredProperties();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _schema
            .WithProperty("id", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
            .WithProperty("name", OpenApiSchemaHelper.CreateType("string"), true)
            .WithProperty("age", OpenApiSchemaHelper.CreateType("integer", "int32"));

        var act = () => _schema.Should().NotContainAnyRequiredProperties("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to not contain any required properties because reasons, but found {"id", "name"}.""");
    }
}
