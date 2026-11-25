using PureQL.CSharp.Model.Samples.Queries.Conversions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Conversions;

public sealed record ReadingTextQueryTests
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
                      "name": "imports",
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "1"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "10115"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "4915112345678"
                            }
                          },
                          {
                            "alias": "discount_text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "0.1"
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "01234"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "alias": "discount_text",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "3"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "75001"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "33612345678"
                            }
                          },
                          {
                            "alias": "discount_text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "0.25"
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "1"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2040"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "+61412345678"
                            }
                          },
                          {
                            "alias": "discount_text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "0.05"
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "2"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": " 94105"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "4155550100"
                            }
                          },
                          {
                            "alias": "discount_text",
                            "expression": {
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "value": null
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
                            "alias": "tier",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "3"
                            }
                          },
                          {
                            "alias": "postal_code",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "n/a"
                            }
                          },
                          {
                            "alias": "mobile",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "415 555 0100"
                            }
                          },
                          {
                            "alias": "discount_text",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "0.2"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "imports",
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
                          "source": "imports",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "greaterThanOrEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "source": "schema_with_foreign_keys.users",
                      "field": "signup_date",
                      "type": {
                        "name": "date"
                      }
                    },
                    "right": {
                      "operator": "asDate",
                      "type": {
                        "name": "date"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "2020-01-01"
                      }
                    }
                  },
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
                      "alias": "user_name",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    },
                    {
                      "alias": "tier_number",
                      "expression": {
                        "operator": "asInteger",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "source": "imports",
                          "field": "tier",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "postal_number",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "source": "imports",
                          "field": "postal_code",
                          "type": {
                            "name": "string"
                          }
                        }
                      }
                    },
                    {
                      "alias": "postal_number_or_zero",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "tryAsInteger",
                            "type": {
                              "name": "integer",
                              "nullable": true
                            },
                            "operand": {
                              "operator": "trim",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "source": "imports",
                                "field": "postal_code",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "removables": [
                                {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": " "
                                }
                              ]
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 0
                          }
                        ]
                      }
                    },
                    {
                      "alias": "mobile_number",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "source": "imports",
                          "field": "mobile",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
                      }
                    },
                    {
                      "alias": "discount",
                      "expression": {
                        "operator": "asDecimal",
                        "type": {
                          "name": "decimal",
                          "nullable": true
                        },
                        "operand": {
                          "source": "imports",
                          "field": "discount_text",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        }
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
            new QueryJson(new ReadingTextQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    },
                    {
                      "name": "tier_number",
                      "type": "long"
                    },
                    {
                      "name": "postal_number",
                      "type": "long"
                    },
                    {
                      "name": "postal_number_or_zero",
                      "type": "long"
                    },
                    {
                      "name": "mobile_number",
                      "type": "long"
                    },
                    {
                      "name": "discount",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "Ann",
                      "1",
                      "10115",
                      "10115",
                      "4915112345678",
                      "0.1"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "Bob",
                      "2",
                      "",
                      "0",
                      "",
                      ""
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "Dan",
                      "1",
                      "2040",
                      "2040",
                      "",
                      "0.05"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "Eve",
                      "2",
                      "",
                      "94105",
                      "4155550100",
                      ""
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "Fay",
                      "3",
                      "",
                      "0",
                      "",
                      "0.2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ReadingTextQuery().Result).TextValue
        );
    }
}
