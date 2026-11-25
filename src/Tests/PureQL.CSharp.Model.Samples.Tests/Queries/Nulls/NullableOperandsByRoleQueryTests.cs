using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullableOperandsByRoleQueryTests
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
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000001-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "digits",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "exponent",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "3"
                            }
                          },
                          {
                            "alias": "base",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "2"
                            }
                          },
                          {
                            "alias": "start",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "length",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          {
                            "alias": "pattern",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "b"
                            }
                          },
                          {
                            "alias": "replacement",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "X"
                            }
                          },
                          {
                            "alias": "delimiter",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "-"
                            }
                          },
                          {
                            "alias": "index",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "fill",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "*"
                            }
                          },
                          {
                            "alias": "count",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000004-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "digits",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          },
                          {
                            "alias": "exponent",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "2"
                            }
                          },
                          {
                            "alias": "base",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "10"
                            }
                          },
                          {
                            "alias": "start",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "length",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "pattern",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "c"
                            }
                          },
                          {
                            "alias": "replacement",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "YY"
                            }
                          },
                          {
                            "alias": "delimiter",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "b"
                            }
                          },
                          {
                            "alias": "index",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "fill",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "xy"
                            }
                          },
                          {
                            "alias": "count",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "settings",
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
                          "source": "settings",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "round_digits",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "digits": {
                          "source": "settings",
                          "field": "digits",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "truncate_digits",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "digits": {
                          "source": "settings",
                          "field": "digits",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "power_exponent",
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
                          "value": "2.5"
                        },
                        "exponent": {
                          "source": "settings",
                          "field": "exponent",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "log_base",
                      "expression": {
                        "operator": "log",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "base": {
                          "source": "settings",
                          "field": "base",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "substring_start",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "start": {
                          "source": "settings",
                          "field": "start",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "substring_length",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "length": {
                          "source": "settings",
                          "field": "length",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "replace_pattern",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "pattern": {
                          "source": "settings",
                          "field": "pattern",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": "x"
                        }
                      }
                    },
                    {
                      "alias": "replace_replacement",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "b"
                        },
                        "replacement": {
                          "source": "settings",
                          "field": "replacement",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "position_pattern",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "pattern": {
                          "source": "settings",
                          "field": "pattern",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "position_start",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "b"
                        },
                        "start": {
                          "source": "settings",
                          "field": "start",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "splitPart_delimiter",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a-b"
                        },
                        "delimiter": {
                          "source": "settings",
                          "field": "delimiter",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "splitPart_index",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a-b"
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        },
                        "index": {
                          "source": "settings",
                          "field": "index",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "padStart_length",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "length": {
                          "source": "settings",
                          "field": "length",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0"
                        }
                      }
                    },
                    {
                      "alias": "padStart_fill",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        },
                        "fill": {
                          "source": "settings",
                          "field": "fill",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "padEnd_length",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "length": {
                          "source": "settings",
                          "field": "length",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0"
                        }
                      }
                    },
                    {
                      "alias": "padEnd_fill",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        },
                        "fill": {
                          "source": "settings",
                          "field": "fill",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "repeat_count",
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
                          "value": "abc"
                        },
                        "count": {
                          "source": "settings",
                          "field": "count",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "replaceRegex_replacement",
                      "expression": {
                        "operator": "replaceRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        },
                        "replacement": {
                          "source": "settings",
                          "field": "replacement",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "b+"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullableOperandsByRoleQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "round_digits",
                      "type": "double"
                    },
                    {
                      "name": "truncate_digits",
                      "type": "double"
                    },
                    {
                      "name": "power_exponent",
                      "type": "double"
                    },
                    {
                      "name": "log_base",
                      "type": "double"
                    },
                    {
                      "name": "substring_start",
                      "type": "string"
                    },
                    {
                      "name": "substring_length",
                      "type": "string"
                    },
                    {
                      "name": "replace_pattern",
                      "type": "string"
                    },
                    {
                      "name": "replace_replacement",
                      "type": "string"
                    },
                    {
                      "name": "position_pattern",
                      "type": "long"
                    },
                    {
                      "name": "position_start",
                      "type": "long"
                    },
                    {
                      "name": "splitPart_delimiter",
                      "type": "string"
                    },
                    {
                      "name": "splitPart_index",
                      "type": "string"
                    },
                    {
                      "name": "padStart_length",
                      "type": "string"
                    },
                    {
                      "name": "padStart_fill",
                      "type": "string"
                    },
                    {
                      "name": "padEnd_length",
                      "type": "string"
                    },
                    {
                      "name": "padEnd_fill",
                      "type": "string"
                    },
                    {
                      "name": "repeat_count",
                      "type": "string"
                    },
                    {
                      "name": "replaceRegex_replacement",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2.5",
                      "2.5",
                      "15.625",
                      "1.3219280948873624",
                      "bc",
                      "abc",
                      "axc",
                      "aXc",
                      "2",
                      "2",
                      "a",
                      "b",
                      "00abc",
                      "****abc",
                      "abc00",
                      "abc****",
                      "abcabc",
                      "aXc"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "3",
                      "2",
                      "6.25",
                      "0.3979400086720376",
                      "c",
                      "ab",
                      "abx",
                      "aYYc",
                      "3",
                      "0",
                      "a-",
                      "a",
                      "abc",
                      "xyxyabc",
                      "abc",
                      "abcxyxy",
                      "",
                      "aYYc"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      "",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullableOperandsByRoleQuery().Result).TextValue
        );
    }
}
