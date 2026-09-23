using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record MaxShiftStartOverAllUsersQueryTests
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
                        "entity": "schema_with_foreign_keys.users",
                        "field": "shift_start",
                        "type": {
                          "name": "time"
                        }
                      },
                      "alias": "max_shift_start"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new MaxShiftStartOverAllUsersQuery().Value).TextValue
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
                      "name": "max_shift_start",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "11:30:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new MaxShiftStartOverAllUsersQuery().Result).TextValue
        );
    }
}
