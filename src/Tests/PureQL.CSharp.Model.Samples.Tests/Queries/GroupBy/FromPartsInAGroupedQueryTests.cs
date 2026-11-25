using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record FromPartsInAGroupedQueryTests
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
                  "groupBy": [
                    {
                      "alias": "signup_month",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "month": {
                          "operator": "month",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "shift_hour",
                      "expression": {
                        "operator": "timeFromParts",
                        "type": {
                          "name": "time"
                        },
                        "hour": {
                          "operator": "hour",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "minute": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "second": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "lessThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "dateFromParts",
                      "type": {
                        "name": "date"
                      },
                      "year": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        }
                      },
                      "month": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 1
                      },
                      "day": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 1
                      }
                    },
                    "right": {
                      "type": {
                        "name": "date"
                      },
                      "value": "2022-01-01"
                    }
                  },
                  "select": [
                    {
                      "alias": "signup_month",
                      "expression": {
                        "key": "signup_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "shift_hour",
                      "expression": {
                        "key": "shift_hour",
                        "type": {
                          "name": "time"
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
                      "alias": "first_call",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "key": "signup_month",
                          "type": {
                            "name": "date"
                          }
                        },
                        "time": {
                          "key": "shift_hour",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "latest_login_year_end",
                      "expression": {
                        "operator": "dateFromParts",
                        "type": {
                          "name": "date"
                        },
                        "year": {
                          "operator": "max",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "operator": "year",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "last_login",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          }
                        },
                        "month": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        },
                        "day": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 31
                        }
                      }
                    },
                    {
                      "alias": "signed_up_mid_month",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          },
                          "right": {
                            "operator": "dateFromParts",
                            "type": {
                              "name": "date"
                            },
                            "year": {
                              "operator": "year",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            "month": {
                              "operator": "month",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            "day": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 15
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "signup_month",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "expression": {
                        "key": "shift_hour",
                        "type": {
                          "name": "time"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FromPartsInAGroupedQuery().Value).TextValue
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
                      "name": "signup_month",
                      "type": "date"
                    },
                    {
                      "name": "shift_hour",
                      "type": "time"
                    },
                    {
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "first_call",
                      "type": "datetime"
                    },
                    {
                      "name": "latest_login_year_end",
                      "type": "date"
                    },
                    {
                      "name": "signed_up_mid_month",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2019-07-01",
                      "09:00:00",
                      "1",
                      "2019-07-01T09:00:00Z",
                      "2024-12-31",
                      "0"
                    ],
                    [
                      "2020-01-01",
                      "09:00:00",
                      "2",
                      "2020-01-01T09:00:00Z",
                      "2024-12-31",
                      "2"
                    ],
                    [
                      "2021-03-01",
                      "10:00:00",
                      "1",
                      "2021-03-01T10:00:00Z",
                      "2024-12-31",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FromPartsInAGroupedQuery().Result).TextValue
        );
    }
}
