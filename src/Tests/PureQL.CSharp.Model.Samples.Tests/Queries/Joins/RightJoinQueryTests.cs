using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record RightJoinQueryTests
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
                      "type": "right",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "and",
                        "conditions": [
                          {
                            "operator": "equal",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        ]
                      }
                    }
                  ],
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
                      "alias": "user_name",
                      "type": {
                        "name": "string",
                        "nullable": true
                      },
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
            new QueryJson(new RightJoinQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "Ann"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "Ann"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      ""
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "Cara"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "Cara"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "Dan"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RightJoinQuery().Result).TextValue
        );
    }
}
