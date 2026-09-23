using PureQL.CSharp.Model.Samples.Queries.Errors;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Errors;

public sealed record JoinEntityNotInSuppliedDatasetsQueryTests
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
                    "entity": "single_table_schema.single_column_table"
                  },
                  "select": [
                    {
                      "entity": "single_table_schema.single_column_table",
                      "field": "id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  ],
                  "joins": [
                    {
                      "type": "inner",
                      "entity": "single_table_schema.nonexistent_join_table",
                      "on": {
                        "operator": "eachEqual",
                        "left": {
                          "entity": "single_table_schema.single_column_table",
                          "field": "id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "entity": "single_table_schema.nonexistent_join_table",
                          "field": "whatever_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new JoinEntityNotInSuppliedDatasetsQuery().Value).TextValue
        );
    }
}
