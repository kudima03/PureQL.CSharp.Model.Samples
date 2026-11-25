using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record MonthsInALocalCalendarQueryTests
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
                            "alias": "user_active",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean"
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
                            "alias": "member_since",
                            "expression": {
                              "operator": "asDatetime",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "zone": {
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
                    "field": "user_active",
                    "type": {
                      "name": "boolean"
                    }
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "members",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "member_since",
                      "expression": {
                        "source": "members",
                        "field": "member_since",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "whole_months_as_a_member",
                      "expression": {
                        "operator": "datetimeDiffMonths",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-08T12:00:00Z"
                        },
                        "right": {
                          "source": "members",
                          "field": "member_since",
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
                      }
                    },
                    {
                      "alias": "whole_years_as_a_member",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "datetimeDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-08T12:00:00Z"
                          },
                          "right": {
                            "source": "members",
                            "field": "member_since",
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
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        }
                      }
                    },
                    {
                      "alias": "next_monthly_renewal",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "member_since",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "months": {
                          "operator": "add",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "operator": "datetimeDiffMonths",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2024-06-08T12:00:00Z"
                              },
                              "right": {
                                "source": "members",
                                "field": "member_since",
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
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          ]
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
                      "alias": "next_anniversary",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "member_since",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "months": {
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "operator": "add",
                              "type": {
                                "name": "integer"
                              },
                              "values": [
                                {
                                  "operator": "integerDivide",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "left": {
                                    "operator": "datetimeDiffMonths",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "left": {
                                      "type": {
                                        "name": "datetime"
                                      },
                                      "value": "2024-06-08T12:00:00Z"
                                    },
                                    "right": {
                                      "source": "members",
                                      "field": "member_since",
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
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 12
                                  }
                                },
                                {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 1
                                }
                              ]
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 12
                            }
                          ]
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
                      "alias": "renews_this_local_month",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "datetimeAddMonths",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "source": "members",
                            "field": "member_since",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "months": {
                            "operator": "add",
                            "type": {
                              "name": "integer"
                            },
                            "values": [
                              {
                                "operator": "datetimeDiffMonths",
                                "type": {
                                  "name": "integer"
                                },
                                "left": {
                                  "type": {
                                    "name": "datetime"
                                  },
                                  "value": "2024-06-08T12:00:00Z"
                                },
                                "right": {
                                  "source": "members",
                                  "field": "member_since",
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
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 1
                              }
                            ]
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
                          "operator": "datetimeAddMonths",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "operator": "startOfMonth",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-08T12:00:00Z"
                            },
                            "zone": {
                              "source": "members",
                              "field": "time_zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "months": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
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
                      "alias": "trial_ended_at",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "source": "members",
                          "field": "member_since",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
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
                  ],
                  "orderBy": [
                    {
                      "column": "next_monthly_renewal"
                    },
                    {
                      "column": "user_id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MonthsInALocalCalendarQuery().Value).TextValue
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
                      "name": "member_since",
                      "type": "datetime"
                    },
                    {
                      "name": "whole_months_as_a_member",
                      "type": "long"
                    },
                    {
                      "name": "whole_years_as_a_member",
                      "type": "long"
                    },
                    {
                      "name": "next_monthly_renewal",
                      "type": "datetime"
                    },
                    {
                      "name": "next_anniversary",
                      "type": "datetime"
                    },
                    {
                      "name": "renews_this_local_month",
                      "type": "bool"
                    },
                    {
                      "name": "trial_ended_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2019-07-10T04:00:00Z",
                      "58",
                      "4",
                      "2024-06-10T04:00:00Z",
                      "2024-07-10T04:00:00Z",
                      "True",
                      "2019-08-10T04:00:00Z"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "2020-01-14T15:00:00Z",
                      "52",
                      "4",
                      "2024-06-14T15:00:00Z",
                      "2025-01-14T15:00:00Z",
                      "True",
                      "2020-02-14T15:00:00Z"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2020-01-15T05:00:00Z",
                      "52",
                      "4",
                      "2024-06-15T04:00:00Z",
                      "2025-01-15T05:00:00Z",
                      "True",
                      "2020-02-15T05:00:00Z"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2022-11-04T15:00:00Z",
                      "19",
                      "1",
                      "2024-07-04T15:00:00Z",
                      "2024-11-04T15:00:00Z",
                      "False",
                      "2022-12-04T15:00:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MonthsInALocalCalendarQuery().Result).TextValue
        );
    }
}
