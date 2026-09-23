using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record ExtremeAndPrecisionSensitiveQueryTests
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
                      "field": "user_precision_value",
                      "type": {
                        "name": "number"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ExtremeAndPrecisionSensitiveQuery().Value).TextValue
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
                      "name": "user_precision_value",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "1.7976931348623157E\u002B308"
                    ],
                    [
                      "-1.7976931348623157E\u002B308"
                    ],
                    [
                      "5E-324"
                    ],
                    [
                      "-5E-324"
                    ],
                    [
                      "1E\u002B308"
                    ],
                    [
                      "123456789.123456"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ExtremeAndPrecisionSensitiveQuery().Result).TextValue
        );
    }
}
