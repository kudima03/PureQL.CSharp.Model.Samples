using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record TrimQueryTests
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
                    "entity": "schema_with_foreign_keys.employees",
                    "alias": "e"
                  },
                  "joins": [
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.employees",
                      "alias": "m",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "m",
                          "field": "employee_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        },
                        "right": {
                          "source": "e",
                          "field": "employee_manager_id",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "trim",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "source": "e",
                        "field": "employee_name",
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
                    },
                    "right": {
                      "type": {
                        "name": "string"
                      },
                      "value": ""
                    }
                  },
                  "select": [
                    {
                      "alias": "employee_name",
                      "expression": {
                        "operator": "trim",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "e",
                          "field": "employee_name",
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
                      "alias": "manager_name",
                      "expression": {
                        "operator": "trim",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "m",
                          "field": "employee_name",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "removables": [
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": " "
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "\t"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "id_without_leading_zeros",
                      "expression": {
                        "operator": "trimStart",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "e",
                            "field": "employee_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "removables": [
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "0"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "id_without_trailing_zeros",
                      "expression": {
                        "operator": "trimEnd",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "e",
                            "field": "employee_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "removables": [
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "-"
                          },
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": "0"
                          }
                        ]
                      }
                    },
                    {
                      "alias": "shift_start_short",
                      "expression": {
                        "operator": "trimEnd",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "asString",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "source": "e",
                            "field": "employee_shift_start",
                            "type": {
                              "name": "time"
                            }
                          }
                        },
                        "removables": [
                          {
                            "type": {
                              "name": "string"
                            },
                            "value": ":00"
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "e",
                        "field": "employee_name",
                        "type": {
                          "name": "string"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new TrimQuery().Value).TextValue
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
                      "name": "employee_name",
                      "type": "string"
                    },
                    {
                      "name": "manager_name",
                      "type": "string"
                    },
                    {
                      "name": "id_without_leading_zeros",
                      "type": "string"
                    },
                    {
                      "name": "id_without_trailing_zeros",
                      "type": "string"
                    },
                    {
                      "name": "shift_start_short",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Grace",
                      "",
                      "2bd-0000-0000-0000-000000000000",
                      "000002bd",
                      "09"
                    ],
                    [
                      "Hank",
                      "Grace",
                      "2be-0000-0000-0000-000000000000",
                      "000002be",
                      "10"
                    ],
                    [
                      "Iris",
                      "Grace",
                      "2bf-0000-0000-0000-000000000000",
                      "000002bf",
                      "11:30"
                    ],
                    [
                      "Jack",
                      "Hank",
                      "2c0-0000-0000-0000-000000000000",
                      "000002c",
                      "08"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new TrimQuery().Result).TextValue
        );
    }
}
