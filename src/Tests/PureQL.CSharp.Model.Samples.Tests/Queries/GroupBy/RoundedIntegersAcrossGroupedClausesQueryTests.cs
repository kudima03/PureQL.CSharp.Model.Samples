using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record RoundedIntegersAcrossGroupedClausesQueryTests
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
                      "name": "lines",
                      "rows": [
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000321-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000c9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000322-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000066-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000ca-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 15
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000323-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cb-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 18
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000324-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cc-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 25
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000325-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000c9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 61
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000326-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000006a-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000ca-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 62
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000327-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cb-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 65
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 6
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000328-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000066-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cc-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 66
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000329-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000c9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 68
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000032a-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000ca-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 100
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000032b-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cb-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 104
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000032c-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000006a-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cc-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 107
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 8
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000032d-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000c9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 145
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 12
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000032e-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "order_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000066-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000ca-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 149
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 9
                            }
                          }
                        ],
                        [
                          {
                            "alias": "id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "0000032f-0000-0000-0000-000000000000"
                            }
                          },
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
                            "alias": "product_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000000cb-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 260
                            }
                          },
                          {
                            "alias": "returned_quantity",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 30
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "lines"
                  },
                  "groupBy": [
                    {
                      "alias": "quantity_band",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "round",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "source": "lines",
                          "field": "quantity",
                          "type": {
                            "name": "integer"
                          }
                        }
                      },
                      "digits": {
                        "type": {
                          "name": "integer"
                        },
                        "value": -2
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 100
                    }
                  },
                  "select": [
                    {
                      "alias": "quantity_band",
                      "expression": {
                        "key": "quantity_band",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "next_band",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "key": "quantity_band",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        ]
                      }
                    },
                    {
                      "alias": "units_in_hundreds",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -2
                        }
                      }
                    },
                    {
                      "alias": "rounded_units",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
                          }
                        }
                      }
                    },
                    {
                      "alias": "largest_rounded",
                      "expression": {
                        "operator": "max",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -1
                          }
                        }
                      }
                    },
                    {
                      "alias": "lines_in_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "lines_of_all_in_tens",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          },
                          "over": "all"
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "rounded_bands",
                      "expression": {
                        "operator": "countDistinct",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "digits": {
                            "type": {
                              "name": "integer"
                            },
                            "value": -2
                          }
                        }
                      }
                    },
                    {
                      "alias": "average_stays_decimal",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "average",
                          "type": {
                            "name": "decimal"
                          },
                          "selector": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "sum",
                          "type": {
                            "name": "integer"
                          },
                          "selector": {
                            "source": "lines",
                            "field": "quantity",
                            "type": {
                              "name": "integer"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -2
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "column": "quantity_band"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundedIntegersAcrossGroupedClausesQuery().Value).TextValue
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
                      "name": "quantity_band",
                      "type": "long"
                    },
                    {
                      "name": "next_band",
                      "type": "long"
                    },
                    {
                      "name": "units_in_hundreds",
                      "type": "long"
                    },
                    {
                      "name": "rounded_units",
                      "type": "long"
                    },
                    {
                      "name": "largest_rounded",
                      "type": "long"
                    },
                    {
                      "name": "lines_in_tens",
                      "type": "long"
                    },
                    {
                      "name": "lines_of_all_in_tens",
                      "type": "long"
                    },
                    {
                      "name": "rounded_bands",
                      "type": "long"
                    },
                    {
                      "name": "average_stays_decimal",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "60",
                      "70",
                      "300",
                      "330",
                      "70",
                      "10",
                      "20",
                      "1",
                      "60"
                    ],
                    [
                      "100",
                      "110",
                      "300",
                      "310",
                      "110",
                      "0",
                      "20",
                      "1",
                      "100"
                    ],
                    [
                      "140",
                      "150",
                      "300",
                      "300",
                      "150",
                      "0",
                      "20",
                      "1",
                      "150"
                    ],
                    [
                      "260",
                      "270",
                      "300",
                      "260",
                      "260",
                      "0",
                      "20",
                      "1",
                      "260"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new RoundedIntegersAcrossGroupedClausesQuery().Result
            ).TextValue
        );
    }
}
