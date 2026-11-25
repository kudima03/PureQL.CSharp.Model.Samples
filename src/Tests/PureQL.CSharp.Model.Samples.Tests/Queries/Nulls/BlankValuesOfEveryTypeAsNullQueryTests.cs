using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record BlankValuesOfEveryTypeAsNullQueryTests
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
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "nullIfEqual",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "left": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        },
                        "then": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        }
                      },
                      "right": {
                        "type": {
                          "name": "string"
                        },
                        "value": ""
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "value": null
                    }
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
                      "alias": "display_name",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "left": {
                              "operator": "trim",
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "operand": {
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
                                  "operator": "concat",
                                  "type": {
                                    "name": "string"
                                  },
                                  "values": [
                                    {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": " @"
                                    },
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
                                      "value": " "
                                    }
                                  ]
                                },
                                "else": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string",
                                    "nullable": true
                                  },
                                  "condition": {
                                    "operator": "greaterThan",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_score",
                                      "type": {
                                        "name": "decimal",
                                        "nullable": true
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 5
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "   "
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
                              "removables": [
                                {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": " "
                                }
                              ]
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": ""
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "phone",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_score",
                                  "type": {
                                    "name": "decimal",
                                    "nullable": true
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 29
                                }
                              },
                              "then": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "+1-"
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ]
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": ""
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": ""
                            }
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "left": {
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
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "+2-"
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ]
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": ""
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": ""
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "age",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 25
                        }
                      }
                    },
                    {
                      "alias": "opted_in",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": false
                        }
                      }
                    },
                    {
                      "alias": "trial_start",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_edge_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "date"
                          },
                          "value": "9999-12-31"
                        }
                      }
                    },
                    {
                      "alias": "call_time",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "time",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_edge_time",
                          "type": {
                            "name": "time"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "00:00:00"
                        }
                      }
                    },
                    {
                      "alias": "returned_at",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "datetime",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "last_login",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-01T08:30:00Z"
                        }
                      }
                    },
                    {
                      "alias": "referrer_id",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "uuid",
                          "nullable": true
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
                      }
                    },
                    {
                      "alias": "other_referrer_id",
                      "expression": {
                        "operator": "nullIfEqual",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "uuid"
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "else": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_tenant_id",
                            "type": {
                              "name": "uuid"
                            }
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new BlankValuesOfEveryTypeAsNullQuery().Value).TextValue
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
                      "name": "display_name",
                      "type": "string"
                    },
                    {
                      "name": "phone",
                      "type": "string"
                    },
                    {
                      "name": "age",
                      "type": "long"
                    },
                    {
                      "name": "opted_in",
                      "type": "bool"
                    },
                    {
                      "name": "trial_start",
                      "type": "date"
                    },
                    {
                      "name": "call_time",
                      "type": "time"
                    },
                    {
                      "name": "returned_at",
                      "type": "datetime"
                    },
                    {
                      "name": "referrer_id",
                      "type": "uuid"
                    },
                    {
                      "name": "other_referrer_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "@Ann",
                      "+1-Ann",
                      "30",
                      "True",
                      "2024-02-29",
                      "",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "@Cara",
                      "+1-Cara",
                      "30",
                      "True",
                      "2024-03-10",
                      "02:30:00",
                      "2024-05-30T14:00:00Z",
                      "",
                      ""
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "Eve",
                      "",
                      "",
                      "",
                      "0001-01-01",
                      "",
                      "2024-06-04T07:05:00Z",
                      "00000386-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "@Fay",
                      "+2-Fay",
                      "28",
                      "True",
                      "",
                      "23:59:59",
                      "",
                      "00000386-0000-0000-0000-000000000000",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new BlankValuesOfEveryTypeAsNullQuery().Result).TextValue
        );
    }
}
