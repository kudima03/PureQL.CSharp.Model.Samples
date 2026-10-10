using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record TypedNullLiteralsQueryTests
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
                      "alias": "shipped_total",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "if",
                        "condition": {
                          "operator": "equal",
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        },
                        "then": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "else": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "always_null",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "subtract",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          {
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "value": null
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TypedNullLiteralsQuery().Value).TextValue
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
                      "name": "shipped_total",
                      "type": "double"
                    },
                    {
                      "name": "always_null",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "100.5",
                      ""
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "",
                      ""
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "200",
                      ""
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "",
                      ""
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "300",
                      ""
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TypedNullLiteralsQuery().Result).TextValue
        );
    }
}
