namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeInt64Type
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_is_int64_type_Does_not_throw_exception()
    {
        _parameter.WithSchema("integer", "int64");

        var act = () => _parameter.Should().BeInt64Type();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().BeInt64Type("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "integer" with format "int64" because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_wrong_format_Throws_exception()
    {
        _parameter.WithSchema("integer");

        var act = () => _parameter.Should().BeInt64Type("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "integer" with format "int64" because reasons, but found format <null>.""");
    }

    [Fact]
    public void When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().BeInt64Type("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "integer" with format "int64" because reasons, but found type "string".""");
    }
}
