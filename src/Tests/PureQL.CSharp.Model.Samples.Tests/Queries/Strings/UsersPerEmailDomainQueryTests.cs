using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record UsersPerEmailDomainQueryTests
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
                              "value": "ann@Example.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann Marie"
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
                              "value": "ann.marie+shop@example.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann Marie"
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
                              "value": "bob+news@EXAMPLE.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob"
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
                              "value": "cara@mail.example.co.uk"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara Lee"
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
                              "value": "daniel@Mail.Example.co.uk"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dan"
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
                              "value": "eve.example.com"
                            }
                          },
                          {
                            "alias": "first_name",
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
                              "value": "fay@example.org"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay Anne"
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
                      "value": 0
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "domain",
                      "expression": {
                        "operator": "lower",
                        "type": {
                          "name": "string"
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
                    },
                    {
                      "alias": "top_level",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "lower",
                          "type": {
                            "name": "string"
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
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": "."
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
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
                      "alias": "domain",
                      "expression": {
                        "key": "domain",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "top_level",
                      "expression": {
                        "key": "top_level",
                        "type": {
                          "name": "string"
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
                      "alias": "shortest_user_part",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
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
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "with_plus_tag",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
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
                              "value": "+"
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
                      "alias": "first_name_without_spaces",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "replace",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "contacts",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": " "
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
                      "alias": "label",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "key": "domain",
                          "type": {
                            "name": "string"
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
                      "alias": "dots_in_domain",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "length",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "key": "domain",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          {
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
                                "key": "domain",
                                "type": {
                                  "name": "string"
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
                                "value": ""
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "domain",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "top_level",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UsersPerEmailDomainQuery().Value).TextValue
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
                      "name": "top_level",
                      "type": "string"
                    },
                    {
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "shortest_user_part",
                      "type": "long"
                    },
                    {
                      "name": "with_plus_tag",
                      "type": "long"
                    },
                    {
                      "name": "first_name_without_spaces",
                      "type": "string"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "dots_in_domain",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "example.com",
                      "com",
                      "3",
                      "3",
                      "2",
                      "AnnMarie",
                      "example dot com",
                      "1"
                    ],
                    [
                      "mail.example.co.uk",
                      "uk",
                      "2",
                      "4",
                      "0",
                      "CaraLee",
                      "mail dot example dot co dot uk",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UsersPerEmailDomainQuery().Result).TextValue
        );
    }
}
