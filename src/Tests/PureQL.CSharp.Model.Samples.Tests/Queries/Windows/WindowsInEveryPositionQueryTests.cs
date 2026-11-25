using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record WindowsInEveryPositionQueryTests
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
                      "name": "numbered",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
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
                            "alias": "order_tenant_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "position",
                            "expression": {
                              "operator": "rowNumber",
                              "type": {
                                "name": "integer"
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_tenant_id",
                                    "type": {
                                      "name": "uuid"
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
                                    },
                                    "direction": "desc"
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
                          {
                            "alias": "previous_total",
                            "expression": {
                              "operator": "previous",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_tenant_id",
                                    "type": {
                                      "name": "uuid"
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
                        ],
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "rowNumber",
                              "type": {
                                "name": "integer"
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_tenant_id",
                                    "type": {
                                      "name": "uuid"
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
                                    },
                                    "direction": "desc"
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
                        "pagination": {
                          "take": 1000
                        }
                      }
                    },
                    {
                      "name": "first_and_last",
                      "query": {
                        "unionAll": [
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "where": {
                              "operator": "equal",
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
                                "value": "shipped"
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
                                "alias": "position",
                                "expression": {
                                  "operator": "rowNumber",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "over": {
                                    "partitionBy": [
                                      {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_tenant_id",
                                        "type": {
                                          "name": "uuid"
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
                            ]
                          },
                          {
                            "from": {
                              "entity": "schema_with_foreign_keys.orders"
                            },
                            "where": {
                              "operator": "equal",
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
                                "value": "pending"
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
                                "alias": "position",
                                "expression": {
                                  "operator": "rowNumber",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "over": {
                                    "partitionBy": [
                                      {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_tenant_id",
                                        "type": {
                                          "name": "uuid"
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
                                        },
                                        "direction": "desc"
                                      },
                                      {
                                        "expression": {
                                          "source": "schema_with_foreign_keys.orders",
                                          "field": "order_id",
                                          "type": {
                                            "name": "uuid"
                                          }
                                        },
                                        "direction": "desc"
                                      }
                                    ]
                                  }
                                }
                              }
                            ]
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
                      "subquery": "numbered",
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
                          "source": "numbered",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "in",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    "list": {
                      "subquery": "first_and_last",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
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
                      "alias": "order_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "position",
                      "expression": {
                        "source": "numbered",
                        "field": "position",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "running_share",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
                            "operator": "sum",
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
                            "over": {
                              "partitionBy": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_tenant_id",
                                  "type": {
                                    "name": "uuid"
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
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "left": {
                              "operator": "sum",
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
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  }
                                ]
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "label",
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
                            "operator": "rank",
                            "type": {
                              "name": "integer"
                            },
                            "over": {
                              "partitionBy": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              ],
                              "orderBy": [
                                {
                                  "expression": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "direction": "desc"
                                }
                              ]
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "largest"
                        },
                        "else": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "#"
                            },
                            {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "denseRank",
                                "type": {
                                  "name": "integer"
                                },
                                "over": {
                                  "partitionBy": [
                                    {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_tenant_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  ],
                                  "orderBy": [
                                    {
                                      "expression": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_total",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      "direction": "desc"
                                    }
                                  ]
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "previous_or_average",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "previous",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "over": {
                              "partitionBy": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_tenant_id",
                                  "type": {
                                    "name": "uuid"
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
                          {
                            "operator": "average",
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
                            "over": {
                              "partitionBy": [
                                {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              ]
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "above_the_one_before",
                      "expression": {
                        "operator": "greaterThan",
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
                          "operator": "previous",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_tenant_id",
                                "type": {
                                  "name": "uuid"
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
                      "alias": "half",
                      "expression": {
                        "operator": "ntile",
                        "type": {
                          "name": "integer"
                        },
                        "buckets": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
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
                  ],
                  "distinctBy": [
                    "order_tenant_id",
                    "half"
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "percentRank",
                        "type": {
                          "name": "decimal"
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            }
                          ]
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
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
                              },
                              "direction": "desc"
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
                  "pagination": {
                    "take": 3
                  }
                }
                """
            ).TextValue,
            new QueryJson(new WindowsInEveryPositionQuery().Value).TextValue
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
                      "name": "order_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "position",
                      "type": "long"
                    },
                    {
                      "name": "running_share",
                      "type": "double"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "previous_or_average",
                      "type": "double"
                    },
                    {
                      "name": "above_the_one_before",
                      "type": "bool"
                    },
                    {
                      "name": "half",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "2",
                      "0.7490636704119851",
                      "largest",
                      "200.25",
                      "False",
                      "2"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "largest",
                      "50",
                      "True",
                      "2"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "00000386-0000-0000-0000-000000000000",
                      "1",
                      "1",
                      "#2",
                      "300",
                      "False",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WindowsInEveryPositionQuery().Result).TextValue
        );
    }
}
