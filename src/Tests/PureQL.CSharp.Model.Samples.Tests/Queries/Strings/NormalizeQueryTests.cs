using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record NormalizeQueryTests
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
                    "operator": "or",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
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
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "form": "NFKC"
                        },
                        "right": {
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
                              "type": {
                                "name": "string"
                              },
                              "value": "\uff21\uff2e\uff2e"
                            }
                          },
                          "form": "NFKC"
                        }
                      },
                      {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
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
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "form": "NFKC"
                        },
                        "right": {
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
                              "type": {
                                "name": "string"
                              },
                              "value": "\uff26\uff21\uff39"
                            }
                          },
                          "form": "NFKC"
                        }
                      }
                    ]
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
                      "alias": "signature",
                      "expression": {
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
                              "value": " Lefe\u0300vre"
                            }
                          ]
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "name_is_composed",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "form": "NFC"
                        }
                      }
                    },
                    {
                      "alias": "signature_is_composed",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
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
                              "value": " Lefe\u0300vre"
                            }
                          ]
                        },
                        "right": {
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
                                "value": " Lefe\u0300vre"
                              }
                            ]
                          },
                          "form": "NFC"
                        }
                      }
                    },
                    {
                      "alias": "code_points_when_decomposed",
                      "expression": {
                        "operator": "length",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
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
                                "value": " Lefe\u0300vre"
                              }
                            ]
                          },
                          "form": "NFD"
                        }
                      }
                    },
                    {
                      "alias": "employee_name",
                      "expression": {
                        "operator": "normalize",
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
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "search_key",
                      "expression": {
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "form": "NFKC"
                      }
                    },
                    {
                      "alias": "plain_label",
                      "expression": {
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
                              "type": {
                                "name": "string"
                              },
                              "value": "\u2116 "
                            },
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        },
                        "form": "NFKC"
                      }
                    },
                    {
                      "alias": "fully_decomposed",
                      "expression": {
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
                              "value": " Lefe\u0300vre"
                            }
                          ]
                        },
                        "form": "NFKD"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "form": "NFC"
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
            new QueryJson(new NormalizeQuery().Value).TextValue
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
                      "name": "signature",
                      "type": "string"
                    },
                    {
                      "name": "name_is_composed",
                      "type": "bool"
                    },
                    {
                      "name": "signature_is_composed",
                      "type": "bool"
                    },
                    {
                      "name": "code_points_when_decomposed",
                      "type": "long"
                    },
                    {
                      "name": "employee_name",
                      "type": "string"
                    },
                    {
                      "name": "search_key",
                      "type": "string"
                    },
                    {
                      "name": "plain_label",
                      "type": "string"
                    },
                    {
                      "name": "fully_decomposed",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann Lef\u00e8vre",
                      "True",
                      "False",
                      "12",
                      "Grace",
                      "ann",
                      "No Ann",
                      "Ann Lefe\u0300vre"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay Lef\u00e8vre",
                      "True",
                      "False",
                      "12",
                      "",
                      "fay",
                      "No Fay",
                      "Fay Lefe\u0300vre"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NormalizeQuery().Result).TextValue
        );
    }
}
