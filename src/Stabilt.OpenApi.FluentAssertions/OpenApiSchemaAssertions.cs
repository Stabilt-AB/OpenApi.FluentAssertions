using System.Globalization;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiSchemaAssertions(OpenApiSchema value)
    : ObjectAssertions<OpenApiSchema, OpenApiSchemaAssertions>(value)
{
    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeArrayOfReferenceTo(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be an array of references to {0}{reason}", expected)
            .ForCondition(Subject.Type == "array")
            .FailWith(", but found type {0}.", Subject.Type)
            .Then
            .ForCondition(Subject.Items != null)
            .FailWith(", but found an array with undefined items.")
            .Then
            .Given(() => Subject.Items!.Reference?.Id)
            .ForCondition(referenceId => referenceId == expected)
            .FailWith(", but found an array of references to {0}.", referenceId => referenceId)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeArrayOfType(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be an array of {0}{reason}", expected)
            .ForCondition(Subject.Type == "array")
            .FailWith(", but found {0} instead of an array.", Subject.Type)
            .Then
            .ForCondition(Subject.Items != null)
            .FailWith(", but found an array with undefined items.")
            .Then
            .Given(() => Subject.Items!.Type)
            .ForCondition(type => type == expected)
            .FailWith(", but found an array of {0} types.", type => type)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeArrayType(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be an array{reason}")
            .ForCondition(Subject.Type == "array")
            .FailWith(", but found {0}.", Subject.Type)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeBase64FileType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("string", "byte", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeBinaryFileType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("string", "binary", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeBooleanType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeHelper("boolean", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeDateTimeType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("string", "date-time", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeDateType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("string", "date", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> BeDeprecated(
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

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeDoubleType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("number", "double", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeEnumType(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0} with enum values{reason}", "string")
            .ForCondition(Subject.Type == "string")
            .FailWith(", but found type {0}.", Subject.Type)
            .Then
            .ForCondition(Subject.Enum.Any())
            .FailWith(", but found no enum values.")
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeEnumType(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0} with only enum values {1}{reason}", "string", expected)
            .ForCondition(Subject.Type == "string")
            .FailWith(", but found type {0}.", Subject.Type)
            .Then
            .ForCondition(Subject.Enum.Any())
            .FailWith(", but found no enum values.")
            .Then
            .Given(() => Subject.Enum.OfType<OpenApiString>().Select(x => x.Value))
            .ForCondition(enumValues => enumValues?.Order().SequenceEqual(expected.Order()) == true)
            .FailWith(", but found enum values {0}.", enumValues => enumValues)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeFloatType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("number", "float", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeInt32Type(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("integer", "int32", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeInt64Type(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("integer", "int64", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeIntegerType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeHelper("integer", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> BeNullable(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be nullable{reason}")
            .ForCondition(Subject.Nullable)
            .FailWith(", but it wasn't.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeNumberType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeHelper("number", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeObjectType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeHelper("object", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BePasswordType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("string", "password", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> BeReadOnly(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be read-only{reason}")
            .ForCondition(Subject.ReadOnly)
            .FailWith(", but it wasn't.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeStringType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeHelper("string", because, becauseArgs);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> BeUuidType(
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTypeAndFormatHelper("string", "uuid", because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> BeWriteOnly(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be write-only{reason}")
            .ForCondition(Subject.WriteOnly)
            .FailWith(", but it wasn't.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> ContainEnumValues(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain enum values {0}{reason}", expected)
            .ForCondition(Subject.Enum.Any() || !expected.Any())
            .FailWith(", but found no enum values.")
            .Then
            .Given(() => Subject.Enum.OfType<OpenApiString>().Select(x => x.Value).ToList())
            .ForCondition(enumValues => expected.All(enumValues.Contains))
            .FailWith(", but found {0}.", enumValues => enumValues)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> ContainProperties(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain properties {0}{reason}", expected)
            .ForCondition(Subject.Properties.Any() || !expected.Any())
            .FailWith(", but found no properties.")
            .Then
            .ForCondition(expected.All(Subject.Properties.ContainsKey))
            .FailWith(", but found {0}.", Subject.Properties.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> ContainProperty(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a property {0}{reason}", expected)
            .ForCondition(Subject.Properties.Any())
            .FailWith(", but found no properties.")
            .Then
            .ForCondition(Subject.Properties.ContainsKey(expected))
            .FailWith(", but found {0}.", Subject.Properties.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject.Properties[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> ContainRequiredProperties(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain required properties {0}{reason}", expected)
            .ForCondition(Subject.Required.Any() || !expected.Any())
            .FailWith(", but found no required properties.")
            .Then
            .ForCondition(expected.All(Subject.Required.Contains))
            .FailWith(", but found {0}.", Subject.Required)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> ContainRequiredProperty(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a required property {0}{reason}", expected)
            .ForCondition(Subject.Required.Any())
            .FailWith(", but found no required properties.")
            .Then
            .ForCondition(Subject.Required.Contains(expected))
            .FailWith(", but found {0}.", Subject.Required)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> HaveAdditionalProperties(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have additional properties{reason}")
            .ForCondition(Subject.AdditionalProperties != null)
            .FailWith(", but found {0}.", Subject.AdditionalProperties)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject.AdditionalProperties!);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyHaveAllOfReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only have AllOf references to {0}{reason}", expected)
            .Given(() => Subject.AllOf?
                .Select(x => x.Reference?.Id)
                .OfType<String>()
                .ToList() ?? new List<String>())
            .ForCondition(ids => ids.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyHaveAllOfTypes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only have AllOf types {0}{reason}", expected)
            .Given(() => Subject.AllOf?
                .Where(x => x.Type != "object")
                .Where(x => x.Reference == null)
                .Select(x => x.Type)
                .ToList() ?? new List<String>())
            .ForCondition(ids => ids.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyHaveAnyOfReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only have AnyOf references to {0}{reason}", expected)
            .Given(() => Subject.AnyOf?
                .Select(x => x.Reference?.Id)
                .OfType<String>()
                .ToList() ?? new List<String>())
            .ForCondition(ids => ids.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyHaveAnyOfTypes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only have AnyOf types {0}{reason}", expected)
            .Given(() => Subject.AnyOf?
                .Where(x => x.Type != "object")
                .Where(x => x.Reference == null)
                .Select(x => x.Type)
                .ToList() ?? new List<String>())
            .ForCondition(ids => ids.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveEmptyFormat(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have empty format{reason}")
            .ForCondition(String.IsNullOrEmpty(Subject.Format))
            .FailWith(", but found {0}.", Subject.Format)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveFormat(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have format {0}{reason}", expected)
            .ForCondition(Subject.Format == expected)
            .FailWith(", but found {0}.", Subject.Format)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMaximum(
        Decimal expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have maximum value of {0}{reason}", Convert.ToDouble(expected))
            .ForCondition(Subject.Maximum != null)
            .FailWith(", but found {0}.", Subject.Maximum)
            .Then
            .ForCondition(Subject.Maximum == expected)
            .FailWith(", but found {0}.", Convert.ToDouble(Subject.Maximum, CultureInfo.InvariantCulture))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMaximumExclusive(
        Decimal expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have exclusive maximum value of {0}{reason}", Convert.ToDouble(expected))
            .ForCondition(Subject.Maximum != null)
            .FailWith(", but found {0}.", Subject.Maximum)
            .Then
            .ForCondition(Subject.Maximum == expected)
            .FailWith(", but found {0}.", Convert.ToDouble(Subject.Maximum, CultureInfo.InvariantCulture))
            .Then
            .ForCondition(Subject.ExclusiveMaximum == true)
            .FailWith(", but it wasn't exclusive.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMaxItems(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have maximum items of {0}{reason}", expected)
            .ForCondition(Subject.MaxItems == expected)
            .FailWith(", but found {0}.", Subject.MaxItems)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMaxLength(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have max length value of {0}{reason}", expected)
            .ForCondition(Subject.MaxLength == expected)
            .FailWith(", but found {0}.", Subject.MaxLength)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMaxProperties(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have maximum properties of {0}{reason}", expected)
            .ForCondition(Subject.MaxProperties == expected)
            .FailWith(", but found {0}.", Subject.MaxProperties)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMinimum(
        Decimal expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have minimum value of {0}{reason}", Convert.ToDouble(expected))
            .ForCondition(Subject.Minimum != null)
            .FailWith(", but found {0}.", Subject.Minimum)
            .Then
            .ForCondition(Subject.Minimum == expected)
            .FailWith(", but found {0}.", Convert.ToDouble(Subject.Minimum, CultureInfo.InvariantCulture))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMinimumExclusive(
        Decimal expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have exclusive minimum value of {0}{reason}", Convert.ToDouble(expected))
            .ForCondition(Subject.Minimum != null)
            .FailWith(", but found {0}.", Subject.Minimum)
            .Then
            .ForCondition(Subject.Minimum == expected)
            .FailWith(", but found {0}.", Convert.ToDouble(Subject.Minimum, CultureInfo.InvariantCulture))
            .Then
            .ForCondition(Subject.ExclusiveMinimum == true)
            .FailWith(", but it wasn't exclusive.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMinItems(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have minimum items of {0}{reason}", expected)
            .ForCondition(Subject.MinItems == expected)
            .FailWith(", but found {0}.", Subject.MinItems)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMinLength(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have min length value of {0}{reason}", expected)
            .ForCondition(Subject.MinLength == expected)
            .FailWith(", but found {0}.", Subject.MinLength)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMinProperties(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have minimum properties of {0}{reason}", expected)
            .ForCondition(Subject.MinProperties == expected)
            .FailWith(", but found {0}.", Subject.MinProperties)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveMultipleOf(
        Decimal expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have multiple of {0}{reason}", Convert.ToDouble(expected))
            .ForCondition(Subject.MultipleOf != null)
            .FailWith(", but found {0}.", Subject.MultipleOf)
            .Then
            .ForCondition(Subject.MultipleOf == expected)
            .FailWith(", but found {0}.", Convert.ToDouble(Subject.MultipleOf, CultureInfo.InvariantCulture))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyHaveOneOfReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only have OneOf references to {0}{reason}", expected)
            .Given(() => Subject.OneOf?
                .Select(x => x.Reference?.Id)
                .OfType<String>()
                .ToList() ?? new List<String>())
            .ForCondition(ids => ids.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyHaveOneOfTypes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only have OneOf types {0}{reason}", expected)
            .Given(() => Subject.OneOf?
                .Where(x => x.Type != "object")
                .Where(x => x.Reference == null)
                .Select(x => x.Type)
                .ToList() ?? new List<String>())
            .ForCondition(ids => ids.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HavePattern(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have pattern {0}{reason}", expected)
            .ForCondition(Subject.Pattern == expected)
            .FailWith(", but found {0}.", Subject.Pattern)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HavePropertyCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} properties{reason}", expected)
            .ForCondition(Subject.Properties.Any())
            .FailWith(", but found no properties.")
            .Then
            .ForCondition(Subject.Properties.Count == expected)
            .FailWith(", but found {0}: {1}.", Subject.Properties.Count, Subject.Properties.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveReferenceTo(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have reference to {0}{reason}", expected)
            .Given(() => Subject.Reference?.Id)
            .ForCondition(referenceId => referenceId == expected)
            .FailWith(", but found {0}.", referenceId => referenceId)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveTitle(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have title {0}{reason}", expected)
            .ForCondition(Subject.Title == expected)
            .FailWith(", but found {0}.", Subject.Title)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> HaveUniqueItems(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have unique items{reason}", true)
            .ForCondition(Subject.UniqueItems == true)
            .FailWith(", but it didn't.", false)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> NotBeDeprecated(
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

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> NotBeNullable(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not be nullable{reason}")
            .ForCondition(!Subject.Nullable)
            .FailWith(", but it was.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> NotBeReadOnly(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not be read-only{reason}")
            .ForCondition(!Subject.ReadOnly)
            .FailWith(", but it was.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> NotBeWriteOnly(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not be write-only{reason}")
            .ForCondition(!Subject.WriteOnly)
            .FailWith(", but it was.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> NotContainAnyRequiredProperties(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any required properties{reason}")
            .ForCondition(!Subject.Required.Any())
            .FailWith(", but found {0}.", Subject.Required)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> NotHaveAdditionalProperties(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not have additional properties{reason}")
            .ForCondition(Subject.AdditionalProperties == null)
            .FailWith(", but it had.")
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyContainEnumValues(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain enum values {0}{reason}", expected)
            .ForCondition(Subject.Enum.Any() || !expected.Any())
            .FailWith(", but found no enum values.")
            .Then
            .Given(() => Subject.Enum?.OfType<OpenApiString>().Select(x => x.Value))
            .ForCondition(enumValues => enumValues?.Order().SequenceEqual(expected.Order()) == true)
            .FailWith(", but found {0}.", enumValues => enumValues)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> OnlyContainProperties(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain properties {0}{reason}", expected)
            .ForCondition(Subject.Properties.Any() || !expected.Any())
            .FailWith(", but found no properties.")
            .Then
            .ForCondition(Subject.Properties.Keys.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", Subject.Properties.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSchemaAssertions> OnlyContainRequiredProperties(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain required properties {0}{reason}", expected)
            .ForCondition(Subject.Required.Any() || !expected.Any())
            .FailWith(", but found no required properties.")
            .Then
            .ForCondition(Subject.Required.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", Subject.Required)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSchemaAssertions>(this);
    }

    [CustomAssertion]
    private AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> HaveTypeAndFormatHelper(
        String expectedType,
        String expectedFormat,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0} with format {1}{reason}", expectedType, expectedFormat)
            .ForCondition(Subject.Type == expectedType)
            .FailWith(", but found type {0}.", Subject.Type)
            .Then
            .ForCondition(Subject.Format == expectedFormat)
            .FailWith(", but found format {0}.", Subject.Format)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }

    [CustomAssertion]
    private AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema> HaveTypeHelper(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be of type {0}{reason}", expected)
            .ForCondition(Subject.Type == expected)
            .FailWith(", but found {0}.", Subject.Type)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSchemaAssertions, OpenApiSchema>(this, Subject);
    }
}
