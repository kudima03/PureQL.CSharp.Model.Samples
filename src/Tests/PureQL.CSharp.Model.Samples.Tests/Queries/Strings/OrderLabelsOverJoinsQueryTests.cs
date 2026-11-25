using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record OrderLabelsOverJoinsQueryTests
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Stone"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "1234"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Day"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bob"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "10115"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Lee"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2040"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fox"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dan"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "94105"
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
                              "value": "00000005-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bell"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Eve"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "80331"
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
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Roy"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "750"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "coupons",
                      "rows": [
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000065-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "SPRING10"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000067-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "GOLD"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000068-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ],
                        [
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000069-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "VIP"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "profiles",
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
                          "source": "profiles",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "coupons",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "coupons",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "startsWith",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "operator": "padStart",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "source": "profiles",
                        "field": "postal_code",
                        "type": {
                          "name": "string"
                        }
                      },
                      "length": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 5
                      },
                      "fill": {
                        "type": {
                          "name": "string"
                        },
                        "value": "0"
                      }
                    },
                    "pattern": {
                      "type": {
                        "name": "string"
                      },
                      "value": "0"
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "customer",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "profiles",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        }
                      }
                    },
                    {
                      "alias": "status_and_coupon",
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "coupons",
                            "field": "code",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ":"
                        }
                      }
                    },
                    {
                      "alias": "coupon_label",
                      "expression": {
                        "operator": "padEnd",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "coupons",
                          "field": "code",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 10
                        },
                        "fill": {
                          "type": {
                            "name": "string"
                          },
                          "value": "-"
                        }
                      }
                    },
                    {
                      "alias": "total_with_two_decimals",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "truncate",
                              "type": {
                                "name": "integer"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "."
                          },
                          {
                            "operator": "padEnd",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
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
                                  "operator": "round",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "digits": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 2
                                  }
                                }
                              },
                              "delimiter": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "."
                              },
                              "index": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 2
                              }
                            },
                            "length": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            "fill": {
                              "type": {
                                "name": "string"
                              },
                              "value": "0"
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "concatWith",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "profiles",
                            "field": "last_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "source": "profiles",
                            "field": "first_name",
                            "type": {
                              "name": "string"
                            }
                          }
                        ],
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderLabelsOverJoinsQuery().Value).TextValue
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
                      "name": "customer",
                      "type": "string"
                    },
                    {
                      "name": "status_and_coupon",
                      "type": "string"
                    },
                    {
                      "name": "coupon_label",
                      "type": "string"
                    },
                    {
                      "name": "total_with_two_decimals",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "Lee, Cara",
                      "cancelled",
                      "",
                      "75.25"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "Lee, Cara",
                      "shipped:VIP",
                      "VIP-------",
                      "300.00"
                    ],
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "Stone, Ann",
                      "shipped:SPRING10",
                      "SPRING10--",
                      "100.50"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "Stone, Ann",
                      "pending",
                      "",
                      "50.00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderLabelsOverJoinsQuery().Result).TextValue
        );
    }
}
