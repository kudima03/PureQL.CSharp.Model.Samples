using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record AsStringOfEveryTypeQueryTests
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
                      "name": "profiles",
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
                            "alias": "verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-01-15"
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-03T10:20:30Z"
                            }
                          },
                          {
                            "alias": "referrer_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000002-0000-0000-0000-000000000000"
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
                            "alias": "verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-05-31T23:59:59Z"
                            }
                          },
                          {
                            "alias": "referrer_id",
                            "expression": {
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "verified",
                            "expression": {
                              "type": {
                                "name": "boolean",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-02-29"
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "referrer_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000001-0000-0000-0000-000000000000"
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
                            "alias": "verified",
                            "expression": {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          },
                          {
                            "alias": "trial_start",
                            "expression": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-07-01"
                            }
                          },
                          {
                            "alias": "last_seen",
                            "expression": {
                              "type": {
                                "name": "datetime"
                              },
                              "value": "2024-06-04T00:00:00Z"
                            }
                          },
                          {
                            "alias": "referrer_id",
                            "expression": {
                              "type": {
                                "name": "uuid",
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "profiles",
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
                          "source": "profiles",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "integer",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "floor",
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "integer_literal",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -42
                        }
                      }
                    },
                    {
                      "alias": "decimal_literal",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "19.9"
                        }
                      }
                    },
                    {
                      "alias": "decimal_computed",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 8
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "boolean",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    },
                    {
                      "alias": "boolean_nullable",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "verified",
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "date",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
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
                    {
                      "alias": "date_nullable",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "trial_start",
                          "type": {
                            "name": "date",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "time",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        }
                      }
                    },
                    {
                      "alias": "datetime",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
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
                    {
                      "alias": "datetime_nullable",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "last_seen",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "uuid",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "alias": "uuid_nullable",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "referrer_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "parameter",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-06T15:00:00Z"
                        }
                      }
                    },
                    {
                      "alias": "date_of_a_datetime",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "last_login",
                            "type": {
                              "name": "datetime"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "time_of_a_datetime",
                      "expression": {
                        "operator": "asString",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asTime",
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "operand": {
                            "source": "profiles",
                            "field": "last_seen",
                            "type": {
                              "name": "datetime",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "length_of_an_integer",
                      "expression": {
                        "operator": "length",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "operator": "floor",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "short_id",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_id",
                            "type": {
                              "name": "uuid"
                            }
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
                          "value": 8
                        }
                      }
                    },
                    {
                      "alias": "name_and_age",
                      "expression": {
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
                            "value": " ("
                          },
                          {
                            "operator": "coalesce",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "operator": "asString",
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "operand": {
                                  "operator": "floor",
                                  "type": {
                                    "name": "integer",
                                    "nullable": true
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_score",
                                    "type": {
                                      "name": "decimal",
                                      "nullable": true
                                    }
                                  }
                                }
                              },
                              {
                                "type": {
                                  "name": "string"
                                },
                                "value": "?"
                              }
                            ]
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ")"
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AsStringOfEveryTypeQuery().Value).TextValue
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
                      "name": "integer",
                      "type": "string"
                    },
                    {
                      "name": "integer_literal",
                      "type": "string"
                    },
                    {
                      "name": "decimal_literal",
                      "type": "string"
                    },
                    {
                      "name": "decimal_computed",
                      "type": "string"
                    },
                    {
                      "name": "boolean",
                      "type": "string"
                    },
                    {
                      "name": "boolean_nullable",
                      "type": "string"
                    },
                    {
                      "name": "date",
                      "type": "string"
                    },
                    {
                      "name": "date_nullable",
                      "type": "string"
                    },
                    {
                      "name": "time",
                      "type": "string"
                    },
                    {
                      "name": "datetime",
                      "type": "string"
                    },
                    {
                      "name": "datetime_nullable",
                      "type": "string"
                    },
                    {
                      "name": "uuid",
                      "type": "string"
                    },
                    {
                      "name": "uuid_nullable",
                      "type": "string"
                    },
                    {
                      "name": "parameter",
                      "type": "string"
                    },
                    {
                      "name": "date_of_a_datetime",
                      "type": "string"
                    },
                    {
                      "name": "time_of_a_datetime",
                      "type": "string"
                    },
                    {
                      "name": "length_of_an_integer",
                      "type": "long"
                    },
                    {
                      "name": "short_id",
                      "type": "string"
                    },
                    {
                      "name": "name_and_age",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "30",
                      "-42",
                      "19.9",
                      "0.125",
                      "true",
                      "true",
                      "2020-01-15",
                      "2024-01-15",
                      "09:00:00",
                      "2024-06-01T08:30:00Z",
                      "2024-06-03T10:20:30Z",
                      "00000001-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00Z",
                      "2024-06-01",
                      "10:20:30",
                      "2",
                      "00000001",
                      "Ann (30)"
                    ],
                    [
                      "",
                      "-42",
                      "19.9",
                      "0.125",
                      "false",
                      "false",
                      "2021-03-20",
                      "",
                      "10:00:00",
                      "2024-06-02T09:15:00Z",
                      "2024-05-31T23:59:59Z",
                      "00000002-0000-0000-0000-000000000000",
                      "",
                      "2024-06-06T15:00:00Z",
                      "2024-06-02",
                      "23:59:59",
                      "",
                      "00000002",
                      "Bob (?)"
                    ],
                    [
                      "30",
                      "-42",
                      "19.9",
                      "0.125",
                      "true",
                      "",
                      "2019-07-10",
                      "2024-02-29",
                      "09:00:00",
                      "2024-05-30T14:00:00Z",
                      "",
                      "00000003-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00Z",
                      "2024-05-30",
                      "",
                      "2",
                      "00000003",
                      "Cara (30)"
                    ],
                    [
                      "",
                      "-42",
                      "19.9",
                      "0.125",
                      "true",
                      "true",
                      "2022-11-05",
                      "2024-07-01",
                      "11:30:00",
                      "2024-06-03T18:45:00Z",
                      "2024-06-04T00:00:00Z",
                      "00000004-0000-0000-0000-000000000000",
                      "",
                      "2024-06-06T15:00:00Z",
                      "2024-06-03",
                      "00:00:00",
                      "",
                      "00000004",
                      "Dan (?)"
                    ],
                    [
                      "10",
                      "-42",
                      "19.9",
                      "0.125",
                      "false",
                      "",
                      "2023-02-28",
                      "",
                      "08:00:00",
                      "2024-06-04T07:05:00Z",
                      "",
                      "00000005-0000-0000-0000-000000000000",
                      "",
                      "2024-06-06T15:00:00Z",
                      "2024-06-04",
                      "",
                      "2",
                      "00000005",
                      "Eve (10)"
                    ],
                    [
                      "28",
                      "-42",
                      "19.9",
                      "0.125",
                      "true",
                      "",
                      "2020-01-15",
                      "",
                      "09:00:00",
                      "2024-06-01T08:30:00Z",
                      "",
                      "00000006-0000-0000-0000-000000000000",
                      "",
                      "2024-06-06T15:00:00Z",
                      "2024-06-01",
                      "",
                      "2",
                      "00000006",
                      "Fay (28)"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AsStringOfEveryTypeQuery().Result).TextValue
        );
    }
}
