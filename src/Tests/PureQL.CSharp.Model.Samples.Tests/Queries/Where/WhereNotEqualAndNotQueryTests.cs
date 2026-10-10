using PureQL.CSharp.Model.Samples.Queries.Where;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Where;

public sealed record WhereNotEqualAndNotQueryTests
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
                  "where": {
                    "operator": "and",
                    "conditions": [
                      {
                        "operator": "notEqual",
                        "left": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Bob"
                        }
                      },
                      {
                        "operator": "not",
                        "condition": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_active",
                          "type": {
                            "name": "boolean"
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "user_id",
                      "type": {
                        "name": "uuid"
                      },
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new WhereNotEqualAndNotQuery().Value).TextValue
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
                      "name": "user_id",
                      "type": "uuid"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000005-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new WhereNotEqualAndNotQuery().Result).TextValue
        );
    }
}
