using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record ScalarUnderWhereQueryTests
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
                      "type": {
                        "name": "string"
                      },
                      "value": "active-user",
                      "alias": "tag"
                    }
                  ],
                  "where": {
                    "entity": "schema_with_foreign_keys.users",
                    "field": "user_active",
                    "type": {
                      "name": "boolean"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarUnderWhereQuery().Value).TextValue
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
                      "name": "tag",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "active-user"
                    ],
                    [
                      "active-user"
                    ],
                    [
                      "active-user"
                    ],
                    [
                      "active-user"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarUnderWhereQuery().Result).TextValue
        );
    }
}
