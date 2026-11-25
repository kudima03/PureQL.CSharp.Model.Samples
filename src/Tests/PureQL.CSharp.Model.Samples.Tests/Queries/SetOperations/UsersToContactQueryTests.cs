using PureQL.CSharp.Model.Samples.Queries.SetOperations;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.SetOperations;

public sealed record UsersToContactQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "except": [
                    {
                      "union": [
                        {
                          "intersect": [
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
                              "from": {
                                "entity": "schema_with_foreign_keys.orders"
                              },
                              "select": [
                                {
                                  "alias": "id",
                                  "expression": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_user_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  }
                                }
                              ]
                            }
                          ]
                        },
                        {
                          "from": {
                            "entity": "schema_with_foreign_keys.orders"
                          },
                          "where": {
                            "operator": "greaterThanOrEqual",
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
                              "value": "200"
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
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
                          "field": "user_score",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "30"
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
                """
            ).TextValue,
            new QueryJson(new UsersToContactQuery().Value).TextValue
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
                      "00000002-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UsersToContactQuery().Result).TextValue
        );
    }
}
