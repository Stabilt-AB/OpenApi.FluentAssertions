using System.Net;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public static class ContainResponseCodes
{
    public class WhenResponsesHasResponseCodes
    {
        private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
            .WithResponseCode(401, OpenApiResponseHelper.Create())
            .WithResponseCode(200, OpenApiResponseHelper.Create())
            .WithResponseCode(404, OpenApiResponseHelper.Create());

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.OK, HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.OK, HttpStatusCode.Unauthorized, HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.OK, HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized, HttpStatusCode.NotFound, HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.NotFound, HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotFound, HttpStatusCode.OK, HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.NotFound, HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.NotFound, HttpStatusCode.Unauthorized, HttpStatusCode.OK)]
        public void With_enum_When_contains_all_Does_not_throw_exception(params HttpStatusCode[] responseCodes)
        {
            var act = () => _operation.Should().ContainResponseCodes(responseCodes);

            act.Should().NotThrow();
        }

        [Fact]
        public void With_enum_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<HttpStatusCode>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_enum_When_not_contains_any_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([HttpStatusCode.IMUsed, HttpStatusCode.ServiceUnavailable], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found {"401", "200", "404"}.""");
        }

        [Fact]
        public void With_enum_When_contains_some_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([HttpStatusCode.IMUsed, HttpStatusCode.Unauthorized], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "401"} because reasons, but found {"401", "200", "404"}.""");
        }

        [Theory]
        [InlineData(200)]
        [InlineData(200, 401)]
        [InlineData(200, 401, 404)]
        [InlineData(200, 404)]
        [InlineData(200, 404, 401)]
        [InlineData(401)]
        [InlineData(401, 200)]
        [InlineData(401, 200, 404)]
        [InlineData(401, 404)]
        [InlineData(401, 404, 200)]
        [InlineData(404)]
        [InlineData(404, 200)]
        [InlineData(404, 200, 401)]
        [InlineData(404, 401)]
        [InlineData(404, 401, 200)]
        public void With_integer_When_contains_all_Does_not_throw_exception(params Int32[] responseCodes)
        {
            var act = () => _operation.Should().ContainResponseCodes(responseCodes);

            act.Should().NotThrow();
        }

        [Fact]
        public void With_integer_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<Int32>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_integer_When_not_contains_any_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([226, 503], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found {"401", "200", "404"}.""");
        }

        [Fact]
        public void With_integer_When_contains_some_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([226, 401], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "401"} because reasons, but found {"401", "200", "404"}.""");
        }

        [Theory]
        [InlineData("200")]
        [InlineData("200", "401")]
        [InlineData("200", "401", "404")]
        [InlineData("200", "404")]
        [InlineData("200", "404", "401")]
        [InlineData("401")]
        [InlineData("401", "200")]
        [InlineData("401", "200", "404")]
        [InlineData("401", "404")]
        [InlineData("401", "404", "200")]
        [InlineData("404")]
        [InlineData("404", "200")]
        [InlineData("404", "200", "401")]
        [InlineData("404", "401")]
        [InlineData("404", "401", "200")]
        public void With_string_When_contains_all_Does_not_throw_exception(params String[] responseCodes)
        {
            var act = () => _operation.Should().ContainResponseCodes(responseCodes);

            act.Should().NotThrow();
        }

        [Fact]
        public void With_string_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<String>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_string_When_not_contains_any_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(["226", "503"], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found {"401", "200", "404"}.""");
        }

        [Fact]
        public void With_stringWhen_contains_some_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(["226", "401"], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "401"} because reasons, but found {"401", "200", "404"}.""");
        }
    }

    public class WhenResponsesIsEmpty
    {
        private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
            .WithResponses();

        [Fact]
        public void With_enum_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<HttpStatusCode>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_enum_When_empty_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([HttpStatusCode.IMUsed, HttpStatusCode.ServiceUnavailable], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found no responses.""");
        }

        [Fact]
        public void With_integer_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<Int32>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_integer_When_empty_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([226, 503], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found no responses.""");
        }

        [Fact]
        public void With_string_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<String>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_string_When_empty_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(["226", "503"], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found no responses.""");
        }
    }

    public class WhenResponsesIsNull
    {
        private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

        [Fact]
        public void With_enum_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<HttpStatusCode>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_enum_When_empty_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([HttpStatusCode.IMUsed, HttpStatusCode.ServiceUnavailable], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found no responses.""");
        }

        [Fact]
        public void With_integer_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<Int32>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_integer_When_empty_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes([226, 503], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found no responses.""");
        }

        [Fact]
        public void With_string_When_asserting_for_none_Does_not_throw_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(Array.Empty<String>(), "because {0}", "reasons");

            act.Should().NotThrow();
        }

        [Fact]
        public void With_string_When_empty_Throws_exception()
        {
            var act = () => _operation.Should().ContainResponseCodes(["226", "503"], "because {0}", "reasons");

            act.Should().Throw<XunitException>()
                .WithMessage("""Expected _operation to contain responses with statuses {"226", "503"} because reasons, but found no responses.""");
        }
    }
}
