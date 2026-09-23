using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingBooleanEqualityOfCountAndMaxComparisonsQueryTests
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
                    "operator": "equal",
                    "left": {
                      "operator": "greaterThan",
                      "left": {
                        "operator": "count",
                        "arg": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      },
                      "right": {
                        "type": {
                          "name": "number"
                        },
                        "value": 1
                      }
                    },
                    "right": {
                      "operator": "greaterThanOrEqual",
                      "left": {
                        "operator": "max_number",
                        "arg": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "number"
                          }
                        }
                      },
                      "right": {
                        "type": {
                          "name": "number"
                        },
                        "value": 300
                      }
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new HavingBooleanEqualityOfCountAndMaxComparisonsQuery().Value
            ).TextValue
        );
    }
}
