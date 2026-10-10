using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record FullJoinQueryTests
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
                      "type": "full",
                      "entity": "schema_with_foreign_keys.employees",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "audit.logins",
                          "field": "login_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_id",
                      "type": {
                        "name": "uuid",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "coalesce",
                        "values": [
                          {
                            "source": "audit.logins",
                            "field": "login_user_id",
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            }
                          },
                          {
                            "source": "schema_with_foreign_keys.employees",
                            "field": "employee_user_id",
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "employee",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "operator": "coalesce",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.employees",
                            "field": "employee_name",
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FullJoinQuery().Value).TextValue
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
                      "name": "employee",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Grace"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Grace"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Hank"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "none"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Jack"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "Iris"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FullJoinQuery().Result).TextValue
        );
    }
}
