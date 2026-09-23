using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachTimeEqualityQueryTests
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
                      "entity": "schema_with_foreign_keys.users",
                      "field": "shift_start",
                      "type": {
                        "name": "time"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "time"
                      },
                      "value": "09:00:00"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachTimeEqualityQuery().Value).TextValue
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
                      "Cara"
                    ],
                    [
                      "Fay"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new EachTimeEqualityQuery().Result).TextValue
        );
    }
}
