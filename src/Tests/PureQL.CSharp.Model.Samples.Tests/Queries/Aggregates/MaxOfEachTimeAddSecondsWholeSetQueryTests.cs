using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record MaxOfEachTimeAddSecondsWholeSetQueryTests
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
                      "operator": "max_time",
                      "arg": {
                        "operator": "eachTimeAddSeconds",
                        "left": {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "shift_start",
                          "type": {
                            "name": "time"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "number"
                          },
                          "value": 3600
                        }
                      },
                      "alias": "latestProjectedTime"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MaxOfEachTimeAddSecondsWholeSetQuery().Value).TextValue
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
                      "name": "latestProjectedTime",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "12:30:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MaxOfEachTimeAddSecondsWholeSetQuery().Result).TextValue
        );
    }
}
