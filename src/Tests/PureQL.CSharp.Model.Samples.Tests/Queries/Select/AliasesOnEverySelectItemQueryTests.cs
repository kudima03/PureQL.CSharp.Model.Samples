using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record AliasesOnEverySelectItemQueryTests
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
                      },
                      "alias": "id"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "alias": "state"
                    },
                    {
                      "entity": "schema_with_foreign_keys.orders",
                      "field": "order_total",
                      "type": {
                        "name": "number"
                      },
                      "alias": "amount"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AliasesOnEverySelectItemQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "state",
                      "type": "string"
                    },
                    {
                      "name": "amount",
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
            new DataSetJson(new AliasesOnEverySelectItemQuery().Result).TextValue
        );
    }
}
