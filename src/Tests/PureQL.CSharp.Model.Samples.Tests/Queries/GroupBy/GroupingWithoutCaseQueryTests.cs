using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupingWithoutCaseQueryTests
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ace"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ACE"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "meyer"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Meier"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "MEIER"
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
                              "value": "DE"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Maier"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Zed"
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
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "zed"
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
                          }
                        ],
                        [
                          {
                            "alias": "country",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "us"
                            }
                          },
                          {
                            "alias": "nickname",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Max"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Smith"
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
                      "alias": "country",
                      "expression": {
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
                    },
                    {
                      "alias": "nickname",
                      "expression": {
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
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "people",
                          "field": "country",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
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
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 0
                    }
                  },
                  "select": [
                    {
                      "alias": "country",
                      "expression": {
                        "key": "country",
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
                          "source": "people",
                          "field": "country",
                          "type": {
                            "name": "string"
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
                              "source": "people",
                              "field": "country",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "first_last_name",
                      "expression": {
                        "operator": "min",
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
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "upper",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "key": "country",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ": "
                          },
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "nickname_or_country",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "key": "nickname",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "operator": "foldCase",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "key": "country",
                              "type": {
                                "name": "string"
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
                        "key": "country",
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
            new QueryJson(new GroupingWithoutCaseQuery().Value).TextValue
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
                      "name": "country",
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
                      "name": "first_last_name",
                      "type": "string"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "nickname_or_country",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "de",
                      "",
                      "3",
                      "DE, De, de",
                      "maier",
                      "DE: 3",
                      "de"
                    ],
                    [
                      "de",
                      "ace",
                      "2",
                      "DE, de",
                      "meyer",
                      "DE: 2",
                      "ace"
                    ],
                    [
                      "fr",
                      "zed",
                      "2",
                      "FR, fr",
                      "strasse",
                      "FR: 2",
                      "zed"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupingWithoutCaseQuery().Result).TextValue
        );
    }
}
