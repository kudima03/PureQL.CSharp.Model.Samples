using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record MinOfEachDateTimeAddSecondsGroupedByOrderUserIdQueryTests
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
                      "operator": "min_datetime",
                      "arg": {
                        "operator": "eachDatetimeAddSeconds",
                        "left": {
                          "entity": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 1800
                        }
                      },
                      "alias": "earliestProjectedInstant"
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
                new MinOfEachDateTimeAddSecondsGroupedByOrderUserIdQuery().Value
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
                      "name": "earliestProjectedInstant",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "2024-06-01T10:30:00"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "2024-06-03T12:30:00"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "2024-06-06T15:30:00"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "2024-06-04T13:30:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new MinOfEachDateTimeAddSecondsGroupedByOrderUserIdQuery().Result
            ).TextValue
        );
    }
}
