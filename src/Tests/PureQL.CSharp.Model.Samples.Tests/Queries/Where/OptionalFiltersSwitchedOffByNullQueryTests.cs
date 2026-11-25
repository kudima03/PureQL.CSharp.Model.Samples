using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record OptionalFiltersSwitchedOffByNullQueryTests
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
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "or",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "param": "status",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "param": "status",
                              "type": {
                                "name": "string",
                                "nullable": true
                              }
                            }
                          }
                        ]
                      },
                      {
                        "operator": "or",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "param": "placed_since",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            },
                            "right": {
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_at",
                              "type": {
                                "name": "datetime"
                              }
                            },
                            "right": {
                              "param": "placed_since",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            }
                          }
                        ]
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_id",
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
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_at",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "direction": "desc"
                    }
                  ],
                  "pagination": {
                    "take": 50
                  }
                }
                """
            ).TextValue,
            new QueryJson(new OptionalFiltersSwitchedOffByNullQuery().Value).TextValue
        );
    }
}
