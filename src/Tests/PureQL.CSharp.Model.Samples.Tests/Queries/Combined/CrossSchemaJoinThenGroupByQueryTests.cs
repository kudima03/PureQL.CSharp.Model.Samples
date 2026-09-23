using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record CrossSchemaJoinThenGroupByQueryTests
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
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CrossSchemaJoinThenGroupByQuery().Value).TextValue
        );
    }
}
