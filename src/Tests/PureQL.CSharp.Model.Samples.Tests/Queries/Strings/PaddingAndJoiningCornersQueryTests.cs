using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record PaddingAndJoiningCornersQueryTests
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
                      "name": "profiles",
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
                              "value": "Stone"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Marie"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "annie"
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
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Day"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "FR"
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
                              "value": "Lee"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Jo"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "cl"
                            }
                          },
                          {
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "PL"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "00-950"
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
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dan"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fox"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "foxy"
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
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "94105"
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
                              "value": "Bell"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "DE"
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
                              "value": "Roy"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "fr"
                            }
                          },
                          {
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "AT"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "1010"
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
                      "subquery": "profiles",
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
                          "source": "profiles",
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
                      "alias": "fill_of_several_code_points",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-="
                        }
                      }
                    },
                    {
                      "alias": "empty_fill_adds_nothing",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    },
                    {
                      "alias": "length_from_a_literal",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 8
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
                      "alias": "computed_length",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "operator": "add",
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
                                "source": "profiles",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          ]
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
                      "alias": "shorter_length_cuts_nothing",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
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
                      "alias": "fill_from_a_field",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        },
                        "fill": {
                          "source": "profiles",
                          "field": "country",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "nullable_operand",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "nickname",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 8
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "."
                        }
                      }
                    },
                    {
                      "alias": "repeated_zero_times",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "count": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    },
                    {
                      "alias": "repeated_by_a_literal",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "ab"
                        },
                        "count": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        }
                      }
                    },
                    {
                      "alias": "repeated_nullable",
                      "expression": {
                        "operator": "repeat",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "nickname",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "count": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "reversed_nullable",
                      "expression": {
                        "operator": "reverse",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "nickname",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "reversed_twice",
                      "expression": {
                        "operator": "reverse",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "reverse",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "profiles",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "reversed_date_text",
                      "expression": {
                        "operator": "reverse",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "only_null_values",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "profiles",
                            "field": "nickname",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "middle_name",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    },
                    {
                      "alias": "separator_from_a_literal",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "profiles",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " & "
                        }
                      }
                    },
                    {
                      "alias": "five_values",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "profiles",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "middle_name",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "nickname",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "country",
                            "type": {
                              "name": "string"
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": "|"
                        }
                      }
                    },
                    {
                      "alias": "empty_separator",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "profiles",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    },
                    {
                      "alias": "nested",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "concatWith",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "source": "profiles",
                                "field": "first_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "source": "profiles",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            ],
                            "separator": {
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            }
                          },
                          {
                            "operator": "concatWith",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "source": "profiles",
                                "field": "postal_code",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "source": "profiles",
                                "field": "country",
                                "type": {
                                  "name": "string"
                                }
                              }
                            ],
                            "separator": {
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": "; "
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new PaddingAndJoiningCornersQuery().Value).TextValue
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
                      "name": "fill_of_several_code_points",
                      "type": "string"
                    },
                    {
                      "name": "empty_fill_adds_nothing",
                      "type": "string"
                    },
                    {
                      "name": "length_from_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "computed_length",
                      "type": "string"
                    },
                    {
                      "name": "shorter_length_cuts_nothing",
                      "type": "string"
                    },
                    {
                      "name": "fill_from_a_field",
                      "type": "string"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "string"
                    },
                    {
                      "name": "repeated_zero_times",
                      "type": "string"
                    },
                    {
                      "name": "repeated_by_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "repeated_nullable",
                      "type": "string"
                    },
                    {
                      "name": "reversed_nullable",
                      "type": "string"
                    },
                    {
                      "name": "reversed_twice",
                      "type": "string"
                    },
                    {
                      "name": "reversed_date_text",
                      "type": "string"
                    },
                    {
                      "name": "only_null_values",
                      "type": "string"
                    },
                    {
                      "name": "separator_from_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "five_values",
                      "type": "string"
                    },
                    {
                      "name": "empty_separator",
                      "type": "string"
                    },
                    {
                      "name": "nested",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "-=-=-=-Ann",
                      "Ann",
                      "     Ann",
                      "Ann    ",
                      "Ann",
                      "AnnDEDEDEDED",
                      "...annie",
                      "",
                      "ababab",
                      "annieannie",
                      "einna",
                      "Ann",
                      "51-10-0202",
                      "annie Marie",
                      "Ann & Stone",
                      "Ann|Marie|Stone|annie|DE",
                      "AnnStone",
                      "Ann Stone; 10115 DE"
                    ],
                    [
                      "-=-=-=-Bob",
                      "Bob",
                      "     Bob",
                      "Bob  ",
                      "Bob",
                      "BobFRFRFRFRF",
                      "",
                      "",
                      "ababab",
                      "",
                      "",
                      "Bob",
                      "02-30-1202",
                      "",
                      "Bob & Day",
                      "Bob|Day|FR",
                      "BobDay",
                      "Bob Day; 75001 FR"
                    ],
                    [
                      "-=-=-=Cara",
                      "Cara",
                      "    Cara",
                      "Cara ",
                      "Cara",
                      "CaraPLPLPLPL",
                      "......cl",
                      "",
                      "ababab",
                      "clcl",
                      "lc",
                      "Cara",
                      "01-70-9102",
                      "cl Jo",
                      "Cara & Lee",
                      "Cara|Jo|Lee|cl|PL",
                      "CaraLee",
                      "Cara Lee; 00-950 PL"
                    ],
                    [
                      "-=-=-=-Dan",
                      "Dan",
                      "     Dan",
                      "Dan  ",
                      "Dan",
                      "DanUSUSUSUSU",
                      "....foxy",
                      "",
                      "ababab",
                      "foxyfoxy",
                      "yxof",
                      "Dan",
                      "50-11-2202",
                      "foxy",
                      "Dan & Fox",
                      "Dan|Fox|foxy|US",
                      "DanFox",
                      "Dan Fox; 94105 US"
                    ],
                    [
                      "-=-=-=-Eve",
                      "Eve",
                      "     Eve",
                      "Eve   ",
                      "Eve",
                      "EveDEDEDEDED",
                      "",
                      "",
                      "ababab",
                      "",
                      "",
                      "Eve",
                      "82-20-3202",
                      "",
                      "Eve & Bell",
                      "Eve|Bell|DE",
                      "EveBell",
                      "Eve Bell; 80331 DE"
                    ],
                    [
                      "-=-=-=-Fay",
                      "Fay",
                      "     Fay",
                      "Fay  ",
                      "Fay",
                      "FayATATATATA",
                      "......fr",
                      "",
                      "ababab",
                      "frfr",
                      "rf",
                      "Fay",
                      "51-10-0202",
                      "fr Ann",
                      "Fay & Roy",
                      "Fay|Ann|Roy|fr|AT",
                      "FayRoy",
                      "Fay Roy; 1010 AT"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PaddingAndJoiningCornersQuery().Result).TextValue
        );
    }
}
