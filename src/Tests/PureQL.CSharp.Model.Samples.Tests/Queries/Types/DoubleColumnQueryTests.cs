using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record DoubleColumnQueryTests
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
                      "field": "user_age",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new DoubleColumnQuery().Value).TextValue
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
                      "name": "user_age",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "30"
                    ],
                    [
                      "25"
                    ],
                    [
                      "30"
                    ],
                    [
                      "42"
                    ],
                    [
                      "25"
                    ],
                    [
                      "28"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DoubleColumnQuery().Result).TextValue
        );
    }
}
