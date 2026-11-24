using PureQL.CSharp.Model.Samples.Queries.Numbers;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Numbers;

public sealed record AgeBandsQueryTests
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
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "where": {
                    "source": "schema_with_foreign_keys.users",
                    "field": "user_active",
                    "type": {
                      "name": "boolean"
                    }
                  },
                  "groupBy": [
                    {
                      "alias": "age_band",
                      "expression": {
                        "operator": "truncate",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "age_band",
                      "expression": {
                        "key": "age_band",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "up_to",
                      "expression": {
                        "operator": "add",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "key": "age_band",
                            "type": {
                              "name": "integer"
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 9
                          }
                        ]
                      }
                    },
                    {
                      "alias": "users",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "users_to_the_nearest_ten",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "count",
                          "type": {
                            "name": "integer"
                          }
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -1
                        }
                      }
                    },
                    {
                      "alias": "youngest",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "integer"
                        },
                        "selector": {
                          "operator": "round",
                          "type": {
                            "name": "integer"
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "operator": "concat",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "key": "age_band",
                              "type": {
                                "name": "integer"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "s"
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "age_band"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AgeBandsQuery().Value).TextValue
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
                      "name": "age_band",
                      "type": "long"
                    },
                    {
                      "name": "up_to",
                      "type": "long"
                    },
                    {
                      "name": "users",
                      "type": "long"
                    },
                    {
                      "name": "users_to_the_nearest_ten",
                      "type": "long"
                    },
                    {
                      "name": "youngest",
                      "type": "long"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "20",
                      "29",
                      "1",
                      "0",
                      "28",
                      "20s"
                    ],
                    [
                      "30",
                      "39",
                      "2",
                      "0",
                      "30",
                      "30s"
                    ],
                    [
                      "40",
                      "49",
                      "1",
                      "0",
                      "42",
                      "40s"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new AgeBandsQuery().Result).TextValue
        );
    }
}
