using System.Collections.Generic;

namespace ApaApi.models
{
    /// <summary>
    /// Define o retorno base da API
    /// </summary>
    /// <typeparam name="T">O tipo de dados</typeparam>
    public class BaseViewModel<T>
    {
        /// <summary>
        /// Status da resposta (Resultado da requisição)
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Dados 
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Erros
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }

    /// <summary>
    /// Define o retorno base da API
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// Status da resposta (Resultado da requisição)
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Erros
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
