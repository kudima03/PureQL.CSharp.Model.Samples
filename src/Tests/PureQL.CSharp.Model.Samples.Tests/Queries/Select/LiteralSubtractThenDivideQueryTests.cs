using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record LiteralSubtractThenDivideQueryTests
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
                      "operator": "divide",
                      "arguments": [
                        {
                          "operator": "subtract",
                          "arguments": [
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 10
                            },
                            {
                              "type": {
                                "name": "number"
                              },
                              "value": 4
                            }
                          ]
                        },
                        {
                          "type": {
                            "name": "number"
                          },
                          "value": 2
                        }
                      ],
                      "alias": "result"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LiteralSubtractThenDivideQuery().Value).TextValue
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
                      "3"
                    ],
                    [
                      "3"
                    ],
                    [
                      "3"
                    ],
                    [
                      "3"
                    ],
                    [
                      "3"
                    ],
                    [
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LiteralSubtractThenDivideQuery().Result).TextValue
        );
    }
}
