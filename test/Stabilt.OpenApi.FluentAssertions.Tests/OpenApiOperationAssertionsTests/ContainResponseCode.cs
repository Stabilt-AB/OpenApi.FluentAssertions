using System.Net;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public static class ContainResponseCode
{
    public class WhenResponsesHasResponseCodes
    {
        private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
            .WithResponseCode(200, OpenApiResponseHelper.Create())
            .WithResponseCode(404, OpenApiResponseHelper.Create());

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotFound)]
        public void With_enum_When_contains_Does_not_throw_exception(HttpStatusCode statusCode)
        {
            var act = () => _operation.Should().ContainResponseCode(statusCode);

            act.Should().NotThrow();
        }

        [Fact]
        public void With_enum_When_not_contains_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode(HttpStatusCode.IMUsed, "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "226" because reasons, but found {"200", "404"}.""");
        }

        [Theory]
        [InlineData(200)]
        [InlineData(404)]
        public void With_integer_When_contains_Does_not_throw_exception(Int32 statusCode)
        {
            var act = () => _operation.Should().ContainResponseCode(statusCode);

            act.Should().NotThrow();
        }

        [Fact]
        public void With_integer_When_not_contains_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode(226, "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "226" because reasons, but found {"200", "404"}.""");
        }

        [Theory]
        [InlineData("200")]
        [InlineData("404")]
        public void With_string_When_contains_Does_not_throw_exception(String statusCode)
        {
            var act = () => _operation.Should().ContainResponseCode(statusCode);

            act.Should().NotThrow();
        }

        [Fact]
        public void With_string_When_not_contains_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode("226", "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "226" because reasons, but found {"200", "404"}.""");
        }
    }

    public class WhenResponsesIsNull
    {
        private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

        [Fact]
        public void With_enum_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode(HttpStatusCode.OK, "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "200" because reasons, but found no responses.""");
        }

        [Fact]
        public void With_integer_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode(200, "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "200" because reasons, but found no responses.""");
        }

        [Fact]
        public void With_string_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode("200", "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "200" because reasons, but found no responses.""");
        }
    }

    public class WhenResponsesIsEmpty
    {
        private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
            .WithResponses();

        [Fact]
        public void With_enum_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode(HttpStatusCode.OK, "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "200" because reasons, but found no responses.""");
        }

        [Fact]
        public void With_integer_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode(200, "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "200" because reasons, but found no responses.""");
        }

        [Fact]
        public void With_string_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCode("200", "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain response with status "200" because reasons, but found no responses.""");
        }
    }
}
