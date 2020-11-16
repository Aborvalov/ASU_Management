using DalContract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DalDB
{
    public class DbProviderDAO : IGetEntitiesDAO<Provider>
    {
        public List<Provider> GeatAll()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("GetAllProvider", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                connection.Open();

                var reader = cmd.ExecuteReader();
                var result = new List<Provider>();

                while (reader.Read())
                    result.Add( new Provider
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                    });

                reader.Close();

                return result;
            }
        }

        public Provider GetById(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть меньше 1.", nameof(id));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("GetByIdProvider", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                cmd.Parameters.Add(param);

                connection.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                    return new Provider
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                    };

                return null;
            }
        }
    }
}
