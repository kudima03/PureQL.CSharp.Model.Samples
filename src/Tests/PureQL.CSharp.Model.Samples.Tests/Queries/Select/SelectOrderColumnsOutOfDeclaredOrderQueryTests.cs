using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectOrderColumnsOutOfDeclaredOrderQueryTests
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
                      "field": "order_id",
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new SelectOrderColumnsOutOfDeclaredOrderQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "order_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "shipped",
                      "00000065-0000-0000-0000-000000000000",
                      "100.5"
                    ],
                    [
                      "pending",
                      "00000066-0000-0000-0000-000000000000",
                      "50"
                    ],
                    [
                      "shipped",
                      "00000067-0000-0000-0000-000000000000",
                      "200"
                    ],
                    [
                      "cancelled",
                      "00000068-0000-0000-0000-000000000000",
                      "75.25"
                    ],
                    [
                      "shipped",
                      "00000069-0000-0000-0000-000000000000",
                      "300"
                    ],
                    [
                      "pending",
                      "0000006a-0000-0000-0000-000000000000",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectOrderColumnsOutOfDeclaredOrderQuery().Result
            ).TextValue
        );
    }
}
