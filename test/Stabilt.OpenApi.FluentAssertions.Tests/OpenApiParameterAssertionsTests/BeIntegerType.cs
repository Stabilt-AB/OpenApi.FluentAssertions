namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeIntegerType
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_is_integer_type_Does_not_throw_exception()
    {
        _parameter.WithSchema("integer");

        var act = () => _parameter.Should().BeIntegerType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().BeIntegerType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "integer" because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().BeIntegerType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "integer" because reasons, but found "string".""");
    }
}
