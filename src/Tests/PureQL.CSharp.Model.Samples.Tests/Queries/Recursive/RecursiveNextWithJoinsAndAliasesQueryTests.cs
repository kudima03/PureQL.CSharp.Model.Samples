using PureQL.CSharp.Model.Samples.Queries.Recursive;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Recursive;

public sealed record RecursiveNextWithJoinsAndAliasesQueryTests
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
                      "name": "buyers",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
                        },
                        "where": {
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
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "key": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "tree",
                      "recursive": {
                        "seed": {
                          "from": {
                            "entity": "schema_with_foreign_keys.employees"
                          },
                          "joins": [
                            {
                              "kind": "left",
                              "entity": "schema_with_foreign_keys.users",
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
                                      "source": "account",
                                      "field": "user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "source": "schema_with_foreign_keys.employees",
                                      "field": "employee_user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  },
                                  {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "account",
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
                              },
                              "alias": "account"
                            }
                          ],
                          "where": {
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
                              "type": {
                                "name": "uuid",
                                "nullable": true
                              },
                              "value": null
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
                              "alias": "parent_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_manager_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "alias": "root_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "root_user_id",
                              "expression": {
                                "source": "schema_with_foreign_keys.employees",
                                "field": "employee_user_id",
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
                              "alias": "path",
                              "expression": {
                                "operator": "asString",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.employees",
                                  "field": "employee_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              }
                            },
                            {
                              "alias": "other_name_than_root",
                              "expression": {
                                "type": {
                                  "name": "boolean"
                                },
                                "value": false
                              }
                            },
                            {
                              "alias": "shown_as",
                              "expression": {
                                "operator": "coalesce",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "source": "account",
                                    "field": "user_name",
                                    "type": {
                                      "name": "string",
                                      "nullable": true
                                    }
                                  },
                                  {
                                    "source": "schema_with_foreign_keys.employees",
                                    "field": "employee_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                ]
                              }
                            }
                          ]
                        },
                        "next": {
                          "from": {
                            "subquery": "tree",
                            "alias": "parent"
                          },
                          "joins": [
                            {
                              "kind": "inner",
                              "entity": "schema_with_foreign_keys.employees",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "child",
                                  "field": "employee_manager_id",
                                  "type": {
                                    "name": "uuid",
                                    "nullable": true
                                  }
                                },
                                "right": {
                                  "source": "parent",
                                  "field": "id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              "alias": "child"
                            },
                            {
                              "kind": "left",
                              "entity": "schema_with_foreign_keys.users",
                              "on": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "top",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "source": "parent",
                                  "field": "root_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              "alias": "top"
                            },
                            {
                              "kind": "left",
                              "entity": "schema_with_foreign_keys.users",
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
                                      "source": "account",
                                      "field": "user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "source": "child",
                                      "field": "employee_user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  },
                                  {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "account",
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
                              },
                              "alias": "account"
                            }
                          ],
                          "where": {
                            "operator": "and",
                            "type": {
                              "name": "boolean"
                            },
                            "conditions": [
                              {
                                "operator": "in",
                                "type": {
                                  "name": "boolean"
                                },
                                "operand": {
                                  "source": "child",
                                  "field": "employee_user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "list": {
                                  "subquery": "buyers",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              {
                                "operator": "lessThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "parent",
                                  "field": "depth",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 2
                                }
                              },
                              {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "top",
                                  "field": "user_active",
                                  "type": {
                                    "name": "boolean",
                                    "nullable": true
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
                          },
                          "select": [
                            {
                              "alias": "id",
                              "expression": {
                                "source": "child",
                                "field": "employee_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "parent_id",
                              "expression": {
                                "source": "child",
                                "field": "employee_manager_id",
                                "type": {
                                  "name": "uuid",
                                  "nullable": true
                                }
                              }
                            },
                            {
                              "alias": "root_id",
                              "expression": {
                                "source": "parent",
                                "field": "root_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            {
                              "alias": "root_user_id",
                              "expression": {
                                "source": "parent",
                                "field": "root_user_id",
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
                                    "source": "parent",
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
                              "alias": "path",
                              "expression": {
                                "operator": "concat",
                                "type": {
                                  "name": "string"
                                },
                                "values": [
                                  {
                                    "source": "parent",
                                    "field": "path",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "/"
                                  },
                                  {
                                    "operator": "asString",
                                    "type": {
                                      "name": "string"
                                    },
                                    "operand": {
                                      "source": "child",
                                      "field": "employee_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  }
                                ]
                              }
                            },
                            {
                              "alias": "other_name_than_root",
                              "expression": {
                                "operator": "notEqual",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "child",
                                  "field": "employee_name",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Grace"
                                }
                              }
                            },
                            {
                              "alias": "shown_as",
                              "expression": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "account",
                                    "field": "user_name",
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
                                "then": {
                                  "source": "child",
                                  "field": "employee_name",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "else": {
                                  "operator": "coalesce",
                                  "type": {
                                    "name": "string"
                                  },
                                  "values": [
                                    {
                                      "source": "account",
                                      "field": "user_name",
                                      "type": {
                                        "name": "string",
                                        "nullable": true
                                      }
                                    },
                                    {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": ""
                                    }
                                  ]
                                }
                              }
                            }
                          ]
                        },
                        "maxDepth": 4
                      }
                    },
                    {
                      "name": "sizes",
                      "query": {
                        "from": {
                          "subquery": "tree"
                        },
                        "groupBy": [
                          {
                            "alias": "root_id",
                            "expression": {
                              "source": "tree",
                              "field": "root_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ],
                        "select": [
                          {
                            "alias": "root_id",
                            "expression": {
                              "key": "root_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          {
                            "alias": "members",
                            "expression": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "subquery": "tree",
                    "alias": "below"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "tree",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "below",
                          "field": "parent_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "above",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      },
                      "alias": "above"
                    },
                    {
                      "kind": "inner",
                      "subquery": "sizes",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "sizes",
                          "field": "root_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "below",
                          "field": "root_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "employee_id",
                      "expression": {
                        "source": "below",
                        "field": "id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "shown_as",
                      "expression": {
                        "source": "below",
                        "field": "shown_as",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "parent_shown_as",
                      "expression": {
                        "source": "above",
                        "field": "shown_as",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "levels_below_root",
                      "expression": {
                        "source": "below",
                        "field": "depth",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "tree_members",
                      "expression": {
                        "source": "sizes",
                        "field": "members",
                        "type": {
                          "name": "integer"
                        }
                      }
                    },
                    {
                      "alias": "path",
                      "expression": {
                        "source": "below",
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
                        "source": "below",
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
            new QueryJson(new RecursiveNextWithJoinsAndAliasesQuery().Value).TextValue
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
                      "name": "employee_id",
                      "type": "uuid"
                    },
                    {
                      "name": "shown_as",
                      "type": "string"
                    },
                    {
                      "name": "parent_shown_as",
                      "type": "string"
                    },
                    {
                      "name": "levels_below_root",
                      "type": "long"
                    },
                    {
                      "name": "tree_members",
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
                      "000002be-0000-0000-0000-000000000000",
                      "Hank",
                      "Ann",
                      "1",
                      "3",
                      "000002bd-0000-0000-0000-000000000000/000002be-0000-0000-0000-000000000000"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "Cara",
                      "Ann",
                      "1",
                      "3",
                      "000002bd-0000-0000-0000-000000000000/000002bf-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RecursiveNextWithJoinsAndAliasesQuery().Result).TextValue
        );
    }
}
