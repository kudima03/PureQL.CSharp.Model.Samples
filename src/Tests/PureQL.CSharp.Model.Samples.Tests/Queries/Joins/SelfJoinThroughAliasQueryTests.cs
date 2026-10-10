using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record SelfJoinThroughAliasQueryTests
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
                    "alias": "e"
                  },
                  "joins": [
                    {
                      "type": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "manager",
                      "on": {
                        "operator": "equal",
                        "left": {
                          "source": "e",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "manager",
                          "field": "employee_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "name",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "manager_name",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
                      "expression": {
                        "source": "manager",
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
            new QueryJson(new SelfJoinThroughAliasQuery().Value).TextValue
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
                      "name": "name",
                      "type": "string"
                    },
                    {
                      "name": "manager_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Grace",
                      ""
                    ],
                    [
                      "Hank",
                      "Grace"
                    ],
                    [
                      "Iris",
                      "Grace"
                    ],
                    [
                      "Jack",
                      "Hank"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SelfJoinThroughAliasQuery().Result).TextValue
        );
    }
}
