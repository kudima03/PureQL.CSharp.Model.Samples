using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record LeftJoinOnKeyAndThresholdQueryTests
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
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "joins": [
                    {
                      "type": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "eachAnd",
                        "conditions": [
                          {
                            "operator": "eachEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "operator": "eachGreaterThan",
                            "left": {
                              "entity": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "number"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 100
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LeftJoinOnKeyAndThresholdQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann"
                    ],
                    [
                      "Bob"
                    ],
                    [
                      "Cara"
                    ],
                    [
                      "Dan"
                    ],
                    [
                      "Eve"
                    ],
                    [
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LeftJoinOnKeyAndThresholdQuery().Result).TextValue
        );
    }
}
