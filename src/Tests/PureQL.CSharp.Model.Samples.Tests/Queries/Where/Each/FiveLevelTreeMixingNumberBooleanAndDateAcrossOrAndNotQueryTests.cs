using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record FiveLevelTreeMixingNumberBooleanAndDateAcrossOrAndNotQueryTests
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
                  "select": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachAnd",
                    "conditions": [
                      {
                        "operator": "eachOr",
                        "conditions": [
                          {
                            "operator": "eachNot",
                            "condition": {
                              "operator": "eachAnd",
                              "conditions": [
                                {
                                  "operator": "eachGreaterThan",
                                  "left": {
                                    "entity": "schema_with_foreign_keys.users",
                                    "field": "user_age",
                                    "type": {
                                      "name": "number"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "number"
                                    },
                                    "value": 26
                                  }
                                },
                                {
                                  "operator": "eachEqual",
                                  "left": {
                                    "entity": "schema_with_foreign_keys.users",
                                    "field": "user_active",
                                    "type": {
                                      "name": "boolean"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "value": true
                                  }
                                }
                              ]
                            }
                          },
                          {
                            "operator": "eachGreaterThanOrEqual",
                            "left": {
                              "entity": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "number"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "number"
                              },
                              "value": 30
                            }
                          }
                        ]
                      },
                      {
                        "operator": "eachOr",
                        "conditions": [
                          {
                            "operator": "eachNot",
                            "condition": {
                              "operator": "eachEqual",
                              "left": {
                                "entity": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": true
                              }
                            }
                          },
                          {
                            "operator": "eachAnd",
                            "conditions": [
                              {
                                "operator": "eachLessThan",
                                "left": {
                                  "entity": "schema_with_foreign_keys.users",
                                  "field": "signup_date",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2021-01-01"
                                }
                              },
                              {
                                "operator": "eachEqual",
                                "left": {
                                  "entity": "schema_with_foreign_keys.users",
                                  "field": "user_active",
                                  "type": {
                                    "name": "boolean"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "value": false
                                }
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  }
                }
                """
            ).TextValue,
            new QueryJson(
                new FiveLevelTreeMixingNumberBooleanAndDateAcrossOrAndNotQuery().Value
            ).TextValue
        );
    }
}
