using System.Text.Json.Nodes;

namespace PureQL.CSharp.Model.Samples.Tests;

// An indented JSON document reduced to the canonical form QueryJson and DataSetJson
// produce, so a test can spell the expected document out readably, in any property
// order.
internal sealed record ExpectedJson
{
    public ExpectedJson(string json)
    {
        TextValue = json;
    }

    public string TextValue => new CanonicalJson(JsonNode.Parse(field)).TextValue;
}
