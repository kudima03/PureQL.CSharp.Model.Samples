using System.Text.Json.Nodes;

namespace PureQL.CSharp.Model.Samples.Tests;

// An indented JSON document reduced to the compact form QueryJson produces,
// so a test can spell the expected document out readably.
internal sealed record ExpectedJson
{
    public ExpectedJson(string json)
    {
        TextValue = json;
    }

    public string TextValue => JsonNode.Parse(field)!.ToJsonString();
}
