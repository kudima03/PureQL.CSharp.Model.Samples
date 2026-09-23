using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record NegativeSkipQueryTests
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
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "number"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "skip": -5,
                    "take": 3
                  }
                }
                """
            ).TextValue,
            new QueryJson(new NegativeSkipQuery().Value).TextValue
        );
    }
}
