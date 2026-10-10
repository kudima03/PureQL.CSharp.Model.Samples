using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record LeftJoinNullableFieldsQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "type": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
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
                    "operator": "or",
                    "conditions": [
                      {
                        "operator": "equal",
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
                      },
                      {
                        "operator": "greaterThan",
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 100
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "status",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "operator": "coalesce",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "none"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "total_after_discount",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "subtract",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 10
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LeftJoinNullableFieldsQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "total_after_discount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "shipped",
                      "90.5"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "shipped",
                      "190"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "shipped",
                      "290"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "pending",
                      "90.5"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "none",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "none",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LeftJoinNullableFieldsQuery().Result).TextValue
        );
    }
}
