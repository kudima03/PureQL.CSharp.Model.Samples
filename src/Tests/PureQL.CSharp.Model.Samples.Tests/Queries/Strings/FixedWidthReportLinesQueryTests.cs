using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record FixedWidthReportLinesQueryTests
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
                      "name": "members",
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
                              "value": "1"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Stone"
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
                              "value": "01"
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
                            "alias": "middle_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "J"
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
                              "value": "1"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Hannah"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Lee"
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
                              "value": "2"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Anna"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Roy"
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
                              "value": "02"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bell"
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
                              "value": "1"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Tan"
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
                      "subquery": "members",
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
                          "source": "members",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "country",
                      "expression": {
                        "source": "members",
                        "field": "country",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "tier",
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "members",
                          "field": "tier",
                          "type": {
                            "name": "string"
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0"
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
                      "operator": "length",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "operator": "repeat",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "#"
                        },
                        "count": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        }
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
                      "alias": "line",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "padEnd",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "key": "country",
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
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            }
                          },
                          {
                            "operator": "padStart",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
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
                            },
                            "length": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 6
                            },
                            "fill": {
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": " "
                          },
                          {
                            "operator": "repeat",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "type": {
                                "name": "string"
                              },
                              "value": "#"
                            },
                            "count": {
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
                      "alias": "tier",
                      "expression": {
                        "key": "tier",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "name_range",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "min",
                            "type": {
                              "name": "string"
                            },
                            "selector": {
                              "source": "members",
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          {
                            "operator": "max",
                            "type": {
                              "name": "string"
                            },
                            "selector": {
                              "source": "members",
                              "field": "last_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " … "
                        }
                      }
                    },
                    {
                      "alias": "longest_full_name",
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
                            "operator": "concatWith",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "source": "members",
                                "field": "first_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "source": "members",
                                "field": "middle_name",
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                }
                              },
                              {
                                "source": "members",
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "palindromes",
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
                            "operator": "reverse",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "lower",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "members",
                                "field": "first_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          },
                          "right": {
                            "operator": "lower",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "members",
                              "field": "first_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "country_backwards",
                      "expression": {
                        "operator": "reverse",
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
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "padStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
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
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "0"
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
                        "key": "tier",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FixedWidthReportLinesQuery().Value).TextValue
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
                      "name": "line",
                      "type": "string"
                    },
                    {
                      "name": "tier",
                      "type": "string"
                    },
                    {
                      "name": "name_range",
                      "type": "string"
                    },
                    {
                      "name": "longest_full_name",
                      "type": "long"
                    },
                    {
                      "name": "palindromes",
                      "type": "long"
                    },
                    {
                      "name": "country_backwards",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "DE               3 ###",
                      "01",
                      "Day … Stone",
                      "10",
                      "2",
                      "ED"
                    ],
                    [
                      "FR               2 ##",
                      "02",
                      "Bell … Roy",
                      "14",
                      "2",
                      "RF"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FixedWidthReportLinesQuery().Result).TextValue
        );
    }
}
