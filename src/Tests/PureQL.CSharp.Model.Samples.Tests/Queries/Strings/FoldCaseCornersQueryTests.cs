using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record FoldCaseCornersQueryTests
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
                      "name": "people",
                      "rows": [
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000001-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Stra\u00dfe"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Annie"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ann@Example.ORG"
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2000-01-15"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000002-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bo\u00df"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "STRASSE"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob@example.com"
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "1999-03-20"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000003-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "M\u00fcller"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "CC"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "CARA@EXAMPLE.org"
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "1985-07-10"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000004-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "dan"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "M\u00dcLLER"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "dan@Example.com"
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "1990-11-05"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000005-0000-0000-0000-000000000000"
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
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "McCoy"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "EVE"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Eve@Example.Org"
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "1995-02-28"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000006-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "mccoy"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "fay@example.COM"
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "1988-12-31"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "people"
                  },
                  "select": [
                    {
                      "alias": "folded_twice",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "fold_of_upper",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "upper",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "lower_is_already_folded",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "lower",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "right": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "of_a_literal",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Stra\u00dfe"
                        }
                      }
                    },
                    {
                      "alias": "of_a_nullable_parameter",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "of_a_concat",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "source": "people",
                              "field": "first_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            },
                            {
                              "source": "people",
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "first_folded_name",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "foldCase",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "operand": {
                              "source": "people",
                              "field": "nickname",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "operator": "foldCase",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "people",
                              "field": "first_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "length_may_change",
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
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "people",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          },
                          {
                            "operator": "length",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "people",
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "position_of_needle",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "pattern": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "SS"
                          }
                        }
                      }
                    },
                    {
                      "alias": "without_needle",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "pattern": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "SS"
                          }
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    },
                    {
                      "alias": "email_domain",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "email",
                            "type": {
                              "name": "string"
                            }
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
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "kind",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "endsWith",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "operator": "foldCase",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "people",
                              "field": "email",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": ".org"
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "organisation"
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": "other"
                        }
                      }
                    },
                    {
                      "alias": "birth_date_key",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "birth_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "position_among_namesakes",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "people",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "foldCase",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "source": "people",
                                  "field": "first_name",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "people",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "namesakes",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "people",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "previous_in_order_without_case",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "people",
                          "field": "last_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "operator": "foldCase",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "source": "people",
                                  "field": "last_name",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "people",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "people",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "people",
                          "field": "last_name",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "people",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FoldCaseCornersQuery().Value).TextValue
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
                      "name": "folded_twice",
                      "type": "string"
                    },
                    {
                      "name": "fold_of_upper",
                      "type": "string"
                    },
                    {
                      "name": "lower_is_already_folded",
                      "type": "bool"
                    },
                    {
                      "name": "of_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "of_a_nullable_parameter",
                      "type": "string"
                    },
                    {
                      "name": "of_a_concat",
                      "type": "string"
                    },
                    {
                      "name": "first_folded_name",
                      "type": "string"
                    },
                    {
                      "name": "length_may_change",
                      "type": "long"
                    },
                    {
                      "name": "position_of_needle",
                      "type": "long"
                    },
                    {
                      "name": "without_needle",
                      "type": "string"
                    },
                    {
                      "name": "email_domain",
                      "type": "string"
                    },
                    {
                      "name": "kind",
                      "type": "string"
                    },
                    {
                      "name": "birth_date_key",
                      "type": "string"
                    },
                    {
                      "name": "position_among_namesakes",
                      "type": "long"
                    },
                    {
                      "name": "namesakes",
                      "type": "long"
                    },
                    {
                      "name": "previous_in_order_without_case",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "eve",
                      "eve",
                      "True",
                      "strasse",
                      "",
                      "eve mccoy",
                      "eve",
                      "0",
                      "0",
                      "mccoy",
                      "example.org",
                      "organisation",
                      "1995-02-28",
                      "1",
                      "2",
                      ""
                    ],
                    [
                      "fay",
                      "fay",
                      "True",
                      "strasse",
                      "",
                      "fay mccoy",
                      "fay",
                      "0",
                      "0",
                      "mccoy",
                      "example.com",
                      "other",
                      "1988-12-31",
                      "2",
                      "2",
                      "McCoy"
                    ],
                    [
                      "cara",
                      "cara",
                      "True",
                      "strasse",
                      "",
                      "cara m\u00fcller",
                      "cc",
                      "0",
                      "0",
                      "m\u00fcller",
                      "example.org",
                      "organisation",
                      "1985-07-10",
                      "1",
                      "2",
                      "M\u00dcLLER"
                    ],
                    [
                      "dan",
                      "dan",
                      "True",
                      "strasse",
                      "",
                      "dan m\u00fcller",
                      "dan",
                      "0",
                      "0",
                      "m\u00fcller",
                      "example.com",
                      "other",
                      "1990-11-05",
                      "2",
                      "2",
                      "mccoy"
                    ],
                    [
                      "ann",
                      "ann",
                      "False",
                      "strasse",
                      "",
                      "ann strasse",
                      "annie",
                      "1",
                      "5",
                      "strae",
                      "example.org",
                      "organisation",
                      "2000-01-15",
                      "1",
                      "2",
                      "STRASSE"
                    ],
                    [
                      "boss",
                      "boss",
                      "True",
                      "strasse",
                      "",
                      "boss strasse",
                      "boss",
                      "0",
                      "5",
                      "strae",
                      "example.com",
                      "other",
                      "1999-03-20",
                      "2",
                      "2",
                      "M\u00fcller"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FoldCaseCornersQuery().Result).TextValue
        );
    }
}
