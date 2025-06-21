// Repositorios/TarefaDiariaRepositorio.cs
using API_ToDo_List.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration; // Para acessar a string de conexão
using System; // Para Convert

namespace API_ToDo_List.Repositorios
{
    public class TarefaDiariaRepositorio : ITarefaDiariaRepositorio
    {
        private readonly string _connectionString;

        public TarefaDiariaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<TarefaDiaria>> GetAllDailyAsync()
        {
            var tarefasDiarias = new List<TarefaDiaria>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Descricao, ConcluidaHoje FROM TarefasDiarias"; // NOVO NOME DE TABELA
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        int idColumnIndex = reader.GetOrdinal("Id");
                        int descricaoColumnIndex = reader.GetOrdinal("Descricao");
                        int concluidaHojeColumnIndex = reader.GetOrdinal("ConcluidaHoje");

                        while (await reader.ReadAsync())
                        {
                            tarefasDiarias.Add(new TarefaDiaria
                            {
                                Id = reader.IsDBNull(idColumnIndex) ? 0 : Convert.ToInt32(reader.GetValue(idColumnIndex)),
                                Descricao = reader.IsDBNull(descricaoColumnIndex) ? string.Empty : Convert.ToString(reader.GetValue(descricaoColumnIndex)),
                                ConcluidaHoje = reader.IsDBNull(concluidaHojeColumnIndex) ? false : Convert.ToBoolean(reader.GetValue(concluidaHojeColumnIndex))
                            });
                        }
                    }
                }
            }
            return tarefasDiarias;
        }

        public async Task<TarefaDiaria> GetDailyByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Descricao, ConcluidaHoje FROM TarefasDiarias WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            int idColumnIndex = reader.GetOrdinal("Id");
                            int descricaoColumnIndex = reader.GetOrdinal("Descricao");
                            int concluidaHojeColumnIndex = reader.GetOrdinal("ConcluidaHoje");

                            return new TarefaDiaria
                            {
                                Id = reader.IsDBNull(idColumnIndex) ? 0 : Convert.ToInt32(reader.GetValue(idColumnIndex)),
                                Descricao = reader.IsDBNull(descricaoColumnIndex) ? string.Empty : Convert.ToString(reader.GetValue(descricaoColumnIndex)),
                                ConcluidaHoje = reader.IsDBNull(concluidaHojeColumnIndex) ? false : Convert.ToBoolean(reader.GetValue(concluidaHojeColumnIndex))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task AddDailyAsync(TarefaDiaria novaTarefaDiaria)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO TarefasDiarias (Descricao, ConcluidaHoje) VALUES (@Descricao, @ConcluidaHoje); SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", novaTarefaDiaria.Descricao);
                    command.Parameters.AddWithValue("@ConcluidaHoje", novaTarefaDiaria.ConcluidaHoje);
                    novaTarefaDiaria.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task UpdateDailyAsync(TarefaDiaria tarefaDiariaAtualizada)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE TarefasDiarias SET Descricao = @Descricao, ConcluidaHoje = @ConcluidaHoje WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", tarefaDiariaAtualizada.Descricao);
                    command.Parameters.AddWithValue("@ConcluidaHoje", tarefaDiariaAtualizada.ConcluidaHoje);
                    command.Parameters.AddWithValue("@Id", tarefaDiariaAtualizada.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteDailyAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM TarefasDiarias WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}