using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace refactorRepositories
{
    public abstract class BaseRepository<T>
    {

        #region Fields

        public ApiDatabase Database { get; set; }

        #endregion


        #region Constructors

        public BaseRepository()
        {
            Database = new ApiDatabase();
        }

        #endregion


        #region Abstract Methods

        protected abstract T ReadRow(SqlDataReader reader);

        #endregion


        #region Private Methods

        protected List<T> RunQuery(string query, SqlParameter[] parameters)
        {
            List<T> list = new List<T>();

            using (SqlConnection connection = Database.GetConnection())
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);

                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = ReadRow(reader);
                    list.Add(item);
                }

                connection.Close();
            }

            return list;
        }

        protected int RunNonQuery(string query, SqlParameter[] parameters)
        {
            int count = 0;

            using (SqlConnection connection = Database.GetConnection())
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);

                connection.Open();

                count = command.ExecuteNonQuery();

                connection.Close();
            }

            return count;
        }


        protected Guid GetGuidValue(SqlDataReader reader, string key)
        {
            return (Guid)reader[key];
        }

        protected string GetStringValue(SqlDataReader reader, string key)
        {
            return (string)reader[key];
        }

        protected decimal GetDecimalValue(SqlDataReader reader, string key)
        {
            return (decimal)reader[key];
        }


        protected void AddGuidParameter(List<SqlParameter> parameters, string name, Guid value)
        {
            var parameter = new SqlParameter(name, value);
            parameters.Add(parameter);
        }

        protected void AddStringParameter(List<SqlParameter> parameters, string name, string value)
        {
            var parameter = new SqlParameter(name, value ?? "");
            parameters.Add(parameter);
        }

        protected void AddDecimalParameter(List<SqlParameter> parameters, string name, decimal value)
        {
            var parameter = new SqlParameter(name, value);
            parameters.Add(parameter);
        }

        #endregion

    }
}
