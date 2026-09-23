using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record CrossSchemaLeftJoinFullPipelineQueryTests
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
                      "type": "left",
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
                  "having": {
                    "operator": "greaterThanOrEqual",
                    "left": {
                      "operator": "count",
                      "arg": {
                        "entity": "audit.logins",
                        "field": "login_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 1
                    }
                  },
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
                    "take": 1
                  },
                  "distinct": true
                }
                """
            ).TextValue,
            new QueryJson(new CrossSchemaLeftJoinFullPipelineQuery().Value).TextValue
        );
    }
}
