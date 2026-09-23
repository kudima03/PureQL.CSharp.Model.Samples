using PureQL.CSharp.Model.Samples.Queries.Aggregates;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Aggregates;

public sealed record CountOfBooleanColumnOverAllProductsQueryTests
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
                    "entity": "schema_with_foreign_keys.products"
                  },
                  "select": [
                    {
                      "operator": "count",
                      "arg": {
                        "entity": "schema_with_foreign_keys.products",
                        "field": "product_in_stock",
                        "type": {
                          "name": "boolean"
                        }
                      },
                      "alias": "n"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new CountOfBooleanColumnOverAllProductsQuery().Value).TextValue
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
                      "4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(
                new CountOfBooleanColumnOverAllProductsQuery().Result
            ).TextValue
        );
    }
}
