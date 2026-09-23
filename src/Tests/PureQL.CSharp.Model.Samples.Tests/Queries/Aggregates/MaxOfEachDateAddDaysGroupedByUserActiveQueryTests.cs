using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record MaxOfEachDateAddDaysGroupedByUserActiveQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    },
                    {
                      "operator": "max_date",
                      "arg": {
                        "operator": "eachDateAddDays",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 30
                        }
                      },
                      "alias": "latestProjectedDate"
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new MaxOfEachDateAddDaysGroupedByUserActiveQuery().Value
            ).TextValue
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
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "latestProjectedDate",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "False",
                      "2023-03-30"
                    ],
                    [
                      "True",
                      "2022-12-05"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new MaxOfEachDateAddDaysGroupedByUserActiveQuery().Result
            ).TextValue
        );
    }
}
