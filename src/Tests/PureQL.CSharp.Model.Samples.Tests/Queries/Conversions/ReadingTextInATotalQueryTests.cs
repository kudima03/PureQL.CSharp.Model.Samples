using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record ReadingTextInATotalQueryTests
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
                      "name": "imports",
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "10115"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "4915112345678"
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
                              "value": "00000002-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "gold"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "01234"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "33612345678"
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
                              "value": "00000003-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "10"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "75001"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "4155550100"
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "5"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2040"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
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
                              "value": "00000005-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "1"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "94105"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "+61412345678"
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
                              "value": "00000006-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": " 4"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "n/a"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "3225551234"
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
                      "kind": "inner",
                      "subquery": "imports",
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
                          "source": "imports",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.users",
                      "field": "last_login",
                      "type": {
                        "name": "datetime"
                      }
                    },
                    "right": {
                      "operator": "asDatetime",
                      "type": {
                        "name": "datetime"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "2024-06-04T00:00:00Z"
                      }
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "users",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "numeric_tiers",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "tryAsInteger",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "imports",
                              "field": "tier",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "highest_tier",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "tryAsInteger",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "imports",
                            "field": "tier",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_postal_number",
                      "expression": {
                        "operator": "average",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "tryAsInteger",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "imports",
                            "field": "postal_code",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "target",
                      "expression": {
                        "operator": "asInteger",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "4"
                        }
                      }
                    },
                    {
                      "alias": "target_reached",
                      "expression": {
                        "operator": "greaterThanOrEqual",
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
                          "operator": "asInteger",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "4"
                          }
                        }
                      }
                    },
                    {
                      "alias": "all_mobiles_are_numbers",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "tryAsInteger",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "imports",
                              "field": "mobile",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ReadingTextInATotalQuery().Value).TextValue
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
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "numeric_tiers",
                      "type": "long"
                    },
                    {
                      "name": "highest_tier",
                      "type": "long"
                    },
                    {
                      "name": "average_postal_number",
                      "type": "double"
                    },
                    {
                      "name": "target",
                      "type": "long"
                    },
                    {
                      "name": "target_reached",
                      "type": "bool"
                    },
                    {
                      "name": "all_mobiles_are_numbers",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "5",
                      "3",
                      "10",
                      "29052",
                      "4",
                      "True",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ReadingTextInATotalQuery().Result).TextValue
        );
    }
}
