using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record ConcatAllNamesPerCountryQueryTests
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
                  "groupBy": [
                    {
                      "alias": "country",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "condition": {
                          "operator": "not",
                          "type": {
                            "name": "boolean"
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          }
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Sweden"
                        },
                        "else": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 40
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Denmark"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Norway"
                          }
                        }
                      }
                    }
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "length",
                      "type": {
                        "name": "integer"
                      },
                      "operand": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
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
                    },
                    "right": {
                      "type": {
                        "name": "integer"
                      },
                      "value": 3
                    }
                  },
                  "select": [
                    {
                      "alias": "country",
                      "expression": {
                        "key": "country",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "first_names",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            }
                          },
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
                    },
                    {
                      "alias": "tiers",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "greaterThanOrEqual",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_age",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 40
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "gold"
                          },
                          "else": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "greaterThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 30
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "silver"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "bronze"
                            }
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": "/"
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "greaterThanOrEqual",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_age",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 40
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "gold"
                              },
                              "else": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "greaterThanOrEqual",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_age",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 30
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "silver"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "bronze"
                                }
                              }
                            },
                            "direction": "desc"
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "nicknames",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "condition": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_active",
                            "type": {
                              "name": "boolean"
                            }
                          },
                          "then": {
                            "operator": "lower",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "else": {
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "value": null
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "operator": "lower",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_name",
                                  "type": {
                                    "name": "string"
                                  }
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "value": null
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "initials",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "upper",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "substring",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "start": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            },
                            "length": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ""
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
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
                    },
                    {
                      "alias": "countries_of_all",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "not",
                            "type": {
                              "name": "boolean"
                            },
                            "condition": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_active",
                              "type": {
                                "name": "boolean"
                              }
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Sweden"
                          },
                          "else": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "greaterThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_age",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 40
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Denmark"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Norway"
                            }
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "not",
                                "type": {
                                  "name": "boolean"
                                },
                                "condition": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_active",
                                  "type": {
                                    "name": "boolean"
                                  }
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Sweden"
                              },
                              "else": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "greaterThanOrEqual",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_age",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 40
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Denmark"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Norway"
                                }
                              }
                            }
                          }
                        ],
                        "distinct": true,
                        "over": "all"
                      }
                    },
                    {
                      "alias": "active_emails",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "operator": "lower",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "@example.com"
                            }
                          ]
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
                              "operator": "concat",
                              "type": {
                                "name": "string"
                              },
                              "values": [
                                {
                                  "operator": "lower",
                                  "type": {
                                    "name": "string"
                                  },
                                  "operand": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_name",
                                    "type": {
                                      "name": "string"
                                    }
                                  }
                                },
                                {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "@example.com"
                                }
                              ]
                            }
                          }
                        ],
                        "predicate": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
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
                            "key": "country",
                            "type": {
                              "name": "string"
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ": "
                          },
                          {
                            "operator": "concatAll",
                            "type": {
                              "name": "string"
                            },
                            "selector": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            "separator": {
                              "type": {
                                "name": "string"
                              },
                              "value": ", "
                            },
                            "orderBy": [
                              {
                                "expression": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "signup_date",
                                  "type": {
                                    "name": "date"
                                  }
                                }
                              },
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
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "length",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "concatAll",
                          "type": {
                            "name": "string"
                          },
                          "selector": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_name",
                            "type": {
                              "name": "string"
                            }
                          },
                          "separator": {
                            "type": {
                              "name": "string"
                            },
                            "value": ", "
                          },
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
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
                      },
                      "direction": "desc"
                    },
                    {
                      "expression": {
                        "key": "country",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConcatAllNamesPerCountryQuery().Value).TextValue
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
                      "name": "country",
                      "type": "string"
                    },
                    {
                      "name": "first_names",
                      "type": "string"
                    },
                    {
                      "name": "tiers",
                      "type": "string"
                    },
                    {
                      "name": "nicknames",
                      "type": "string"
                    },
                    {
                      "name": "initials",
                      "type": "string"
                    },
                    {
                      "name": "countries_of_all",
                      "type": "string"
                    },
                    {
                      "name": "active_emails",
                      "type": "string"
                    },
                    {
                      "name": "label",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Norway",
                      "Cara, Ann, Fay",
                      "silver/bronze",
                      "ann, cara, fay",
                      "ACF",
                      "Denmark, Norway, Sweden",
                      "ann@example.com; cara@example.com; fay@example.com",
                      "Norway: Cara, Ann, Fay"
                    ],
                    [
                      "Sweden",
                      "Bob, Eve",
                      "bronze",
                      "",
                      "BE",
                      "Denmark, Norway, Sweden",
                      "",
                      "Sweden: Bob, Eve"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConcatAllNamesPerCountryQuery().Result).TextValue
        );
    }
}
