using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record InnerJoinOnKeyEqualityAndQtyAtMostOrderTotalQueryTests
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
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.order_items",
                      "field": "item_qty",
                      "type": {
                        "name": "number"
                      }
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "eachAnd",
                        "conditions": [
                          {
                            "operator": "eachEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.order_items",
                              "field": "item_order_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "eachLessThanOrEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "number"
                              }
                            },
                            "right": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "number"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new InnerJoinOnKeyEqualityAndQtyAtMostOrderTotalQuery().Value
            ).TextValue
        );
    }
}
