using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record EachSubtractWithNullScoreOperandQueryTests
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
                      "operator": "eachSubtract",
                      "values": [
                        {
                          "entity": "schema_with_foreign_keys.users",
                          "field": "user_score",
                          "type": {
                            "name": "number"
                          }
                        },
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 10
                        }
                      ]
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
            new QueryJson(new EachSubtractWithNullScoreOperandQuery().Value).TextValue
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
            new DataSetJson(new EachSubtractWithNullScoreOperandQuery().Result).TextValue
        );
    }
}
