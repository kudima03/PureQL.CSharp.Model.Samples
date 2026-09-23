using PureQL.CSharp.Model.Samples.Queries.Select;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Select;

public sealed record SelectIdAndNameFromTableWithoutIndexesQueryTests
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
                    "entity": "schema_without_foreign_keys.table_without_indexes"
                  },
                  "select": [
                    {
                      "entity": "schema_without_foreign_keys.table_without_indexes",
                      "field": "id",
                      "type": {
                        "name": "uuid"
                      }
                    },
                    {
                      "entity": "schema_without_foreign_keys.table_without_indexes",
                      "field": "name",
                      "type": {
                        "name": "string"
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(
                new SelectIdAndNameFromTableWithoutIndexesQuery().Value
            ).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "name",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": []
                }
                """
            ).TextValue,
            new DataSetJson(
                new SelectIdAndNameFromTableWithoutIndexesQuery().Result
            ).TextValue
        );
    }
}
