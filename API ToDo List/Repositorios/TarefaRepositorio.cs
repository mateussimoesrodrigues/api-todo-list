using API_ToDo_List.Models;
using MySql.Data.MySqlClient; // Importante: classe para MySQL
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration; // Para acessar a string de conexão

namespace API_ToDo_List.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly string _connectionString;

        public TarefaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            var tarefas = new List<Tarefa>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Descricao, Concluida FROM Tarefas";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // >>> NOVO: Obtenha os índices das colunas uma vez <<<
                        int idColumnIndex = reader.GetOrdinal("Id");
                        int descricaoColumnIndex = reader.GetOrdinal("Descricao");
                        int concluidaColumnIndex = reader.GetOrdinal("Concluida");

                        while (await reader.ReadAsync())
                        {
                            tarefas.Add(new Tarefa
                            {
                                // >>> Alterado para usar o índice e o GetValue/Convert <<<
                                Id = reader.IsDBNull(idColumnIndex) ? 0 : Convert.ToInt32(reader.GetValue(idColumnIndex)),
                                Descricao = reader.IsDBNull(descricaoColumnIndex) ? string.Empty : Convert.ToString(reader.GetValue(descricaoColumnIndex)),
                                Concluida = reader.IsDBNull(concluidaColumnIndex) ? false : Convert.ToBoolean(reader.GetValue(concluidaColumnIndex))
                            });
                        }
                    }
                }
            }
            return tarefas;
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Descricao, Concluida FROM Tarefas WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // >>> NOVO: Obtenha os índices das colunas uma vez <<<
                            int idColumnIndex = reader.GetOrdinal("Id");
                            int descricaoColumnIndex = reader.GetOrdinal("Descricao");
                            int concluidaColumnIndex = reader.GetOrdinal("Concluida");

                            return new Tarefa
                            {
                                // >>> Alterado para usar o índice e o GetValue/Convert <<<
                                Id = reader.IsDBNull(idColumnIndex) ? 0 : Convert.ToInt32(reader.GetValue(idColumnIndex)),
                                Descricao = reader.IsDBNull(descricaoColumnIndex) ? string.Empty : Convert.ToString(reader.GetValue(descricaoColumnIndex)),
                                Concluida = reader.IsDBNull(concluidaColumnIndex) ? false : Convert.ToBoolean(reader.GetValue(concluidaColumnIndex))
                            };
                        }
                    }
                }
            }
            return null;
        }


        public async Task AddAsync(Tarefa tarefa)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Query para inserir e obter o ID gerado automaticamente pelo banco
                var query = "INSERT INTO Tarefas (Descricao, Concluida) VALUES (@Descricao, @Concluida); SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                    command.Parameters.AddWithValue("@Concluida", tarefa.Concluida);
                    // ExecuteScalarAsync é usado para obter um único valor (o ID neste caso)
                    tarefa.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task UpdateAsync(Tarefa tarefa)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Tarefas SET Descricao = @Descricao, Concluida = @Concluida WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                    command.Parameters.AddWithValue("@Concluida", tarefa.Concluida);
                    command.Parameters.AddWithValue("@Id", tarefa.Id);
                    await command.ExecuteNonQueryAsync(); // ExecuteNonQueryAsync para UPDATE/INSERT/DELETE
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Tarefas WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
