using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record OrdersPerListedStatusQueryTests
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
                      "name": "statuses",
                      "rows": [
                        [
                          {
                            "alias": "status",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "pending"
                            }
                          },
                          {
                            "alias": "rank",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          {
                            "alias": "label",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Awaiting shipment"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "status",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          },
                          {
                            "alias": "rank",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            }
                          },
                          {
                            "alias": "label",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Shipped"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "status",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "cancelled"
                            }
                          },
                          {
                            "alias": "rank",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3
                            }
                          },
                          {
                            "alias": "label",
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
                            "alias": "status",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "refunded"
                            }
                          },
                          {
                            "alias": "rank",
                            "expression": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 4
                            }
                          },
                          {
                            "alias": "label",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Refunded"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "statuses"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "statuses",
                          "field": "status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "alias": "rank",
                      "expression": {
                        "source": "statuses",
                        "field": "rank",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "status",
                      "expression": {
                        "source": "statuses",
                        "field": "status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "source": "statuses",
                        "field": "label",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "status",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "key": "label",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "key": "status",
                            "type": {
                              "name": "string"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "orders",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid",
                              "nullable": true
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
                      }
                    },
                    {
                      "alias": "revenue",
                      "expression": {
                        "operator": "sum",
                        "type": {
                          "name": "decimal"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": "rank",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrdersPerListedStatusQuery().Value).TextValue
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "revenue",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Awaiting shipment",
                      "2",
                      "150.5"
                    ],
                    [
                      "Shipped",
                      "3",
                      "600.5"
                    ],
                    [
                      "cancelled",
                      "1",
                      "75.25"
                    ],
                    [
                      "Refunded",
                      "0",
                      "0"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrdersPerListedStatusQuery().Result).TextValue
        );
    }
}
