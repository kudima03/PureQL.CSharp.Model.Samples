using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record ShiftWindowsInBusinessHoursQueryTests
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
                    "entity": "schema_with_foreign_keys.employees"
                  },
                  "where": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
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
                      {
                        "operator": "lessThan",
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
                          "value": "18:00:00"
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "employee_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "window_start",
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    },
                    {
                      "alias": "window_end",
                      "expression": {
                        "operator": "timeAddSeconds",
                        "type": {
                          "name": "time"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.employees",
                          "field": "employee_shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "seconds": {
                          "operator": "multiply",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 3600
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "hours_until_closing",
                      "expression": {
                        "operator": "divide",
                        "type": {
                          "name": "decimal"
                        },
                        "values": [
                          {
                            "operator": "timeDiffSeconds",
                            "type": {
                              "name": "decimal"
                            },
                            "left": {
                              "type": {
                                "name": "time"
                              },
                              "value": "18:00:00"
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.employees",
                              "field": "employee_shift_start",
                              "type": {
                                "name": "time"
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 3600
                          }
                        ]
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.employees",
                        "field": "employee_shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ShiftWindowsInBusinessHoursQuery().Value).TextValue
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
                      "name": "window_start",
                      "type": "time"
                    },
                    {
                      "name": "window_end",
                      "type": "time"
                    },
                    {
                      "name": "hours_until_closing",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "000002bd-0000-0000-0000-000000000000",
                      "09:00:00",
                      "11:00:00",
                      "9"
                    ],
                    [
                      "000002be-0000-0000-0000-000000000000",
                      "10:00:00",
                      "12:00:00",
                      "8"
                    ],
                    [
                      "000002bf-0000-0000-0000-000000000000",
                      "11:30:00",
                      "13:30:00",
                      "6.5"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ShiftWindowsInBusinessHoursQuery().Result).TextValue
        );
    }
}
