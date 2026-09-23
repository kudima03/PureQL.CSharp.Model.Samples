using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record AverageOfEachDateAddDaysQueryTests
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
                      "operator": "average_date",
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
                      "alias": "meanProjectedDate"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new AverageOfEachDateAddDaysQuery().Value).TextValue
        );
    }
}
