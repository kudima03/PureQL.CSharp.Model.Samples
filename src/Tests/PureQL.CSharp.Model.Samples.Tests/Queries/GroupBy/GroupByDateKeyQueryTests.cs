using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupByDateKeyQueryTests
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
                      "field": "signup_date",
                      "type": {
                        "name": "date"
                      }
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "signup_date",
                      "type": {
                        "name": "date"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupByDateKeyQuery().Value).TextValue
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
                      "name": "signup_date",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2019-07-10"
                    ],
                    [
                      "2023-02-28"
                    ],
                    [
                      "2022-11-05"
                    ],
                    [
                      "2020-01-15"
                    ],
                    [
                      "2021-03-20"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupByDateKeyQuery().Result).TextValue
        );
    }
}
