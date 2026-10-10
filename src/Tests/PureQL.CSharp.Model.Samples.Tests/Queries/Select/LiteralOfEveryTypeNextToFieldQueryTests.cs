using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record LiteralOfEveryTypeNextToFieldQueryTests
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
                      "alias": "order_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "an_integer",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "type": {
                          "name": "integer"
                        },
                        "value": 42
                      }
                    },
                    {
                      "alias": "a_decimal",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "type": {
                          "name": "decimal"
                        },
                        "value": 19.99
                      }
                    },
                    {
                      "alias": "a_string",
                      "type": {
                        "name": "string"
                      },
                      "expression": {
                        "type": {
                          "name": "string"
                        },
                        "value": "hello"
                      }
                    },
                    {
                      "alias": "a_boolean",
                      "type": {
                        "name": "boolean"
                      },
                      "expression": {
                        "type": {
                          "name": "boolean"
                        },
                        "value": true
                      }
                    },
                    {
                      "alias": "a_date",
                      "type": {
                        "name": "date"
                      },
                      "expression": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-01-31"
                      }
                    },
                    {
                      "alias": "a_time",
                      "type": {
                        "name": "time"
                      },
                      "expression": {
                        "type": {
                          "name": "time"
                        },
                        "value": "18:30:00"
                      }
                    },
                    {
                      "alias": "a_datetime",
                      "type": {
                        "name": "datetime"
                      },
                      "expression": {
                        "type": {
                          "name": "datetime"
                        },
                        "value": "2024-01-31T18:30:00Z"
                      }
                    },
                    {
                      "alias": "a_uuid",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "type": {
                          "name": "uuid"
                        },
                        "value": "000003e7-0000-0000-0000-000000000000"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LiteralOfEveryTypeNextToFieldQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "an_integer",
                      "type": "long"
                    },
                    {
                      "name": "a_decimal",
                      "type": "double"
                    },
                    {
                      "name": "a_string",
                      "type": "string"
                    },
                    {
                      "name": "a_boolean",
                      "type": "bool"
                    },
                    {
                      "name": "a_date",
                      "type": "date"
                    },
                    {
                      "name": "a_time",
                      "type": "time"
                    },
                    {
                      "name": "a_datetime",
                      "type": "datetime"
                    },
                    {
                      "name": "a_uuid",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "42",
                      "19.99",
                      "hello",
                      "True",
                      "2024-01-31",
                      "18:30:00",
                      "2024-01-31T18:30:00",
                      "000003e7-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "42",
                      "19.99",
                      "hello",
                      "True",
                      "2024-01-31",
                      "18:30:00",
                      "2024-01-31T18:30:00",
                      "000003e7-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "42",
                      "19.99",
                      "hello",
                      "True",
                      "2024-01-31",
                      "18:30:00",
                      "2024-01-31T18:30:00",
                      "000003e7-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "42",
                      "19.99",
                      "hello",
                      "True",
                      "2024-01-31",
                      "18:30:00",
                      "2024-01-31T18:30:00",
                      "000003e7-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "42",
                      "19.99",
                      "hello",
                      "True",
                      "2024-01-31",
                      "18:30:00",
                      "2024-01-31T18:30:00",
                      "000003e7-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000",
                      "42",
                      "19.99",
                      "hello",
                      "True",
                      "2024-01-31",
                      "18:30:00",
                      "2024-01-31T18:30:00",
                      "000003e7-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LiteralOfEveryTypeNextToFieldQuery().Result).TextValue
        );
    }
}
