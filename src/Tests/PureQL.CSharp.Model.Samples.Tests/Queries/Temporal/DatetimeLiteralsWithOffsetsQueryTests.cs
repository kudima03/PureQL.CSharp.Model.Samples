using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record DatetimeLiteralsWithOffsetsQueryTests
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
                  "where": {
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-02T03:00:00+03:00"
                        }
                      },
                      {
                        "operator": "lessThan",
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_at",
                          "type": {
                            "name": "datetime"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-05T14:00:00.123456+05:30"
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DatetimeLiteralsWithOffsetsQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DatetimeLiteralsWithOffsetsQuery().Result).TextValue
        );
    }
}
