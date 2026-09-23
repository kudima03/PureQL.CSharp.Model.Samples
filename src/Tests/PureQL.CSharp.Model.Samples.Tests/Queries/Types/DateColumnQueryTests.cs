using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record DateColumnQueryTests
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
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DateColumnQuery().Value).TextValue
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
                      "2020-01-15"
                    ],
                    [
                      "2021-03-20"
                    ],
                    [
                      "2019-07-10"
                    ],
                    [
                      "2022-11-05"
                    ],
                    [
                      "2023-02-28"
                    ],
                    [
                      "2020-01-15"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DateColumnQuery().Result).TextValue
        );
    }
}
