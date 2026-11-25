using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record UsersCommonToSpendersActiveAndNamedQueryTests
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
                      "name": "spenders",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "having": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "sum",
                            "type": {
                              "name": "decimal"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": "100"
                          }
                        },
                        "select": [
                          {
                            "alias": "id",
                            "expression": {
                              "key": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "intersect": [
                    {
                      "from": {
                        "subquery": "spenders"
                      },
                      "select": [
                        {
                          "alias": "id",
                          "expression": {
                            "source": "spenders",
                            "field": "id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        }
                      ]
                    },
                    {
                      "from": {
                        "entity": "schema_with_foreign_keys.users"
                      },
                      "where": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_active",
                        "type": {
                          "name": "boolean"
                        }
                      },
                      "select": [
                        {
                          "alias": "id",
                          "expression": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        }
                      ]
                    },
                    {
                      "unionAll": [
                        {
                          "from": {
                            "entity": "schema_with_foreign_keys.users"
                          },
                          "where": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        },
                        {
                          "from": {
                            "entity": "schema_with_foreign_keys.users"
                          },
                          "where": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      ]
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UsersCommonToSpendersActiveAndNamedQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new UsersCommonToSpendersActiveAndNamedQuery().Result
            ).TextValue
        );
    }
}
