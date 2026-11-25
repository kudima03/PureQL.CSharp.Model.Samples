using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record RoundOfAddNestedThirtyLevelsDeepQueryTests
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
                      "alias": "rounded_total",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "add",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "round",
                              "type": {
                                "name": "decimal"
                              },
                              "operand": {
                                "operator": "add",
                                "type": {
                                  "name": "decimal"
                                },
                                "values": [
                                  {
                                    "operator": "round",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "operand": {
                                      "operator": "add",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "values": [
                                        {
                                          "operator": "round",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "operand": {
                                            "operator": "add",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "round",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "operand": {
                                                  "operator": "add",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "operator": "round",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "operand": {
                                                        "operator": "add",
                                                        "type": {
                                                          "name": "decimal"
                                                        },
                                                        "values": [
                                                          {
                                                            "operator": "round",
                                                            "type": {
                                                              "name": "decimal"
                                                            },
                                                            "operand": {
                                                              "operator": "add",
                                                              "type": {
                                                                "name": "decimal"
                                                              },
                                                              "values": [
                                                                {
                                                                  "operator": "round",
                                                                  "type": {
                                                                    "name": "decimal"
                                                                  },
                                                                  "operand": {
                                                                    "operator": "add",
                                                                    "type": {
                                                                      "name": "decimal"
                                                                    },
                                                                    "values": [
                                                                      {
                                                                        "operator": "round",
                                                                        "type": {
                                                                          "name": "decimal"
                                                                        },
                                                                        "operand": {
                                                                          "operator": "add",
                                                                          "type": {
                                                                            "name": "decimal"
                                                                          },
                                                                          "values": [
                                                                            {
                                                                              "operator": "round",
                                                                              "type": {
                                                                                "name": "decimal"
                                                                              },
                                                                              "operand": {
                                                                                "operator": "add",
                                                                                "type": {
                                                                                  "name": "decimal"
                                                                                },
                                                                                "values": [
                                                                                  {
                                                                                    "operator": "round",
                                                                                    "type": {
                                                                                      "name": "decimal"
                                                                                    },
                                                                                    "operand": {
                                                                                      "operator": "add",
                                                                                      "type": {
                                                                                        "name": "decimal"
                                                                                      },
                                                                                      "values": [
                                                                                        {
                                                                                          "operator": "round",
                                                                                          "type": {
                                                                                            "name": "decimal"
                                                                                          },
                                                                                          "operand": {
                                                                                            "operator": "add",
                                                                                            "type": {
                                                                                              "name": "decimal"
                                                                                            },
                                                                                            "values": [
                                                                                              {
                                                                                                "operator": "round",
                                                                                                "type": {
                                                                                                  "name": "decimal"
                                                                                                },
                                                                                                "operand": {
                                                                                                  "operator": "add",
                                                                                                  "type": {
                                                                                                    "name": "decimal"
                                                                                                  },
                                                                                                  "values": [
                                                                                                    {
                                                                                                      "operator": "round",
                                                                                                      "type": {
                                                                                                        "name": "decimal"
                                                                                                      },
                                                                                                      "operand": {
                                                                                                        "operator": "add",
                                                                                                        "type": {
                                                                                                          "name": "decimal"
                                                                                                        },
                                                                                                        "values": [
                                                                                                          {
                                                                                                            "operator": "round",
                                                                                                            "type": {
                                                                                                              "name": "decimal"
                                                                                                            },
                                                                                                            "operand": {
                                                                                                              "operator": "add",
                                                                                                              "type": {
                                                                                                                "name": "decimal"
                                                                                                              },
                                                                                                              "values": [
                                                                                                                {
                                                                                                                  "operator": "round",
                                                                                                                  "type": {
                                                                                                                    "name": "decimal"
                                                                                                                  },
                                                                                                                  "operand": {
                                                                                                                    "operator": "add",
                                                                                                                    "type": {
                                                                                                                      "name": "decimal"
                                                                                                                    },
                                                                                                                    "values": [
                                                                                                                      {
                                                                                                                        "operator": "round",
                                                                                                                        "type": {
                                                                                                                          "name": "decimal"
                                                                                                                        },
                                                                                                                        "operand": {
                                                                                                                          "operator": "add",
                                                                                                                          "type": {
                                                                                                                            "name": "decimal"
                                                                                                                          },
                                                                                                                          "values": [
                                                                                                                            {
                                                                                                                              "operator": "round",
                                                                                                                              "type": {
                                                                                                                                "name": "decimal"
                                                                                                                              },
                                                                                                                              "operand": {
                                                                                                                                "operator": "add",
                                                                                                                                "type": {
                                                                                                                                  "name": "decimal"
                                                                                                                                },
                                                                                                                                "values": [
                                                                                                                                  {
                                                                                                                                    "operator": "round",
                                                                                                                                    "type": {
                                                                                                                                      "name": "decimal"
                                                                                                                                    },
                                                                                                                                    "operand": {
                                                                                                                                      "operator": "add",
                                                                                                                                      "type": {
                                                                                                                                        "name": "decimal"
                                                                                                                                      },
                                                                                                                                      "values": [
                                                                                                                                        {
                                                                                                                                          "operator": "round",
                                                                                                                                          "type": {
                                                                                                                                            "name": "decimal"
                                                                                                                                          },
                                                                                                                                          "operand": {
                                                                                                                                            "operator": "add",
                                                                                                                                            "type": {
                                                                                                                                              "name": "decimal"
                                                                                                                                            },
                                                                                                                                            "values": [
                                                                                                                                              {
                                                                                                                                                "operator": "round",
                                                                                                                                                "type": {
                                                                                                                                                  "name": "decimal"
                                                                                                                                                },
                                                                                                                                                "operand": {
                                                                                                                                                  "operator": "add",
                                                                                                                                                  "type": {
                                                                                                                                                    "name": "decimal"
                                                                                                                                                  },
                                                                                                                                                  "values": [
                                                                                                                                                    {
                                                                                                                                                      "operator": "round",
                                                                                                                                                      "type": {
                                                                                                                                                        "name": "decimal"
                                                                                                                                                      },
                                                                                                                                                      "operand": {
                                                                                                                                                        "operator": "add",
                                                                                                                                                        "type": {
                                                                                                                                                          "name": "decimal"
                                                                                                                                                        },
                                                                                                                                                        "values": [
                                                                                                                                                          {
                                                                                                                                                            "operator": "round",
                                                                                                                                                            "type": {
                                                                                                                                                              "name": "decimal"
                                                                                                                                                            },
                                                                                                                                                            "operand": {
                                                                                                                                                              "operator": "add",
                                                                                                                                                              "type": {
                                                                                                                                                                "name": "decimal"
                                                                                                                                                              },
                                                                                                                                                              "values": [
                                                                                                                                                                {
                                                                                                                                                                  "operator": "round",
                                                                                                                                                                  "type": {
                                                                                                                                                                    "name": "decimal"
                                                                                                                                                                  },
                                                                                                                                                                  "operand": {
                                                                                                                                                                    "operator": "add",
                                                                                                                                                                    "type": {
                                                                                                                                                                      "name": "decimal"
                                                                                                                                                                    },
                                                                                                                                                                    "values": [
                                                                                                                                                                      {
                                                                                                                                                                        "operator": "round",
                                                                                                                                                                        "type": {
                                                                                                                                                                          "name": "decimal"
                                                                                                                                                                        },
                                                                                                                                                                        "operand": {
                                                                                                                                                                          "operator": "add",
                                                                                                                                                                          "type": {
                                                                                                                                                                            "name": "decimal"
                                                                                                                                                                          },
                                                                                                                                                                          "values": [
                                                                                                                                                                            {
                                                                                                                                                                              "operator": "round",
                                                                                                                                                                              "type": {
                                                                                                                                                                                "name": "decimal"
                                                                                                                                                                              },
                                                                                                                                                                              "operand": {
                                                                                                                                                                                "operator": "add",
                                                                                                                                                                                "type": {
                                                                                                                                                                                  "name": "decimal"
                                                                                                                                                                                },
                                                                                                                                                                                "values": [
                                                                                                                                                                                  {
                                                                                                                                                                                    "operator": "round",
                                                                                                                                                                                    "type": {
                                                                                                                                                                                      "name": "decimal"
                                                                                                                                                                                    },
                                                                                                                                                                                    "operand": {
                                                                                                                                                                                      "operator": "add",
                                                                                                                                                                                      "type": {
                                                                                                                                                                                        "name": "decimal"
                                                                                                                                                                                      },
                                                                                                                                                                                      "values": [
                                                                                                                                                                                        {
                                                                                                                                                                                          "operator": "round",
                                                                                                                                                                                          "type": {
                                                                                                                                                                                            "name": "decimal"
                                                                                                                                                                                          },
                                                                                                                                                                                          "operand": {
                                                                                                                                                                                            "operator": "add",
                                                                                                                                                                                            "type": {
                                                                                                                                                                                              "name": "decimal"
                                                                                                                                                                                            },
                                                                                                                                                                                            "values": [
                                                                                                                                                                                              {
                                                                                                                                                                                                "operator": "round",
                                                                                                                                                                                                "type": {
                                                                                                                                                                                                  "name": "decimal"
                                                                                                                                                                                                },
                                                                                                                                                                                                "operand": {
                                                                                                                                                                                                  "operator": "add",
                                                                                                                                                                                                  "type": {
                                                                                                                                                                                                    "name": "decimal"
                                                                                                                                                                                                  },
                                                                                                                                                                                                  "values": [
                                                                                                                                                                                                    {
                                                                                                                                                                                                      "operator": "round",
                                                                                                                                                                                                      "type": {
                                                                                                                                                                                                        "name": "decimal"
                                                                                                                                                                                                      },
                                                                                                                                                                                                      "operand": {
                                                                                                                                                                                                        "operator": "add",
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
                                                                                                                                                                                                            "type": {
                                                                                                                                                                                                              "name": "decimal"
                                                                                                                                                                                                            },
                                                                                                                                                                                                            "value": "2.5"
                                                                                                                                                                                                          }
                                                                                                                                                                                                        ]
                                                                                                                                                                                                      },
                                                                                                                                                                                                      "digits": {
                                                                                                                                                                                                        "type": {
                                                                                                                                                                                                          "name": "integer"
                                                                                                                                                                                                        },
                                                                                                                                                                                                        "value": -1
                                                                                                                                                                                                      }
                                                                                                                                                                                                    },
                                                                                                                                                                                                    {
                                                                                                                                                                                                      "type": {
                                                                                                                                                                                                        "name": "decimal"
                                                                                                                                                                                                      },
                                                                                                                                                                                                      "value": "2.5"
                                                                                                                                                                                                    }
                                                                                                                                                                                                  ]
                                                                                                                                                                                                },
                                                                                                                                                                                                "digits": {
                                                                                                                                                                                                  "type": {
                                                                                                                                                                                                    "name": "integer"
                                                                                                                                                                                                  },
                                                                                                                                                                                                  "value": -1
                                                                                                                                                                                                }
                                                                                                                                                                                              },
                                                                                                                                                                                              {
                                                                                                                                                                                                "type": {
                                                                                                                                                                                                  "name": "decimal"
                                                                                                                                                                                                },
                                                                                                                                                                                                "value": "2.5"
                                                                                                                                                                                              }
                                                                                                                                                                                            ]
                                                                                                                                                                                          },
                                                                                                                                                                                          "digits": {
                                                                                                                                                                                            "type": {
                                                                                                                                                                                              "name": "integer"
                                                                                                                                                                                            },
                                                                                                                                                                                            "value": -1
                                                                                                                                                                                          }
                                                                                                                                                                                        },
                                                                                                                                                                                        {
                                                                                                                                                                                          "type": {
                                                                                                                                                                                            "name": "decimal"
                                                                                                                                                                                          },
                                                                                                                                                                                          "value": "2.5"
                                                                                                                                                                                        }
                                                                                                                                                                                      ]
                                                                                                                                                                                    },
                                                                                                                                                                                    "digits": {
                                                                                                                                                                                      "type": {
                                                                                                                                                                                        "name": "integer"
                                                                                                                                                                                      },
                                                                                                                                                                                      "value": -1
                                                                                                                                                                                    }
                                                                                                                                                                                  },
                                                                                                                                                                                  {
                                                                                                                                                                                    "type": {
                                                                                                                                                                                      "name": "decimal"
                                                                                                                                                                                    },
                                                                                                                                                                                    "value": "2.5"
                                                                                                                                                                                  }
                                                                                                                                                                                ]
                                                                                                                                                                              },
                                                                                                                                                                              "digits": {
                                                                                                                                                                                "type": {
                                                                                                                                                                                  "name": "integer"
                                                                                                                                                                                },
                                                                                                                                                                                "value": -1
                                                                                                                                                                              }
                                                                                                                                                                            },
                                                                                                                                                                            {
                                                                                                                                                                              "type": {
                                                                                                                                                                                "name": "decimal"
                                                                                                                                                                              },
                                                                                                                                                                              "value": "2.5"
                                                                                                                                                                            }
                                                                                                                                                                          ]
                                                                                                                                                                        },
                                                                                                                                                                        "digits": {
                                                                                                                                                                          "type": {
                                                                                                                                                                            "name": "integer"
                                                                                                                                                                          },
                                                                                                                                                                          "value": -1
                                                                                                                                                                        }
                                                                                                                                                                      },
                                                                                                                                                                      {
                                                                                                                                                                        "type": {
                                                                                                                                                                          "name": "decimal"
                                                                                                                                                                        },
                                                                                                                                                                        "value": "2.5"
                                                                                                                                                                      }
                                                                                                                                                                    ]
                                                                                                                                                                  },
                                                                                                                                                                  "digits": {
                                                                                                                                                                    "type": {
                                                                                                                                                                      "name": "integer"
                                                                                                                                                                    },
                                                                                                                                                                    "value": -1
                                                                                                                                                                  }
                                                                                                                                                                },
                                                                                                                                                                {
                                                                                                                                                                  "type": {
                                                                                                                                                                    "name": "decimal"
                                                                                                                                                                  },
                                                                                                                                                                  "value": "2.5"
                                                                                                                                                                }
                                                                                                                                                              ]
                                                                                                                                                            },
                                                                                                                                                            "digits": {
                                                                                                                                                              "type": {
                                                                                                                                                                "name": "integer"
                                                                                                                                                              },
                                                                                                                                                              "value": -1
                                                                                                                                                            }
                                                                                                                                                          },
                                                                                                                                                          {
                                                                                                                                                            "type": {
                                                                                                                                                              "name": "decimal"
                                                                                                                                                            },
                                                                                                                                                            "value": "2.5"
                                                                                                                                                          }
                                                                                                                                                        ]
                                                                                                                                                      },
                                                                                                                                                      "digits": {
                                                                                                                                                        "type": {
                                                                                                                                                          "name": "integer"
                                                                                                                                                        },
                                                                                                                                                        "value": -1
                                                                                                                                                      }
                                                                                                                                                    },
                                                                                                                                                    {
                                                                                                                                                      "type": {
                                                                                                                                                        "name": "decimal"
                                                                                                                                                      },
                                                                                                                                                      "value": "2.5"
                                                                                                                                                    }
                                                                                                                                                  ]
                                                                                                                                                },
                                                                                                                                                "digits": {
                                                                                                                                                  "type": {
                                                                                                                                                    "name": "integer"
                                                                                                                                                  },
                                                                                                                                                  "value": -1
                                                                                                                                                }
                                                                                                                                              },
                                                                                                                                              {
                                                                                                                                                "type": {
                                                                                                                                                  "name": "decimal"
                                                                                                                                                },
                                                                                                                                                "value": "2.5"
                                                                                                                                              }
                                                                                                                                            ]
                                                                                                                                          },
                                                                                                                                          "digits": {
                                                                                                                                            "type": {
                                                                                                                                              "name": "integer"
                                                                                                                                            },
                                                                                                                                            "value": -1
                                                                                                                                          }
                                                                                                                                        },
                                                                                                                                        {
                                                                                                                                          "type": {
                                                                                                                                            "name": "decimal"
                                                                                                                                          },
                                                                                                                                          "value": "2.5"
                                                                                                                                        }
                                                                                                                                      ]
                                                                                                                                    },
                                                                                                                                    "digits": {
                                                                                                                                      "type": {
                                                                                                                                        "name": "integer"
                                                                                                                                      },
                                                                                                                                      "value": -1
                                                                                                                                    }
                                                                                                                                  },
                                                                                                                                  {
                                                                                                                                    "type": {
                                                                                                                                      "name": "decimal"
                                                                                                                                    },
                                                                                                                                    "value": "2.5"
                                                                                                                                  }
                                                                                                                                ]
                                                                                                                              },
                                                                                                                              "digits": {
                                                                                                                                "type": {
                                                                                                                                  "name": "integer"
                                                                                                                                },
                                                                                                                                "value": -1
                                                                                                                              }
                                                                                                                            },
                                                                                                                            {
                                                                                                                              "type": {
                                                                                                                                "name": "decimal"
                                                                                                                              },
                                                                                                                              "value": "2.5"
                                                                                                                            }
                                                                                                                          ]
                                                                                                                        },
                                                                                                                        "digits": {
                                                                                                                          "type": {
                                                                                                                            "name": "integer"
                                                                                                                          },
                                                                                                                          "value": -1
                                                                                                                        }
                                                                                                                      },
                                                                                                                      {
                                                                                                                        "type": {
                                                                                                                          "name": "decimal"
                                                                                                                        },
                                                                                                                        "value": "2.5"
                                                                                                                      }
                                                                                                                    ]
                                                                                                                  },
                                                                                                                  "digits": {
                                                                                                                    "type": {
                                                                                                                      "name": "integer"
                                                                                                                    },
                                                                                                                    "value": -1
                                                                                                                  }
                                                                                                                },
                                                                                                                {
                                                                                                                  "type": {
                                                                                                                    "name": "decimal"
                                                                                                                  },
                                                                                                                  "value": "2.5"
                                                                                                                }
                                                                                                              ]
                                                                                                            },
                                                                                                            "digits": {
                                                                                                              "type": {
                                                                                                                "name": "integer"
                                                                                                              },
                                                                                                              "value": -1
                                                                                                            }
                                                                                                          },
                                                                                                          {
                                                                                                            "type": {
                                                                                                              "name": "decimal"
                                                                                                            },
                                                                                                            "value": "2.5"
                                                                                                          }
                                                                                                        ]
                                                                                                      },
                                                                                                      "digits": {
                                                                                                        "type": {
                                                                                                          "name": "integer"
                                                                                                        },
                                                                                                        "value": -1
                                                                                                      }
                                                                                                    },
                                                                                                    {
                                                                                                      "type": {
                                                                                                        "name": "decimal"
                                                                                                      },
                                                                                                      "value": "2.5"
                                                                                                    }
                                                                                                  ]
                                                                                                },
                                                                                                "digits": {
                                                                                                  "type": {
                                                                                                    "name": "integer"
                                                                                                  },
                                                                                                  "value": -1
                                                                                                }
                                                                                              },
                                                                                              {
                                                                                                "type": {
                                                                                                  "name": "decimal"
                                                                                                },
                                                                                                "value": "2.5"
                                                                                              }
                                                                                            ]
                                                                                          },
                                                                                          "digits": {
                                                                                            "type": {
                                                                                              "name": "integer"
                                                                                            },
                                                                                            "value": -1
                                                                                          }
                                                                                        },
                                                                                        {
                                                                                          "type": {
                                                                                            "name": "decimal"
                                                                                          },
                                                                                          "value": "2.5"
                                                                                        }
                                                                                      ]
                                                                                    },
                                                                                    "digits": {
                                                                                      "type": {
                                                                                        "name": "integer"
                                                                                      },
                                                                                      "value": -1
                                                                                    }
                                                                                  },
                                                                                  {
                                                                                    "type": {
                                                                                      "name": "decimal"
                                                                                    },
                                                                                    "value": "2.5"
                                                                                  }
                                                                                ]
                                                                              },
                                                                              "digits": {
                                                                                "type": {
                                                                                  "name": "integer"
                                                                                },
                                                                                "value": -1
                                                                              }
                                                                            },
                                                                            {
                                                                              "type": {
                                                                                "name": "decimal"
                                                                              },
                                                                              "value": "2.5"
                                                                            }
                                                                          ]
                                                                        },
                                                                        "digits": {
                                                                          "type": {
                                                                            "name": "integer"
                                                                          },
                                                                          "value": -1
                                                                        }
                                                                      },
                                                                      {
                                                                        "type": {
                                                                          "name": "decimal"
                                                                        },
                                                                        "value": "2.5"
                                                                      }
                                                                    ]
                                                                  },
                                                                  "digits": {
                                                                    "type": {
                                                                      "name": "integer"
                                                                    },
                                                                    "value": -1
                                                                  }
                                                                },
                                                                {
                                                                  "type": {
                                                                    "name": "decimal"
                                                                  },
                                                                  "value": "2.5"
                                                                }
                                                              ]
                                                            },
                                                            "digits": {
                                                              "type": {
                                                                "name": "integer"
                                                              },
                                                              "value": -1
                                                            }
                                                          },
                                                          {
                                                            "type": {
                                                              "name": "decimal"
                                                            },
                                                            "value": "2.5"
                                                          }
                                                        ]
                                                      },
                                                      "digits": {
                                                        "type": {
                                                          "name": "integer"
                                                        },
                                                        "value": -1
                                                      }
                                                    },
                                                    {
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "value": "2.5"
                                                    }
                                                  ]
                                                },
                                                "digits": {
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "value": -1
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "value": "2.5"
                                              }
                                            ]
                                          },
                                          "digits": {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": -1
                                          }
                                        },
                                        {
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "value": "2.5"
                                        }
                                      ]
                                    },
                                    "digits": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": -1
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "2.5"
                                  }
                                ]
                              },
                              "digits": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": -1
                              }
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "2.5"
                            }
                          ]
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
                """
            ).TextValue,
            new QueryJson(new RoundOfAddNestedThirtyLevelsDeepQuery().Value).TextValue
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
                      "name": "rounded_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "100"
                    ],
                    [
                      "50"
                    ],
                    [
                      "200"
                    ],
                    [
                      "80"
                    ],
                    [
                      "300"
                    ],
                    [
                      "100"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundOfAddNestedThirtyLevelsDeepQuery().Result).TextValue
        );
    }
}
