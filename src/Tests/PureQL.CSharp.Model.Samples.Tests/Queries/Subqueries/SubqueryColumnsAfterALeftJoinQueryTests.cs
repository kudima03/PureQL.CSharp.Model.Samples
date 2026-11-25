using PureQL.CSharp.Model.Samples.Queries.Subqueries;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Subqueries;

public sealed record SubqueryColumnsAfterALeftJoinQueryTests
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
                      "name": "managers",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.employees"
                        },
                        "select": [
                          {
                            "alias": "id",
                            "expression": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "issued_by",
                            "expression": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.employees"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "managers",
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
                              "source": "managers",
                              "field": "id",
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
                              "source": "managers",
                              "field": "issued_by",
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
                          "source": "managers",
                          "field": "issued_by",
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
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "manager_user_id",
                      "expression": {
                        "source": "managers",
                        "field": "issued_by",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    },
                    {
                      "alias": "manager_name",
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
            new QueryJson(new SubqueryColumnsAfterALeftJoinQuery().Value).TextValue
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
                      "name": "manager_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "manager_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "",
                      ""
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "Ann"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "Ann"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "Bob"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SubqueryColumnsAfterALeftJoinQuery().Result).TextValue
        );
    }
}
