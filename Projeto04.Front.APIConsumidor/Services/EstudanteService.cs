using Projeto04.Front.APIConsumidor.Models;

namespace Projeto04.Front.APIConsumidor.Services
{
    public class EstudanteService
    {
        private HttpClient _httpClient;

        // definindo o construtor da classe Service para receber a DI (Injeção de Dependência)
        public EstudanteService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // aqui, abaixo será definido o "endereço" para onde
            // o front tem que apontar para acessar os endpoints das APIs

            _httpClient.BaseAddress = new Uri("http://localhost:5198");
        }

        /*
         * ===================================================
         * Implementação dos acessos aos endpoints da API
         *====================================================
         *           
         */

        // 1º tarefa CRUD: Read - definir a tarefa assíncrona para
        // a recuperação de todos os registros da base

        public async Task<List<Estudante>> GetEstudantesAsync()
        {
            // criar uma "consulta" para acessar a API com o 
            // propósito de acessar o endpoint (composição do
            // endereço-base + a rota adequada) para o acesso a todos
            // os dados da base. 

            var apiResposta = await 
                _httpClient.GetFromJsonAsync<List<Estudante>>
                ($"/api/Estudante/Get/GetAll");

            // definir a expressão de retorno que irá retornar a var
            // apiResposta

            return apiResposta;
        }

        // 2ª tarefa CRUD - Seleção: selecionar um único registro pelo seu
        // elemento identificador - id. 
        public async Task<Estudante> GetEstudanteByIdAsync(int id)
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<Estudante>
                ($"api/Estudante/GetOne/GetOne{id}");
            return apiResposta;
        }

        // 3ª tarefa CRUD - Create: 
        public async Task<Estudante> AddEstudanteAsync(Estudante estudante)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/Estudante/Post/AddRegister", estudante);

            apiResposta.EnsureSuccessStatusCode();
            return await apiResposta.Content.ReadFromJsonAsync<Estudante>();
        }

        // 4ª tarefa CRUD - Update:

        public async Task<Estudante> UpdateEstudanteAsync(int id, Estudante 
            estudante)
        {
            var apiResposta = await _httpClient.PutAsJsonAsync($"/api/Estudante/PutRegister/UpRegister/{id}", estudante);
            apiResposta.EnsureSuccessStatusCode();
            return await apiResposta.Content.ReadFromJsonAsync<Estudante>();
        }

        // 5ª tarefa CRUD - Delete - Exclusão:

        public async Task DeleteEstudanteAsync(int id)
        {
            var apiResposta = await _httpClient.DeleteAsync($"/api/Estudante/Delete/delRegister{id}");
            apiResposta.EnsureSuccessStatusCode();
        }
    }
}
