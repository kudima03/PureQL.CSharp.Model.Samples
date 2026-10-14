using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record JoinOnNullableKeyQueryTests
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
                    "entity": "schema_with_foreign_keys.employees",
                    "alias": "a"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "b",
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
                              "source": "a",
                              "field": "employee_manager_id",
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              }
                            },
                            "right": {
                              "source": "b",
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
                              "source": "a",
                              "field": "employee_manager_id",
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
                            "operator": "notEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "a",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "b",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "employee",
                      "expression": {
                        "source": "a",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "colleague",
                      "expression": {
                        "source": "b",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new JoinOnNullableKeyQuery().Value).TextValue
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
                      "name": "employee",
                      "type": "string"
                    },
                    {
                      "name": "colleague",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Hank",
                      "Iris"
                    ],
                    [
                      "Iris",
                      "Hank"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new JoinOnNullableKeyQuery().Result).TextValue
        );
    }
}
