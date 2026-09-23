using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record PaginationWindowOverTiesQueryTests
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
                    },
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
                    "skip": 2,
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(new PaginationWindowOverTiesQuery().Value).TextValue
        );
    }
}
