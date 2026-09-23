using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachDateTimeAddSecondsInEqualityQueryTests
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
                    "operator": "eachEqual",
                    "left": {
                      "operator": "eachDatetimeAddSeconds",
                      "left": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "last_login",
                        "type": {
                          "name": "datetime"
                        }
                      },
                      "right": {
                        "type": {
                          "name": "number"
                        },
                        "value": 3600
                      }
                    },
                    "right": {
                      "type": {
                        "name": "datetime"
                      },
                      "value": "2024-06-01T09:30:00"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachDateTimeAddSecondsInEqualityQuery().Value).TextValue
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
                      "Ann"
                    ],
                    [
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new EachDateTimeAddSecondsInEqualityQuery().Result).TextValue
        );
    }
}
