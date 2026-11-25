using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record CodesMatchedByRegexQueryTests
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
                      "name": "skus",
                      "rows": [
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "WGT-0-00000C9-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "description",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Basic widget"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "GZM-0-00000CA-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "description",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Premium gadget, recalled in 2024"
                            }
                          }
                        ],
                        [
                          {
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "giz-000-000CB-000-000-000-000-000-000-000-000"
                            }
                          },
                          {
                            "alias": "description",
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
                            "alias": "sku",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "LUX-0-00000CC-0000-0000-0000-000000000000"
                            }
                          },
                          {
                            "alias": "description",
                            "expression": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Deluxe bundle"
                            }
                          }
                        ]
                      ]
                    }
                  ],
                  "from": {
                    "entity": "schema_with_foreign_keys.order_items"
                  },
                  "joins": [
                    {
                      "kind": "inner",
                      "subquery": "skus",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "replaceRegex",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "asString",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_product_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "-"
                          },
                          "replacement": {
                            "type": {
                              "name": "string"
                            },
                            "value": ""
                          }
                        },
                        "right": {
                          "operator": "replaceRegex",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "lower",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "source": "skus",
                              "field": "sku",
                              "type": {
                                "name": "string"
                              }
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "[^0-9a-f]"
                          },
                          "replacement": {
                            "type": {
                              "name": "string"
                            },
                            "value": ""
                          }
                        }
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
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "source": "skus",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^([A-Za-z]+)-([0-9]+)"
                        }
                      },
                      {
                        "operator": "not",
                        "type": {
                          "name": "boolean"
                        },
                        "condition": {
                          "operator": "matchesRegex",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "source": "skus",
                            "field": "description",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "\\b(?:discontinued|recalled)\\b"
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "sku_series",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "skus",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^([A-Z]+)-([0-9]+)"
                        },
                        "group": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "sku_number",
                      "expression": {
                        "operator": "tryAsInteger",
                        "type": {
                          "name": "integer",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "extractRegex",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "source": "skus",
                            "field": "sku",
                            "type": {
                              "name": "string"
                            }
                          },
                          "pattern": {
                            "type": {
                              "name": "string"
                            },
                            "value": "^([A-Z]+)-([0-9]+)"
                          },
                          "group": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        }
                      }
                    },
                    {
                      "alias": "chosen_group",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "skus",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^([A-Za-z]+)-([0-9]+)"
                        },
                        "group": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        }
                      }
                    },
                    {
                      "alias": "first_word_of_description",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "skus",
                          "field": "description",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\\w+"
                        }
                      }
                    },
                    {
                      "alias": "sku_or_placeholder",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "extractRegex",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "operand": {
                              "source": "skus",
                              "field": "sku",
                              "type": {
                                "name": "string"
                              }
                            },
                            "pattern": {
                              "type": {
                                "name": "string"
                              },
                              "value": "[0-9]{4,}"
                            }
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "n/a"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "replacement_from_a_field",
                      "expression": {
                        "operator": "replaceRegex",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "skus",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^[A-Z]+"
                        },
                        "replacement": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "round",
                            "type": {
                              "name": "integer"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.order_items",
                              "field": "item_qty",
                              "type": {
                                "name": "decimal"
                              }
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "skus",
                          "field": "sku",
                          "type": {
                            "name": "string"
                          }
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[0-9]+"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.order_items",
                        "field": "item_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CodesMatchedByRegexQuery().Value).TextValue
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
                      "name": "sku_series",
                      "type": "string"
                    },
                    {
                      "name": "sku_number",
                      "type": "long"
                    },
                    {
                      "name": "chosen_group",
                      "type": "string"
                    },
                    {
                      "name": "first_word_of_description",
                      "type": "string"
                    },
                    {
                      "name": "sku_or_placeholder",
                      "type": "string"
                    },
                    {
                      "name": "replacement_from_a_field",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "0000012d-0000-0000-0000-000000000000",
                      "WGT",
                      "0",
                      "0",
                      "Basic",
                      "00000",
                      "2-0-00000C9-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000130-0000-0000-0000-000000000000",
                      "WGT",
                      "0",
                      "0",
                      "Basic",
                      "00000",
                      "3-0-00000C9-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000012f-0000-0000-0000-000000000000",
                      "",
                      "",
                      "000",
                      "",
                      "n/a",
                      "giz-000-000CB-000-000-000-000-000-000-000-000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CodesMatchedByRegexQuery().Result).TextValue
        );
    }
}
