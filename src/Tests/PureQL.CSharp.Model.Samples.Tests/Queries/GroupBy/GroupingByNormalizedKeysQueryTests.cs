using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupingByNormalizedKeysQueryTests
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
                              "value": "Jos\u00e9"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Mu\u0308ller"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Jose\u0301"
                            }
                          }
                        ],
                        [
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
                          }
                        ],
                        [
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "\uff2d\u00dcLLER"
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
                          }
                        ],
                        [
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
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
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
                          }
                        ],
                        [
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Smith"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Zed"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "people"
                  },
                  "groupBy": [
                    {
                      "alias": "last_name",
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
                            "source": "people",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "form": "NFKC"
                      }
                    },
                    {
                      "alias": "nickname",
                      "expression": {
                        "operator": "normalize",
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
                        },
                        "form": "NFC"
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
                      "alias": "last_name",
                      "expression": {
                        "key": "last_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "nickname",
                      "expression": {
                        "key": "nickname",
                        "type": {
                          "name": "string",
                          "nullable": true
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
                      "alias": "spellings",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "form": "NFC"
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " | "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "normalize",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "people",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "form": "NFC"
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "stored_decomposed",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "people",
                            "field": "last_name",
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
                              "source": "people",
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "form": "NFC"
                          }
                        }
                      }
                    },
                    {
                      "alias": "longest_decomposed",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
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
                              "source": "people",
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "form": "NFD"
                          }
                        }
                      }
                    },
                    {
                      "alias": "key_decomposed",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "key": "last_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "form": "NFD"
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
                        "key": "last_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "nickname",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupingByNormalizedKeysQuery().Value).TextValue
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
                      "name": "last_name",
                      "type": "string"
                    },
                    {
                      "name": "nickname",
                      "type": "string"
                    },
                    {
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "spellings",
                      "type": "string"
                    },
                    {
                      "name": "stored_decomposed",
                      "type": "long"
                    },
                    {
                      "name": "longest_decomposed",
                      "type": "long"
                    },
                    {
                      "name": "key_decomposed",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "m\u00fcller",
                      "",
                      "2",
                      "M\u00dcLLER | \uff2d\u00dcLLER",
                      "0",
                      "7",
                      "mu\u0308ller"
                    ],
                    [
                      "m\u00fcller",
                      "Jos\u00e9",
                      "2",
                      "M\u00fcller",
                      "1",
                      "7",
                      "mu\u0308ller"
                    ],
                    [
                      "strasse",
                      "",
                      "2",
                      "STRASSE | Stra\u00dfe",
                      "0",
                      "7",
                      "strasse"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupingByNormalizedKeysQuery().Result).TextValue
        );
    }
}
