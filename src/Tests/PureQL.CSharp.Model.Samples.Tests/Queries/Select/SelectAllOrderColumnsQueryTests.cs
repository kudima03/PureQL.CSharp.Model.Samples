using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectAllOrderColumnsQueryTests
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
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    },
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
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "placed_on",
                      "type": {
                        "name": "date"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SelectAllOrderColumnsQuery().Value).TextValue
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
                    },
                    {
                      "name": "order_user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "placed_at",
                      "type": "datetime"
                    },
                    {
                      "name": "placed_on",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "100.5",
                      "shipped",
                      "2024-06-01T10:00:00",
                      "2024-06-01"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "00000001-0000-0000-0000-000000000000",
                      "50",
                      "pending",
                      "2024-06-02T11:00:00",
                      "2024-06-02"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "200",
                      "shipped",
                      "2024-06-03T12:00:00",
                      "2024-06-03"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "75.25",
                      "cancelled",
                      "2024-06-04T13:00:00",
                      "2024-06-04"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "300",
                      "shipped",
                      "2024-06-05T14:00:00",
                      "2024-06-05"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "00000004-0000-0000-0000-000000000000",
                      "100.5",
                      "pending",
                      "2024-06-06T15:00:00",
                      "2024-06-06"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new SelectAllOrderColumnsQuery().Result).TextValue
        );
    }
}
