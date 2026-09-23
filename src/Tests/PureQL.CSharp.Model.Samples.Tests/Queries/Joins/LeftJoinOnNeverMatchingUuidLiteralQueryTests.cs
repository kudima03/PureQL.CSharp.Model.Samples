using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record LeftJoinOnNeverMatchingUuidLiteralQueryTests
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
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "hours"
                    },
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      },
                      "alias": "customer"
                    }
                  ],
                  "joins": [
                    {
                      "type": "left",
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
                          "type": {
                            "name": "uuid"
                          },
                          "value": "00000000-0000-0000-0000-000000000000"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LeftJoinOnNeverMatchingUuidLiteralQuery().Value).TextValue
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
                      "name": "hours",
                      "type": "double"
                    },
                    {
                      "name": "customer",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "100.5",
                      ""
                    ],
                    [
                      "50",
                      ""
                    ],
                    [
                      "200",
                      ""
                    ],
                    [
                      "75.25",
                      ""
                    ],
                    [
                      "300",
                      ""
                    ],
                    [
                      "100.5",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new LeftJoinOnNeverMatchingUuidLiteralQuery().Result
            ).TextValue
        );
    }
}
