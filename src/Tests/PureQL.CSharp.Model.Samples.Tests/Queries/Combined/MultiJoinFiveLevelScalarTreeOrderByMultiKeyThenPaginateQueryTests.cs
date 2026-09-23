using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record MultiJoinFiveLevelScalarTreeOrderByMultiKeyThenPaginateQueryTests
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
                      "field": "item_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.order_items",
                      "field": "item_qty",
                      "type": {
                        "name": "number"
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
                  "where": {
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "or",
                        "conditions": [
                          {
                            "operator": "not",
                            "condition": {
                              "operator": "and",
                              "conditions": [
                                {
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "value": true
                                },
                                {
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "value": false
                                }
                              ]
                            }
                          },
                          {
                            "type": {
                              "name": "boolean"
                            },
                            "value": true
                          }
                        ]
                      },
                      {
                        "operator": "or",
                        "conditions": [
                          {
                            "operator": "not",
                            "condition": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "operator": "and",
                            "conditions": [
                              {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": true
                              },
                              {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": true
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.order_items",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
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
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.order_items",
                        "field": "item_qty",
                        "type": {
                          "name": "number"
                        }
                      },
                      "direction": "desc"
                    }
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new MultiJoinFiveLevelScalarTreeOrderByMultiKeyThenPaginateQuery().Value
            ).TextValue
        );
    }
}
