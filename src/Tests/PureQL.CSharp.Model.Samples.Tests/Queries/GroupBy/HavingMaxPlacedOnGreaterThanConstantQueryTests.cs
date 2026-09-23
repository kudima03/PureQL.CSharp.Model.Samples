using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingMaxPlacedOnGreaterThanConstantQueryTests
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
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "left": {
                      "operator": "max_date",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2024-06-03"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new HavingMaxPlacedOnGreaterThanConstantQuery().Value).TextValue
        );
    }
}
