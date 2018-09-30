using ApaApi.models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApaApi.controllers
{
    /// <summary>
    /// Define o controller base da api
    /// </summary>
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// O identificador do usuário autenticado
        /// </summary>
        protected string UserId
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        /// <summary>
        /// A resposta da api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">O resultado</param>
        protected new IActionResult Response<T>(T result)
        {
            return Ok(new BaseViewModel<T>
            {
                Success = true,
                Data = result
            });
        }

        /// <summary>
        /// A resposta da api
        /// </summary>
        protected new IActionResult Response()
        {
            return Ok(new BaseViewModel
            {
                Success = true
            });
        }
    }
}
