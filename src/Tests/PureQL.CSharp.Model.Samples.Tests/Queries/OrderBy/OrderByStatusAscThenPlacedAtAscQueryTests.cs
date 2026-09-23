using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByStatusAscThenPlacedAtAscQueryTests
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
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_at",
                      "type": {
                        "name": "datetime"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByStatusAscThenPlacedAtAscQuery().Value).TextValue
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
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "placed_at",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "cancelled",
                      "2024-06-04T13:00:00"
                    ],
                    [
                      "pending",
                      "2024-06-02T11:00:00"
                    ],
                    [
                      "pending",
                      "2024-06-06T15:00:00"
                    ],
                    [
                      "shipped",
                      "2024-06-01T10:00:00"
                    ],
                    [
                      "shipped",
                      "2024-06-03T12:00:00"
                    ],
                    [
                      "shipped",
                      "2024-06-05T14:00:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByStatusAscThenPlacedAtAscQuery().Result).TextValue
        );
    }
}
