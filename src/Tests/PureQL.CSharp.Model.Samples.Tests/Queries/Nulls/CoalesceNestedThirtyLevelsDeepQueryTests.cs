using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record CoalesceNestedThirtyLevelsDeepQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new DeepJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "alias": "order_status",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          },
                          {
                            "operator": "coalesce",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              },
                              {
                                "operator": "coalesce",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "type": {
                                      "name": "string",
                                      "nullable": true
                                    },
                                    "value": null
                                  },
                                  {
                                    "operator": "coalesce",
                                    "type": {
                                      "name": "string"
                                    },
                                    "values": [
                                      {
                                        "type": {
                                          "name": "string",
                                          "nullable": true
                                        },
                                        "value": null
                                      },
                                      {
                                        "operator": "coalesce",
                                        "type": {
                                          "name": "string"
                                        },
                                        "values": [
                                          {
                                            "type": {
                                              "name": "string",
                                              "nullable": true
                                            },
                                            "value": null
                                          },
                                          {
                                            "operator": "coalesce",
                                            "type": {
                                              "name": "string"
                                            },
                                            "values": [
                                              {
                                                "type": {
                                                  "name": "string",
                                                  "nullable": true
                                                },
                                                "value": null
                                              },
                                              {
                                                "operator": "coalesce",
                                                "type": {
                                                  "name": "string"
                                                },
                                                "values": [
                                                  {
                                                    "type": {
                                                      "name": "string",
                                                      "nullable": true
                                                    },
                                                    "value": null
                                                  },
                                                  {
                                                    "operator": "coalesce",
                                                    "type": {
                                                      "name": "string"
                                                    },
                                                    "values": [
                                                      {
                                                        "type": {
                                                          "name": "string",
                                                          "nullable": true
                                                        },
                                                        "value": null
                                                      },
                                                      {
                                                        "operator": "coalesce",
                                                        "type": {
                                                          "name": "string"
                                                        },
                                                        "values": [
                                                          {
                                                            "type": {
                                                              "name": "string",
                                                              "nullable": true
                                                            },
                                                            "value": null
                                                          },
                                                          {
                                                            "operator": "coalesce",
                                                            "type": {
                                                              "name": "string"
                                                            },
                                                            "values": [
                                                              {
                                                                "type": {
                                                                  "name": "string",
                                                                  "nullable": true
                                                                },
                                                                "value": null
                                                              },
                                                              {
                                                                "operator": "coalesce",
                                                                "type": {
                                                                  "name": "string"
                                                                },
                                                                "values": [
                                                                  {
                                                                    "type": {
                                                                      "name": "string",
                                                                      "nullable": true
                                                                    },
                                                                    "value": null
                                                                  },
                                                                  {
                                                                    "operator": "coalesce",
                                                                    "type": {
                                                                      "name": "string"
                                                                    },
                                                                    "values": [
                                                                      {
                                                                        "type": {
                                                                          "name": "string",
                                                                          "nullable": true
                                                                        },
                                                                        "value": null
                                                                      },
                                                                      {
                                                                        "operator": "coalesce",
                                                                        "type": {
                                                                          "name": "string"
                                                                        },
                                                                        "values": [
                                                                          {
                                                                            "type": {
                                                                              "name": "string",
                                                                              "nullable": true
                                                                            },
                                                                            "value": null
                                                                          },
                                                                          {
                                                                            "operator": "coalesce",
                                                                            "type": {
                                                                              "name": "string"
                                                                            },
                                                                            "values": [
                                                                              {
                                                                                "type": {
                                                                                  "name": "string",
                                                                                  "nullable": true
                                                                                },
                                                                                "value": null
                                                                              },
                                                                              {
                                                                                "operator": "coalesce",
                                                                                "type": {
                                                                                  "name": "string"
                                                                                },
                                                                                "values": [
                                                                                  {
                                                                                    "type": {
                                                                                      "name": "string",
                                                                                      "nullable": true
                                                                                    },
                                                                                    "value": null
                                                                                  },
                                                                                  {
                                                                                    "operator": "coalesce",
                                                                                    "type": {
                                                                                      "name": "string"
                                                                                    },
                                                                                    "values": [
                                                                                      {
                                                                                        "type": {
                                                                                          "name": "string",
                                                                                          "nullable": true
                                                                                        },
                                                                                        "value": null
                                                                                      },
                                                                                      {
                                                                                        "operator": "coalesce",
                                                                                        "type": {
                                                                                          "name": "string"
                                                                                        },
                                                                                        "values": [
                                                                                          {
                                                                                            "type": {
                                                                                              "name": "string",
                                                                                              "nullable": true
                                                                                            },
                                                                                            "value": null
                                                                                          },
                                                                                          {
                                                                                            "operator": "coalesce",
                                                                                            "type": {
                                                                                              "name": "string"
                                                                                            },
                                                                                            "values": [
                                                                                              {
                                                                                                "type": {
                                                                                                  "name": "string",
                                                                                                  "nullable": true
                                                                                                },
                                                                                                "value": null
                                                                                              },
                                                                                              {
                                                                                                "operator": "coalesce",
                                                                                                "type": {
                                                                                                  "name": "string"
                                                                                                },
                                                                                                "values": [
                                                                                                  {
                                                                                                    "type": {
                                                                                                      "name": "string",
                                                                                                      "nullable": true
                                                                                                    },
                                                                                                    "value": null
                                                                                                  },
                                                                                                  {
                                                                                                    "operator": "coalesce",
                                                                                                    "type": {
                                                                                                      "name": "string"
                                                                                                    },
                                                                                                    "values": [
                                                                                                      {
                                                                                                        "type": {
                                                                                                          "name": "string",
                                                                                                          "nullable": true
                                                                                                        },
                                                                                                        "value": null
                                                                                                      },
                                                                                                      {
                                                                                                        "operator": "coalesce",
                                                                                                        "type": {
                                                                                                          "name": "string"
                                                                                                        },
                                                                                                        "values": [
                                                                                                          {
                                                                                                            "type": {
                                                                                                              "name": "string",
                                                                                                              "nullable": true
                                                                                                            },
                                                                                                            "value": null
                                                                                                          },
                                                                                                          {
                                                                                                            "operator": "coalesce",
                                                                                                            "type": {
                                                                                                              "name": "string"
                                                                                                            },
                                                                                                            "values": [
                                                                                                              {
                                                                                                                "type": {
                                                                                                                  "name": "string",
                                                                                                                  "nullable": true
                                                                                                                },
                                                                                                                "value": null
                                                                                                              },
                                                                                                              {
                                                                                                                "operator": "coalesce",
                                                                                                                "type": {
                                                                                                                  "name": "string"
                                                                                                                },
                                                                                                                "values": [
                                                                                                                  {
                                                                                                                    "type": {
                                                                                                                      "name": "string",
                                                                                                                      "nullable": true
                                                                                                                    },
                                                                                                                    "value": null
                                                                                                                  },
                                                                                                                  {
                                                                                                                    "operator": "coalesce",
                                                                                                                    "type": {
                                                                                                                      "name": "string"
                                                                                                                    },
                                                                                                                    "values": [
                                                                                                                      {
                                                                                                                        "type": {
                                                                                                                          "name": "string",
                                                                                                                          "nullable": true
                                                                                                                        },
                                                                                                                        "value": null
                                                                                                                      },
                                                                                                                      {
                                                                                                                        "operator": "coalesce",
                                                                                                                        "type": {
                                                                                                                          "name": "string"
                                                                                                                        },
                                                                                                                        "values": [
                                                                                                                          {
                                                                                                                            "type": {
                                                                                                                              "name": "string",
                                                                                                                              "nullable": true
                                                                                                                            },
                                                                                                                            "value": null
                                                                                                                          },
                                                                                                                          {
                                                                                                                            "operator": "coalesce",
                                                                                                                            "type": {
                                                                                                                              "name": "string"
                                                                                                                            },
                                                                                                                            "values": [
                                                                                                                              {
                                                                                                                                "type": {
                                                                                                                                  "name": "string",
                                                                                                                                  "nullable": true
                                                                                                                                },
                                                                                                                                "value": null
                                                                                                                              },
                                                                                                                              {
                                                                                                                                "operator": "coalesce",
                                                                                                                                "type": {
                                                                                                                                  "name": "string"
                                                                                                                                },
                                                                                                                                "values": [
                                                                                                                                  {
                                                                                                                                    "type": {
                                                                                                                                      "name": "string",
                                                                                                                                      "nullable": true
                                                                                                                                    },
                                                                                                                                    "value": null
                                                                                                                                  },
                                                                                                                                  {
                                                                                                                                    "operator": "coalesce",
                                                                                                                                    "type": {
                                                                                                                                      "name": "string"
                                                                                                                                    },
                                                                                                                                    "values": [
                                                                                                                                      {
                                                                                                                                        "type": {
                                                                                                                                          "name": "string",
                                                                                                                                          "nullable": true
                                                                                                                                        },
                                                                                                                                        "value": null
                                                                                                                                      },
                                                                                                                                      {
                                                                                                                                        "operator": "coalesce",
                                                                                                                                        "type": {
                                                                                                                                          "name": "string"
                                                                                                                                        },
                                                                                                                                        "values": [
                                                                                                                                          {
                                                                                                                                            "type": {
                                                                                                                                              "name": "string",
                                                                                                                                              "nullable": true
                                                                                                                                            },
                                                                                                                                            "value": null
                                                                                                                                          },
                                                                                                                                          {
                                                                                                                                            "operator": "coalesce",
                                                                                                                                            "type": {
                                                                                                                                              "name": "string"
                                                                                                                                            },
                                                                                                                                            "values": [
                                                                                                                                              {
                                                                                                                                                "type": {
                                                                                                                                                  "name": "string",
                                                                                                                                                  "nullable": true
                                                                                                                                                },
                                                                                                                                                "value": null
                                                                                                                                              },
                                                                                                                                              {
                                                                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                                                                "field": "order_status",
                                                                                                                                                "type": {
                                                                                                                                                  "name": "string"
                                                                                                                                                }
                                                                                                                                              }
                                                                                                                                            ]
                                                                                                                                          }
                                                                                                                                        ]
                                                                                                                                      }
                                                                                                                                    ]
                                                                                                                                  }
                                                                                                                                ]
                                                                                                                              }
                                                                                                                            ]
                                                                                                                          }
                                                                                                                        ]
                                                                                                                      }
                                                                                                                    ]
                                                                                                                  }
                                                                                                                ]
                                                                                                              }
                                                                                                            ]
                                                                                                          }
                                                                                                        ]
                                                                                                      }
                                                                                                    ]
                                                                                                  }
                                                                                                ]
                                                                                              }
                                                                                            ]
                                                                                          }
                                                                                        ]
                                                                                      }
                                                                                    ]
                                                                                  }
                                                                                ]
                                                                              }
                                                                            ]
                                                                          }
                                                                        ]
                                                                      }
                                                                    ]
                                                                  }
                                                                ]
                                                              }
                                                            ]
                                                          }
                                                        ]
                                                      }
                                                    ]
                                                  }
                                                ]
                                              }
                                            ]
                                          }
                                        ]
                                      }
                                    ]
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
                """
            ).TextValue,
            new DeepJson(new CoalesceNestedThirtyLevelsDeepQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped"
                    ],
                    [
                      "pending"
                    ],
                    [
                      "shipped"
                    ],
                    [
                      "cancelled"
                    ],
                    [
                      "shipped"
                    ],
                    [
                      "pending"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CoalesceNestedThirtyLevelsDeepQuery().Result).TextValue
        );
    }
}
