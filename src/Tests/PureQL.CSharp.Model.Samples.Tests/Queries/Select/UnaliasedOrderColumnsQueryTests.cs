using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record UnaliasedOrderColumnsQueryTests
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
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new UnaliasedOrderColumnsQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "shipped"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "pending"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "shipped"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "cancelled"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "shipped"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "pending"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new UnaliasedOrderColumnsQuery().Result).TextValue
        );
    }
}
