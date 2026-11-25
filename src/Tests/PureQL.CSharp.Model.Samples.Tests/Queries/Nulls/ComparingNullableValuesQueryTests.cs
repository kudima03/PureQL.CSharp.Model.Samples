using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record ComparingNullableValuesQueryTests
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
                      "name": "latest_logins",
                      "query": {
                        "from": {
                          "entity": "audit.logins"
                        },
                        "groupBy": [
                          {
                            "alias": "user_id",
                            "expression": {
                              "source": "audit.logins",
                              "field": "login_user_id",
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
                          },
                          {
                            "alias": "last_audit_login",
                            "expression": {
                              "operator": "max",
                              "type": {
                                "name": "datetime"
                              },
                              "selector": {
                                "source": "audit.logins",
                                "field": "login_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "name": "first_orders",
                      "query": {
                        "from": {
                          "entity": "schema_with_foreign_keys.orders"
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
                          },
                          {
                            "alias": "first_order_at",
                            "expression": {
                              "operator": "min",
                              "type": {
                                "name": "datetime"
                              },
                              "selector": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_at",
                                "type": {
                                  "name": "datetime"
                                }
                              }
                            }
                          }
                        ]
                      }
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "subquery": "latest_logins",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "latest_logins",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    },
                    {
                      "kind": "left",
                      "subquery": "first_orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "first_orders",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "select": [
                    {
                      "alias": "user_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "logged_in_since_first_order",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "latest_logins",
                          "field": "last_audit_login",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "first_orders",
                          "field": "first_order_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "logged_in_after_first_order",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "latest_logins",
                          "field": "last_audit_login",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "first_orders",
                          "field": "first_order_at",
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "after_or_at_the_same_time",
                      "expression": {
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
                              "source": "latest_logins",
                              "field": "last_audit_login",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            },
                            "right": {
                              "source": "first_orders",
                              "field": "first_order_at",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "latest_logins",
                              "field": "last_audit_login",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            },
                            "right": {
                              "source": "first_orders",
                              "field": "first_order_at",
                              "type": {
                                "name": "datetime",
                                "nullable": true
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "neither_happened",
                      "expression": {
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
                              "source": "latest_logins",
                              "field": "last_audit_login",
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
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "first_orders",
                              "field": "first_order_at",
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
                          }
                        ]
                      }
                    },
                    {
                      "alias": "logged_in_by_deadline",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "latest_logins",
                          "field": "last_audit_login",
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
                      }
                    },
                    {
                      "alias": "logged_in_exactly_at_deadline",
                      "expression": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "lessThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "latest_logins",
                              "field": "last_audit_login",
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
                              "source": "latest_logins",
                              "field": "last_audit_login",
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
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ComparingNullableValuesQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    },
                    {
                      "name": "logged_in_since_first_order",
                      "type": "bool"
                    },
                    {
                      "name": "logged_in_after_first_order",
                      "type": "bool"
                    },
                    {
                      "name": "after_or_at_the_same_time",
                      "type": "bool"
                    },
                    {
                      "name": "neither_happened",
                      "type": "bool"
                    },
                    {
                      "name": "logged_in_by_deadline",
                      "type": "bool"
                    },
                    {
                      "name": "logged_in_exactly_at_deadline",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "True",
                      "False",
                      "True",
                      "True",
                      "True",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ComparingNullableValuesQuery().Result).TextValue
        );
    }
}
