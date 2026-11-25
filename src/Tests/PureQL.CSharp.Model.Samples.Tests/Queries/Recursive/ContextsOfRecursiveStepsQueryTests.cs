using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record ContextsOfRecursiveStepsQueryTests
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
                      "name": "chain",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "where": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bd-0000-0000-0000-000000000000"
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "depth",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "chain",
                              "on": {
                                "operator": "and",
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
                                      "source": "schema_with_foreign_keys.employees",
                                      "field": "employee_manager_id",
                                      "type": {
                                        "name": "uuid",
                                        "nullable": true
                                      }
                                    },
                                    "right": {
                                      "source": "chain",
                                      "field": "id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  },
                                  {
                                    "operator": "notEqual",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "operator": "foldCase",
                                      "type": {
                                        "name": "string"
                                      },
                                      "operand": {
                                        "source": "schema_with_foreign_keys.employees",
                                        "field": "employee_name",
                                        "type": {
                                          "name": "string"
                                        }
                                      }
                                    },
                                    "right": {
                                      "operator": "foldCase",
                                      "type": {
                                        "name": "string"
                                      },
                                      "operand": {
                                        "source": "chain",
                                        "field": "label",
                                        "type": {
                                          "name": "string"
                                        }
                                      }
                                    }
                                  }
                                ]
                              }
                            }
                          ],
                          "where": {
                            "operator": "and",
                            "type": {
                              "name": "boolean"
                            },
                            "conditions": [
                              {
                                "operator": "lessThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "chain",
                                  "field": "depth",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 10
                                }
                              },
                              {
                                "operator": "contains",
                                "type": {
                                  "name": "boolean"
                                },
                                "operand": {
                                  "operator": "concat",
                                  "type": {
                                    "name": "string"
                                  },
                                  "values": [
                                    {
                                      "source": "schema_with_foreign_keys.employees",
                                      "field": "employee_name",
                                      "type": {
                                        "name": "string"
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "@example.org"
                                    }
                                  ]
                                },
                                "pattern": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "k@"
                                }
                              }
                            ]
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "depth",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "source": "chain",
                                    "field": "depth",
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
                              "alias": "label",
                              "expression": {
                                "operator": "concatWith",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "source": "chain",
                                    "field": "label",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "operator": "if",
                                    "type": {
                                      "name": "string",
                                      "nullable": true
                                    },
                                    "condition": {
                                      "operator": "greaterThan",
                                      "type": {
                                        "name": "boolean"
                                      },
                                      "left": {
                                        "source": "schema_with_foreign_keys.employees",
                                        "field": "employee_shift_start",
                                        "type": {
                                          "name": "time"
                                        }
                                      },
                                      "right": {
                                        "type": {
                                          "name": "time"
                                        },
                                        "value": "09:00:00"
                                      }
                                    },
                                    "then": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "Mid"
                                    },
                                    "else": {
                                      "type": {
                                        "name": "string",
                                        "nullable": true
                                      },
                                      "value": null
                                    }
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.employees",
                                    "field": "employee_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ],
                                "separator": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": " > "
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 50
                      }
                    },
                    {
                      "name": "reach",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "where": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "000002bd-0000-0000-0000-000000000000"
                            }
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "depth",
                              "expression": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 0
                              }
                            },
                            {
                              "alias": "label",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "subquery": "reach",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_manager_id",
                                  "type": {
                                    "name": "uuid",
                                    "nullable": true
                                  }
                                },
                                "right": {
                                  "source": "reach",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            }
                          ],
                          "where": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "reach",
                              "field": "depth",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          "groupBy": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "name",
                              "expression": {
                                "operator": "upper",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_name",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              }
                            }
                          ],
                          "having": {
                            "operator": "or",
                            "type": {
                              "name": "boolean"
                            },
                            "conditions": [
                              {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 0
                                }
                              },
                              {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "key": "name",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "ROOT"
                                }
                              }
                            ]
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "key": "id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "depth",
                              "expression": {
                                "operator": "add",
                                "type": {
                                  "name": "integer"
                                },
                                "values": [
                                  {
                                    "operator": "min",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "selector": {
                                      "source": "reach",
                                      "field": "depth",
                                      "type": {
                                        "name": "integer"
                                      }
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
                              "alias": "label",
                              "expression": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "key": "name",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": " of "
                                  },
                                  {
                                    "operator": "asString",
                                    "type": {
                                      "name": "string"
                                    },
                                    "operand": {
                                      "operator": "count",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "over": "all"
                                    }
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "maxDepth": 1
                      }
                    }
                  ],
                  "from": {
                    "subquery": "chain"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "reach",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "chain",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "reach",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "chain",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "depth",
                      "expression": {
                        "source": "chain",
                        "field": "depth",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "label",
                      "expression": {
                        "source": "chain",
                        "field": "label",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "reach_label",
                      "expression": {
                        "source": "reach",
                        "field": "label",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "chain",
                        "field": "depth",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "chain",
                        "field": "label",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "chain",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ContextsOfRecursiveStepsQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "depth",
                      "type": "long"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    },
                    {
                      "name": "reach_label",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "0",
                      "Grace",
                      "Grace"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "1",
                      "Grace > Mid > Hank",
                      "HANK of 2"
                    ],
                    [
                      "000002c0-0000-0000-0000-000000000000",
                      "2",
                      "Grace > Mid > Hank > Jack",
                      ""
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ContextsOfRecursiveStepsQuery().Result).TextValue
        );
    }
}
