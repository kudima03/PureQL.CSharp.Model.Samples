using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record InlineStatusRowsQueryTests
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
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "statuses",
                      "on": {
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
                          "source": "statuses",
                          "field": "status",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    }
                  ],
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
                      "alias": "status",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "source": "statuses",
                            "field": "label",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
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
                    },
                    {
                      "alias": "rank",
                      "expression": {
                        "source": "statuses",
                        "field": "rank",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "statuses",
                        "field": "rank",
                        "type": {
                          "name": "integer",
                          "nullable": true
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
            new QueryJson(new InlineStatusRowsQuery().Value).TextValue
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "rank",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "Awaiting shipment",
                      "1"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "Awaiting shipment",
                      "1"
                    ],
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "Shipped",
                      "2"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "Shipped",
                      "2"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "Shipped",
                      "2"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "cancelled",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InlineStatusRowsQuery().Result).TextValue
        );
    }
}
