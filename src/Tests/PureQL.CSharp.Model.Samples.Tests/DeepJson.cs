using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using PureQL.CSharp.Model.Serialization;

namespace PureQL.CSharp.Model.Samples.Tests;

// The canonical JSON text (the properties of every object in ordinal order, arrays as
// they are) of a document nested far deeper than the default JSON depth of 64, as the
// specification's deep samples are: either an expected document spelled out as text, or
// the PureQL document a query serializes to through PureQL.CSharp.Model.Serialization.
internal sealed record DeepJson
{
    private const int MaxDepth = 512;

    private readonly JsonNode? _node;

    public DeepJson(string json)
    {
        _node = JsonNode.Parse(
            json,
            documentOptions: new JsonDocumentOptions { MaxDepth = MaxDepth }
        );
    }

    public DeepJson(PureQLQuery query)
    {
        _node = JsonNode.Parse(
            JsonSerializer.Serialize(query, Options()),
            documentOptions: new JsonDocumentOptions { MaxDepth = MaxDepth }
        );
    }

    public string TextValue =>
        Canonical(_node)?.ToJsonString(new JsonSerializerOptions { MaxDepth = MaxDepth })
        ?? "null";

    private static JsonSerializerOptions Options()
    {
        JsonSerializerOptions options = new JsonSerializerOptions { MaxDepth = MaxDepth };

        foreach (JsonConverter converter in new PureQLConverters())
        {
            options.Converters.Add(converter);
        }

        return options;
    }

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
