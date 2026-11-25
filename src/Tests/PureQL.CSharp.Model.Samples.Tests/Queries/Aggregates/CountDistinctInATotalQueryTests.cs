using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record CountDistinctInATotalQueryTests
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
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "notEqual",
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
                      "value": "cancelled"
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "customers",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "coupons",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "customers_with_a_coupon",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.employees",
                            "field": "employee_manager_id",
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            }
                          },
                          "right": {
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "orders_per_customer",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "operator": "countDistinct",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "every_order_has_its_own_customer",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "right": {
                          "operator": "countDistinct",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "months",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "startOfMonth",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CountDistinctInATotalQuery().Value).TextValue
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
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "customers",
                      "type": "long"
                    },
                    {
                      "name": "coupons",
                      "type": "long"
                    },
                    {
                      "name": "customers_with_a_coupon",
                      "type": "long"
                    },
                    {
                      "name": "orders_per_customer",
                      "type": "double"
                    },
                    {
                      "name": "every_order_has_its_own_customer",
                      "type": "bool"
                    },
                    {
                      "name": "months",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "5",
                      "4",
                      "2",
                      "3",
                      "1.25",
                      "False",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CountDistinctInATotalQuery().Result).TextValue
        );
    }
}
