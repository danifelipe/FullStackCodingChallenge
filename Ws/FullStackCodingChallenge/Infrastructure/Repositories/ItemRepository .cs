using Domain.Interface;
using Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly DatabaseContext _context;

        public ItemRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            try
            {
                var items = new List<Item>();

                using var connection = (SqlConnection)_context.CreateConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand("GetAllItems", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    items.Add(new Item
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2)
                    });
                }

                return items;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos al obtener los items.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los items.", ex);
            }
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            try
            {
                using var connection = (SqlConnection)_context.CreateConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand("GetItemById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new Item
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2)
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al obtener el item con ID {id}.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener el item con ID {id}.", ex);
            }
        }

        public async Task<int> AddAsync(Item entity)
        {
            try
            {
                using var connection = (SqlConnection)_context.CreateConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand("AddItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);

                var returnParameter = new SqlParameter("@InsertedId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnParameter);

                await command.ExecuteNonQueryAsync();
                return (int)returnParameter.Value;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos al insertar el item.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al insertar el item.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Item entity)
        {
            try
            {
                using var connection = (SqlConnection)_context.CreateConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand("UpdateItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al actualizar el item con ID {entity.Id}.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al actualizar el item con ID {entity.Id}.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using var connection = (SqlConnection)_context.CreateConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand("DeleteItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al eliminar el item con ID {id}.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al eliminar el item con ID {id}.", ex);
            }
        }
    }
}
