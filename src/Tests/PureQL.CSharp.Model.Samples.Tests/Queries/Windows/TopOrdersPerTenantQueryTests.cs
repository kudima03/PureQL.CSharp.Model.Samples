using PureQL.CSharp.Model.Samples.Queries.Windows;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Windows;

public sealed record TopOrdersPerTenantQueryTests
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
                      "name": "ranked",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
                          "operator": "notEqual",
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
                            "type": {
                              "name": "string"
                            },
                            "value": "cancelled"
                          }
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
                            "alias": "tenant_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "total",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          {
                            "alias": "position",
                            "expression": {
                              "operator": "rowNumber",
                              "type": {
                                "name": "integer"
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  }
                                ],
                                "orderBy": [
                                  {
                                    "expression": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_total",
                                      "type": {
                                        "name": "decimal"
                                      }
                                    },
                                    "direction": "desc"
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
                          },
                          {
                            "alias": "tenant_orders",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              },
                              "over": {
                                "partitionBy": [
                                  {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  }
                                ]
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "ranked"
                  },
                  "where": {
                    "operator": "lessThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "ranked",
                      "field": "position",
                      "type": {
                        "name": "integer"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 2
                    }
                  },
                  "select": [
                    {
                      "alias": "tenant_id",
                      "expression": {
                        "source": "ranked",
                        "field": "tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "position",
                      "expression": {
                        "source": "ranked",
                        "field": "position",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "id",
                      "expression": {
                        "source": "ranked",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "total",
                      "expression": {
                        "source": "ranked",
                        "field": "total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "tenant_orders",
                      "expression": {
                        "source": "ranked",
                        "field": "tenant_orders",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "ranked",
                        "field": "tenant_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "ranked",
                        "field": "position",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TopOrdersPerTenantQuery().Value).TextValue
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
                      "name": "tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "position",
                      "type": "long"
                    },
                    {
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "total",
                      "type": "double"
                    },
                    {
                      "name": "tenant_orders",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "1",
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      "3"
                    ],
                    [
                      "00000385-0000-0000-0000-000000000000",
                      "2",
                      "00000065-0000-0000-0000-000000000000",
                      "100.5",
                      "3"
                    ],
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "1",
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      "2"
                    ],
                    [
                      "00000386-0000-0000-0000-000000000000",
                      "2",
                      "0000006a-0000-0000-0000-000000000000",
                      "100.5",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TopOrdersPerTenantQuery().Result).TextValue
        );
    }
}
