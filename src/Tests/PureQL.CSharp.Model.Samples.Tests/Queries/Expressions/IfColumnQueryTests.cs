using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record IfColumnQueryTests
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
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "size",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 100
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "large"
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": "small"
                        }
                      }
                    },
                    {
                      "alias": "discount",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        },
                        "then": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.1"
                            }
                          ]
                        },
                        "else": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new IfColumnQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "size",
                      "type": "string"
                    },
                    {
                      "name": "discount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "large",
                      "10.05"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "small",
                      "0"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "large",
                      "20"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "small",
                      "0"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "large",
                      "30"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "large",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new IfColumnQuery().Result).TextValue
        );
    }
}
