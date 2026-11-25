using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record DecimalRemaindersQueryTests
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
                      "alias": "fractional_part",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
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
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "is_multiple_of_ten_cents",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "modulo",
                          "type": {
                            "name": "decimal"
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
                              "name": "decimal"
                            },
                            "value": "0.1"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "whole_fifties",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
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
                          "value": 50
                        }
                      }
                    },
                    {
                      "alias": "above_the_last_fifty",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
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
                          "value": 50
                        }
                      }
                    },
                    {
                      "alias": "hours_since_cutoff",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "datetimeDiffSeconds",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-05-31T23:45:30Z"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3600
                        }
                      }
                    },
                    {
                      "alias": "seconds_past_the_hour",
                      "expression": {
                        "operator": "modulo",
                        "type": {
                          "name": "decimal"
                        },
                        "left": {
                          "operator": "datetimeDiffSeconds",
                          "type": {
                            "name": "decimal"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-05-31T23:45:30Z"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3600
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DecimalRemaindersQuery().Value).TextValue
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
                      "name": "fractional_part",
                      "type": "double"
                    },
                    {
                      "name": "is_multiple_of_ten_cents",
                      "type": "bool"
                    },
                    {
                      "name": "whole_fifties",
                      "type": "long"
                    },
                    {
                      "name": "above_the_last_fifty",
                      "type": "double"
                    },
                    {
                      "name": "hours_since_cutoff",
                      "type": "long"
                    },
                    {
                      "name": "seconds_past_the_hour",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "0.5",
                      "True",
                      "2",
                      "0.5",
                      "10",
                      "870"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "0",
                      "True",
                      "1",
                      "0",
                      "35",
                      "870"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "0",
                      "True",
                      "4",
                      "0",
                      "60",
                      "870"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "0.25",
                      "False",
                      "1",
                      "25.25",
                      "85",
                      "870"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "0",
                      "True",
                      "6",
                      "0",
                      "110",
                      "870"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "0.5",
                      "True",
                      "2",
                      "0.5",
                      "135",
                      "870"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DecimalRemaindersQuery().Result).TextValue
        );
    }
}
