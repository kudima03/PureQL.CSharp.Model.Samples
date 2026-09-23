using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachTreeOverRestrictiveJoinQueryTests
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
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachOr",
                    "conditions": [
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
                          "value": 0
                        }
                      },
                      {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "nonexistent"
                        }
                      }
                    ]
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "eachGreaterThan",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_age",
                          "type": {
                            "name": "number"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 9999
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new EachTreeOverRestrictiveJoinQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": []
                }
                """
            ).TextValue,
            new DataSetJson(new EachTreeOverRestrictiveJoinQuery().Result).TextValue
        );
    }
}
