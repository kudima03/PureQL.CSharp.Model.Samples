using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record FoldCaseInATotalQueryTests
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "DE"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Gold"
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
                              "value": "Mac"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "de"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "GOLD"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Meyer"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "De"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Silver"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "brandt"
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
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "FR"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "gold"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dupont"
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
                        ],
                        [
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bronze"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Zimmer"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Zim"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "fr"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "silver"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Martin"
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
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "people"
                  },
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "foldCase",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "source": "people",
                        "field": "country",
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
                        "type": {
                          "name": "string"
                        },
                        "value": "FR"
                      }
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "gold",
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
                            "operator": "foldCase",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "people",
                              "field": "tier",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "gold"
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_last_name",
                      "expression": {
                        "operator": "foldCase",
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
                      "alias": "first_folded_last_name",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
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
                      "alias": "tiers",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "tier",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "people",
                                "field": "tier",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "any_mc",
                      "expression": {
                        "operator": "any",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "startsWith",
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
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "mc"
                          }
                        }
                      }
                    },
                    {
                      "alias": "most_common_country",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "people",
                            "field": "country",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "nicknames",
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
                          "right": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FoldCaseInATotalQuery().Value).TextValue
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
                      "name": "gold",
                      "type": "long"
                    },
                    {
                      "name": "first_last_name",
                      "type": "string"
                    },
                    {
                      "name": "first_folded_last_name",
                      "type": "string"
                    },
                    {
                      "name": "tiers",
                      "type": "string"
                    },
                    {
                      "name": "any_mc",
                      "type": "bool"
                    },
                    {
                      "name": "most_common_country",
                      "type": "string"
                    },
                    {
                      "name": "nicknames",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2",
                      "mccoy",
                      "brandt",
                      "bronze, gold, silver",
                      "True",
                      "de",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FoldCaseInATotalQuery().Result).TextValue
        );
    }
}
