using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record CountOfDateColumnOverAllOrdersQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "select": [
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.orders",
                        "field": "placed_on",
                        "type": {
                          "name": "date"
                        }
                      },
                      "alias": "n"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CountOfDateColumnOverAllOrdersQuery().Value).TextValue
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
                      "name": "n",
                      "type": "double"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "6"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new CountOfDateColumnOverAllOrdersQuery().Result).TextValue
        );
    }
}
