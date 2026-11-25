using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record RoundedIntegersFromSubqueriesQueryTests
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
                    },
                    {
                      "name": "bands",
                      "query": {
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
                        ]
                      }
                    },
                    {
                      "name": "totals",
                      "query": {
                        "from": {
                          "subquery": "lines"
                        },
                        "aggregate": [
                          {
                            "alias": "lines_in_hundreds",
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
                                "value": -2
                              }
                            }
                          },
                          {
                            "alias": "units_in_thousands",
                            "expression": {
                              "operator": "truncate",
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
                                "value": -3
                              }
                            }
                          },
                          {
                            "alias": "largest_in_tens",
                            "expression": {
                              "operator": "round",
                              "type": {
                                "name": "integer",
                                "nullable": true
                              },
                              "operand": {
                                "operator": "max",
                                "type": {
                                  "name": "integer",
                                  "nullable": true
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
                          },
                          {
                            "alias": "median_stays_decimal",
                            "expression": {
                              "operator": "round",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              },
                              "operand": {
                                "operator": "median",
                                "type": {
                                  "name": "decimal",
                                  "nullable": true
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
                          },
                          {
                            "alias": "rounded_units",
                            "expression": {
                              "operator": "sum",
                              "type": {
                                "name": "integer"
                              },
                              "selector": {
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
                          }
                        ]
                      }
                    },
                    {
                      "name": "steps",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "lines"
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "lines",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "units",
                              "expression": {
                                "source": "lines",
                                "field": "quantity",
                                "type": {
                                  "name": "integer"
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "steps"
                          },
                          "where": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "modulo",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "source": "steps",
                                "field": "units",
                                "type": {
                                  "name": "integer"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 10
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "steps",
                                "field": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "units",
                              "expression": {
                                "operator": "truncate",
                                "type": {
                                  "name": "integer"
                                },
                                "operand": {
                                  "source": "steps",
                                  "field": "units",
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
                          ]
                        },
                        "maxDepth": 3
                      }
                    }
                  ],
                  "from": {
                    "subquery": "lines"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "bands",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
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
                        },
                        "right": {
                          "source": "bands",
                          "field": "quantity_band",
                          "type": {
                            "name": "integer"
                          }
                        }
                      }
                    },
                    {
                      "kind": "inner",
                      "subquery": "totals",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "steps",
                      "on": {
                        "operator": "and",
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
                              "source": "steps",
                              "field": "id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "lines",
                              "field": "id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "steps",
                              "field": "units",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
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
                        ]
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "lines",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "running_units_in_tens",
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
                          },
                          "over": {
                            "partitionBy": [
                              {
                                "source": "lines",
                                "field": "product_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            ],
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "lines",
                                  "field": "id",
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
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "rank_in_tens",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "rowNumber",
                          "type": {
                            "name": "integer"
                          },
                          "over": {
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "lines",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            ]
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
                      "alias": "share_of_rounded_units",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "values": [
                          {
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
                          },
                          {
                            "operator": "nullIfEqual",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "left": {
                              "source": "totals",
                              "field": "rounded_units",
                              "type": {
                                "name": "integer"
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
                      "alias": "band_units",
                      "expression": {
                        "source": "bands",
                        "field": "units_in_hundreds",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "step_units",
                      "expression": {
                        "source": "steps",
                        "field": "units",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundedIntegersFromSubqueriesQuery().Value).TextValue
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
                      "name": "running_units_in_tens",
                      "type": "long"
                    },
                    {
                      "name": "rank_in_tens",
                      "type": "long"
                    },
                    {
                      "name": "share_of_rounded_units",
                      "type": "double"
                    },
                    {
                      "name": "band_units",
                      "type": "long"
                    },
                    {
                      "name": "step_units",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000325-0000-0000-0000-000000000000",
                      "60",
                      "0",
                      "0.05084745762711865",
                      "300",
                      "60"
                    ],
                    [
                      "00000326-0000-0000-0000-000000000000",
                      "60",
                      "0",
                      "0.05084745762711865",
                      "300",
                      "60"
                    ],
                    [
                      "00000327-0000-0000-0000-000000000000",
                      "70",
                      "0",
                      "0.059322033898305086",
                      "300",
                      ""
                    ],
                    [
                      "00000328-0000-0000-0000-000000000000",
                      "70",
                      "0",
                      "0.059322033898305086",
                      "300",
                      ""
                    ],
                    [
                      "00000329-0000-0000-0000-000000000000",
                      "130",
                      "0",
                      "0.059322033898305086",
                      "300",
                      ""
                    ],
                    [
                      "0000032a-0000-0000-0000-000000000000",
                      "160",
                      "0",
                      "0.0847457627118644",
                      "300",
                      "100"
                    ],
                    [
                      "0000032b-0000-0000-0000-000000000000",
                      "170",
                      "0",
                      "0.0847457627118644",
                      "300",
                      "100"
                    ],
                    [
                      "0000032c-0000-0000-0000-000000000000",
                      "170",
                      "0",
                      "0.09322033898305085",
                      "300",
                      ""
                    ],
                    [
                      "0000032d-0000-0000-0000-000000000000",
                      "270",
                      "0",
                      "0.1271186440677966",
                      "300",
                      ""
                    ],
                    [
                      "0000032e-0000-0000-0000-000000000000",
                      "310",
                      "10",
                      "0.1271186440677966",
                      "300",
                      ""
                    ],
                    [
                      "0000032f-0000-0000-0000-000000000000",
                      "430",
                      "10",
                      "0.22033898305084745",
                      "300",
                      "260"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundedIntegersFromSubqueriesQuery().Result).TextValue
        );
    }
}
