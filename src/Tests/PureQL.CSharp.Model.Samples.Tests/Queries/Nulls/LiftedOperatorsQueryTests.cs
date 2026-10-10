using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record LiftedOperatorsQueryTests
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
                  "select": [
                    {
                      "alias": "user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "next_score",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "add",
                        "values": [
                          {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          }
                        ]
                      }
                    },
                    {
                      "alias": "score_date",
                      "type": {
                        "name": "date",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "dateAddDays",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "operator": "round",
                          "value": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_score",
                            "type": {
                              "name": "decimal",
                              "nullable": true
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "double_score",
                      "type": {
                        "name": "integer",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "multiply",
                        "values": [
                          {
                            "operator": "round",
                            "value": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_score",
                              "type": {
                                "name": "decimal",
                                "nullable": true
                              }
                            }
                          },
                          {
                            "type": {
                              "name": "integer"
                            },
                            "value": 2
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LiftedOperatorsQuery().Value).TextValue
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
                      "name": "next_score",
                      "type": "double"
                    },
                    {
                      "name": "score_date",
                      "type": "date"
                    },
                    {
                      "name": "double_score",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "31",
                      "2020-02-14",
                      "60"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "31",
                      "2019-08-09",
                      "60"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "",
                      "",
                      ""
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "11",
                      "2023-03-10",
                      "20"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "29",
                      "2020-02-12",
                      "56"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LiftedOperatorsQuery().Result).TextValue
        );
    }
}
