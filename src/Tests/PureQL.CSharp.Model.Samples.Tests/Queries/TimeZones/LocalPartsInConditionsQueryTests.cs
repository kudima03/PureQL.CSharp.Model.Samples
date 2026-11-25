using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalPartsInConditionsQueryTests
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
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.users"
                        },
                        "select": [
                          {
                            "alias": "id",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "time_zone",
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          {
                            "alias": "created_at",
                            "expression": {
                              "operator": "datetimeFromParts",
                              "type": {
                                "name": "datetime"
                              },
                              "date": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "time": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "shift_start",
                                "type": {
                                  "name": "time"
                                }
                              }
                            }
                          },
                          {
                            "alias": "birth_date",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
                          {
                            "alias": "preferred_call_time",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "shift_start",
                              "type": {
                                "name": "time"
                              }
                            }
                          },
                          {
                            "alias": "is_active",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "members"
                  },
                  "where": {
                    "source": "members",
                    "field": "is_active",
                    "type": {
                      "name": "boolean"
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "members",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "local_hour_now",
                      "expression": {
                        "operator": "hour",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2020-01-15T01:00:00Z"
                        },
                        "zone": {
                          "source": "members",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "can_be_called_now",
                      "expression": {
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
                              "operator": "hour",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2020-01-15T01:00:00Z"
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            "right": {
                              "operator": "hour",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "members",
                                "field": "preferred_call_time",
                                "type": {
                                  "name": "time"
                                }
                              }
                            }
                          },
                          {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "hour",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2020-01-15T01:00:00Z"
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 21
                            }
                          },
                          {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "dayOfWeek",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2020-01-15T01:00:00Z"
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 6
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "greeting",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "lessThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "hour",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2020-01-15T01:00:00Z"
                            },
                            "zone": {
                              "source": "members",
                              "field": "time_zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 12
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Good morning"
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Good afternoon"
                        }
                      }
                    },
                    {
                      "alias": "signed_up_this_local_year",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "members",
                            "field": "created_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "right": {
                          "operator": "year",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2020-01-15T01:00:00Z"
                          },
                          "zone": {
                            "source": "members",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "birthday_today",
                      "expression": {
                        "operator": "and",
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
                              "operator": "month",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "members",
                                "field": "birth_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            "right": {
                              "operator": "month",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2020-01-15T01:00:00Z"
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "day",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "members",
                                "field": "birth_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            "right": {
                              "operator": "day",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2020-01-15T01:00:00Z"
                              },
                              "zone": {
                                "source": "members",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalPartsInConditionsQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "local_hour_now",
                      "type": "long"
                    },
                    {
                      "name": "can_be_called_now",
                      "type": "bool"
                    },
                    {
                      "name": "greeting",
                      "type": "string"
                    },
                    {
                      "name": "signed_up_this_local_year",
                      "type": "bool"
                    },
                    {
                      "name": "birthday_today",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "20",
                      "True",
                      "Good afternoon",
                      "True",
                      "False"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "20",
                      "True",
                      "Good afternoon",
                      "False",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "10",
                      "False",
                      "Good morning",
                      "False",
                      "False"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "10",
                      "True",
                      "Good morning",
                      "True",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalPartsInConditionsQuery().Result).TextValue
        );
    }
}
