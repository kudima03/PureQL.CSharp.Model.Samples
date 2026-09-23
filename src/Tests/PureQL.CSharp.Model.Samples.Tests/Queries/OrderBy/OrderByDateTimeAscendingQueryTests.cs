using PureQL.CSharp.Model.Samples.Queries.OrderBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.OrderBy;

public sealed record OrderByDateTimeAscendingQueryTests
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
                      "field": "last_login",
                      "type": {
                        "name": "datetime"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new OrderByDateTimeAscendingQuery().Value).TextValue
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
                      "name": "last_login",
                      "type": "datetime"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2024-05-30T14:00:00"
                    ],
                    [
                      "2024-06-01T08:30:00"
                    ],
                    [
                      "2024-06-01T08:30:00"
                    ],
                    [
                      "2024-06-02T09:15:00"
                    ],
                    [
                      "2024-06-03T18:45:00"
                    ],
                    [
                      "2024-06-04T07:05:00"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new OrderByDateTimeAscendingQuery().Result).TextValue
        );
    }
}
