using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record GreatCircleDistanceQueryTests
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
                      "name": "stores",
                      "rows": [
                        [
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Berlin"
                            }
                          },
                          {
                            "alias": "latitude",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "52.52"
                            }
                          },
                          {
                            "alias": "longitude",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "13.405"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Paris"
                            }
                          },
                          {
                            "alias": "latitude",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "48.8566"
                            }
                          },
                          {
                            "alias": "longitude",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "2.3522"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Madrid"
                            }
                          },
                          {
                            "alias": "latitude",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "40.4168"
                            }
                          },
                          {
                            "alias": "longitude",
                            "expression": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "-3.7038"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "stores"
                  },
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "multiply",
                      "type": {
                        "name": "decimal"
                      },
                      "values": [
                        {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "6371.0088"
                        },
                        {
                          "operator": "asin",
                          "type": {
                            "name": "decimal"
                          },
                          "operand": {
                            "operator": "sqrt",
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
                                  "operator": "power",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "operand": {
                                    "operator": "sin",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "operand": {
                                      "operator": "divide",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "values": [
                                        {
                                          "operator": "subtract",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "values": [
                                            {
                                              "operator": "multiply",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "source": "stores",
                                                  "field": "latitude",
                                                  "type": {
                                                    "name": "decimal"
                                                  }
                                                },
                                                {
                                                  "operator": "divide",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "operator": "pi",
                                                      "type": {
                                                        "name": "decimal"
                                                      }
                                                    },
                                                    {
                                                      "type": {
                                                        "name": "integer"
                                                      },
                                                      "value": 180
                                                    }
                                                  ]
                                                }
                                              ]
                                            },
                                            {
                                              "operator": "multiply",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "value": "50.1109"
                                                },
                                                {
                                                  "operator": "divide",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "operator": "pi",
                                                      "type": {
                                                        "name": "decimal"
                                                      }
                                                    },
                                                    {
                                                      "type": {
                                                        "name": "integer"
                                                      },
                                                      "value": 180
                                                    }
                                                  ]
                                                }
                                              ]
                                            }
                                          ]
                                        },
                                        {
                                          "type": {
                                            "name": "integer"
                                          },
                                          "value": 2
                                        }
                                      ]
                                    }
                                  },
                                  "exponent": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 2
                                  }
                                },
                                {
                                  "operator": "multiply",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "operator": "cos",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "value": "50.1109"
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    },
                                    {
                                      "operator": "cos",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "source": "stores",
                                            "field": "latitude",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    },
                                    {
                                      "operator": "power",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "sin",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "operand": {
                                          "operator": "divide",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "values": [
                                            {
                                              "operator": "subtract",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "operator": "multiply",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "source": "stores",
                                                      "field": "longitude",
                                                      "type": {
                                                        "name": "decimal"
                                                      }
                                                    },
                                                    {
                                                      "operator": "divide",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "values": [
                                                        {
                                                          "operator": "pi",
                                                          "type": {
                                                            "name": "decimal"
                                                          }
                                                        },
                                                        {
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "value": 180
                                                        }
                                                      ]
                                                    }
                                                  ]
                                                },
                                                {
                                                  "operator": "multiply",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "value": "8.6821"
                                                    },
                                                    {
                                                      "operator": "divide",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "values": [
                                                        {
                                                          "operator": "pi",
                                                          "type": {
                                                            "name": "decimal"
                                                          }
                                                        },
                                                        {
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "value": 180
                                                        }
                                                      ]
                                                    }
                                                  ]
                                                }
                                              ]
                                            },
                                            {
                                              "type": {
                                                "name": "integer"
                                              },
                                              "value": 2
                                            }
                                          ]
                                        }
                                      },
                                      "exponent": {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 2
                                      }
                                    }
                                  ]
                                }
                              ]
                            }
                          }
                        }
                      ]
                    },
                    "right": {
                      "type": {
                        "name": "decimal"
                      },
                      "value": "600"
                    }
                  },
                  "select": [
                    {
                      "alias": "name",
                      "expression": {
                        "source": "stores",
                        "field": "name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "distance_km",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "operator": "multiply",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "6371.0088"
                            },
                            {
                              "operator": "asin",
                              "type": {
                                "name": "decimal"
                              },
                              "operand": {
                                "operator": "sqrt",
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
                                      "operator": "power",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "sin",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "operand": {
                                          "operator": "divide",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "values": [
                                            {
                                              "operator": "subtract",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "operator": "multiply",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "source": "stores",
                                                      "field": "latitude",
                                                      "type": {
                                                        "name": "decimal"
                                                      }
                                                    },
                                                    {
                                                      "operator": "divide",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "values": [
                                                        {
                                                          "operator": "pi",
                                                          "type": {
                                                            "name": "decimal"
                                                          }
                                                        },
                                                        {
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "value": 180
                                                        }
                                                      ]
                                                    }
                                                  ]
                                                },
                                                {
                                                  "operator": "multiply",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "value": "50.1109"
                                                    },
                                                    {
                                                      "operator": "divide",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "values": [
                                                        {
                                                          "operator": "pi",
                                                          "type": {
                                                            "name": "decimal"
                                                          }
                                                        },
                                                        {
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "value": 180
                                                        }
                                                      ]
                                                    }
                                                  ]
                                                }
                                              ]
                                            },
                                            {
                                              "type": {
                                                "name": "integer"
                                              },
                                              "value": 2
                                            }
                                          ]
                                        }
                                      },
                                      "exponent": {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 2
                                      }
                                    },
                                    {
                                      "operator": "multiply",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "values": [
                                        {
                                          "operator": "cos",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "operand": {
                                            "operator": "multiply",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "value": "50.1109"
                                              },
                                              {
                                                "operator": "divide",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "pi",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  },
                                                  {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 180
                                                  }
                                                ]
                                              }
                                            ]
                                          }
                                        },
                                        {
                                          "operator": "cos",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "operand": {
                                            "operator": "multiply",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "source": "stores",
                                                "field": "latitude",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "operator": "divide",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "pi",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  },
                                                  {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 180
                                                  }
                                                ]
                                              }
                                            ]
                                          }
                                        },
                                        {
                                          "operator": "power",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "operand": {
                                            "operator": "sin",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "operand": {
                                              "operator": "divide",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "operator": "subtract",
                                                  "type": {
                                                    "name": "decimal"
                                                  },
                                                  "values": [
                                                    {
                                                      "operator": "multiply",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "values": [
                                                        {
                                                          "source": "stores",
                                                          "field": "longitude",
                                                          "type": {
                                                            "name": "decimal"
                                                          }
                                                        },
                                                        {
                                                          "operator": "divide",
                                                          "type": {
                                                            "name": "decimal"
                                                          },
                                                          "values": [
                                                            {
                                                              "operator": "pi",
                                                              "type": {
                                                                "name": "decimal"
                                                              }
                                                            },
                                                            {
                                                              "type": {
                                                                "name": "integer"
                                                              },
                                                              "value": 180
                                                            }
                                                          ]
                                                        }
                                                      ]
                                                    },
                                                    {
                                                      "operator": "multiply",
                                                      "type": {
                                                        "name": "decimal"
                                                      },
                                                      "values": [
                                                        {
                                                          "type": {
                                                            "name": "decimal"
                                                          },
                                                          "value": "8.6821"
                                                        },
                                                        {
                                                          "operator": "divide",
                                                          "type": {
                                                            "name": "decimal"
                                                          },
                                                          "values": [
                                                            {
                                                              "operator": "pi",
                                                              "type": {
                                                                "name": "decimal"
                                                              }
                                                            },
                                                            {
                                                              "type": {
                                                                "name": "integer"
                                                              },
                                                              "value": 180
                                                            }
                                                          ]
                                                        }
                                                      ]
                                                    }
                                                  ]
                                                },
                                                {
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "value": 2
                                                }
                                              ]
                                            }
                                          },
                                          "exponent": {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": 2
                                          }
                                        }
                                      ]
                                    }
                                  ]
                                }
                              }
                            }
                          ]
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "bearing_degrees",
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "atan2",
                            "type": {
                              "name": "decimal"
                            },
                            "y": {
                              "operator": "multiply",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "operator": "sin",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "operand": {
                                    "operator": "subtract",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "source": "stores",
                                            "field": "longitude",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      },
                                      {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "value": "8.6821"
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    ]
                                  }
                                },
                                {
                                  "operator": "cos",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "operand": {
                                    "operator": "multiply",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "source": "stores",
                                        "field": "latitude",
                                        "type": {
                                          "name": "decimal"
                                        }
                                      },
                                      {
                                        "operator": "divide",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "operator": "pi",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          },
                                          {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": 180
                                          }
                                        ]
                                      }
                                    ]
                                  }
                                }
                              ]
                            },
                            "x": {
                              "operator": "subtract",
                              "type": {
                                "name": "decimal"
                              },
                              "values": [
                                {
                                  "operator": "multiply",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "operator": "cos",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "value": "50.1109"
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    },
                                    {
                                      "operator": "sin",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "source": "stores",
                                            "field": "latitude",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    }
                                  ]
                                },
                                {
                                  "operator": "multiply",
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "values": [
                                    {
                                      "operator": "sin",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "value": "50.1109"
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    },
                                    {
                                      "operator": "cos",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "multiply",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "source": "stores",
                                            "field": "latitude",
                                            "type": {
                                              "name": "decimal"
                                            }
                                          },
                                          {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "pi",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 180
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    },
                                    {
                                      "operator": "cos",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "subtract",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "operator": "multiply",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "source": "stores",
                                                "field": "longitude",
                                                "type": {
                                                  "name": "decimal"
                                                }
                                              },
                                              {
                                                "operator": "divide",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "pi",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  },
                                                  {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 180
                                                  }
                                                ]
                                              }
                                            ]
                                          },
                                          {
                                            "operator": "multiply",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "value": "8.6821"
                                              },
                                              {
                                                "operator": "divide",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "pi",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  },
                                                  {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 180
                                                  }
                                                ]
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    }
                                  ]
                                }
                              ]
                            }
                          },
                          {
                            "operator": "divide",
                            "type": {
                              "name": "decimal"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 180
                              },
                              {
                                "operator": "pi",
                                "type": {
                                  "name": "decimal"
                                }
                              }
                            ]
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "multiply",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          },
                          {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "6371.0088"
                          },
                          {
                            "operator": "asin",
                            "type": {
                              "name": "decimal"
                            },
                            "operand": {
                              "operator": "sqrt",
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
                                    "operator": "power",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "operand": {
                                      "operator": "sin",
                                      "type": {
                                        "name": "decimal"
                                      },
                                      "operand": {
                                        "operator": "divide",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "values": [
                                          {
                                            "operator": "subtract",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "multiply",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "source": "stores",
                                                    "field": "latitude",
                                                    "type": {
                                                      "name": "decimal"
                                                    }
                                                  },
                                                  {
                                                    "operator": "divide",
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "values": [
                                                      {
                                                        "operator": "pi",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      },
                                                      {
                                                        "type": {
                                                          "name": "integer"
                                                        },
                                                        "value": 180
                                                      }
                                                    ]
                                                  }
                                                ]
                                              },
                                              {
                                                "operator": "multiply",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "value": "50.1109"
                                                  },
                                                  {
                                                    "operator": "divide",
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "values": [
                                                      {
                                                        "operator": "pi",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      },
                                                      {
                                                        "type": {
                                                          "name": "integer"
                                                        },
                                                        "value": 180
                                                      }
                                                    ]
                                                  }
                                                ]
                                              }
                                            ]
                                          },
                                          {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": 2
                                          }
                                        ]
                                      }
                                    },
                                    "exponent": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 2
                                    }
                                  },
                                  {
                                    "operator": "multiply",
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "values": [
                                      {
                                        "operator": "cos",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "operand": {
                                          "operator": "multiply",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "values": [
                                            {
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "value": "50.1109"
                                            },
                                            {
                                              "operator": "divide",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "operator": "pi",
                                                  "type": {
                                                    "name": "decimal"
                                                  }
                                                },
                                                {
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "value": 180
                                                }
                                              ]
                                            }
                                          ]
                                        }
                                      },
                                      {
                                        "operator": "cos",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "operand": {
                                          "operator": "multiply",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "values": [
                                            {
                                              "source": "stores",
                                              "field": "latitude",
                                              "type": {
                                                "name": "decimal"
                                              }
                                            },
                                            {
                                              "operator": "divide",
                                              "type": {
                                                "name": "decimal"
                                              },
                                              "values": [
                                                {
                                                  "operator": "pi",
                                                  "type": {
                                                    "name": "decimal"
                                                  }
                                                },
                                                {
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "value": 180
                                                }
                                              ]
                                            }
                                          ]
                                        }
                                      },
                                      {
                                        "operator": "power",
                                        "type": {
                                          "name": "decimal"
                                        },
                                        "operand": {
                                          "operator": "sin",
                                          "type": {
                                            "name": "decimal"
                                          },
                                          "operand": {
                                            "operator": "divide",
                                            "type": {
                                              "name": "decimal"
                                            },
                                            "values": [
                                              {
                                                "operator": "subtract",
                                                "type": {
                                                  "name": "decimal"
                                                },
                                                "values": [
                                                  {
                                                    "operator": "multiply",
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "values": [
                                                      {
                                                        "source": "stores",
                                                        "field": "longitude",
                                                        "type": {
                                                          "name": "decimal"
                                                        }
                                                      },
                                                      {
                                                        "operator": "divide",
                                                        "type": {
                                                          "name": "decimal"
                                                        },
                                                        "values": [
                                                          {
                                                            "operator": "pi",
                                                            "type": {
                                                              "name": "decimal"
                                                            }
                                                          },
                                                          {
                                                            "type": {
                                                              "name": "integer"
                                                            },
                                                            "value": 180
                                                          }
                                                        ]
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "operator": "multiply",
                                                    "type": {
                                                      "name": "decimal"
                                                    },
                                                    "values": [
                                                      {
                                                        "type": {
                                                          "name": "decimal"
                                                        },
                                                        "value": "8.6821"
                                                      },
                                                      {
                                                        "operator": "divide",
                                                        "type": {
                                                          "name": "decimal"
                                                        },
                                                        "values": [
                                                          {
                                                            "operator": "pi",
                                                            "type": {
                                                              "name": "decimal"
                                                            }
                                                          },
                                                          {
                                                            "type": {
                                                              "name": "integer"
                                                            },
                                                            "value": 180
                                                          }
                                                        ]
                                                      }
                                                    ]
                                                  }
                                                ]
                                              },
                                              {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 2
                                              }
                                            ]
                                          }
                                        },
                                        "exponent": {
                                          "type": {
                                            "name": "integer"
                                          },
                                          "value": 2
                                        }
                                      }
                                    ]
                                  }
                                ]
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "expression": {
                        "source": "stores",
                        "field": "name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GreatCircleDistanceQuery().Value).TextValue
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
                      "name": "name",
                      "type": "string"
                    },
                    {
                      "name": "distance_km",
                      "type": "double"
                    },
                    {
                      "name": "bearing_degrees",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Berlin",
                      "423.5",
                      "48.95589927738846"
                    ],
                    [
                      "Paris",
                      "477.9",
                      "-104.53847359097716"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GreatCircleDistanceQuery().Result).TextValue
        );
    }
}
