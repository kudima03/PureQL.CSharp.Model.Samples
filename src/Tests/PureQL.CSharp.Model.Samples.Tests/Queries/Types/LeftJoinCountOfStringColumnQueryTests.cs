using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record LeftJoinCountOfStringColumnQueryTests
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
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      },
                      "alias": "status_count"
                    }
                  ],
                  "joins": [
                    {
                      "type": "left",
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LeftJoinCountOfStringColumnQuery().Value).TextValue
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
                      "name": "status_count",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LeftJoinCountOfStringColumnQuery().Result).TextValue
        );
    }
}
