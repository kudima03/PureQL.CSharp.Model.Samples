using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using PureQL.CSharp.Model.Serialization;

namespace PureQL.CSharp.Model.Samples.Tests;

// The canonical PureQL JSON document a query serializes to through the
// converters of PureQL.CSharp.Model.Serialization. Every string, collection
// and flag of a sample shows up in it, so comparing it against the expected
// document asserts the whole query tree at once. The serializer writes
// properties in the specification's order; the comparison does not depend on it.
internal sealed record QueryJson
{
    private readonly PureQLQuery _query;

    public QueryJson(PureQLQuery query)
    {
        _query = query;
    }

    public string TextValue =>
        new CanonicalJson(
            JsonNode.Parse(JsonSerializer.Serialize(_query, Options()))
        ).TextValue;

    private static JsonSerializerOptions Options()
    {
        // Deeply nested expressions can exceed the default depth of 64.
        JsonSerializerOptions options = new JsonSerializerOptions { MaxDepth = 256 };

        foreach (JsonConverter converter in new PureQLConverters())
        {
            options.Converters.Add(converter);
        }

        return options;
    }
}
