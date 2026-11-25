using PureQL.CSharp.Model.Samples.Queries.Joins;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Joins;

public sealed record MatchingWithoutCaseOverJoinsQueryTests
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
                      "name": "profiles",
                      "rows": [
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000001-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann@Example.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ann"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Strau\u00df"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Gold"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000002-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ann@EXAMPLE.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ANN"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "STRAUSS"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "silver"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000003-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "ANN@example.COM"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Cara"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Lang"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Silver"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000004-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "team@example.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Dan"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Day"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "bronze"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000005-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Team@Example.com"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Eve"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Ray"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "GOLD"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "user_id",
                            "expression": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000006-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "email",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "TEAM@EXAMPLE.COM"
                            }
                          },
                          {
                            "alias": "first_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Fay"
                            }
                          },
                          {
                            "alias": "last_name",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Poe"
                            }
                          },
                          {
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "gold"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "subquery": "profiles"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "profiles",
                      "alias": "other",
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
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "profiles",
                                "field": "email",
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
                                "source": "other",
                                "field": "email",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          },
                          {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "profiles",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "other",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "profiles",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "in",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "operator": "foldCase",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "source": "profiles",
                        "field": "tier",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    "list": {
                      "type": {
                        "name": "stringList"
                      },
                      "value": [
                        "gold",
                        "silver"
                      ]
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "profiles",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "other_id",
                      "expression": {
                        "source": "other",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "email",
                      "expression": {
                        "source": "profiles",
                        "field": "email",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "other_email",
                      "expression": {
                        "source": "other",
                        "field": "email",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "same_name",
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
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "profiles",
                                "field": "first_name",
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
                                "source": "other",
                                "field": "first_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "profiles",
                                "field": "last_name",
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
                                "source": "other",
                                "field": "last_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "status_key",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "is_shipped_status",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "foldCase",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "shipped"
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "profiles",
                          "field": "email",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "profiles",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "other",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MatchingWithoutCaseOverJoinsQuery().Value).TextValue
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
                      "name": "other_id",
                      "type": "uuid"
                    },
                    {
                      "name": "email",
                      "type": "string"
                    },
                    {
                      "name": "other_email",
                      "type": "string"
                    },
                    {
                      "name": "same_name",
                      "type": "bool"
                    },
                    {
                      "name": "status_key",
                      "type": "string"
                    },
                    {
                      "name": "is_shipped_status",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "Ann@Example.com",
                      "ann@EXAMPLE.com",
                      "True",
                      "shipped",
                      "True"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "Ann@Example.com",
                      "ann@EXAMPLE.com",
                      "True",
                      "pending",
                      "False"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "Ann@Example.com",
                      "ANN@example.COM",
                      "False",
                      "shipped",
                      "True"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "Ann@Example.com",
                      "ANN@example.COM",
                      "False",
                      "pending",
                      "False"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "ann@EXAMPLE.com",
                      "ANN@example.COM",
                      "False",
                      "shipped",
                      "True"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "00000006-0000-0000-0000-000000000000",
                      "Team@Example.com",
                      "TEAM@EXAMPLE.COM",
                      "False",
                      "",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MatchingWithoutCaseOverJoinsQuery().Result).TextValue
        );
    }
}
