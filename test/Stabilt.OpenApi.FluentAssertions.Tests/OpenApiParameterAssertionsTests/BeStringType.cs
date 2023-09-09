﻿namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeStringType
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_is_string_type_Does_not_throw_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().BeStringType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().BeStringType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("number");

        var act = () => _parameter.Should().BeStringType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" because reasons, but found "number".""");
    }
}