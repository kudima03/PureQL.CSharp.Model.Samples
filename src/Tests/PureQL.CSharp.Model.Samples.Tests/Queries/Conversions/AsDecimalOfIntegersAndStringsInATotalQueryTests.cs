using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record AsDecimalOfIntegersAndStringsInATotalQueryTests
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
                  "aggregate": [
                    {
                      "alias": "units",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_qty",
                                  "type": {
                                    "name": "decimal"
                                  }
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
                        }
                      }
                    },
                    {
                      "alias": "lines",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "largest",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "max",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "source": "schema_with_foreign_keys.order_items",
                                  "field": "item_qty",
                                  "type": {
                                    "name": "decimal"
                                  }
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
                        }
                      }
                    },
                    {
                      "alias": "text_as_decimal",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "12.5"
                        }
                      }
                    },
                    {
                      "alias": "text_as_integer_as_decimal",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "asInteger",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "42"
                          }
                        }
                      }
                    },
                    {
                      "alias": "tried_text",
                      "expression": {
                        "operator": "tryAsDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asNullable",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "7.25"
                          }
                        }
                      }
                    },
                    {
                      "alias": "tried_missing_text",
                      "expression": {
                        "operator": "tryAsDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "nullable_integer",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "rounded_again",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "asDecimal",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "sum",
                            "type": {
                              "name": "integer"
                            },
                            "selector": {
                              "operator": "round",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "operator": "multiply",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "source": "schema_with_foreign_keys.order_items",
                                    "field": "item_qty",
                                    "type": {
                                      "name": "decimal"
                                    }
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "optional_lines",
                      "expression": {
                        "operator": "asNullable",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new AsDecimalOfIntegersAndStringsInATotalQuery().Value
            ).TextValue
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
                      "name": "units",
                      "type": "double"
                    },
                    {
                      "name": "lines",
                      "type": "double"
                    },
                    {
                      "name": "largest",
                      "type": "double"
                    },
                    {
                      "name": "text_as_decimal",
                      "type": "double"
                    },
                    {
                      "name": "text_as_integer_as_decimal",
                      "type": "double"
                    },
                    {
                      "name": "tried_text",
                      "type": "double"
                    },
                    {
                      "name": "tried_missing_text",
                      "type": "double"
                    },
                    {
                      "name": "nullable_integer",
                      "type": "double"
                    },
                    {
                      "name": "rounded_again",
                      "type": "long"
                    },
                    {
                      "name": "optional_lines",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "110",
                      "4",
                      "50",
                      "12.5",
                      "42",
                      "7.25",
                      "",
                      "",
                      "110",
                      "4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new AsDecimalOfIntegersAndStringsInATotalQuery().Result
            ).TextValue
        );
    }
}
