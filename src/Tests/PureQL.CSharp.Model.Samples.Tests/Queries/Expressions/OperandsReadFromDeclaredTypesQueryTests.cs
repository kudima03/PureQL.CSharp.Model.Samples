using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record OperandsReadFromDeclaredTypesQueryTests
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
                      "name": "facts",
                      "rows": [
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000065-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "#Ann"
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-02"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "09:00:00"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000066-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "10:00:00"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000067-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "#Eve"
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-06"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "11:30:00"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000068-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "08:00:00"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000069-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "#Cara"
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-06"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "12:30:00"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000006a-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "is_paid",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dan"
                            }
                          },
                          {
                            "alias": "coupon_code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "shipped_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-06"
                            }
                          },
                          {
                            "alias": "window_start",
                            "expression": {
                              "type": {
                                "name": "time"
                              },
                              "value": "14:00:00"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "codes",
                      "query": {
                        "from": {
                          "subquery": "facts"
                        },
                        "select": [
                          {
                            "alias": "code",
                            "expression": {
                              "source": "facts",
                              "field": "coupon_code",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "facts",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "facts",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "a",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "coalesce",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "if",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "condition": {
                                "source": "facts",
                                "field": "is_paid",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "operator": "coalesce",
                                "type": {
                                  "name": "integer",
                                  "nullable": true
                                },
                                "values": [
                                  {
                                    "type": {
                                      "name": "integer",
                                      "nullable": true
                                    },
                                    "value": null
                                  },
                                  {
                                    "type": {
                                      "name": "integer",
                                      "nullable": true
                                    },
                                    "value": null
                                  }
                                ]
                              },
                              "else": {
                                "type": {
                                  "name": "integer",
                                  "nullable": true
                                },
                                "value": null
                              }
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "2.5"
                            }
                          ]
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
                      "alias": "b",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "condition": {
                            "source": "facts",
                            "field": "is_paid",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "value": null
                          },
                          "else": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "75.25"
                        }
                      }
                    },
                    {
                      "alias": "c",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "operator": "floor",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "d",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "left": {
                            "source": "facts",
                            "field": "shipped_date",
                            "type": {
                              "name": "date",
                              "nullable": true
                            }
                          },
                          "right": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1.5"
                        }
                      }
                    },
                    {
                      "alias": "e",
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
                          "type": {
                            "name": "decimal"
                          },
                          "value": "6"
                        }
                      }
                    },
                    {
                      "alias": "f",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "min",
                          "type": {
                            "name": "date"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-01"
                        }
                      }
                    },
                    {
                      "alias": "g",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "date"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "list": {
                          "type": {
                            "name": "dateList"
                          },
                          "value": [
                            "2024-06-04",
                            "2024-07-04"
                          ]
                        }
                      }
                    },
                    {
                      "alias": "h",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "#"
                            },
                            {
                              "source": "facts",
                              "field": "name",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        },
                        "list": {
                          "subquery": "codes",
                          "field": "code",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "i",
                      "expression": {
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "average",
                          "type": {
                            "name": "time"
                          },
                          "selector": {
                            "source": "facts",
                            "field": "window_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "right": {
                          "operator": "timeAddSeconds",
                          "type": {
                            "name": "time"
                          },
                          "operand": {
                            "source": "facts",
                            "field": "window_start",
                            "type": {
                              "name": "time"
                            }
                          },
                          "seconds": {
                            "operator": "divide",
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
                                "type": {
                                  "name": "integer"
                                },
                                "value": 2
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "j",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "datetimeAddSeconds",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "seconds": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 60
                          }
                        },
                        "right": {
                          "operator": "max",
                          "type": {
                            "name": "datetime"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "condition": {
                          "source": "facts",
                          "field": "is_paid",
                          "type": {
                            "name": "boolean"
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "else": {
                          "source": "facts",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "uuid"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            },
                            "value": null
                          },
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "decimal"
                        },
                        "condition": {
                          "source": "facts",
                          "field": "is_paid",
                          "type": {
                            "name": "boolean"
                          }
                        },
                        "then": {
                          "operator": "round",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        },
                        "else": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OperandsReadFromDeclaredTypesQuery().Value).TextValue
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
                      "name": "a",
                      "type": "bool"
                    },
                    {
                      "name": "b",
                      "type": "bool"
                    },
                    {
                      "name": "c",
                      "type": "bool"
                    },
                    {
                      "name": "d",
                      "type": "bool"
                    },
                    {
                      "name": "e",
                      "type": "bool"
                    },
                    {
                      "name": "f",
                      "type": "bool"
                    },
                    {
                      "name": "g",
                      "type": "bool"
                    },
                    {
                      "name": "h",
                      "type": "bool"
                    },
                    {
                      "name": "i",
                      "type": "bool"
                    },
                    {
                      "name": "j",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True"
                    ],
                    [
                      "False",
                      "False",
                      "True",
                      "False",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "True"
                    ],
                    [
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True"
                    ],
                    [
                      "False",
                      "False",
                      "True",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True"
                    ],
                    [
                      "False",
                      "True",
                      "True",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True"
                    ],
                    [
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OperandsReadFromDeclaredTypesQuery().Result).TextValue
        );
    }
}
