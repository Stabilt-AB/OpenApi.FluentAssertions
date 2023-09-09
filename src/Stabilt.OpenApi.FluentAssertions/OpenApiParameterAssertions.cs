using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiParameterAssertions(OpenApiParameter value)
    : ObjectAssertions<OpenApiParameter, OpenApiParameterAssertions>(value)
{
    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> AllowEmptyValues(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to allow empty values{reason}")
            .ForCondition(Subject.AllowEmptyValue)
            .FailWith(", but it didn't.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeBase64Type(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("string", "byte", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeBinaryType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("string", "binary", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeBooleanType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeHelper("boolean", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeDateTimeType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("string", "date-time", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeDateType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("string", "date", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeDeprecated(
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

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeDoubleType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("number", "double", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeEnumType(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0} with enum values{reason}", "string")
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Type == "string")
            .FailWith(", but found type {0}.", schema => schema.Type)
            .Then
            .ForCondition(schema => schema.Enum?.Any() == true)
            .FailWith(", but found no enum values.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeEnumType(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0} with enum values {1}{reason}", "string", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Type == "string")
            .FailWith(", but found type {0}.", schema => schema.Type)
            .Then
            .ForCondition(schema => schema.Enum.Any())
            .FailWith(", but found no enum values.")
            .Then
            .Given(schema => schema.Enum.OfType<OpenApiString>().Select(x => x.Value))
            .ForCondition(enumValues => enumValues?.Order().SequenceEqual(expected.Order()) == true)
            .FailWith(", but found enum values {0}.", enumValues => enumValues)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeFloatType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("number", "float", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeIn(
        ParameterLocation expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be in {0}{reason}", expected.ToString())
            .ForCondition(Subject.In == expected)
            .FailWith(", but found {0}.", Subject.In.ToString())
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeInCookie(String because = "", params Object[] becauseArgs) =>
        BeIn(ParameterLocation.Cookie, because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeInHeader(String because = "", params Object[] becauseArgs) =>
        BeIn(ParameterLocation.Header, because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeInPath(String because = "", params Object[] becauseArgs) =>
        BeIn(ParameterLocation.Path, because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeInQuery(String because = "", params Object[] becauseArgs) =>
        BeIn(ParameterLocation.Query, because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeInt32Type(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("integer", "int32", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeInt64Type(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("integer", "int64", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeIntegerType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeHelper("integer", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeNumberType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeHelper("number", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BePasswordType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("string", "password", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeRequired(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be required{reason}")
            .ForCondition(Subject.Required)
            .FailWith(", but it wasn't.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeStringType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeHelper("string", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> BeUuidType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveSchemaTypeAndFormatHelper("string", "uuid", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> ContainEnumValues(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain enum values {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Enum?.Any() == true || !expected.Any())
            .FailWith(", but found no enum values.")
            .Then
            .Given(schema => schema.Enum!.OfType<OpenApiString>().Select(x => x.Value))
            .ForCondition(enumValues => expected.All(enumValues.Contains))
            .FailWith(", but found {0}.", enumValues => enumValues)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveAllOfReferenceTo(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have AllOf reference {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!.AllOf.Select(x => x.Reference?.Id).OfType<String>().ToList())
            .ForCondition(referenceIds => referenceIds.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveAnyOfReferenceTo(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have AnyOf reference {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!.AnyOf.Select(x => x.Reference?.Id).OfType<String>().ToList())
            .ForCondition(referenceIds => referenceIds.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveName(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have name {0}{reason}", expected)
            .ForCondition(Subject.Name == expected)
            .FailWith(", but found {0}.", Subject.Name)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveOneOfReferenceTo(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have OneOf reference {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!.OneOf.Select(x => x.Reference?.Id).OfType<String>().ToList())
            .ForCondition(referenceIds => referenceIds.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveReferenceTo(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have schema reference {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Reference != null)
            .FailWith(", but the schema reference was <null>.")
            .Then
            .Given(schema => schema.Reference!)
            .ForCondition(reference => reference.Id == expected)
            .FailWith(", but found {0}.", reference => reference.Id)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveSchemaFormat(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have schema format {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Format == expected)
            .FailWith(", but found {0}.", schema => schema.Format)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveSchemaType(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have schema type {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Type == expected)
            .FailWith(", but found {0}.", schema => schema.Type)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> HaveStyle(
        ParameterStyle expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have style {0}{reason}", expected.ToString())
            .ForCondition(Subject.Style == expected)
            .FailWith(", but found {0}.", Subject.Style.ToString())
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> NotAllowEmptyValues(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not allow empty values{reason}")
            .ForCondition(!Subject.AllowEmptyValue)
            .FailWith(", but it did.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> NotBeDeprecated(
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

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> NotBeRequired(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not be required{reason}")
            .ForCondition(!Subject.Required)
            .FailWith(", but it was.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiParameterAssertions> OnlyContainEnumValues(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain enum values {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Enum?.Any() == true || !expected.Any())
            .FailWith(", but found no enum values.")
            .Then
            .Given(schema => schema.Enum!.OfType<OpenApiString>().Select(x => x.Value))
            .ForCondition(enumValues => enumValues?.Order().SequenceEqual(expected.Order()) == true)
            .FailWith(", but found {0}.", enumValues => enumValues)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    private AndConstraint<OpenApiParameterAssertions> HaveSchemaTypeAndFormatHelper(
        String expectedType,
        String expectedFormat,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0} with format {1}{reason}", expectedType, expectedFormat)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema.Type == expectedType)
            .FailWith(", but found type {0}.", schema => schema.Type)
            .Then
            .ForCondition(schema => schema.Format == expectedFormat)
            .FailWith(", but found format {0}.", schema => schema.Format)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }

    [CustomAssertion]
    private AndConstraint<OpenApiParameterAssertions> HaveSchemaTypeHelper(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0}{reason}", expected)
            .ForCondition(Subject.Schema != null)
            .FailWith(", but the schema was <null>.")
            .Then
            .Given(() => Subject.Schema!)
            .ForCondition(schema => schema!.Type == expected)
            .FailWith(", but found {0}.", schema => schema.Type)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiParameterAssertions>(this);
    }
}
