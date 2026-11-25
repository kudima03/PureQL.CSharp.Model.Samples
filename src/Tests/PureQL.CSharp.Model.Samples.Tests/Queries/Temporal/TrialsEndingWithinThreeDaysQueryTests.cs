using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record TrialsEndingWithinThreeDaysQueryTests
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
                      "name": "trials",
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
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-05-28"
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
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-05-25"
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
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-01"
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
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-05-27"
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
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-05-30"
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
                      "kind": "left",
                      "subquery": "trials",
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
                          "source": "trials",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "dateAddDays",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "operand": {
                            "source": "trials",
                            "field": "trial_start",
                            "type": {
                              "name": "date",
                              "nullable": true
                            }
                          },
                          "days": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 14
                          }
                        },
                        "right": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-10"
                        }
                      },
                      {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "dateDiffDays",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "left": {
                            "operator": "dateAddDays",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "operand": {
                              "source": "trials",
                              "field": "trial_start",
                              "type": {
                                "name": "date",
                                "nullable": true
                              }
                            },
                            "days": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 14
                            }
                          },
                          "right": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-10"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 3
                        }
                      }
                    ]
                  },
                  "select": [
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
                      "alias": "trial_ends_on",
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "trials",
                          "field": "trial_start",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        },
                        "days": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 14
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "dateAddDays",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "trials",
                          "field": "trial_start",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        },
                        "days": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 14
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TrialsEndingWithinThreeDaysQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "trial_ends_on",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Dan",
                      "2024-06-10"
                    ],
                    [
                      "Ann",
                      "2024-06-11"
                    ],
                    [
                      "Fay",
                      "2024-06-13"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TrialsEndingWithinThreeDaysQuery().Result).TextValue
        );
    }
}
