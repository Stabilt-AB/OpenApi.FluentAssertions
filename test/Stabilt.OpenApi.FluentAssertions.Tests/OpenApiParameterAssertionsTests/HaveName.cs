namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveName
{
    [Fact]
    public void When_parameter_has_name_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create("testParameter");

        var act = () => parameter.Should().HaveName("testParameter");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_name_is_wrong_Throws_exception()
    {
        var parameter = OpenApiParameterHelper.Create("testParameter");

        var act = () => parameter.Should().HaveName("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected parameter to have name "foo" because reasons, but found "testParameter".""");
    }
}
