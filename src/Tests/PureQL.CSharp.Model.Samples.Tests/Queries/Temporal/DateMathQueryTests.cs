using PureQL.CSharp.Model.Samples.Queries.Temporal;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Temporal;

public sealed record DateMathQueryTests
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
                  "where": {
                    "operator": "lessThanOrEqual",
                    "left": {
                      "operator": "dateDiffDays",
                      "left": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "date"
                        },
                        "value": "2024-06-01"
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
                      "alias": "due_date",
                      "type": {
                        "name": "date"
                      },
                      "expression": {
                        "operator": "dateAddDays",
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 14
                        }
                      }
                    },
                    {
                      "alias": "days_open",
                      "type": {
                        "name": "integer"
                      },
                      "expression": {
                        "operator": "dateDiffDays",
                        "left": {
                          "type": {
                            "name": "date"
                          },
                          "value": "2024-06-30"
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DateMathQuery().Value).TextValue
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
                      "name": "due_date",
                      "type": "date"
                    },
                    {
                      "name": "days_open",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000",
                      "2024-06-15",
                      "29"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "2024-06-16",
                      "28"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-17",
                      "27"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "2024-06-18",
                      "26"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DateMathQuery().Result).TextValue
        );
    }
}
