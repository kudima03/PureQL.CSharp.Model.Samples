using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record ComputedGroupKeyQueryTests
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
                  "groupBy": [
                    {
                      "alias": "size_bucket",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "operator": "if",
                        "condition": {
                          "operator": "greaterThan",
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_total",
                            "type": {
                              "name": "decimal"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "decimal"
                            },
                            "value": 100
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "large"
                        },
                        "else": {
                          "type": {
                            "name": "string"
                          },
                          "value": "small"
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "size_bucket",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "orders",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "count"
                      }
                    },
                    {
                      "alias": "average_total",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "average",
                        "selector": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_total",
                          "type": {
                            "name": "decimal"
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "key": 0,
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ComputedGroupKeyQuery().Value).TextValue
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
                      "name": "size_bucket",
                      "type": "string"
                    },
                    {
                      "name": "orders",
                      "type": "long"
                    },
                    {
                      "name": "average_total",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "large",
                      "4",
                      "175.25"
                    ],
                    [
                      "small",
                      "2",
                      "62.625"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ComputedGroupKeyQuery().Result).TextValue
        );
    }
}
