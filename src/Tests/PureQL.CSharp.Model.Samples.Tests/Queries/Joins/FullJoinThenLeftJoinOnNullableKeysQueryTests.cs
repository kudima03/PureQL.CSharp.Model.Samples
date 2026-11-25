using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record FullJoinThenLeftJoinOnNullableKeysQueryTests
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
                    "entity": "schema_with_foreign_keys.employees"
                  },
                  "joins": [
                    {
                      "kind": "full",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "boss",
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
                              "source": "boss",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_manager_id",
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "operator": "notEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "boss",
                              "field": "employee_user_id",
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
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
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
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "employee_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "boss_id",
                      "expression": {
                        "source": "boss",
                        "field": "employee_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "boss_user_id",
                      "expression": {
                        "source": "boss",
                        "field": "employee_user_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
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
            new QueryJson(new FullJoinThenLeftJoinOnNullableKeysQuery().Value).TextValue
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
                      "name": "employee_id",
                      "type": "uuid"
                    },
                    {
                      "name": "boss_id",
                      "type": "uuid"
                    },
                    {
                      "name": "boss_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "",
                      "",
                      "Ann"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "Bob"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "000002bd-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "Cara"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "000002be-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "Dan"
                    ],
                    [
                      "",
                      "000002bf-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      ""
                    ],
                    [
                      "",
                      "000002c0-0000-0000-0000-000000000000",
                      "00000004-0000-0000-0000-000000000000",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new FullJoinThenLeftJoinOnNullableKeysQuery().Result
            ).TextValue
        );
    }
}
