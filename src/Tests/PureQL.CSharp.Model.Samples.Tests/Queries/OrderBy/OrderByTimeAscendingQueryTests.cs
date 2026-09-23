using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByTimeAscendingQueryTests
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
                      "field": "shift_start",
                      "type": {
                        "name": "time"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "shift_start",
                        "type": {
                          "name": "time"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByTimeAscendingQuery().Value).TextValue
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
                      "name": "shift_start",
                      "type": "time"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "08:00:00"
                    ],
                    [
                      "09:00:00"
                    ],
                    [
                      "09:00:00"
                    ],
                    [
                      "09:00:00"
                    ],
                    [
                      "10:00:00"
                    ],
                    [
                      "11:30:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByTimeAscendingQuery().Result).TextValue
        );
    }
}
