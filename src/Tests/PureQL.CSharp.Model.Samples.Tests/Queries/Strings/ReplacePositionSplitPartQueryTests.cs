using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record ReplacePositionSplitPartQueryTests
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
                    "entity": "schema_with_foreign_keys.employees",
                    "alias": "e"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "m",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "m",
                          "field": "employee_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "e",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "position",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      },
                      "pattern": {
                        "type": {
                          "name": "string"
                        },
                        "value": "a"
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
                      "alias": "employee_name",
                      "expression": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "shift_hour",
                      "expression": {
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
                            "source": "e",
                            "field": "employee_shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
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
                      "alias": "shift_second",
                      "expression": {
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
                            "source": "e",
                            "field": "employee_shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "delimiter": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
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
                      "alias": "first_hyphen_in_id",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "e",
                            "field": "employee_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        }
                      }
                    },
                    {
                      "alias": "second_hyphen_in_id",
                      "expression": {
                        "operator": "position",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "e",
                            "field": "employee_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
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
                                "operator": "asString",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "source": "e",
                                  "field": "employee_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              "pattern": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "-"
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
                      "alias": "masked_name",
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
                              "source": "e",
                              "field": "employee_name",
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
                            "value": "***"
                          },
                          {
                            "operator": "substring",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "e",
                              "field": "employee_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "start": {
                              "operator": "position",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "e",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "pattern": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "a"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "manager_name_without_a_and_e",
                      "expression": {
                        "operator": "replace",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "replace",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "source": "m",
                            "field": "employee_name",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "a"
                          },
                          "replacement": {
                            "type": {
                              "name": "string"
                            },
                            "value": ""
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "e"
                        },
                        "replacement": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ReplacePositionSplitPartQuery().Value).TextValue
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
                      "name": "employee_name",
                      "type": "string"
                    },
                    {
                      "name": "shift_hour",
                      "type": "string"
                    },
                    {
                      "name": "shift_second",
                      "type": "string"
                    },
                    {
                      "name": "first_hyphen_in_id",
                      "type": "long"
                    },
                    {
                      "name": "second_hyphen_in_id",
                      "type": "long"
                    },
                    {
                      "name": "masked_name",
                      "type": "string"
                    },
                    {
                      "name": "manager_name_without_a_and_e",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Grace",
                      "09",
                      "00",
                      "9",
                      "14",
                      "G***ace",
                      ""
                    ],
                    [
                      "Hank",
                      "10",
                      "00",
                      "9",
                      "14",
                      "H***ank",
                      "Grc"
                    ],
                    [
                      "Jack",
                      "08",
                      "00",
                      "9",
                      "14",
                      "J***ack",
                      "Hnk"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ReplacePositionSplitPartQuery().Result).TextValue
        );
    }
}
