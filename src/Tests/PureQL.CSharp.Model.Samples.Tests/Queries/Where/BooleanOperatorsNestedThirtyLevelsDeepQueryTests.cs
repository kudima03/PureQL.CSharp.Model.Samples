using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record BooleanOperatorsNestedThirtyLevelsDeepQueryTests
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
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "not",
                    "type": {
                      "name": "boolean"
                    },
                    "condition": {
                      "operator": "or",
                      "type": {
                        "name": "boolean"
                      },
                      "conditions": [
                        {
                          "type": {
                            "name": "boolean"
                          },
                          "value": false
                        },
                        {
                          "operator": "and",
                          "type": {
                            "name": "boolean"
                          },
                          "conditions": [
                            {
                              "operator": "not",
                              "type": {
                                "name": "boolean"
                              },
                              "condition": {
                                "operator": "or",
                                "type": {
                                  "name": "boolean"
                                },
                                "conditions": [
                                  {
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "value": false
                                  },
                                  {
                                    "operator": "and",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "conditions": [
                                      {
                                        "operator": "not",
                                        "type": {
                                          "name": "boolean"
                                        },
                                        "condition": {
                                          "operator": "or",
                                          "type": {
                                            "name": "boolean"
                                          },
                                          "conditions": [
                                            {
                                              "type": {
                                                "name": "boolean"
                                              },
                                              "value": false
                                            },
                                            {
                                              "operator": "and",
                                              "type": {
                                                "name": "boolean"
                                              },
                                              "conditions": [
                                                {
                                                  "operator": "not",
                                                  "type": {
                                                    "name": "boolean"
                                                  },
                                                  "condition": {
                                                    "operator": "or",
                                                    "type": {
                                                      "name": "boolean"
                                                    },
                                                    "conditions": [
                                                      {
                                                        "type": {
                                                          "name": "boolean"
                                                        },
                                                        "value": false
                                                      },
                                                      {
                                                        "operator": "and",
                                                        "type": {
                                                          "name": "boolean"
                                                        },
                                                        "conditions": [
                                                          {
                                                            "operator": "not",
                                                            "type": {
                                                              "name": "boolean"
                                                            },
                                                            "condition": {
                                                              "operator": "or",
                                                              "type": {
                                                                "name": "boolean"
                                                              },
                                                              "conditions": [
                                                                {
                                                                  "type": {
                                                                    "name": "boolean"
                                                                  },
                                                                  "value": false
                                                                },
                                                                {
                                                                  "operator": "and",
                                                                  "type": {
                                                                    "name": "boolean"
                                                                  },
                                                                  "conditions": [
                                                                    {
                                                                      "operator": "not",
                                                                      "type": {
                                                                        "name": "boolean"
                                                                      },
                                                                      "condition": {
                                                                        "operator": "or",
                                                                        "type": {
                                                                          "name": "boolean"
                                                                        },
                                                                        "conditions": [
                                                                          {
                                                                            "type": {
                                                                              "name": "boolean"
                                                                            },
                                                                            "value": false
                                                                          },
                                                                          {
                                                                            "operator": "and",
                                                                            "type": {
                                                                              "name": "boolean"
                                                                            },
                                                                            "conditions": [
                                                                              {
                                                                                "operator": "not",
                                                                                "type": {
                                                                                  "name": "boolean"
                                                                                },
                                                                                "condition": {
                                                                                  "operator": "or",
                                                                                  "type": {
                                                                                    "name": "boolean"
                                                                                  },
                                                                                  "conditions": [
                                                                                    {
                                                                                      "type": {
                                                                                        "name": "boolean"
                                                                                      },
                                                                                      "value": false
                                                                                    },
                                                                                    {
                                                                                      "operator": "and",
                                                                                      "type": {
                                                                                        "name": "boolean"
                                                                                      },
                                                                                      "conditions": [
                                                                                        {
                                                                                          "operator": "not",
                                                                                          "type": {
                                                                                            "name": "boolean"
                                                                                          },
                                                                                          "condition": {
                                                                                            "operator": "or",
                                                                                            "type": {
                                                                                              "name": "boolean"
                                                                                            },
                                                                                            "conditions": [
                                                                                              {
                                                                                                "type": {
                                                                                                  "name": "boolean"
                                                                                                },
                                                                                                "value": false
                                                                                              },
                                                                                              {
                                                                                                "operator": "and",
                                                                                                "type": {
                                                                                                  "name": "boolean"
                                                                                                },
                                                                                                "conditions": [
                                                                                                  {
                                                                                                    "operator": "not",
                                                                                                    "type": {
                                                                                                      "name": "boolean"
                                                                                                    },
                                                                                                    "condition": {
                                                                                                      "operator": "or",
                                                                                                      "type": {
                                                                                                        "name": "boolean"
                                                                                                      },
                                                                                                      "conditions": [
                                                                                                        {
                                                                                                          "type": {
                                                                                                            "name": "boolean"
                                                                                                          },
                                                                                                          "value": false
                                                                                                        },
                                                                                                        {
                                                                                                          "operator": "and",
                                                                                                          "type": {
                                                                                                            "name": "boolean"
                                                                                                          },
                                                                                                          "conditions": [
                                                                                                            {
                                                                                                              "operator": "not",
                                                                                                              "type": {
                                                                                                                "name": "boolean"
                                                                                                              },
                                                                                                              "condition": {
                                                                                                                "operator": "or",
                                                                                                                "type": {
                                                                                                                  "name": "boolean"
                                                                                                                },
                                                                                                                "conditions": [
                                                                                                                  {
                                                                                                                    "type": {
                                                                                                                      "name": "boolean"
                                                                                                                    },
                                                                                                                    "value": false
                                                                                                                  },
                                                                                                                  {
                                                                                                                    "operator": "and",
                                                                                                                    "type": {
                                                                                                                      "name": "boolean"
                                                                                                                    },
                                                                                                                    "conditions": [
                                                                                                                      {
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
                                                                                                                          "type": {
                                                                                                                            "name": "decimal"
                                                                                                                          },
                                                                                                                          "value": "100"
                                                                                                                        }
                                                                                                                      },
                                                                                                                      {
                                                                                                                        "type": {
                                                                                                                          "name": "boolean"
                                                                                                                        },
                                                                                                                        "value": true
                                                                                                                      }
                                                                                                                    ]
                                                                                                                  }
                                                                                                                ]
                                                                                                              }
                                                                                                            },
                                                                                                            {
                                                                                                              "type": {
                                                                                                                "name": "boolean"
                                                                                                              },
                                                                                                              "value": true
                                                                                                            }
                                                                                                          ]
                                                                                                        }
                                                                                                      ]
                                                                                                    }
                                                                                                  },
                                                                                                  {
                                                                                                    "type": {
                                                                                                      "name": "boolean"
                                                                                                    },
                                                                                                    "value": true
                                                                                                  }
                                                                                                ]
                                                                                              }
                                                                                            ]
                                                                                          }
                                                                                        },
                                                                                        {
                                                                                          "type": {
                                                                                            "name": "boolean"
                                                                                          },
                                                                                          "value": true
                                                                                        }
                                                                                      ]
                                                                                    }
                                                                                  ]
                                                                                }
                                                                              },
                                                                              {
                                                                                "type": {
                                                                                  "name": "boolean"
                                                                                },
                                                                                "value": true
                                                                              }
                                                                            ]
                                                                          }
                                                                        ]
                                                                      }
                                                                    },
                                                                    {
                                                                      "type": {
                                                                        "name": "boolean"
                                                                      },
                                                                      "value": true
                                                                    }
                                                                  ]
                                                                }
                                                              ]
                                                            }
                                                          },
                                                          {
                                                            "type": {
                                                              "name": "boolean"
                                                            },
                                                            "value": true
                                                          }
                                                        ]
                                                      }
                                                    ]
                                                  }
                                                },
                                                {
                                                  "type": {
                                                    "name": "boolean"
                                                  },
                                                  "value": true
                                                }
                                              ]
                                            }
                                          ]
                                        }
                                      },
                                      {
                                        "type": {
                                          "name": "boolean"
                                        },
                                        "value": true
                                      }
                                    ]
                                  }
                                ]
                              }
                            },
                            {
                              "type": {
                                "name": "boolean"
                              },
                              "value": true
                            }
                          ]
                        }
                      ]
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new BooleanOperatorsNestedThirtyLevelsDeepQuery().Value
            ).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new BooleanOperatorsNestedThirtyLevelsDeepQuery().Result
            ).TextValue
        );
    }
}
