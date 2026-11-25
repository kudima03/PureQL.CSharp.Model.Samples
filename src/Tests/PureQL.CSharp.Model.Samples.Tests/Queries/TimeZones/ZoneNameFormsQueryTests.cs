using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record ZoneNameFormsQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "alias": "zone_0",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Moscow"
                        }
                      }
                    },
                    {
                      "alias": "zone_1",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "America/New_York"
                        }
                      }
                    },
                    {
                      "alias": "zone_2",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "America/Argentina/Buenos_Aires"
                        }
                      }
                    },
                    {
                      "alias": "zone_3",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "America/North_Dakota/New_Salem"
                        }
                      }
                    },
                    {
                      "alias": "zone_4",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "America/Port-au-Prince"
                        }
                      }
                    },
                    {
                      "alias": "zone_5",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "America/Blanc-Sablon"
                        }
                      }
                    },
                    {
                      "alias": "zone_6",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Asia/Ho_Chi_Minh"
                        }
                      }
                    },
                    {
                      "alias": "zone_7",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Africa/Dar_es_Salaam"
                        }
                      }
                    },
                    {
                      "alias": "zone_8",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Antarctica/DumontDUrville"
                        }
                      }
                    },
                    {
                      "alias": "zone_9",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Asia/Kolkata"
                        }
                      }
                    },
                    {
                      "alias": "zone_10",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Asia/Kathmandu"
                        }
                      }
                    },
                    {
                      "alias": "zone_11",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Australia/Lord_Howe"
                        }
                      }
                    },
                    {
                      "alias": "zone_12",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Pacific/Apia"
                        }
                      }
                    },
                    {
                      "alias": "zone_13",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Kyiv"
                        }
                      }
                    },
                    {
                      "alias": "zone_14",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Europe/Ljubljana"
                        }
                      }
                    },
                    {
                      "alias": "zone_15",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Etc/GMT+1"
                        }
                      }
                    },
                    {
                      "alias": "zone_16",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Etc/GMT+12"
                        }
                      }
                    },
                    {
                      "alias": "zone_17",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Etc/GMT-1"
                        }
                      }
                    },
                    {
                      "alias": "zone_18",
                      "expression": {
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
                          "type": {
                            "name": "string"
                          },
                          "value": "Etc/GMT-14"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ZoneNameFormsQuery().Value).TextValue
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
                      "name": "zone_0",
                      "type": "date"
                    },
                    {
                      "name": "zone_1",
                      "type": "date"
                    },
                    {
                      "name": "zone_2",
                      "type": "date"
                    },
                    {
                      "name": "zone_3",
                      "type": "date"
                    },
                    {
                      "name": "zone_4",
                      "type": "date"
                    },
                    {
                      "name": "zone_5",
                      "type": "date"
                    },
                    {
                      "name": "zone_6",
                      "type": "date"
                    },
                    {
                      "name": "zone_7",
                      "type": "date"
                    },
                    {
                      "name": "zone_8",
                      "type": "date"
                    },
                    {
                      "name": "zone_9",
                      "type": "date"
                    },
                    {
                      "name": "zone_10",
                      "type": "date"
                    },
                    {
                      "name": "zone_11",
                      "type": "date"
                    },
                    {
                      "name": "zone_12",
                      "type": "date"
                    },
                    {
                      "name": "zone_13",
                      "type": "date"
                    },
                    {
                      "name": "zone_14",
                      "type": "date"
                    },
                    {
                      "name": "zone_15",
                      "type": "date"
                    },
                    {
                      "name": "zone_16",
                      "type": "date"
                    },
                    {
                      "name": "zone_17",
                      "type": "date"
                    },
                    {
                      "name": "zone_18",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-06-01",
                      "2024-05-31",
                      "2024-06-01",
                      "2024-06-02"
                    ],
                    [
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-03",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-02",
                      "2024-06-01",
                      "2024-06-02",
                      "2024-06-03"
                    ],
                    [
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-04",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-03",
                      "2024-06-04"
                    ],
                    [
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-05",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-04",
                      "2024-06-05"
                    ],
                    [
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-06",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-05",
                      "2024-06-06"
                    ],
                    [
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-07",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-07",
                      "2024-06-07",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-06",
                      "2024-06-07"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ZoneNameFormsQuery().Result).TextValue
        );
    }
}
