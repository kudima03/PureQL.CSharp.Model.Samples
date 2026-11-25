using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record RightJoinAfterLeftJoinsQueryTests
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
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "notEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "staff",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "staff",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "kind": "right",
                      "entity": "schema_with_foreign_keys.order_items",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.order_items",
                          "field": "item_order_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "item_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "user_tenant_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_tenant_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "employee_name",
                      "expression": {
                        "source": "staff",
                        "field": "employee_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RightJoinAfterLeftJoinsQuery().Value).TextValue
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
                      "name": "item_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "user_tenant_id",
                      "type": "uuid"
                    },
                    {
                      "name": "employee_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "00000065-0000-0000-0000-000000000000",
                      "",
                      "",
                      ""
                    ],
                    [
                      "0000012e-0000-0000-0000-000000000000",
                      "00000065-0000-0000-0000-000000000000",
                      "",
                      "",
                      ""
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "00000067-0000-0000-0000-000000000000",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "00000069-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "00000385-0000-0000-0000-000000000000",
                      "Iris"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RightJoinAfterLeftJoinsQuery().Result).TextValue
        );
    }
}
