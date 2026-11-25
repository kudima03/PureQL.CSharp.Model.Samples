using System.Text.Json;
using System.Text.Json.Nodes;

namespace PureQL.CSharp.Model.Samples.Tests;

// An indented JSON document reduced to the canonical form QueryJson and DataSetJson
// produce, so a test can spell the expected document out readably, in any property
// order. The specification's deep samples nest far beyond the default depth of 64, so
// the document is read with a maximum depth of 512.
internal sealed record ExpectedJson
{
    public ExpectedJson(string json)
    {
        TextValue = json;
    }

    public string TextValue =>
        new CanonicalJson(
            JsonNode.Parse(
                field,
                documentOptions: new JsonDocumentOptions { MaxDepth = 512 }
            )
        ).TextValue;
}
