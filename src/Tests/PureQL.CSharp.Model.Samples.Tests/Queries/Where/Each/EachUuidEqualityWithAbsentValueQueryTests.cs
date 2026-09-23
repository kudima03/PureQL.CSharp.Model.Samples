using PureQL.CSharp.Model.Samples.Queries.Where.Each;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where.Each;

public sealed record EachUuidEqualityWithAbsentValueQueryTests
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
                    "operator": "eachEqual",
                    "left": {
                      "entity": "schema_with_foreign_keys.users",
                      "field": "user_id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    "right": {
                      "type": {
                        "name": "uuid"
                      },
                      "value": "ffffffff-ffff-ffff-ffff-ffffffffffff"
                    }
                  }
                }
                """
            ).TextValue,
            new QueryJson(new EachUuidEqualityWithAbsentValueQuery().Value).TextValue
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
                  "rows": []
                }
                """
            ).TextValue,
            new DataSetJson(new EachUuidEqualityWithAbsentValueQuery().Result).TextValue
        );
    }
}
