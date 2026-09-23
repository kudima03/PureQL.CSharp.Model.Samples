using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeBooleanArrayEqualityOfLiteralAgainstFieldQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "equal",
                    "left": {
                      "type": {
                        "name": "booleanArray"
                      },
                      "value": [
                        true,
                        true,
                        false,
                        true
                      ]
                    },
                    "right": {
                      "entity": "schema_with_foreign_keys.products",
                      "field": "product_in_stock",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeBooleanArrayEqualityOfLiteralAgainstFieldQuery().Value
            ).TextValue
        );
    }
}
