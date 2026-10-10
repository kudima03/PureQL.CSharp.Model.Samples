using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record RoundingAndIntegerDivisionQueryTests
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
                    "entity": "schema_with_foreign_keys.products"
                  },
                  "select": [
                    {
                      "alias": "price_floor",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "floor",
                        "value": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "price_ceiling",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "ceiling",
                        "value": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "price_rounded",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "round",
                        "value": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    },
                    {
                      "alias": "price_tenths",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "round",
                        "value": {
                          "source": "schema_with_foreign_keys.products",
                          "field": "product_price",
                          "type": {
                            "name": "decimal"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "packs",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "integerDivide",
                        "left": {
                          "operator": "round",
                          "value": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        }
                      }
                    },
                    {
                      "alias": "loose",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "modulo",
                        "left": {
                          "operator": "round",
                          "value": {
                            "source": "schema_with_foreign_keys.products",
                            "field": "product_price",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 6
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RoundingAndIntegerDivisionQuery().Value).TextValue
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
                      "name": "price_floor",
                      "type": "long"
                    },
                    {
                      "name": "price_ceiling",
                      "type": "long"
                    },
                    {
                      "name": "price_rounded",
                      "type": "long"
                    },
                    {
                      "name": "price_tenths",
                      "type": "double"
                    },
                    {
                      "name": "packs",
                      "type": "long"
                    },
                    {
                      "name": "loose",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "9",
                      "10",
                      "10",
                      "10",
                      "1",
                      "4"
                    ],
                    [
                      "19",
                      "20",
                      "20",
                      "20",
                      "3",
                      "2"
                    ],
                    [
                      "4",
                      "5",
                      "5",
                      "4.5",
                      "0",
                      "5"
                    ],
                    [
                      "250",
                      "250",
                      "250",
                      "250",
                      "41",
                      "4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RoundingAndIntegerDivisionQuery().Result).TextValue
        );
    }
}
