using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Result
{
    /// <summary>
    /// Classe de transporte entre business e controllers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        public Result()
        {
            this.Mensagem = new ResultMensagem();
            this.StatusCode = System.Net.HttpStatusCode.OK;
        }

        /// <summary>
        /// Objeto a ser transportado
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Código HTTP referente ao objeto
        /// </summary>
        public System.Net.HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Mensagem e campo para erros
        /// </summary>
        public ResultMensagem Mensagem { get; set; }

	}

    public class ResultMensagem
    {
        public string Mensagem { get; set; }
        public string Campo { get; set; }
    }

    public class ResultConsultaCep
    {
        public string resultado { get; set; }
        public string resultado_txt { get; set; }
        public string uf { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string tipo_logradouro { get; set; }
        public string logradouro { get; set; }
    }

}