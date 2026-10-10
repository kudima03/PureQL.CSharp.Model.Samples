using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record CoalesceQueryTests
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
                      "alias": "score_or_age",
                      "type": {
                        "name": "decimal"
                      },
                      "expression": {
                        "operator": "coalesce",
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
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_age",
                            "type": {
                              "name": "decimal"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "score_or_null",
                      "type": {
                        "name": "decimal",
                        "nullable": true
                      },
                      "expression": {
                        "operator": "coalesce",
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
                              "name": "decimal",
                              "nullable": true
                            },
                            "value": null
                          }
                        ]
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CoalesceQuery().Value).TextValue
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
                      "name": "score_or_age",
                      "type": "double"
                    },
                    {
                      "name": "score_or_null",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "30",
                      "30"
                    ],
                    [
                      "25",
                      ""
                    ],
                    [
                      "30",
                      "30"
                    ],
                    [
                      "42",
                      ""
                    ],
                    [
                      "10",
                      "10"
                    ],
                    [
                      "28",
                      "28"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CoalesceQuery().Result).TextValue
        );
    }
}
