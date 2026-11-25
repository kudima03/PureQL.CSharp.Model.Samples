using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record QuadrantsOfTwoInlineListsQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "subqueries": [
                    {
                      "name": "urgency",
                      "rows": [
                        [
                          {
                            "alias": "level",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "urgent"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "level",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "not urgent"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "importance",
                      "rows": [
                        [
                          {
                            "alias": "level",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "important"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "level",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "not important"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "urgency"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "importance",
                      "on": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    }
                  ],
                  "aggregate": [
                    {
                      "alias": "quadrants",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "names",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "concatWith",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "source": "urgency",
                              "field": "level",
                              "type": {
                                "name": "string"
                              }
                            },
                            {
                              "source": "importance",
                              "field": "level",
                              "type": {
                                "name": "string"
                              }
                            }
                          ],
                          "separator": {
                            "type": {
                              "name": "string"
                            },
                            "value": " and "
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": "; "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "urgency",
                              "field": "level",
                              "type": {
                                "name": "string"
                              }
                            },
                            "direction": "desc"
                          },
                          {
                            "expression": {
                              "source": "importance",
                              "field": "level",
                              "type": {
                                "name": "string"
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
            new QueryJson(new QuadrantsOfTwoInlineListsQuery().Value).TextValue
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
                      "name": "quadrants",
                      "type": "long"
                    },
                    {
                      "name": "names",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "4",
                      "urgent and important; urgent and not important; not urgent and important; not urgent and not important"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new QuadrantsOfTwoInlineListsQuery().Result).TextValue
        );
    }
}
