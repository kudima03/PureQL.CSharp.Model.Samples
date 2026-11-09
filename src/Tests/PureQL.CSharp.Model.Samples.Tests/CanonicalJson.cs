using System.Text.Json.Nodes;

namespace PureQL.CSharp.Model.Samples.Tests;

// The compact text of a JSON document with the properties of every object in ordinal
// order. JSON objects are unordered, so two documents that differ only in the order of
// their properties have the same canonical text; arrays keep their order, which is part
// of what a query or a result means.
internal sealed record CanonicalJson
{
    private readonly JsonNode? _node;

    public CanonicalJson(JsonNode? node)
    {
        _node = node;
    }

    public string TextValue => Canonical(_node)?.ToJsonString() ?? "null";

    private static JsonNode? Canonical(JsonNode? node)
    {
        return node switch
        {
            JsonObject properties => new JsonObject(
                properties
                    .OrderBy(property => property.Key, StringComparer.Ordinal)
                    .Select(property =>
                        KeyValuePair.Create(property.Key, Canonical(property.Value))
                    )
            ),
            JsonArray items => new JsonArray([.. items.Select(Canonical)]),
            _ => node?.DeepClone(),
        };
    }
}
