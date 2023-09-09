﻿namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeIn
{
    [Theory]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Path)]
    [InlineData(ParameterLocation.Query)]
    public void When_locations_is_as_expected_Does_not_throw_exception(ParameterLocation location)
    {
        var parameter = OpenApiParameterHelper.Create(location: location);

        var act = () => parameter.Should().BeIn(location);

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Query, ParameterLocation.Header)]
    [InlineData(ParameterLocation.Query, ParameterLocation.Path)]
    [InlineData(ParameterLocation.Query, ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Header, ParameterLocation.Query)]
    [InlineData(ParameterLocation.Header, ParameterLocation.Path)]
    [InlineData(ParameterLocation.Header, ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Path, ParameterLocation.Query)]
    [InlineData(ParameterLocation.Path, ParameterLocation.Header)]
    [InlineData(ParameterLocation.Path, ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Cookie, ParameterLocation.Query)]
    [InlineData(ParameterLocation.Cookie, ParameterLocation.Header)]
    [InlineData(ParameterLocation.Cookie, ParameterLocation.Path)]
    public void When_location_is_not_as_expected_Throws_exception(ParameterLocation expected, ParameterLocation actual)
    {
        var parameter = OpenApiParameterHelper.Create(location: actual);

        var act = () => parameter.Should().BeIn(expected, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected parameter to be in "{expected}" because reasons, but found "{actual}".""");
    }
}