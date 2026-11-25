using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record LiteralEdgeValuesOfEveryTypeQueryTests
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
                      "alias": "int_written_as_5_0",
                      "expression": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 5
                      }
                    },
                    {
                      "alias": "negative_int",
                      "expression": {
                        "type": {
                          "name": "integer"
                        },
                        "value": -42
                      }
                    },
                    {
                      "alias": "decimal_without_fraction",
                      "expression": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": "5"
                      }
                    },
                    {
                      "alias": "decimal_exponent",
                      "expression": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": "0.00000015"
                      }
                    },
                    {
                      "alias": "empty_string",
                      "expression": {
                        "type": {
                          "name": "string"
                        },
                        "value": ""
                      }
                    },
                    {
                      "alias": "leap_day",
                      "expression": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-02-29"
                      }
                    },
                    {
                      "alias": "first_day",
                      "expression": {
                        "type": {
                          "name": "date"
                        },
                        "value": "0001-01-01"
                      }
                    },
                    {
                      "alias": "last_day",
                      "expression": {
                        "type": {
                          "name": "date"
                        },
                        "value": "9999-12-31"
                      }
                    },
                    {
                      "alias": "midnight",
                      "expression": {
                        "type": {
                          "name": "time"
                        },
                        "value": "00:00:00"
                      }
                    },
                    {
                      "alias": "microseconds",
                      "expression": {
                        "type": {
                          "name": "time"
                        },
                        "value": "23:59:59.999999"
                      }
                    },
                    {
                      "alias": "utc_half_second",
                      "expression": {
                        "type": {
                          "name": "datetime"
                        },
                        "value": "2024-12-31T23:59:59.5Z"
                      }
                    },
                    {
                      "alias": "utc_one_microsecond",
                      "expression": {
                        "type": {
                          "name": "datetime"
                        },
                        "value": "2024-01-01T00:00:00.000001Z"
                      }
                    },
                    {
                      "alias": "hex_letters_uuid",
                      "expression": {
                        "type": {
                          "name": "uuid"
                        },
                        "value": "abcdefab-cdef-4abc-8def-abcdefabcdef"
                      }
                    },
                    {
                      "alias": "decimal_28_digits",
                      "expression": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": "-9999999999999999999999999999"
                      }
                    },
                    {
                      "alias": "decimal_28_digits_with_fraction",
                      "expression": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": "1234567890123456.789012345678"
                      }
                    },
                    {
                      "alias": "decimal_28_places",
                      "expression": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": "-0.0000000000000000000000000001"
                      }
                    },
                    {
                      "alias": "largest_integer",
                      "expression": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 9223372036854775807
                      }
                    },
                    {
                      "alias": "smallest_integer",
                      "expression": {
                        "type": {
                          "name": "integer"
                        },
                        "value": -9223372036854775808
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LiteralEdgeValuesOfEveryTypeQuery().Value).TextValue
        );
    }
}
