using DalContract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalDB
{
    public class DbOrderItemDAO : IContractEntitiesDAO<OrderItem>
    {
        public int Add(OrderItem entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("AddOrderItem", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter paramOrderId = new SqlParameter
                {
                    ParameterName = "@OrderId",
                    Value = entity.OrderId
                };
                SqlParameter paramName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = entity.Name
                };
                SqlParameter paramQuantity = new SqlParameter
                {
                    ParameterName = "@Quantity",
                    Value = entity.Quantity
                };
                SqlParameter paramUnit = new SqlParameter
                {
                    ParameterName = "@Unit",
                    Value = entity.Unit
                };
                
                cmd.Parameters.AddRange(new SqlParameter [] 
                                        { paramOrderId, paramName, paramQuantity, paramUnit});

                connection.Open();

                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public List<OrderItem> GeatAll()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("GetAllOrderItem", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                connection.Open();

                var reader = cmd.ExecuteReader();
                var result = new List<OrderItem>();

                while (reader.Read())
                    result.Add(new OrderItem
                    {
                        Id = (int)reader["Id"],
                        OrderId = (int)reader["OrderId"],
                        Quantity = (decimal)reader["Quantity"],
                        Unit = (string)reader["Unit"],
                        Name = (string)reader["Name"],
                    });

                reader.Close();

                return result;
            }
        }

        public OrderItem GetById(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть меньше 1.", nameof(id));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("GetByIdOrderItem", connection)
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
                    return new OrderItem
                    {
                        Id = (int)reader["Id"],
                        OrderId = (int)reader["OrderId"],
                        Quantity = (decimal)reader["Quantity"],
                        Unit = (string)reader["Unit"],
                        Name = (string)reader["Name"],
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
                var cmd = new SqlCommand("DeleteOrderItem", connection)
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

        public bool Update(OrderItem entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlConnection connection = new SqlConnection(Connection.ConStr))
            {
                var cmd = new SqlCommand("UpdateOrderItem", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter paramId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = entity.Id
                };
                SqlParameter paramOrderId = new SqlParameter
                {
                    ParameterName = "@OrderId",
                    Value = entity.OrderId
                };
                SqlParameter paramName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = entity.Name
                };
                SqlParameter paramQuantity = new SqlParameter
                {
                    ParameterName = "@Quantity",
                    Value = entity.Quantity
                };
                SqlParameter paramUnit = new SqlParameter
                {
                    ParameterName = "@Unit",
                    Value = entity.Unit
                };
                SqlParameter paramResult = new SqlParameter
                {
                    ParameterName = "@Result",
                    Value = 0
                };
                cmd.Parameters.AddRange(new SqlParameter[]
                                        {paramId, paramOrderId, paramName, paramQuantity, paramUnit, paramResult});

                connection.Open();

                return (int)cmd.Parameters["@Result"].Value > 0;
            }
        }
    }
}
