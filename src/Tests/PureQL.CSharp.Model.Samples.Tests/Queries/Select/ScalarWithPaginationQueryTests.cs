using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record ScalarWithPaginationQueryTests
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
                        "name": "number"
                      },
                      "value": 9,
                      "alias": "page_marker"
                    }
                  ],
                  "pagination": {
                    "skip": 1,
                    "take": 2
                  }
                }
                """
            ).TextValue,
            new QueryJson(new ScalarWithPaginationQuery().Value).TextValue
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
                      "name": "page_marker",
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
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ScalarWithPaginationQuery().Result).TextValue
        );
    }
}
