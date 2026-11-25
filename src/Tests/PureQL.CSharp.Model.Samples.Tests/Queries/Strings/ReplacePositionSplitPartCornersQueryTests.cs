using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record ReplacePositionSplitPartCornersQueryTests
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
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Mr. Ann Lee"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann_the_Great"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Marie-Claire"
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
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob Stone"
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
                            "alias": "middle_name",
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
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Stone, Cara Jo"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "the_Cara"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Jo-Anne"
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
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Mr. Dan Fox"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "d_fox"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": ""
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
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Eve"
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
                            "alias": "middle_name",
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
                              "value": "00000006-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay Rose Day"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay"
                            }
                          },
                          {
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Lee"
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
                      "alias": "empty_pattern_changes_nothing",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
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
                      "alias": "removed",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
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
                    },
                    {
                      "alias": "pattern_from_a_field",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": "?"
                        }
                      }
                    },
                    {
                      "alias": "replacement_from_a_literal",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Mr. "
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Dr. "
                        }
                      }
                    },
                    {
                      "alias": "nullable_operand",
                      "expression": {
                        "operator": "replace",
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
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "_"
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        }
                      }
                    },
                    {
                      "alias": "replace_in_a_literal",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "2024-01-31"
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": "/"
                        }
                      }
                    },
                    {
                      "alias": "position_of_the_empty_pattern",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    },
                    {
                      "alias": "position_from_a_literal_start",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
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
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 5
                        }
                      }
                    },
                    {
                      "alias": "position_from_a_computed_start",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
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
                        "start": {
                          "operator": "add",
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
                                "source": "profiles",
                                "field": "name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "pattern": {
                                "type": {
                                  "name": "string"
                                },
                                "value": " "
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
                      "alias": "position_in_nullable",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer",
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
                        "pattern": {
                          "source": "profiles",
                          "field": "first_name",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "last_part",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
                        },
                        "index": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "second_part",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": " "
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
                      "alias": "part_of_nullable",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "middle_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
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
                      "alias": "empty_delimiter_gives_one_part",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
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
                      "alias": "long_delimiter",
                      "expression": {
                        "operator": "splitPart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
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
                      "alias": "birth_year",
                      "expression": {
                        "operator": "asInteger",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "splitPart",
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
                          },
                          "delimiter": {
                            "type": {
                              "name": "string"
                            },
                            "value": "-"
                          },
                          "index": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        }
                      }
                    },
                    {
                      "alias": "initial_and_last_name",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "substring",
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
                            "start": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            },
                            "length": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ". "
                          },
                          {
                            "operator": "splitPart",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "profiles",
                              "field": "name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "delimiter": {
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            },
                            "index": {
                              "type": {
                                "name": "integer"
                              },
                              "value": -1
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ReplacePositionSplitPartCornersQuery().Value).TextValue
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
                      "name": "empty_pattern_changes_nothing",
                      "type": "string"
                    },
                    {
                      "name": "removed",
                      "type": "string"
                    },
                    {
                      "name": "pattern_from_a_field",
                      "type": "string"
                    },
                    {
                      "name": "replacement_from_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "string"
                    },
                    {
                      "name": "replace_in_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "position_of_the_empty_pattern",
                      "type": "long"
                    },
                    {
                      "name": "position_from_a_literal_start",
                      "type": "long"
                    },
                    {
                      "name": "position_from_a_computed_start",
                      "type": "long"
                    },
                    {
                      "name": "position_in_nullable",
                      "type": "long"
                    },
                    {
                      "name": "last_part",
                      "type": "string"
                    },
                    {
                      "name": "second_part",
                      "type": "string"
                    },
                    {
                      "name": "part_of_nullable",
                      "type": "string"
                    },
                    {
                      "name": "empty_delimiter_gives_one_part",
                      "type": "string"
                    },
                    {
                      "name": "long_delimiter",
                      "type": "string"
                    },
                    {
                      "name": "birth_year",
                      "type": "long"
                    },
                    {
                      "name": "initial_and_last_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Mr. Ann Lee",
                      "Mr.AnnLee",
                      "Mr. ? Lee",
                      "Dr. Ann Lee",
                      "Ann the Great",
                      "2024/01/31",
                      "1",
                      "8",
                      "8",
                      "1",
                      "Lee",
                      "Ann",
                      "Marie",
                      "Mr. Ann Lee",
                      "",
                      "2020",
                      "A. Lee"
                    ],
                    [
                      "Bob Stone",
                      "BobStone",
                      "? Stone",
                      "Bob Stone",
                      "",
                      "2024/01/31",
                      "1",
                      "0",
                      "0",
                      "",
                      "Stone",
                      "Stone",
                      "",
                      "Bob Stone",
                      "",
                      "2021",
                      "B. Stone"
                    ],
                    [
                      "Stone, Cara Jo",
                      "Stone,CaraJo",
                      "Stone, ? Jo",
                      "Stone, Cara Jo",
                      "the Cara",
                      "2024/01/31",
                      "1",
                      "7",
                      "12",
                      "5",
                      "Jo",
                      "Cara",
                      "Jo",
                      "Stone, Cara Jo",
                      "Cara Jo",
                      "2019",
                      "C. Jo"
                    ],
                    [
                      "Mr. Dan Fox",
                      "Mr.DanFox",
                      "Mr. ? Fox",
                      "Dr. Dan Fox",
                      "d fox",
                      "2024/01/31",
                      "1",
                      "8",
                      "8",
                      "0",
                      "Fox",
                      "Dan",
                      "",
                      "Mr. Dan Fox",
                      "",
                      "2022",
                      "D. Fox"
                    ],
                    [
                      "Eve",
                      "Eve",
                      "?",
                      "Eve",
                      "",
                      "2024/01/31",
                      "1",
                      "0",
                      "0",
                      "",
                      "Eve",
                      "",
                      "",
                      "Eve",
                      "",
                      "2023",
                      "E. Eve"
                    ],
                    [
                      "Fay Rose Day",
                      "FayRoseDay",
                      "? Rose Day",
                      "Fay Rose Day",
                      "Fay",
                      "2024/01/31",
                      "1",
                      "9",
                      "9",
                      "1",
                      "Day",
                      "Rose",
                      "Lee",
                      "Fay Rose Day",
                      "",
                      "2020",
                      "F. Day"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ReplacePositionSplitPartCornersQuery().Result).TextValue
        );
    }
}
