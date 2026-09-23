using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record DeMorganEquivalentFiveLevelEachTreeQueryTests
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
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "operator": "sum",
                      "arg": {
                        "entity": "schema_with_foreign_keys.order_items",
                        "field": "item_qty",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "qtySum"
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "itemCount"
                    }
                  ],
                  "where": {
                    "operator": "eachAnd",
                    "conditions": [
                      {
                        "operator": "eachOr",
                        "conditions": [
                          {
                            "operator": "eachOr",
                            "conditions": [
                              {
                                "operator": "eachNot",
                                "condition": {
                                  "operator": "eachGreaterThan",
                                  "left": {
                                    "entity": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "number"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "number"
                                    },
                                    "value": 100
                                  }
                                }
                              },
                              {
                                "operator": "eachNot",
                                "condition": {
                                  "operator": "eachEqual",
                                  "left": {
                                    "entity": "schema_with_foreign_keys.orders",
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
                                }
                              }
                            ]
                          },
                          {
                            "operator": "eachGreaterThanOrEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "number"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 300
                            }
                          }
                        ]
                      },
                      {
                        "operator": "eachOr",
                        "conditions": [
                          {
                            "operator": "eachNot",
                            "condition": {
                              "operator": "eachEqual",
                              "left": {
                                "entity": "schema_with_foreign_keys.orders",
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
                            }
                          },
                          {
                            "operator": "eachAnd",
                            "conditions": [
                              {
                                "operator": "eachLessThan",
                                "left": {
                                  "entity": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "number"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "number"
                                  },
                                  "value": 100
                                }
                              },
                              {
                                "operator": "eachEqual",
                                "left": {
                                  "entity": "schema_with_foreign_keys.orders",
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
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.order_items",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "having": {
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "or",
                        "conditions": [
                          {
                            "operator": "greaterThanOrEqual",
                            "left": {
                              "operator": "count",
                              "arg": {
                                "entity": "schema_with_foreign_keys.order_items",
                                "field": "item_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 2
                            }
                          },
                          {
                            "operator": "greaterThanOrEqual",
                            "left": {
                              "operator": "sum",
                              "arg": {
                                "entity": "schema_with_foreign_keys.order_items",
                                "field": "item_qty",
                                "type": {
                                  "name": "number"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 5
                            }
                          }
                        ]
                      },
                      {
                        "operator": "not",
                        "condition": {
                          "operator": "greaterThanOrEqual",
                          "left": {
                            "operator": "count",
                            "arg": {
                              "entity": "schema_with_foreign_keys.order_items",
                              "field": "item_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "number"
                            },
                            "value": 100
                          }
                        }
                      }
                    ]
                  },
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(new DeMorganEquivalentFiveLevelEachTreeQuery().Value).TextValue
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
                      "name": "qtySum",
                      "type": "double"
                    },
                    {
                      "name": "itemCount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "3",
                      "2"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "5",
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new DeMorganEquivalentFiveLevelEachTreeQuery().Result
            ).TextValue
        );
    }
}
