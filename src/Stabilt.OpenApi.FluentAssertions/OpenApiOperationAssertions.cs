using System.Globalization;
using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiOperationAssertions(OpenApiOperation value)
    : ObjectAssertions<OpenApiOperation, OpenApiOperationAssertions>(value)
{
    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> BeDeprecated(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be deprecated{reason}")
            .ForCondition(Subject.Deprecated)
            .FailWith(", but it wasn't.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiOperationAssertions, OpenApiParameter> ContainParameter(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a parameter with name {0}{reason}", expected)
            .ForCondition(Subject.Parameters.Count > 0)
            .FailWith(", but found no parameters.")
            .Then
            .ForCondition(Subject.Parameters.Any(x => x.Name == expected))
            .FailWith(", but found {0}.", Subject.Parameters.Select(x => x.Name))
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiOperationAssertions, OpenApiParameter>(this, Subject.Parameters.First(p => p.Name == expected));
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainParameters(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain parameters {0}{reason}", expected)
            .ForCondition(Subject.Parameters.Count > 0 || !expected.Any())
            .FailWith(", but found no parameters.")
            .Then
            .Given(() => Subject.Parameters.Select(x => x.Name))
            .ForCondition(parameters => expected.All(parameters.Contains))
            .FailWith(", but found {0}.", parameters => parameters)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiOperationAssertions, OpenApiMediaType> ContainRequestContentType(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a request body with content type {0}{reason}", expected)
            .Given(() => Subject.RequestBody?.Content)
            .ForCondition(content => content?.Any() == true)
            .FailWith(", but found no request body.")
            .Then
            .Given(content => content!)
            .ForCondition(content => content.ContainsKey(expected))
            .FailWith(", but found {0}.", content => content.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiOperationAssertions, OpenApiMediaType>(this, Subject.RequestBody.Content[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainRequestContentTypes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain request body content types {0}{reason}", expected)
            .Given(() => Subject.RequestBody?.Content ?? new Dictionary<String, OpenApiMediaType>())
            .ForCondition(content => content?.Any() == true || !expected.Any())
            .FailWith(", but found no request body.")
            .Then
            .Given(content => content!)
            .ForCondition(content => expected.All(content.ContainsKey))
            .FailWith(", but found {0}.", content => content.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiOperationAssertions, OpenApiResponse> ContainResponseCode(
        HttpStatusCode expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return ContainResponseCode(Convert.ToInt32(expected, CultureInfo.InvariantCulture), because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiOperationAssertions, OpenApiResponse> ContainResponseCode(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return ContainResponseCode(expected.ToString(CultureInfo.InvariantCulture), because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiOperationAssertions, OpenApiResponse> ContainResponseCode(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain response with status {0}{reason}", expected)
            .ForCondition(Subject.Responses.Any())
            .FailWith(", but found no responses.")
            .Then
            .ForCondition(Subject.Responses.ContainsKey(expected))
            .FailWith(", but found {0}.", Subject.Responses.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiOperationAssertions, OpenApiResponse>(this, Subject.Responses[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainResponseCodes(
        IEnumerable<HttpStatusCode> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return ContainResponseCodes(expected.Select(i => Convert.ToInt32(i, CultureInfo.InvariantCulture)), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainResponseCodes(
        IEnumerable<Int32> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return ContainResponseCodes(expected.Select(i => i.ToString(CultureInfo.InvariantCulture)), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainResponseCodes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain responses with statuses {0}{reason}", expected)
            .Given(() => Subject.Responses)
            .ForCondition(responses => responses.Any() || !expected.Any())
            .FailWith(", but found no responses.")
            .Then
            .ForCondition(responses => expected.All(responses.ContainsKey))
            .FailWith(", but found {0}.", responses => responses.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiOperationAssertions, OpenApiSecurityScheme> ContainSecurityRequirementReference(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a security requirement {0}{reason}", expected)
            .ForCondition(Subject.Security.Any())
            .FailWith(", but found no security requirements.")
            .Then
            .Given(() => Subject.Security.SelectMany(x => x.Keys).Select(x => x.Reference.Id))
            .ForCondition(referenceIds => referenceIds.Contains(expected))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        var scheme = Subject.Security.SelectMany(x => x.Keys).First(x => x.Reference.Id == expected);

        return new AndWhichConstraint<OpenApiOperationAssertions, OpenApiSecurityScheme>(this, scheme);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainSecurityRequirementReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain security requirements {0}{reason}", expected)
            .ForCondition(Subject.Security.Any() || !expected.Any())
            .FailWith(", but found no security requirements.")
            .Then
            .Given(() => Subject.Security.SelectMany(x => x.Keys).Select(x => x.Reference.Id))
            .ForCondition(referenceIds => expected.All(referenceIds.Contains))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainTag(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain tag {0}{reason}", expected)
            .ForCondition(Subject.Tags.Any())
            .FailWith(", but found no tags.")
            .Then
            .Given(() => Subject.Tags.Select(x => x.Name).ToList())
            .ForCondition(tags => tags.Contains(expected))
            .FailWith(", but found {0}.", tags => tags)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> ContainTags(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain tags {0}{reason}", expected)
            .ForCondition(Subject.Tags.Any() || !expected.Any())
            .FailWith(", but found no tags.")
            .Then
            .Given(() => Subject.Tags.Select(x => x.Name).ToList())
            .ForCondition(tags => expected.All(tags.Contains))
            .FailWith(", but found {0}.", tags => tags)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> HaveId(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have ID {0}{reason}", expected)
            .ForCondition(Subject.OperationId == expected)
            .FailWith(", but found {0}.", Subject.OperationId)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> HaveParameterCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} parameters{reason}", expected)
            .ForCondition(Subject.Parameters.Count > 0 || expected == 0)
            .FailWith(", but found no parameters.")
            .Then
            .ForCondition(Subject.Parameters.Count == expected)
            .FailWith(", but found {0}: {1}.", Subject.Parameters.Count, Subject.Parameters.Select(x => x.Name))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> HaveResponseCodeCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} responses{reason}", expected)
            .ForCondition(Subject.Responses.Any() || expected == 0)
            .FailWith(", but found no responses.")
            .Then
            .ForCondition(Subject.Responses.Count == expected)
            .FailWith(", but found {0}.", Subject.Responses.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> NotBeDeprecated(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not be deprecated{reason}")
            .ForCondition(!Subject.Deprecated)
            .FailWith(", but it was.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> NotContainAnyParameters(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any parameters{reason}")
            .ForCondition(!Subject.Parameters.Any())
            .FailWith(", but found {0}.", Subject.Parameters.Select(x => x.Name))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> NotContainAnyRequestBodies(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any request bodies{reason}")
            .Given(() => Subject.RequestBody?.Content)
            .ForCondition(content => content is null || !content.Any())
            .FailWith(", but found {0}.", content => content?.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> NotContainSecurityRequirementReferences(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any security requirements{reason}")
            .ForCondition(!Subject.Security.Any())
            .FailWith(", but found {0}.", Subject.Security.SelectMany(x => x.Keys).Select(x => x.Reference.Id))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainParameters(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain parameters {0}{reason}", expected)
            .ForCondition(Subject.Parameters.Count > 0 || !expected.Any())
            .FailWith(", but found no parameters.")
            .Then
            .Given(() => Subject.Parameters.Select(x => x.Name))
            .ForCondition(parameters => parameters.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", parameters => parameters)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainParametersInOrder(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain parameters {0} in order{reason}", expected)
            .ForCondition(Subject.Parameters.Count > 0 || !expected.Any())
            .FailWith(", but found no parameters.")
            .Then
            .Given(() => Subject.Parameters.Select(x => x.Name))
            .ForCondition(parameters => parameters.SequenceEqual(expected))
            .FailWith(", but found {0}.", parameters => parameters)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainRequestContentTypes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain request body content types {0}{reason}", expected)
            .Given(() => Subject.RequestBody?.Content)
            .ForCondition(content => content != null || !expected.Any())
            .FailWith(", but found no request body.")
            .Then
            .Given(content => content?.Keys ?? new List<String>())
            .ForCondition(contentTypes => contentTypes.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", contentTypes => contentTypes)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainResponseCodes(
        IEnumerable<HttpStatusCode> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return OnlyContainResponseCodes(expected.Select(i => Convert.ToInt32(i, CultureInfo.InvariantCulture)), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainResponseCodes(
        IEnumerable<Int32> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return OnlyContainResponseCodes(expected.Select(i => i.ToString(CultureInfo.InvariantCulture)), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainResponseCodes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain responses with statuses {0}{reason}", expected)
            .ForCondition(Subject.Responses.Any() || !expected.Any())
            .FailWith(", but found no responses.")
            .Then
            .ForCondition(Subject.Responses.Keys.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", Subject.Responses.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainSecurityRequirementReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain security requirements {0}{reason}", expected)
            .ForCondition(Subject.Security.Any() || !expected.Any())
            .FailWith(", but found no security requirements.")
            .Then
            .Given(() => Subject.Security.SelectMany(x => x.Keys).Select(x => x.Reference.Id))
            .ForCondition(referenceIds => referenceIds.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOperationAssertions> OnlyContainTags(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain tags {0}{reason}", expected)
            .ForCondition(Subject.Tags.Any() || !expected.Any())
            .FailWith(", but found no tags.")
            .Then
            .Given(() => Subject.Tags.Select(x => x.Name).ToList())
            .ForCondition(tags => tags.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", tags => tags)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOperationAssertions>(this);
    }
}
