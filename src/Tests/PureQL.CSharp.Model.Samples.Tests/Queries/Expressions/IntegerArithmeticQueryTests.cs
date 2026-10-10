using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record IntegerArithmeticQueryTests
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
                      "alias": "item_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "double_quantity",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "multiply",
                        "values": [
                          {
                            "operator": "round",
                            "value": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        ]
                      }
                    },
                    {
                      "alias": "one_less",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "subtract",
                        "values": [
                          {
                            "operator": "round",
                            "value": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "with_bonus",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "add",
                        "values": [
                          {
                            "operator": "round",
                            "value": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new IntegerArithmeticQuery().Value).TextValue
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
                      "name": "item_id",
                      "type": "uuid"
                    },
                    {
                      "name": "double_quantity",
                      "type": "long"
                    },
                    {
                      "name": "one_less",
                      "type": "long"
                    },
                    {
                      "name": "with_bonus",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "4",
                      "1",
                      "13"
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "2",
                      "0",
                      "12"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "10",
                      "4",
                      "16"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "6",
                      "2",
                      "14"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new IntegerArithmeticQuery().Result).TextValue
        );
    }
}
