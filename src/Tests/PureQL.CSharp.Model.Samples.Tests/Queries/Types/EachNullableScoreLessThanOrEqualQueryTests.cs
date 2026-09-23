using PureQL.CSharp.Model.Samples.Queries.Types;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Types;

public sealed record EachNullableScoreLessThanOrEqualQueryTests
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
                    "operator": "eachLessThanOrEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_score",
                      "type": {
                        "name": "number"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "number"
                      },
                      "value": 10
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachNullableScoreLessThanOrEqualQuery().Value).TextValue
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
                      "Eve"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new EachNullableScoreLessThanOrEqualQuery().Result).TextValue
        );
    }
}
