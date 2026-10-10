using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record OuterJoinChainQueryTests
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
                    "entity": "audit.logins"
                  },
                  "joins": [
                    {
                      "type": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "audit.logins",
                          "field": "login_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "type": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "report",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "report",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_id",
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
                      "alias": "login_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "audit.logins",
                        "field": "login_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "employee",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_name",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "report",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "expression": {
                        "source": "report",
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
            new QueryJson(new OuterJoinChainQuery().Value).TextValue
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
                      "name": "login_id",
                      "type": "uuid"
                    },
                    {
                      "name": "employee",
                      "type": "string"
                    },
                    {
                      "name": "report",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000191-0000-0000-0000-000000000000",
                      "Grace",
                      "Hank"
                    ],
                    [
                      "00000191-0000-0000-0000-000000000000",
                      "Grace",
                      "Iris"
                    ],
                    [
                      "00000192-0000-0000-0000-000000000000",
                      "Grace",
                      "Hank"
                    ],
                    [
                      "00000192-0000-0000-0000-000000000000",
                      "Grace",
                      "Iris"
                    ],
                    [
                      "00000193-0000-0000-0000-000000000000",
                      "Hank",
                      "Jack"
                    ],
                    [
                      "00000194-0000-0000-0000-000000000000",
                      "",
                      "Grace"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OuterJoinChainQuery().Result).TextValue
        );
    }
}
