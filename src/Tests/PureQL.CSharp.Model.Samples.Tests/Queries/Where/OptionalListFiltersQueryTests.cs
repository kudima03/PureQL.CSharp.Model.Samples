using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record OptionalListFiltersQueryTests
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
                        "operator": "not",
                        "type": {
                          "name": "boolean"
                        },
                        "condition": {
                          "operator": "in",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "list": {
                            "type": {
                              "name": "stringList"
                            },
                            "value": [
                              "cancelled"
                            ]
                          }
                        }
                      },
                      {
                        "operator": "or",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "not",
                            "type": {
                              "name": "boolean"
                            },
                            "condition": {
                              "param": "filter_by_status",
                              "type": {
                                "name": "boolean"
                              }
                            }
                          },
                          {
                            "operator": "in",
                            "type": {
                              "name": "boolean"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "list": {
                              "param": "statuses",
                              "type": {
                                "name": "stringList"
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
                            "operator": "not",
                            "type": {
                              "name": "boolean"
                            },
                            "condition": {
                              "param": "filter_by_user",
                              "type": {
                                "name": "boolean"
                              }
                            }
                          },
                          {
                            "operator": "in",
                            "type": {
                              "name": "boolean"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "list": {
                              "param": "user_ids",
                              "type": {
                                "name": "uuidList"
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
                    },
                    {
                      "alias": "order_total",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_total",
                        "type": {
                          "name": "decimal"
                        }
                      }
                    },
                    {
                      "alias": "is_open",
                      "expression": {
                        "operator": "in",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string"
                          }
                        },
                        "list": {
                          "type": {
                            "name": "stringList"
                          },
                          "value": [
                            "pending",
                            "processing"
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
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
            new QueryJson(new OptionalListFiltersQuery().Value).TextValue
        );
    }
}
