using System.Data;
using System.Data.SqlClient;

namespace EmployeesAPI.Utils;

public static class SqlUtils
{
    public static async Task<List<T>> GetData<T>(string connectionString,
        string procName,
        Func<SqlDataReader, T> mapper,
        params SqlParameter[] procParams)
    {
        using var reader = await ExecSql(connectionString, procName, procParams);
        var result = MapResult<T>(reader, mapper);
        return result;
    }

    public static async Task<SqlDataReader> ExecSql(string connectionString,
        string procName,
        params SqlParameter[] procParams)
    {
        var connection = new SqlConnection(connectionString);
        using var sqlCommand = connection.CreateCommand();
        await connection.OpenAsync();
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandText = procName;
        if (procParams != null)
        {
            sqlCommand.Parameters.AddRange(procParams);
        }

        var reader = await sqlCommand.ExecuteReaderAsync(
            CommandBehavior.CloseConnection);
        sqlCommand.Parameters.Clear();
        return reader;
    }

    public static List<T> MapResult<T>(SqlDataReader reader,
        Func<SqlDataReader, T> mapper)
    {
        var data = new List<T>();
        if (reader.HasRows)
            while (reader.Read())
            {
                data.Add(mapper(reader));
            }
        return data;
    }
}
