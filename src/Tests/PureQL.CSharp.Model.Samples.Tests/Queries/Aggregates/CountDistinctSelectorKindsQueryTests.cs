using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record CountDistinctSelectorKindsQueryTests
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
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
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
                    },
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
                  "select": [
                    {
                      "alias": "of_a_uuid",
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
                      "alias": "of_a_nullable_uuid",
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
                      "alias": "of_a_string",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_boolean",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_decimal",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_date",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_datetime",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_time",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_computed_value",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
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
                    },
                    {
                      "alias": "of_a_folded_string",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_condition",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
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
                        }
                      }
                    },
                    {
                      "alias": "of_a_literal",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "of_a_null",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "of_a_parameter",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "type": {
                            "name": "uuid"
                          },
                          "value": "00000385-0000-0000-0000-000000000000"
                        }
                      }
                    },
                    {
                      "alias": "with_a_predicate",
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
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CountDistinctSelectorKindsQuery().Value).TextValue
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
                      "name": "of_a_uuid",
                      "type": "long"
                    },
                    {
                      "name": "of_a_nullable_uuid",
                      "type": "long"
                    },
                    {
                      "name": "of_a_string",
                      "type": "long"
                    },
                    {
                      "name": "of_a_boolean",
                      "type": "long"
                    },
                    {
                      "name": "of_a_decimal",
                      "type": "long"
                    },
                    {
                      "name": "of_a_date",
                      "type": "long"
                    },
                    {
                      "name": "of_a_datetime",
                      "type": "long"
                    },
                    {
                      "name": "of_a_time",
                      "type": "long"
                    },
                    {
                      "name": "of_a_computed_value",
                      "type": "long"
                    },
                    {
                      "name": "of_a_folded_string",
                      "type": "long"
                    },
                    {
                      "name": "of_a_condition",
                      "type": "long"
                    },
                    {
                      "name": "of_a_literal",
                      "type": "long"
                    },
                    {
                      "name": "of_a_null",
                      "type": "long"
                    },
                    {
                      "name": "of_a_parameter",
                      "type": "long"
                    },
                    {
                      "name": "with_a_predicate",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "4",
                      "2",
                      "3",
                      "2",
                      "5",
                      "6",
                      "6",
                      "3",
                      "1",
                      "3",
                      "2",
                      "1",
                      "0",
                      "1",
                      "3"
                    ],
                    [
                      "4",
                      "2",
                      "3",
                      "2",
                      "5",
                      "6",
                      "6",
                      "3",
                      "1",
                      "3",
                      "2",
                      "1",
                      "0",
                      "1",
                      "3"
                    ],
                    [
                      "4",
                      "2",
                      "3",
                      "2",
                      "5",
                      "6",
                      "6",
                      "3",
                      "1",
                      "3",
                      "2",
                      "1",
                      "0",
                      "1",
                      "3"
                    ],
                    [
                      "4",
                      "2",
                      "3",
                      "2",
                      "5",
                      "6",
                      "6",
                      "3",
                      "1",
                      "3",
                      "2",
                      "1",
                      "0",
                      "1",
                      "3"
                    ],
                    [
                      "4",
                      "2",
                      "3",
                      "2",
                      "5",
                      "6",
                      "6",
                      "3",
                      "1",
                      "3",
                      "2",
                      "1",
                      "0",
                      "1",
                      "3"
                    ],
                    [
                      "4",
                      "2",
                      "3",
                      "2",
                      "5",
                      "6",
                      "6",
                      "3",
                      "1",
                      "3",
                      "2",
                      "1",
                      "0",
                      "1",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CountDistinctSelectorKindsQuery().Result).TextValue
        );
    }
}
