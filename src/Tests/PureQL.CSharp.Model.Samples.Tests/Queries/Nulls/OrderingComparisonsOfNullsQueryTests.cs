using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record OrderingComparisonsOfNullsQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "alias": "greaterThanOrEqual_integer",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_integer",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_integer",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_integer",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_integer_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_integer_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        },
                        "right": {
                          "type": {
                            "name": "integer",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_decimal",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_decimal",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_decimal",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_decimal",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_decimal_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_decimal_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.5"
                        },
                        "right": {
                          "type": {
                            "name": "decimal",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_string",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_string",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_string",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_string",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_string_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a"
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_string_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "string"
                          },
                          "value": "a"
                        },
                        "right": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_boolean",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_boolean",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_boolean",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_boolean",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_boolean_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": true
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_boolean_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "boolean"
                          },
                          "value": true
                        },
                        "right": {
                          "type": {
                            "name": "boolean",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_date",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_date",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_date",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_date",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_date_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-01-31"
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_date_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-01-31"
                        },
                        "right": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_time",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_time",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_time",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_time",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_time_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "time"
                          },
                          "value": "08:30:00"
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_time_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "time"
                          },
                          "value": "08:30:00"
                        },
                        "right": {
                          "type": {
                            "name": "time",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_datetime",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          },
                          "value": null
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
                      "alias": "lessThanOrEqual_datetime",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          },
                          "value": null
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
                      "alias": "greaterThan_datetime",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          },
                          "value": null
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
                      "alias": "lessThan_datetime",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          },
                          "value": null
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
                      "alias": "greaterThanOrEqual_datetime_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "datetime",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-01-31T08:30:00Z"
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_datetime_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-01-31T08:30:00Z"
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
                      "alias": "greaterThanOrEqual_uuid",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_uuid",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThan_uuid",
                      "expression": {
                        "operator": "greaterThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "lessThan_uuid",
                      "expression": {
                        "operator": "lessThan",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    },
                    {
                      "alias": "greaterThanOrEqual_uuid_null_left",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        },
                        "right": {
                          "type": {
                            "name": "uuid"
                          },
                          "value": "00000007-0000-0000-0000-000000000000"
                        }
                      }
                    },
                    {
                      "alias": "lessThanOrEqual_uuid_null_right",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "type": {
                            "name": "uuid"
                          },
                          "value": "00000007-0000-0000-0000-000000000000"
                        },
                        "right": {
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "value": null
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderingComparisonsOfNullsQuery().Value).TextValue
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
                      "name": "greaterThanOrEqual_integer",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_integer",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_integer",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_integer",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_integer_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_integer_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_decimal",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_decimal",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_decimal",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_decimal",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_decimal_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_decimal_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_string",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_string",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_string",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_string",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_string_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_string_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_boolean_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_boolean_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_date",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_date",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_date",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_date",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_date_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_date_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_time",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_time",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_time",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_time",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_time_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_time_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_datetime",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_datetime",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_datetime",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_datetime",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_datetime_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_datetime_null_right",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_uuid",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_uuid",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThan_uuid",
                      "type": "bool"
                    },
                    {
                      "name": "lessThan_uuid",
                      "type": "bool"
                    },
                    {
                      "name": "greaterThanOrEqual_uuid_null_left",
                      "type": "bool"
                    },
                    {
                      "name": "lessThanOrEqual_uuid_null_right",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False"
                    ],
                    [
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False",
                      "True",
                      "True",
                      "False",
                      "False",
                      "False",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderingComparisonsOfNullsQuery().Result).TextValue
        );
    }
}
