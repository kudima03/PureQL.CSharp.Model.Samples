using PureQL.CSharp.Model.Samples.Queries.GroupBy;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.GroupBy;

public sealed record GroupByAgeOrderedByAgeDescQueryTests
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
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_age",
                      "type": {
                        "name": "number"
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "field": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_age",
                        "type": {
                          "name": "number"
                        }
                      },
                      "direction": "desc"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupByAgeOrderedByAgeDescQuery().Value).TextValue
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
                      "42"
                    ],
                    [
                      "30"
                    ],
                    [
                      "28"
                    ],
                    [
                      "25"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupByAgeOrderedByAgeDescQuery().Result).TextValue
        );
    }
}
