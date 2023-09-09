namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveReferenceTo
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_has_reference_Does_not_throw_exception()
    {
        _parameter.WithReference("ItemType");

        var act = () => _parameter.Should().HaveReferenceTo("ItemType");

        act.Should().NotThrow();
    }

    [Fact]
    public void HaveReferenceTo_WhenReferenceIsNull_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().HaveReferenceTo("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema reference "Foo" because reasons, but the schema reference was <null>.""");
    }

    [Fact]
    public void HaveReferenceTo_WhenReferenceIsWrong_Throws_exception()
    {
        _parameter.WithReference("ItemType");

        var act = () => _parameter.Should().HaveReferenceTo("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema reference "Foo" because reasons, but found "ItemType".""");
    }

    [Fact]
    public void HaveReferenceTo_WhenSchemaIsNull_Throws_exception()
    {
        var act = () => _parameter.Should().HaveReferenceTo("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema reference "Foo" because reasons, but the schema was <null>.""");
    }
}
