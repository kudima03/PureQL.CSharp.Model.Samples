using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectUuidStringAndDoubleColumnsTogetherFromOrdersQueryTests
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
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      }
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SelectUuidStringAndDoubleColumnsTogetherFromOrdersQuery().Value
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_status",
                      "type": "string"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "shipped",
                      "100.5"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "pending",
                      "50"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "shipped",
                      "200"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "cancelled",
                      "75.25"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "shipped",
                      "300"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "pending",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectUuidStringAndDoubleColumnsTogetherFromOrdersQuery().Result
            ).TextValue
        );
    }
}
