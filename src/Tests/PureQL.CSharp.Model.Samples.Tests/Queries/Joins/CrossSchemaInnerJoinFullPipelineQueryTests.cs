using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record CrossSchemaInnerJoinFullPipelineQueryTests
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
                      "operator": "count",
                      "arg": {
                        "entity": "audit.logins",
                        "field": "login_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "loginCount"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "audit.logins",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "audit.logins",
                          "field": "login_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "loginCount",
                        "type": {
                          "name": "number"
                        }
                      },
                      "direction": "desc"
                    }
                  ],
                  "pagination": {
                    "skip": 0,
                    "take": 5
                  },
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(new CrossSchemaInnerJoinFullPipelineQuery().Value).TextValue
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
                      "name": "loginCount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2"
                    ],
                    [
                      "1"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CrossSchemaInnerJoinFullPipelineQuery().Result).TextValue
        );
    }
}
