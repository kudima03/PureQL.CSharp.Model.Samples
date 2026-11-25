using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullableRoleOperandsInAGroupedQueryTests
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
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "country",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_tenant_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "uuid"
                            },
                            "value": "00000385-0000-0000-0000-000000000000"
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "D"
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": "F"
                        }
                      }
                    },
                    {
                      "alias": "initials",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "length": {
                          "operator": "round",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "power",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "operand": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      },
                      "exponent": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 1
                    }
                  },
                  "select": [
                    {
                      "alias": "country",
                      "expression": {
                        "key": "country",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "initials",
                      "expression": {
                        "key": "initials",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "bar",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "#"
                        },
                        "count": {
                          "operator": "max",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "rounded_average",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "growth",
                      "expression": {
                        "operator": "power",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "1.05"
                        },
                        "exponent": {
                          "operator": "min",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "round",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "padded_key",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "key": "country",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    },
                    {
                      "alias": "padded_to_count",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "key": "country",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "."
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "country",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "initials",
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
            new QueryJson(new NullableRoleOperandsInAGroupedQuery().Value).TextValue
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
                      "name": "country",
                      "type": "string"
                    },
                    {
                      "name": "initials",
                      "type": "string"
                    },
                    {
                      "name": "bar",
                      "type": "string"
                    },
                    {
                      "name": "rounded_average",
                      "type": "double"
                    },
                    {
                      "name": "growth",
                      "type": "double"
                    },
                    {
                      "name": "padded_key",
                      "type": "string"
                    },
                    {
                      "name": "padded_to_count",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "D",
                      "Ann",
                      "##############################",
                      "30",
                      "4.321942375150662",
                      "     D",
                      "D."
                    ],
                    [
                      "F",
                      "Cara",
                      "##############################",
                      "30",
                      "4.321942375150662",
                      "     F",
                      "F."
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullableRoleOperandsInAGroupedQuery().Result).TextValue
        );
    }
}
