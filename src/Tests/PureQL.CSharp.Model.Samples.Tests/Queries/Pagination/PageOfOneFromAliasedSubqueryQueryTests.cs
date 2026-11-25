using PureQL.CSharp.Model.Samples.Queries.Pagination;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Pagination;

public sealed record PageOfOneFromAliasedSubqueryQueryTests
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
                      "name": "recent",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "joins": [
                          {
                            "kind": "inner",
                            "entity": "schema_with_foreign_keys.users",
                            "on": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          }
                        ],
                        "where": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": true
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
                            "alias": "is_paid",
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "recent",
                    "alias": "r"
                  },
                  "where": {
                    "source": "r",
                    "field": "is_paid",
                    "type": {
                      "name": "boolean"
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "r",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "r",
                        "field": "is_paid",
                        "type": {
                          "name": "boolean"
                        }
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "source": "r",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "value": null
                      }
                    },
                    {
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    }
                  ],
                  "pagination": {
                    "take": 1
                  }
                }
                """
            ).TextValue,
            new QueryJson(new PageOfOneFromAliasedSubqueryQuery().Value).TextValue
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
                      "00000065-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new PageOfOneFromAliasedSubqueryQuery().Result).TextValue
        );
    }
}
