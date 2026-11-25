using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record WalkThatStopsAtACycleQueryTests
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
                      "name": "edges",
                      "rows": [
                        [
                          {
                            "alias": "from",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "a"
                            }
                          },
                          {
                            "alias": "to",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "b"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "from",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "b"
                            }
                          },
                          {
                            "alias": "to",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "c"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "from",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "c"
                            }
                          },
                          {
                            "alias": "to",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "a"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "from",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "c"
                            }
                          },
                          {
                            "alias": "to",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "d"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "start",
                      "rows": [
                        [
                          {
                            "alias": "node",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "a"
                            }
                          }
                        ]
                      ]
                    },
                    {
                      "name": "walk",
                      "recursive": {
                        "seed": {
                          "from": {
                            "subquery": "start"
                          },
                          "select": [
                            {
                              "alias": "node",
                              "expression": {
                                "source": "start",
                                "field": "node",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "alias": "steps",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "path",
                              "expression": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "/"
                                  },
                                  {
                                    "source": "start",
                                    "field": "node",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "/"
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "walk"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "edges",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "edges",
                                  "field": "from",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "source": "walk",
                                  "field": "node",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            }
                          ],
                          "where": {
                            "operator": "not",
                            "type": {
                              "name": "boolean"
                            },
                            "condition": {
                              "operator": "contains",
                              "type": {
                                "name": "boolean"
                              },
                              "operand": {
                                "source": "walk",
                                "field": "path",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "pattern": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "/"
                                  },
                                  {
                                    "source": "edges",
                                    "field": "to",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "/"
                                  }
                                ]
                              }
                            }
                          },
                          "select": [
                            {
                              "alias": "node",
                              "expression": {
                                "source": "edges",
                                "field": "to",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "alias": "steps",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "walk",
                                    "field": "steps",
                                    "type": {
                                      "name": "integer"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 1
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "path",
                              "expression": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "source": "walk",
                                    "field": "path",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "source": "edges",
                                    "field": "to",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "/"
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "maxDepth": 100
                      }
                    }
                  ],
                  "from": {
                    "subquery": "walk"
                  },
                  "select": [
                    {
                      "alias": "node",
                      "expression": {
                        "source": "walk",
                        "field": "node",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "steps",
                      "expression": {
                        "source": "walk",
                        "field": "steps",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "path",
                      "expression": {
                        "source": "walk",
                        "field": "path",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "walk",
                        "field": "steps",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "walk",
                        "field": "path",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WalkThatStopsAtACycleQuery().Value).TextValue
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
                      "name": "node",
                      "type": "string"
                    },
                    {
                      "name": "steps",
                      "type": "long"
                    },
                    {
                      "name": "path",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "a",
                      "0",
                      "/a/"
                    ],
                    [
                      "b",
                      "1",
                      "/a/b/"
                    ],
                    [
                      "c",
                      "2",
                      "/a/b/c/"
                    ],
                    [
                      "d",
                      "3",
                      "/a/b/c/d/"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WalkThatStopsAtACycleQuery().Result).TextValue
        );
    }
}
