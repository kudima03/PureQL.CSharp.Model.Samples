using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record RegularExpressionDialectQueryTests
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
                      "name": "samples",
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
                            "alias": "text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "abc"
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
                            "alias": "text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "aaabbcc ababc bdd"
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
                            "alias": "text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2024-06 3.14 12 \t\r\n"
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
                            "alias": "text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": ".*+?()[]{}|^$\\ /- (a) ]"
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
                            "alias": "text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "éééß 𝒳𝒳 word a\nc"
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
                            "alias": "text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "abc\n"
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
                      "subquery": "samples",
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
                          "source": "samples",
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
                      "alias": "empty_pattern",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
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
                      "alias": "literal",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "abc"
                        }
                      }
                    },
                    {
                      "alias": "any_code_point",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a.c"
                        }
                      }
                    },
                    {
                      "alias": "class",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[abc]"
                        }
                      }
                    },
                    {
                      "alias": "negated_class",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[^abc]"
                        }
                      }
                    },
                    {
                      "alias": "ranges",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[a-zA-Z0-9_]"
                        }
                      }
                    },
                    {
                      "alias": "escapes_in_a_class",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[\\]\\[\\\\\\^\\-]"
                        }
                      }
                    },
                    {
                      "alias": "hyphen_first_and_last",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[-a][a-]"
                        }
                      }
                    },
                    {
                      "alias": "shorthands_in_a_class",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[\\d\\s,;]"
                        }
                      }
                    },
                    {
                      "alias": "metacharacters_in_a_class",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[*+?{}().|$]"
                        }
                      }
                    },
                    {
                      "alias": "digit_word_space",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\d\\w\\s"
                        }
                      }
                    },
                    {
                      "alias": "negated_shorthands",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\D\\W\\S"
                        }
                      }
                    },
                    {
                      "alias": "control_characters",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\t\\r\\n"
                        }
                      }
                    },
                    {
                      "alias": "anchors",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^abc$"
                        }
                      }
                    },
                    {
                      "alias": "word_boundaries",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\bword\\b\\B"
                        }
                      }
                    },
                    {
                      "alias": "alternation",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "cat|dog|"
                        }
                      }
                    },
                    {
                      "alias": "capturing_group",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "(ab)c"
                        }
                      }
                    },
                    {
                      "alias": "non_capturing_group",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "(?:ab)c"
                        }
                      }
                    },
                    {
                      "alias": "nested_groups",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "((a)(?:b(c)))"
                        }
                      }
                    },
                    {
                      "alias": "empty_group",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "()"
                        }
                      }
                    },
                    {
                      "alias": "star_plus_optional",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a*b+c?"
                        }
                      }
                    },
                    {
                      "alias": "counted",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a{3}b{2,}c{2,5}"
                        }
                      }
                    },
                    {
                      "alias": "lazy",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a*?b+?c??d{2,3}?"
                        }
                      }
                    },
                    {
                      "alias": "quantified_group",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "(?:ab){2}(c|d)+"
                        }
                      }
                    },
                    {
                      "alias": "quantified_class",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[0-9]{4}-[0-9]{2}"
                        }
                      }
                    },
                    {
                      "alias": "quantified_escape",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\d+\\.\\d*"
                        }
                      }
                    },
                    {
                      "alias": "escaped_metacharacters",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\.\\*\\+\\?\\(\\)\\[\\]\\{\\}\\|\\^\\$\\\\"
                        }
                      }
                    },
                    {
                      "alias": "escaped_slash",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\/[\\/\\-\\.]"
                        }
                      }
                    },
                    {
                      "alias": "literal_closing_parenthesis_escaped",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\(a\\)"
                        }
                      }
                    },
                    {
                      "alias": "non_ascii_literals",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "é+ß"
                        }
                      }
                    },
                    {
                      "alias": "astral_literal",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "samples",
                          "field": "text",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "𝒳{2}"
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
            new QueryJson(new RegularExpressionDialectQuery().Value).TextValue
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
                      "name": "empty_pattern",
                      "type": "bool"
                    },
                    {
                      "name": "literal",
                      "type": "bool"
                    },
                    {
                      "name": "any_code_point",
                      "type": "bool"
                    },
                    {
                      "name": "class",
                      "type": "bool"
                    },
                    {
                      "name": "negated_class",
                      "type": "bool"
                    },
                    {
                      "name": "ranges",
                      "type": "bool"
                    },
                    {
                      "name": "escapes_in_a_class",
                      "type": "bool"
                    },
                    {
                      "name": "hyphen_first_and_last",
                      "type": "bool"
                    },
                    {
                      "name": "shorthands_in_a_class",
                      "type": "bool"
                    },
                    {
                      "name": "metacharacters_in_a_class",
                      "type": "bool"
                    },
                    {
                      "name": "digit_word_space",
                      "type": "bool"
                    },
                    {
                      "name": "negated_shorthands",
                      "type": "bool"
                    },
                    {
                      "name": "control_characters",
                      "type": "bool"
                    },
                    {
                      "name": "anchors",
                      "type": "bool"
                    },
                    {
                      "name": "word_boundaries",
                      "type": "bool"
                    },
                    {
                      "name": "alternation",
                      "type": "bool"
                    },
                    {
                      "name": "capturing_group",
                      "type": "bool"
                    },
                    {
                      "name": "non_capturing_group",
                      "type": "bool"
                    },
                    {
                      "name": "nested_groups",
                      "type": "bool"
                    },
                    {
                      "name": "empty_group",
                      "type": "bool"
                    },
                    {
                      "name": "star_plus_optional",
                      "type": "bool"
                    },
                    {
                      "name": "counted",
                      "type": "bool"
                    },
                    {
                      "name": "lazy",
                      "type": "bool"
                    },
                    {
                      "name": "quantified_group",
                      "type": "bool"
                    },
                    {
                      "name": "quantified_class",
                      "type": "bool"
                    },
                    {
                      "name": "quantified_escape",
                      "type": "bool"
                    },
                    {
                      "name": "escaped_metacharacters",
                      "type": "bool"
                    },
                    {
                      "name": "escaped_slash",
                      "type": "bool"
                    },
                    {
                      "name": "literal_closing_parenthesis_escaped",
                      "type": "bool"
                    },
                    {
                      "name": "non_ascii_literals",
                      "type": "bool"
                    },
                    {
                      "name": "astral_literal",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "True",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True",
                      "True",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "True",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RegularExpressionDialectQuery().Result).TextValue
        );
    }
}
