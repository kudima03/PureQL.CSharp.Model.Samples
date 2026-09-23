using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record InnerJoinOnKeyAndQuantityQueryTests
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
                      "entity": "schema_with_foreign_keys.order_items",
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
                            "operator": "eachGreaterThan",
                            "left": {
                              "entity": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "number"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 1
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InnerJoinOnKeyAndQuantityQuery().Value).TextValue
        );
    }

    [Fact]
    public void ResultMatchesExpectedRows()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "name": "",
                  "columns": [
                    {
                      "name": "item_qty",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2"
                    ],
                    [
                      "5"
                    ],
                    [
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InnerJoinOnKeyAndQuantityQuery().Result).TextValue
        );
    }
}
