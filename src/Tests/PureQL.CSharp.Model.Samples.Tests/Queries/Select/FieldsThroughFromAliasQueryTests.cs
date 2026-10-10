using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record FieldsThroughFromAliasQueryTests
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
                    "entity": "schema_with_foreign_keys.orders",
                    "alias": "o"
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "o",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "placed_at",
                      "type": {
                        "name": "datetime"
                      },
                      "expression": {
                        "source": "o",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    },
                    {
                      "alias": "amount",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "o",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FieldsThroughFromAliasQuery().Value).TextValue
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
                      "name": "placed_at",
                      "type": "datetime"
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
                      "2024-06-01T10:00:00",
                      "100.5"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-02T11:00:00",
                      "50"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-03T12:00:00",
                      "200"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-04T13:00:00",
                      "75.25"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05T14:00:00",
                      "300"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "2024-06-06T15:00:00",
                      "100.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new FieldsThroughFromAliasQuery().Result).TextValue
        );
    }
}
