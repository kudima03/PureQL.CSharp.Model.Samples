using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record HavingAverageQueryTests
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
                      "field": "user_active",
                      "type": {
                        "name": "boolean"
                      }
                    },
                    {
                      "operator": "average_number",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_score",
                        "type": {
                          "name": "number"
                        }
                      },
                      "alias": "avg_score"
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
                  ],
                  "having": {
                    "operator": "greaterThan",
                    "left": {
                      "operator": "average_number",
                      "arg": {
                        "entity": "schema_with_foreign_keys.users",
                        "field": "user_score",
                        "type": {
                          "name": "number"
                        }
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 15
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new HavingAverageQuery().Value).TextValue
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
                      "name": "user_active",
                      "type": "bool"
                    },
                    {
                      "name": "avg_score",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "True",
                      "29.333333333333332"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new HavingAverageQuery().Result).TextValue
        );
    }
}
