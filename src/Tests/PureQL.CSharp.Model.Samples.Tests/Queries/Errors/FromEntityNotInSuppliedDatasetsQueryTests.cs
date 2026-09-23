using PureQL.CSharp.Model.Samples.Queries.Errors;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Errors;

public sealed record FromEntityNotInSuppliedDatasetsQueryTests
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
                    "entity": "single_table_schema.nonexistent_table"
                  },
                  "select": [
                    {
                      "entity": "single_table_schema.nonexistent_table",
                      "field": "whatever",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new FromEntityNotInSuppliedDatasetsQuery().Value).TextValue
        );
    }
}
