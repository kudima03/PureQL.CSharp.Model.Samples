using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record GroupByNullKeyQueryTests
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
                      "field": "user_score",
                      "type": {
                        "name": "number"
                      }
                    },
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "alias": "n"
                    }
                  ],
                  "groupBy": [
                    {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_score",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new GroupByNullKeyQuery().Value).TextValue
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
                      "name": "user_score",
                      "type": "double"
                    },
                    {
                      "name": "n",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "",
                      "2"
                    ],
                    [
                      "10",
                      "1"
                    ],
                    [
                      "28",
                      "1"
                    ],
                    [
                      "30",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new GroupByNullKeyQuery().Result).TextValue
        );
    }
}
