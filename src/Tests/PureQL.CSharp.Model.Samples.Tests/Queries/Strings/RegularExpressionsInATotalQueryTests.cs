using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record RegularExpressionsInATotalQueryTests
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
                      "name": "contacts",
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ann@example.com"
                            }
                          },
                          {
                            "alias": "full_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann Lee"
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "bob@mail.example.org"
                            }
                          },
                          {
                            "alias": "full_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob van der Berg"
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "cara@example.net"
                            }
                          },
                          {
                            "alias": "full_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "dan@localhost"
                            }
                          },
                          {
                            "alias": "full_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dan O'Neil"
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "eve.at.example.net"
                            }
                          },
                          {
                            "alias": "full_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Eve"
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "fay@example.com"
                            }
                          },
                          {
                            "alias": "full_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay Wong"
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
                      "subquery": "contacts",
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
                          "source": "contacts",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "aggregate": [
                    {
                      "alias": "well_formed_emails",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "matchesRegex",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "contacts",
                            "field": "email",
                            "type": {
                              "name": "string"
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "^[^@\\s]+@[^@\\s]+\\.[a-z]{2,}$"
                          }
                        }
                      }
                    },
                    {
                      "alias": "all_emails_have_an_at",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "matchesRegex",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "contacts",
                            "field": "email",
                            "type": {
                              "name": "string"
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "@"
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_domain",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "extractRegex",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "source": "contacts",
                            "field": "email",
                            "type": {
                              "name": "string"
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "@(.+)$"
                          },
                          "group": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_domain_top_level",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "selector": {
                            "operator": "extractRegex",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "operand": {
                              "source": "contacts",
                              "field": "email",
                              "type": {
                                "name": "string"
                              }
                            },
                            "pattern": {
                              "type": {
                                "name": "string"
                              },
                              "value": "@(.+)$"
                            },
                            "group": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[a-z]+$"
                        }
                      }
                    },
                    {
                      "alias": "longest_name_without_spaces",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "length",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "operator": "replaceRegex",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "contacts",
                              "field": "full_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "pattern": {
                              "type": {
                                "name": "string"
                              },
                              "value": "\\s"
                            },
                            "replacement": {
                              "type": {
                                "name": "string"
                              },
                              "value": ""
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "count_has_three_digits",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^[0-9]{3}$"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RegularExpressionsInATotalQuery().Value).TextValue
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
                      "name": "well_formed_emails",
                      "type": "long"
                    },
                    {
                      "name": "all_emails_have_an_at",
                      "type": "bool"
                    },
                    {
                      "name": "first_domain",
                      "type": "string"
                    },
                    {
                      "name": "first_domain_top_level",
                      "type": "string"
                    },
                    {
                      "name": "longest_name_without_spaces",
                      "type": "long"
                    },
                    {
                      "name": "count_has_three_digits",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "4",
                      "False",
                      "example.com",
                      "com",
                      "13",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegularExpressionsInATotalQuery().Result).TextValue
        );
    }
}
