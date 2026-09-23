using System.Text.Json;
using System.Text.Json.Serialization;
using PureQL.CSharp.Model.Serialization;

namespace PureQL.CSharp.Model.Samples.Tests;

// The compact PureQL JSON document a query serializes to through the
// converters of PureQL.CSharp.Model.Serialization. Every string, collection
// and flag of a sample shows up in it, so comparing it against the expected
// document asserts the whole query tree at once.
internal sealed record QueryJson
{
    private readonly Query _query;

    public QueryJson(Query query)
    {
        _query = query;
    }

    public string TextValue => JsonSerializer.Serialize(_query, Options());

    private static JsonSerializerOptions Options()
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
        };

        foreach (JsonConverter converter in new PureQLConverters())
        {
            options.Converters.Add(converter);
        }

        return options;
    }
}
