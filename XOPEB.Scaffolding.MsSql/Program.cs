
using Dapper;
using System.Data.SqlClient;
using XOPEB.Scaffolding;

Console.WriteLine("XOPEB Scaffolding for Microsoft SQL Server");

var connectionString = "Data Source=.;Initial Catalog=gent_root;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

using (var connection = new SqlConnection(connectionString))
{
    connection.Open();

    var tables = connection
        .Query<SqlTable>("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'")
        .ToArray()
        .Select(t => CreateTable(t, connection))
        .ToArray();

    var cg = new CSharpGenerator()
    {
        Namespace = "GENT"
    };

    var cs = cg.GetDatabaseClasses(tables);

    File.WriteAllText("out.cs", cs);
}

Table CreateTable(SqlTable sqlTable, SqlConnection connection)
{
    var columns = connection
        .Query<SqlColumn>($"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{sqlTable.TABLE_NAME}'")
        .ToArray()
        .OrderBy(c => c.ORDINAL_POSITION)
        .Select(c => CreateColumn(c, connection))
        .ToArray();

    var table = new Table(sqlTable.TABLE_NAME, columns);

    return table;
}

Column CreateColumn(SqlColumn sqlColumn, SqlConnection connection)
{
    var column = new Column(sqlColumn.COLUMN_NAME);

    column.Nullable = sqlColumn.IS_NULLABLE == "YES";

    column.Type = GetType(sqlColumn);

    column.Index = sqlColumn.ORDINAL_POSITION;

    column.MaxLength = sqlColumn.CHARACTER_MAXIMUM_LENGTH;

    return column;
}

string GetType(SqlColumn sqlColumn)
{
    if (sqlColumn.DATA_TYPE == "int")
    {
        return "int";
    }

    if (sqlColumn.DATA_TYPE == "bit")
    {
        return "bool";
    }

    if (sqlColumn.DATA_TYPE == "datetime2" || sqlColumn.DATA_TYPE == "datetime")
    {
        return "DateTime";
    }

    return "string";
}

internal class SqlColumn
{
    internal string COLUMN_NAME { get; set; } = string.Empty;

    internal string IS_NULLABLE { get; set; } = string.Empty;

    internal int ORDINAL_POSITION { get; set; }

    internal int CHARACTER_MAXIMUM_LENGTH { get; set; }

    internal string DATA_TYPE { get; set; } = string.Empty;
}

internal class SqlTable
{
    internal string TABLE_NAME { get; set; } = string.Empty;
}