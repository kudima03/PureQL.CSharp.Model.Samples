using PureQL.CSharp.Model.Samples.Queries.Parameters;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Parameters;

public sealed record OrdersWithTaxFromAParameterRowQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "subqueries": [
                    {
                      "name": "settings",
                      "rows": [
                        [
                          {
                            "alias": "rate",
                            "expression": {
                              "param": "tax_rate",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          {
                            "alias": "as_of",
                            "expression": {
                              "param": "as_of",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "alias": "note",
                            "expression": {
                              "param": "note",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "alias": "currency",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "EUR"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "settings",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "source": "settings",
                      "field": "as_of",
                      "type": {
                        "name": "date"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "total_with_tax",
                      "expression": {
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
                            "operator": "add",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              },
                              {
                                "source": "settings",
                                "field": "rate",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            ]
                          }
                        ]
                      }
                    },
                    {
                      "alias": "currency",
                      "expression": {
                        "source": "settings",
                        "field": "currency",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "note",
                      "expression": {
                        "source": "settings",
                        "field": "note",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrdersWithTaxFromAParameterRowQuery().Value).TextValue
        );
    }
}
