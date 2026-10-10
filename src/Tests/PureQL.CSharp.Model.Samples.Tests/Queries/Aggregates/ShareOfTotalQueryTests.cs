using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record ShareOfTotalQueryTests
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
                    },
                    {
                      "alias": "order_total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "share_of_total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "divide",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "operator": "sum",
                            "selector": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ShareOfTotalQuery().Value).TextValue
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
                      "name": "order_total",
                      "type": "double"
                    },
                    {
                      "name": "share_of_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "100.5",
                      "0.12163388804841149"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "50",
                      "0.060514372163388806"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      "0.24205748865355523"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "75.25",
                      "0.09107413010590015"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      "0.3630862329803328"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "100.5",
                      "0.12163388804841149"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ShareOfTotalQuery().Result).TextValue
        );
    }
}
