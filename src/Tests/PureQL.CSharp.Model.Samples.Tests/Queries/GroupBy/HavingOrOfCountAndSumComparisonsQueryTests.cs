using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record HavingOrOfCountAndSumComparisonsQueryTests
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
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "orderCount"
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
                    "operator": "or",
                    "conditions": [
                      {
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
                      {
                        "operator": "greaterThan",
                        "left": {
                          "operator": "sum",
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
                          "value": 150
                        }
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(new HavingOrOfCountAndSumComparisonsQuery().Value).TextValue
        );
    }
}
