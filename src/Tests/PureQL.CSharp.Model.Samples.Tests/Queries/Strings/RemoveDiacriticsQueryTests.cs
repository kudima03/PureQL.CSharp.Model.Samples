using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record RemoveDiacriticsQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "contains",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "operator": "removeDiacritics",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    "pattern": {
                      "operator": "removeDiacritics",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\u00c1"
                        }
                      }
                    }
                  },
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
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "label_without_accents",
                      "expression": {
                        "operator": "removeDiacritics",
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
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": " from \u0141\u00f3d\u017a"
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "label_search_key",
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
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
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "type": {
                                  "name": "string"
                                },
                                "value": " from \u0141\u00f3d\u017a"
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "label_has_accents",
                      "expression": {
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "normalize",
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
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "type": {
                                  "name": "string"
                                },
                                "value": " from \u0141\u00f3d\u017a"
                              }
                            ]
                          },
                          "form": "NFC"
                        },
                        "right": {
                          "operator": "removeDiacritics",
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
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "type": {
                                  "name": "string"
                                },
                                "value": " from \u0141\u00f3d\u017a"
                              }
                            ]
                          }
                        }
                      }
                    },
                    {
                      "alias": "employee_name_without_accents",
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "is_ann",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "removeDiacritics",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "foldCase",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        },
                        "right": {
                          "operator": "removeDiacritics",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "foldCase",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "type": {
                                "name": "string"
                              },
                              "value": "\u00c1NN"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "wide_search_key",
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
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
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_name",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": " \ufb01anc\u00e9e"
                                }
                              ]
                            }
                          },
                          "form": "NFKC"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
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
            new QueryJson(new RemoveDiacriticsQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "label_without_accents",
                      "type": "string"
                    },
                    {
                      "name": "label_search_key",
                      "type": "string"
                    },
                    {
                      "name": "label_has_accents",
                      "type": "bool"
                    },
                    {
                      "name": "employee_name_without_accents",
                      "type": "string"
                    },
                    {
                      "name": "is_ann",
                      "type": "bool"
                    },
                    {
                      "name": "wide_search_key",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "Ann from \u0141odz",
                      "ann from \u0142odz",
                      "True",
                      "Grace",
                      "True",
                      "ann fiancee"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Cara",
                      "Cara from \u0141odz",
                      "cara from \u0142odz",
                      "True",
                      "Iris",
                      "False",
                      "cara fiancee"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "Dan from \u0141odz",
                      "dan from \u0142odz",
                      "True",
                      "Jack",
                      "False",
                      "dan fiancee"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay",
                      "Fay from \u0141odz",
                      "fay from \u0142odz",
                      "True",
                      "",
                      "False",
                      "fay fiancee"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RemoveDiacriticsQuery().Result).TextValue
        );
    }
}
