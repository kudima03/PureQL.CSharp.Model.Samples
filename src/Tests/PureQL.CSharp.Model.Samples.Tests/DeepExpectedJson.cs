using System.Text.Json;
using System.Text.Json.Nodes;

namespace PureQL.CSharp.Model.Samples.Tests;

// ExpectedJson for a document nested deeper than the default parse depth of 64: an
// indented JSON document reduced to the canonical form QueryJson and DataSetJson
// produce, read with a maximum depth of 256.
internal sealed record DeepExpectedJson
{
    public DeepExpectedJson(string json)
    {
        TextValue = json;
    }

    public string TextValue =>
        new CanonicalJson(
            JsonNode.Parse(
                field,
                documentOptions: new JsonDocumentOptions { MaxDepth = 256 }
            )
        ).TextValue;
}
