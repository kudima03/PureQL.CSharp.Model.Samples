using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record SumOfEachDateDiffDaysGroupedByOrderUserIdQueryTests
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
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "operator": "sum",
                      "arg": {
                        "operator": "eachDateDiffDays",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        }
                      },
                      "alias": "totalSpanDays"
                    }
                  ],
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
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SumOfEachDateDiffDaysGroupedByOrderUserIdQuery().Value
            ).TextValue
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
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "totalSpanDays",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "3199"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "1171"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "579"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "3583"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SumOfEachDateDiffDaysGroupedByOrderUserIdQuery().Result
            ).TextValue
        );
    }
}
