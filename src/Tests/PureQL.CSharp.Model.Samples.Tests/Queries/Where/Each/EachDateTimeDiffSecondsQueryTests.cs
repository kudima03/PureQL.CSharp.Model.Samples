using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachDateTimeDiffSecondsQueryTests
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
                      "field": "user_name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ],
                  "where": {
                    "operator": "eachGreaterThan",
                    "left": {
                      "operator": "eachDatetimeDiffSeconds",
                      "left": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "datetime"
                        },
                        "value": "2024-06-02T00:00:00"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 0
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachDateTimeDiffSecondsQuery().Value).TextValue
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
                      "name": "user_name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Bob"
                    ],
                    [
                      "Dan"
                    ],
                    [
                      "Eve"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new EachDateTimeDiffSecondsQuery().Result).TextValue
        );
    }
}
