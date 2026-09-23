using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record CountOfTimeColumnGroupedByActiveStatusQueryTests
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
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "shift_start",
                        "type": {
                          "name": "time"
                        }
                      },
                      "alias": "n"
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new CountOfTimeColumnGroupedByActiveStatusQuery().Value
            ).TextValue
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
                      "name": "n",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2"
                    ],
                    [
                      "4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new CountOfTimeColumnGroupedByActiveStatusQuery().Result
            ).TextValue
        );
    }
}
