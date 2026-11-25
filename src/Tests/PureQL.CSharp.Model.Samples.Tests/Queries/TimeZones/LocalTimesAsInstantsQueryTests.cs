using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalTimesAsInstantsQueryTests
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
                      "name": "user_zones",
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
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
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
                    },
                    {
                      "kind": "inner",
                      "subquery": "user_zones",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "user_zones",
                          "field": "user_id",
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
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "operator": "asDatetime",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-03"
                          },
                          "zone": {
                            "source": "user_zones",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
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
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "operator": "asDatetime",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "operator": "dateAddDays",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-03"
                            },
                            "days": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 5
                            }
                          },
                          "zone": {
                            "source": "user_zones",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "placed_at",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "local_time",
                      "expression": {
                        "operator": "asTime",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "zone": {
                          "source": "user_zones",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "cutoff",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_at",
                            "type": {
                              "name": "datetime"
                            }
                          },
                          "zone": {
                            "source": "user_zones",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        },
                        "time": {
                          "type": {
                            "name": "time"
                          },
                          "value": "18:00:00"
                        },
                        "zone": {
                          "source": "user_zones",
                          "field": "time_zone",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "ships_today",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "operator": "datetimeFromParts",
                          "type": {
                            "name": "datetime"
                          },
                          "date": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "zone": {
                              "source": "user_zones",
                              "field": "time_zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "time": {
                            "type": {
                              "name": "time"
                            },
                            "value": "18:00:00"
                          },
                          "zone": {
                            "source": "user_zones",
                            "field": "time_zone",
                            "type": {
                              "name": "string"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "minutes_to_the_cutoff",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "datetimeDiffSeconds",
                            "type": {
                              "name": "decimal"
                            },
                            "left": {
                              "operator": "datetimeFromParts",
                              "type": {
                                "name": "datetime"
                              },
                              "date": {
                                "operator": "asDate",
                                "type": {
                                  "name": "date"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
                                  }
                                },
                                "zone": {
                                  "source": "user_zones",
                                  "field": "time_zone",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              },
                              "time": {
                                "type": {
                                  "name": "time"
                                },
                                "value": "18:00:00"
                              },
                              "zone": {
                                "source": "user_zones",
                                "field": "time_zone",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 60
                          }
                        ]
                      }
                    },
                    {
                      "alias": "delivery_window_opens_at",
                      "expression": {
                        "operator": "datetimeFromParts",
                        "type": {
                          "name": "datetime"
                        },
                        "date": {
                          "operator": "dateAddDays",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "operator": "asDate",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "zone": {
                              "source": "user_zones",
                              "field": "time_zone",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "days": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        },
                        "time": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "zone": {
                          "source": "user_zones",
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
                      "column": "placed_at"
                    },
                    {
                      "column": "order_id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalTimesAsInstantsQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "local_time",
                      "type": "time"
                    },
                    {
                      "name": "cutoff",
                      "type": "datetime"
                    },
                    {
                      "name": "ships_today",
                      "type": "bool"
                    },
                    {
                      "name": "minutes_to_the_cutoff",
                      "type": "double"
                    },
                    {
                      "name": "delivery_window_opens_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-03T12:00:00Z",
                      "08:00:00",
                      "2024-06-03T22:00:00Z",
                      "True",
                      "600",
                      "2024-06-04T14:00:00Z"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04T13:00:00Z",
                      "09:00:00",
                      "2024-06-04T22:00:00Z",
                      "True",
                      "540",
                      "2024-06-05T13:00:00Z"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05T14:00:00Z",
                      "10:00:00",
                      "2024-06-05T22:00:00Z",
                      "True",
                      "480",
                      "2024-06-06T13:00:00Z"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00Z",
                      "00:00:00",
                      "2024-06-07T09:00:00Z",
                      "True",
                      "1080",
                      "2024-06-08T02:30:00Z"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalTimesAsInstantsQuery().Result).TextValue
        );
    }
}
