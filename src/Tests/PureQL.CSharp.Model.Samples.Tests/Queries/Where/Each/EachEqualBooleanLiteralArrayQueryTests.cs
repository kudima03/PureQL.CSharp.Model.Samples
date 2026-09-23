using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachEqualBooleanLiteralArrayQueryTests
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
                      "entity": "schema_with_foreign_keys.products",
                      "field": "product_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.products",
                      "field": "product_in_stock",
                      "type": {
                        "name": "boolean"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "booleanArray"
                      },
                      "value": [
                        true
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachEqualBooleanLiteralArrayQuery().Value).TextValue
        );
    }
}
