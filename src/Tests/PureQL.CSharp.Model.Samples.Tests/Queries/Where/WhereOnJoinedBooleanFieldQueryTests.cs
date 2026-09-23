using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record WhereOnJoinedBooleanFieldQueryTests
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
                    "entity": "schema_with_foreign_keys.users",
                    "field": "user_active",
                    "type": {
                      "name": "boolean"
                    }
                  },
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_id",
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
            new QueryJson(new WhereOnJoinedBooleanFieldQuery().Value).TextValue
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
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WhereOnJoinedBooleanFieldQuery().Result).TextValue
        );
    }
}
