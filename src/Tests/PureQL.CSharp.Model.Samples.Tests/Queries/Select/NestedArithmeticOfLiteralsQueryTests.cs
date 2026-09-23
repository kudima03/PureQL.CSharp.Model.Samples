using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record NestedArithmeticOfLiteralsQueryTests
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
                      "operator": "multiply",
                      "arguments": [
                        {
                          "operator": "add",
                          "arguments": [
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 1
                            },
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 2
                            }
                          ]
                        },
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 3
                        }
                      ],
                      "alias": "result"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NestedArithmeticOfLiteralsQuery().Value).TextValue
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
                      "name": "result",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "9"
                    ],
                    [
                      "9"
                    ],
                    [
                      "9"
                    ],
                    [
                      "9"
                    ],
                    [
                      "9"
                    ],
                    [
                      "9"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NestedArithmeticOfLiteralsQuery().Result).TextValue
        );
    }
}
