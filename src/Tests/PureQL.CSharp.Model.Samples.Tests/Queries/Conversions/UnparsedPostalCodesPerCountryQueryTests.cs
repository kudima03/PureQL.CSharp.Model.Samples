using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record UnparsedPostalCodesPerCountryQueryTests
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "DE"
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
                              "value": "10115"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "DE"
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
                              "value": "n/a"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "FR"
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
                              "value": "75001"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "DE"
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
                              "value": "80331"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "US"
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
                              "value": "n/a"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "US"
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
                              "value": "tbd"
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
                  "groupBy": [
                    {
                      "alias": "country",
                      "expression": {
                        "source": "imports",
                        "field": "country",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "tier",
                      "expression": {
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
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "count",
                      "type": {
                        "name": "integer"
                      },
                      "predicate": {
                        "operator": "equal",
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
                            "field": "postal_code",
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
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
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
                      "alias": "tier",
                      "expression": {
                        "key": "tier",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    },
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
                      "alias": "unparsed_postal_codes",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "equal",
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
                              "field": "postal_code",
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
                      "alias": "lowest_postal_number",
                      "expression": {
                        "operator": "min",
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
                            "field": "postal_code",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "highest_postal_number",
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
                            "field": "postal_code",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "sum_of_parsed",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "coalesce",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
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
                    },
                    {
                      "alias": "next_tier",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "values": [
                          {
                            "key": "tier",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "as_of",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-06-30"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "equal",
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
                              "field": "postal_code",
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
                      },
                      "direction": "desc"
                    },
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
                        "key": "tier",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UnparsedPostalCodesPerCountryQuery().Value).TextValue
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
                      "name": "tier",
                      "type": "long"
                    },
                    {
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "unparsed_postal_codes",
                      "type": "long"
                    },
                    {
                      "name": "lowest_postal_number",
                      "type": "long"
                    },
                    {
                      "name": "highest_postal_number",
                      "type": "long"
                    },
                    {
                      "name": "sum_of_parsed",
                      "type": "long"
                    },
                    {
                      "name": "next_tier",
                      "type": "long"
                    },
                    {
                      "name": "as_of",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "US",
                      "",
                      "2",
                      "2",
                      "",
                      "",
                      "0",
                      "",
                      "2024-06-30"
                    ],
                    [
                      "DE",
                      "1",
                      "3",
                      "1",
                      "10115",
                      "80331",
                      "90446",
                      "2",
                      "2024-06-30"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UnparsedPostalCodesPerCountryQuery().Result).TextValue
        );
    }
}
