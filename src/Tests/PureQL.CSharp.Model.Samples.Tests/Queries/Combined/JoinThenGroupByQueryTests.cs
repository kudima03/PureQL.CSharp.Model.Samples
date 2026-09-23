using PureQL.CSharp.Model.Samples.Queries.Combined;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Combined;

public sealed record JoinThenGroupByQueryTests
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
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "orderCount"
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.orders",
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
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
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
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new JoinThenGroupByQuery().Value).TextValue
        );
    }
}
