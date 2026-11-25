using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using PureQL.CSharp.Model.Serialization;

namespace PureQL.CSharp.Model.Samples.Tests;

// QueryJson for a query nested deeper than the default parse depth of 64: the canonical
// PureQL JSON document a query serializes to through the converters of
// PureQL.CSharp.Model.Serialization, written and read with a maximum depth of 256.
internal sealed record DeepQueryJson
{
    private readonly PureQLQuery _query;

    public DeepQueryJson(PureQLQuery query)
    {
        _query = query;
    }

    public string TextValue =>
        new CanonicalJson(
            JsonNode.Parse(
                JsonSerializer.Serialize(_query, Options()),
                documentOptions: new JsonDocumentOptions { MaxDepth = 256 }
            )
        ).TextValue;

    private static JsonSerializerOptions Options()
    {
        JsonSerializerOptions options = new JsonSerializerOptions { MaxDepth = 256 };

        foreach (JsonConverter converter in new PureQLConverters())
        {
            options.Converters.Add(converter);
        }

        return options;
    }
}
