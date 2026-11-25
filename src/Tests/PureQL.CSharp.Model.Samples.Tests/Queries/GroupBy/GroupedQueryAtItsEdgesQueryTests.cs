using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupedQueryAtItsEdgesQueryTests
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
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "discount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.5"
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
                            "alias": "order_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-01"
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
                              "value": true
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 50
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          },
                          {
                            "alias": "discount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.25"
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
                            "alias": "order_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-03"
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
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "discount",
                            "expression": {
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "order_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-02"
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
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "discount",
                            "expression": {
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "order_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-02"
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
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          {
                            "alias": "discount",
                            "expression": {
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Eve"
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
                            "alias": "order_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-02"
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
                              "value": true
                            }
                          },
                          {
                            "alias": "bonus_points",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "discount",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "0.5"
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay"
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
                            "alias": "order_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-01"
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
                  "groupBy": [
                    {
                      "alias": "points",
                      "expression": {
                        "source": "facts",
                        "field": "bonus_points",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "day",
                      "expression": {
                        "source": "facts",
                        "field": "order_date",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "paid",
                      "expression": {
                        "source": "facts",
                        "field": "is_paid",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "verified",
                      "expression": {
                        "source": "facts",
                        "field": "is_paid",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "one",
                      "expression": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 1
                      }
                    },
                    {
                      "alias": "bucket",
                      "expression": {
                        "type": {
                          "name": "string"
                        },
                        "value": "web"
                      }
                    },
                    {
                      "alias": "discount",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "facts",
                            "field": "discount",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    }
                  ],
                  "having": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "key": "paid",
                        "type": {
                          "name": "boolean"
                        }
                      },
                      {
                        "operator": "any",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "source": "facts",
                          "field": "is_paid",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      },
                      {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          },
                          "over": "all"
                        },
                        "right": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
                      },
                      {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "max",
                          "type": {
                            "name": "decimal"
                          },
                          "over": "all",
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 100
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "avg_quantity",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "facts",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "avg_discount",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "source": "facts",
                          "field": "discount",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "last_name",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "facts",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "min_quantity",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "facts",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "alias": "bucket_param",
                      "expression": {
                        "type": {
                          "name": "string"
                        },
                        "value": "web"
                      }
                    },
                    {
                      "alias": "points_match",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "key": "points",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        },
                        "right": {
                          "operator": "sum",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
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
                      "alias": "verified_key",
                      "expression": {
                        "key": "verified",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "span_end",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "date"
                          },
                          "selector": {
                            "source": "facts",
                            "field": "order_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "days": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "operator": "max",
                            "type": {
                              "name": "date"
                            },
                            "selector": {
                              "source": "facts",
                              "field": "order_date",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          "right": {
                            "key": "day",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "key": "paid",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "paid "
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "open "
                            }
                          },
                          {
                            "operator": "max",
                            "type": {
                              "name": "string"
                            },
                            "over": "all",
                            "selector": {
                              "source": "facts",
                              "field": "name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "min_points_is_key",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "min",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "operator": "if",
                            "type": {
                              "name": "integer"
                            },
                            "condition": {
                              "source": "facts",
                              "field": "is_paid",
                              "type": {
                                "name": "boolean"
                              }
                            },
                            "then": {
                              "source": "facts",
                              "field": "quantity",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "else": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        },
                        "right": {
                          "key": "points",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "late_day_listed",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "max",
                          "type": {
                            "name": "date"
                          },
                          "selector": {
                            "operator": "dateAddDays",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "facts",
                              "field": "order_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "days": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        },
                        "list": {
                          "type": {
                            "name": "dateList"
                          },
                          "value": [
                            "2024-06-03",
                            "2024-12-25"
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "over": "all",
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
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "operator": "min",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "over": "all",
                            "selector": {
                              "source": "facts",
                              "field": "shipped_date",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "key": "day",
                            "type": {
                              "name": "date"
                            }
                          }
                        ]
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupedQueryAtItsEdgesQuery().Value).TextValue
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
                      "name": "avg_quantity",
                      "type": "double"
                    },
                    {
                      "name": "avg_discount",
                      "type": "double"
                    },
                    {
                      "name": "last_name",
                      "type": "string"
                    },
                    {
                      "name": "min_quantity",
                      "type": "long"
                    },
                    {
                      "name": "bucket_param",
                      "type": "string"
                    },
                    {
                      "name": "points_match",
                      "type": "bool"
                    },
                    {
                      "name": "verified_key",
                      "type": "bool"
                    },
                    {
                      "name": "span_end",
                      "type": "date"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "min_points_is_key",
                      "type": "bool"
                    },
                    {
                      "name": "late_day_listed",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "4",
                      "0.25",
                      "Bob",
                      "4",
                      "web",
                      "True",
                      "True",
                      "2024-06-03",
                      "paid Fay",
                      "False",
                      "False"
                    ],
                    [
                      "3",
                      "",
                      "Eve",
                      "1",
                      "web",
                      "False",
                      "True",
                      "2024-06-02",
                      "paid Fay",
                      "False",
                      "True"
                    ],
                    [
                      "2.5",
                      "0.5",
                      "Fay",
                      "2",
                      "web",
                      "False",
                      "True",
                      "2024-06-01",
                      "paid Fay",
                      "True",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupedQueryAtItsEdgesQuery().Result).TextValue
        );
    }
}
