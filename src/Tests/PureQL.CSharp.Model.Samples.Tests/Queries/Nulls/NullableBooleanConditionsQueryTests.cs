using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record NullableBooleanConditionsQueryTests
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
                      "type": "left",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "and",
                        "conditions": [
                          {
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
                          },
                          {
                            "operator": "lessThan",
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 40
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "where": {
                    "operator": "or",
                    "conditions": [
                      {
                        "operator": "equal",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          }
                        },
                        "right": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": false
                        }
                      },
                      {
                        "operator": "not",
                        "condition": {
                          "operator": "coalesce",
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean",
                                "nullable": true
                              }
                            },
                            {
                              "type": {
                                "name": "boolean"
                              },
                              "value": false
                            }
                          ]
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "user_active",
                      "type": {
                        "name": "boolean",
                        "nullable": true
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_active",
                        "type": {
                          "name": "boolean",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NullableBooleanConditionsQuery().Value).TextValue
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
                      "name": "user_active",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "False"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NullableBooleanConditionsQuery().Result).TextValue
        );
    }
}
