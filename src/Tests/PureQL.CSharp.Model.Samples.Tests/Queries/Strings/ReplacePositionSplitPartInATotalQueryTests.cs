using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record ReplacePositionSplitPartInATotalQueryTests
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
                          }
                        ],
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
                              "value": "ann.lee@work.example.org"
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
                              "value": "bob@example.com"
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
                              "value": "cara@mail.co.uk"
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
                              "value": "cara.mail"
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
                              "value": "dan@dan.net"
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
                              "value": "@eve.org"
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
                              "value": "eve@eve.org"
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
                              "value": "fay@x.io"
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
                      "alias": "emails_without_at",
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
                            "operator": "position",
                            "type": {
                              "name": "integer"
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
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        }
                      }
                    },
                    {
                      "alias": "longest_domain",
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
                            "operator": "splitPart",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "contacts",
                              "field": "email",
                              "type": {
                                "name": "string"
                              }
                            },
                            "delimiter": {
                              "type": {
                                "name": "string"
                              },
                              "value": "@"
                            },
                            "index": {
                              "type": {
                                "name": "integer"
                              },
                              "value": -1
                            }
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
                          "operator": "splitPart",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "contacts",
                            "field": "email",
                            "type": {
                              "name": "string"
                            }
                          },
                          "delimiter": {
                            "type": {
                              "name": "string"
                            },
                            "value": "@"
                          },
                          "index": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_domain_label",
                      "expression": {
                        "operator": "replace",
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
                            "operator": "splitPart",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "contacts",
                              "field": "email",
                              "type": {
                                "name": "string"
                              }
                            },
                            "delimiter": {
                              "type": {
                                "name": "string"
                              },
                              "value": "@"
                            },
                            "index": {
                              "type": {
                                "name": "integer"
                              },
                              "value": -1
                            }
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "."
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": " dot "
                        }
                      }
                    },
                    {
                      "alias": "digits_in_count",
                      "expression": {
                        "operator": "length",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "replace",
                          "type": {
                            "name": "string"
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
                            "value": "0"
                          },
                          "replacement": {
                            "type": {
                              "name": "string"
                            },
                            "value": ""
                          }
                        }
                      }
                    },
                    {
                      "alias": "all_have_a_user_part",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "position",
                            "type": {
                              "name": "integer"
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
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ReplacePositionSplitPartInATotalQuery().Value).TextValue
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
                      "name": "emails_without_at",
                      "type": "long"
                    },
                    {
                      "name": "longest_domain",
                      "type": "long"
                    },
                    {
                      "name": "first_domain",
                      "type": "string"
                    },
                    {
                      "name": "first_domain_label",
                      "type": "string"
                    },
                    {
                      "name": "digits_in_count",
                      "type": "long"
                    },
                    {
                      "name": "all_have_a_user_part",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1",
                      "16",
                      "cara.mail",
                      "cara dot mail",
                      "1",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ReplacePositionSplitPartInATotalQuery().Result).TextValue
        );
    }
}
