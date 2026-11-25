using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record UsersPerEmailDomainByRegexQueryTests
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ann"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "+49 151 1234567"
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
                              "value": "bob@Example.com"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "bob7"
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
                              "value": "00000003-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "cara@mail.example.org"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "cara"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "336 1234 5678"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "dan"
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
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "eve.at.example.net"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "eve"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "415-555-0100"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "fay"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "415 555 0100"
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
                  "where": {
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
                  },
                  "groupBy": [
                    {
                      "alias": "domain",
                      "expression": {
                        "operator": "lower",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
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
                      "alias": "has_digits",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "contacts",
                          "field": "nickname",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\d"
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
                      "alias": "domain",
                      "expression": {
                        "key": "domain",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "has_digits",
                      "expression": {
                        "key": "has_digits",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    },
                    {
                      "alias": "well_formed",
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
                      "alias": "shortest_user_part",
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
                            "value": "^[^@]+"
                          }
                        }
                      }
                    },
                    {
                      "alias": "longest_digits",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "length",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "operator": "replaceRegex",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "operand": {
                              "source": "contacts",
                              "field": "mobile",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            },
                            "pattern": {
                              "type": {
                                "name": "string"
                              },
                              "value": "\\D"
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
                      "alias": "top_level",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "key": "domain",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\.([a-z]+)$"
                        },
                        "group": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "is_company_domain",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "key": "domain",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^example\\.com$"
                        }
                      }
                    },
                    {
                      "alias": "masked_domain",
                      "expression": {
                        "operator": "replaceRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "key": "domain",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[a-z]"
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": "*"
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
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "domain",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "has_digits",
                        "type": {
                          "name": "boolean"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UsersPerEmailDomainByRegexQuery().Value).TextValue
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
                      "name": "domain",
                      "type": "string"
                    },
                    {
                      "name": "has_digits",
                      "type": "bool"
                    },
                    {
                      "name": "well_formed",
                      "type": "long"
                    },
                    {
                      "name": "shortest_user_part",
                      "type": "string"
                    },
                    {
                      "name": "longest_digits",
                      "type": "long"
                    },
                    {
                      "name": "top_level",
                      "type": "string"
                    },
                    {
                      "name": "is_company_domain",
                      "type": "bool"
                    },
                    {
                      "name": "masked_domain",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "example.com",
                      "False",
                      "2",
                      "ann",
                      "12",
                      "com",
                      "True",
                      "*******.***"
                    ],
                    [
                      "example.com",
                      "True",
                      "1",
                      "bob",
                      "",
                      "com",
                      "True",
                      "*******.***"
                    ],
                    [
                      "mail.example.org",
                      "False",
                      "1",
                      "cara",
                      "11",
                      "org",
                      "False",
                      "****.*******.***"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UsersPerEmailDomainByRegexQuery().Result).TextValue
        );
    }
}
