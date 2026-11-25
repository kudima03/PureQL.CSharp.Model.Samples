using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record StreaksAndFirstValuesQueryTests
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
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": "cancelled"
                    }
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
                      "alias": "order_status",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "placed_on",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      }
                    },
                    {
                      "alias": "days_since_previous",
                      "expression": {
                        "operator": "dateDiffDays",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "operator": "previous",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
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
                        }
                      }
                    },
                    {
                      "alias": "starts_a_streak",
                      "expression": {
                        "operator": "or",
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
                              "operator": "previous",
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_status",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ],
                                "orderBy": [
                                  {
                                    "expression": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "placed_at",
                                      "type": {
                                        "name": "datetime"
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
                            },
                            "right": {
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "operator": "greaterThan",
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
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "right": {
                                "operator": "previous",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "over": {
                                  "partitionBy": [
                                    {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_status",
                                      "type": {
                                        "name": "string"
                                      }
                                    }
                                  ],
                                  "orderBy": [
                                    {
                                      "expression": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "placed_at",
                                        "type": {
                                          "name": "datetime"
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
                      }
                    },
                    {
                      "alias": "first_size",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "large"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "small"
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
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
                        ],
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "latest_size_so_far",
                      "expression": {
                        "operator": "last",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "large"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "small"
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
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
                        ],
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "sizes_so_far",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "large"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "small"
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": " > "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
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
                        ],
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    },
                    {
                      "alias": "total_of_first_order",
                      "expression": {
                        "operator": "first",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
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
                        ],
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "change_from_first",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "first",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
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
                            ],
                            "over": {
                              "partitionBy": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "is_latest",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "next",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_at",
                                  "type": {
                                    "name": "datetime"
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
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "all_below_250_so_far",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "lessThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "250"
                          }
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
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
                          ],
                          "frame": {
                            "preceding": "all"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
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
            new QueryJson(new StreaksAndFirstValuesQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "placed_on",
                      "type": "date"
                    },
                    {
                      "name": "days_since_previous",
                      "type": "long"
                    },
                    {
                      "name": "starts_a_streak",
                      "type": "bool"
                    },
                    {
                      "name": "first_size",
                      "type": "string"
                    },
                    {
                      "name": "latest_size_so_far",
                      "type": "string"
                    },
                    {
                      "name": "sizes_so_far",
                      "type": "string"
                    },
                    {
                      "name": "total_of_first_order",
                      "type": "double"
                    },
                    {
                      "name": "change_from_first",
                      "type": "double"
                    },
                    {
                      "name": "is_latest",
                      "type": "bool"
                    },
                    {
                      "name": "all_below_250_so_far",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "pending",
                      "2024-06-02",
                      "",
                      "True",
                      "small",
                      "small",
                      "small",
                      "50",
                      "0",
                      "False",
                      "True"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "pending",
                      "2024-06-06",
                      "4",
                      "True",
                      "small",
                      "large",
                      "small > large",
                      "50",
                      "50.5",
                      "True",
                      "True"
                    ],
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "shipped",
                      "2024-06-01",
                      "",
                      "True",
                      "large",
                      "large",
                      "large",
                      "100.5",
                      "0",
                      "False",
                      "True"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "shipped",
                      "2024-06-03",
                      "2",
                      "False",
                      "large",
                      "large",
                      "large > large",
                      "100.5",
                      "99.5",
                      "False",
                      "True"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "shipped",
                      "2024-06-05",
                      "2",
                      "False",
                      "large",
                      "large",
                      "large > large > large",
                      "100.5",
                      "199.5",
                      "True",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new StreaksAndFirstValuesQuery().Result).TextValue
        );
    }
}
