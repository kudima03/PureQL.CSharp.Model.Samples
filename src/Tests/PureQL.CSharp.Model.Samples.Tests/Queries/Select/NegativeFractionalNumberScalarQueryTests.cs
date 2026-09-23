using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record NegativeFractionalNumberScalarQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.products"
                  },
                  "select": [
                    {
                      "type": {
                        "name": "number"
                      },
                      "value": -12.75,
                      "alias": "adjustment"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NegativeFractionalNumberScalarQuery().Value).TextValue
        );
    }
}
