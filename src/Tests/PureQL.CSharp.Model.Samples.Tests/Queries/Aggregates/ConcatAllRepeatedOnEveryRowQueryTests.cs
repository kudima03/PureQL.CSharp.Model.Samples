using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record ConcatAllRepeatedOnEveryRowQueryTests
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
                      "alias": "id",
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
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "all_statuses",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "status_is_listed_first",
                      "expression": {
                        "operator": "startsWith",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "concatAll",
                          "type": {
                            "name": "string"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "separator": {
                            "type": {
                              "name": "string"
                            },
                            "value": ", "
                          },
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ],
                          "distinct": true
                        },
                        "pattern": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "timeline",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "concatWith",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "operator": "asString",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "separator": {
                            "type": {
                              "name": "string"
                            },
                            "value": " "
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\n"
                        },
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
                """
            ).TextValue,
            new QueryJson(new ConcatAllRepeatedOnEveryRowQuery().Value).TextValue
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
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "all_statuses",
                      "type": "string"
                    },
                    {
                      "name": "status_is_listed_first",
                      "type": "bool"
                    },
                    {
                      "name": "timeline",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "shipped",
                      "cancelled, pending, shipped",
                      "False",
                      "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "pending",
                      "cancelled, pending, shipped",
                      "False",
                      "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "shipped",
                      "cancelled, pending, shipped",
                      "False",
                      "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "cancelled",
                      "cancelled, pending, shipped",
                      "True",
                      "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "shipped",
                      "cancelled, pending, shipped",
                      "False",
                      "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "pending",
                      "cancelled, pending, shipped",
                      "False",
                      "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConcatAllRepeatedOnEveryRowQuery().Result).TextValue
        );
    }
}
