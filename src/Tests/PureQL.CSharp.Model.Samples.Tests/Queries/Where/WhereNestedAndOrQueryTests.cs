using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record WhereNestedAndOrQueryTests
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
                    "operator": "or",
                    "conditions": [
                      {
                        "operator": "and",
                        "conditions": [
                          {
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
                          {
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
                                "name": "integer"
                              },
                              "value": 100
                            }
                          }
                        ]
                      },
                      {
                        "operator": "and",
                        "conditions": [
                          {
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
                              "value": "pending"
                            }
                          },
                          {
                            "operator": "lessThan",
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 60
                            }
                          }
                        ]
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
                    },
                    {
                      "alias": "order_status",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WhereNestedAndOrQuery().Value).TextValue
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
                      "00000069-0000-0000-0000-000000000000",
                      "shipped"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WhereNestedAndOrQuery().Result).TextValue
        );
    }
}
