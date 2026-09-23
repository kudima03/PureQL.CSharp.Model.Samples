using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record WholeArrayEqualityOfLiteralAgainstFieldQueryTests
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
                        "name": "numberArray"
                      },
                      "value": [
                        100.5,
                        300,
                        75.25,
                        200,
                        50,
                        100.5
                      ]
                    },
                    "right": {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new WholeArrayEqualityOfLiteralAgainstFieldQuery().Value
            ).TextValue
        );
    }
}
