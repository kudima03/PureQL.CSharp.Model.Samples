using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record ZoneExpressionKindsQueryTests
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
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "audit.logins",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "audit.logins",
                          "field": "login_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
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
                      "alias": "literal_zone",
                      "expression": {
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
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "parameter_zone",
                      "expression": {
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
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Pacific/Auckland"
                        }
                      }
                    },
                    {
                      "alias": "column_zone",
                      "expression": {
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
                    },
                    {
                      "alias": "computed_zone",
                      "expression": {
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
                        },
                        "zone": {
                          "operator": "coalesce",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "operator": "if",
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Europe/Moscow"
                              },
                              "else": {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              }
                            },
                            {
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
                          ]
                        }
                      }
                    },
                    {
                      "alias": "conditional_zone",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
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
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/New_York"
                          }
                        }
                      }
                    },
                    {
                      "alias": "nullable_zone",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Europe/Moscow"
                          },
                          "else": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "null_zone",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "nullable_operand",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "source": "audit.logins",
                          "field": "login_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
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
                    },
                    {
                      "alias": "nullable_operand_and_zone",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "operand": {
                          "source": "audit.logins",
                          "field": "login_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Europe/Moscow"
                          },
                          "else": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        }
                      }
                    },
                    {
                      "alias": "null_operand",
                      "expression": {
                        "operator": "asDate",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          },
                          "value": null
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "literal_operand",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-03-31T22:30:00Z"
                        },
                        "zone": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "guarded",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "date"
                        },
                        "values": [
                          {
                            "operator": "asDate",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "operand": {
                              "source": "audit.logins",
                              "field": "login_at",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Europe/Moscow"
                              },
                              "else": {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              }
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "signup_date",
                            "type": {
                              "name": "date"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "as_text",
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ZoneExpressionKindsQuery().Value).TextValue
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
                      "name": "literal_zone",
                      "type": "date"
                    },
                    {
                      "name": "parameter_zone",
                      "type": "date"
                    },
                    {
                      "name": "column_zone",
                      "type": "date"
                    },
                    {
                      "name": "computed_zone",
                      "type": "date"
                    },
                    {
                      "name": "conditional_zone",
                      "type": "time"
                    },
                    {
                      "name": "nullable_zone",
                      "type": "date"
                    },
                    {
                      "name": "null_zone",
                      "type": "time"
                    },
                    {
                      "name": "nullable_operand",
                      "type": "date"
                    },
                    {
                      "name": "nullable_operand_and_zone",
                      "type": "time"
                    },
                    {
                      "name": "null_operand",
                      "type": "date"
                    },
                    {
                      "name": "literal_operand",
                      "type": "time"
                    },
                    {
                      "name": "guarded",
                      "type": "date"
                    },
                    {
                      "name": "as_text",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "04:30:00",
                      "2024-06-01",
                      "",
                      "2024-06-01",
                      "10:00:00",
                      "",
                      "01:30:00",
                      "2024-06-01",
                      "2024-06-01"
                    ],
                    [
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "04:30:00",
                      "2024-06-01",
                      "",
                      "2024-06-02",
                      "10:30:00",
                      "",
                      "01:30:00",
                      "2024-06-02",
                      "2024-06-01"
                    ],
                    [
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "05:15:00",
                      "",
                      "",
                      "2024-06-03",
                      "",
                      "",
                      "01:30:00",
                      "2021-03-20",
                      "2024-06-02"
                    ],
                    [
                      "2024-05-30",
                      "2024-05-31",
                      "2024-05-30",
                      "2024-05-30",
                      "10:00:00",
                      "2024-05-30",
                      "",
                      "",
                      "",
                      "",
                      "01:30:00",
                      "2019-07-10",
                      "2024-05-30"
                    ],
                    [
                      "2024-06-03",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-03",
                      "03:45:00",
                      "2024-06-03",
                      "",
                      "",
                      "",
                      "",
                      "01:30:00",
                      "2022-11-05",
                      "2024-06-04"
                    ],
                    [
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "03:05:00",
                      "",
                      "",
                      "2024-06-04",
                      "",
                      "",
                      "01:30:00",
                      "2023-02-28",
                      "2024-06-04"
                    ],
                    [
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "17:30:00",
                      "2024-06-01",
                      "",
                      "",
                      "",
                      "",
                      "01:30:00",
                      "2020-01-15",
                      "2024-06-01"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ZoneExpressionKindsQuery().Result).TextValue
        );
    }
}
