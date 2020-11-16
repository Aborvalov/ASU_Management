using DalContract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DalDB
{
    public class DbOrderDAO : IContractEntitiesDAO<Order>
    {
        public int Add(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("AddOrder", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };             
                SqlParameter paramNumber = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = entity.Number
                };
                SqlParameter paramDate = new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = entity.Date
                };
                SqlParameter paramProviderId = new SqlParameter
                {
                    ParameterName = "@ProviderId",
                    Value = entity.ProviderId
                };

                cmd.Parameters.AddRange(new SqlParameter[]
                                        { paramNumber, paramDate, paramProviderId});

                connection.Open();

                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public List<Order> GeatAll()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("GetAllOrder", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                connection.Open();

                var reader = cmd.ExecuteReader();
                var result = new List<Order>();

                while (reader.Read())
                    result.Add(new Order
                    {
                        Id = (int)reader["Id"],
                        Number = (string)reader["Number"],
                        Date = (DateTime)reader["Date"],
                        ProviderId = (int)reader["ProviderId"],
                    });

                reader.Close();

                return result;
            }
        }

        public Order GetById(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть меньше 1.", nameof(id));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("GetByIdOrder", connection)
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
                    return new Order
                    {
                        Id = (int)reader["Id"],
                        Number = (string)reader["Number"],
                        Date = (DateTime)reader["Date"],
                        ProviderId = (int)reader["ProviderId"],
                    };

                return null;
            }
        }

        public bool Remove(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть меньше 1.", nameof(id));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("DeleteOrder", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                SqlParameter paramResult = new SqlParameter
                {
                    ParameterName = "@Result",
                    Value = 0
                };
                cmd.Parameters.Add(param);
                cmd.Parameters.Add(paramResult);

                connection.Open();

                return (int)cmd.Parameters["@Result"].Value > 0;
            }
        }

        public bool Update(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("UpdateOrder", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                
                SqlParameter paramNumber = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = entity.Number
                };
                SqlParameter paramDate = new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = entity.Date
                };
                SqlParameter paramProviderId = new SqlParameter
                {
                    ParameterName = "@ProviderId",
                    Value = entity.ProviderId
                };
                SqlParameter paramResult = new SqlParameter
                {
                    ParameterName = "@Result",
                    Value = 0
                };
                cmd.Parameters.AddRange(new SqlParameter[]
                                        { paramNumber, paramDate, paramProviderId, paramResult});
                
                connection.Open();

                return (int)cmd.Parameters["@Result"].Value > 0;
            }
        }
    }
}
