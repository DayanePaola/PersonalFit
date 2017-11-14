using Newtonsoft.Json;
using Result;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Util
{
    public static class Validation
    {
        //Busca de CEP
        public static ResultConsultaCep BuscaCEP(string cep)
        {
            string url = "http://cep.republicavirtual.com.br/web_cep.php?cep=@cep&formato=json".Replace("@cep", cep);

            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                return new ResultConsultaCep() { resultado = "0" };

            var json = "";

            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(url);
            }

            return JsonConvert.DeserializeObject<ResultConsultaCep>(json);
        }

        //Validação de CPF
        public static bool ValidaCPF(string cpf)
        {
            if (cpf == "" || cpf.Length != 11)
            {
                return false;
            }

            switch (cpf)
            {
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            int d1, d2, soma = 0;
            string digitado = "";
            string calculado = "";

            // Pesos para calcular os digitos
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];

            try
            {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1));
                n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1));
                n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1));
                n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1));
                n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
                soma += (peso1[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
                soma += (peso2[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            if (cpf == "00000000000")
            {
                return true;
            }

            // Confere se o cálculo bate com os dados inseridos pelo usuário
            if (calculado == digitado)
                return true;
            else
                return false;
        }

        //Validação de CNPJ
        public static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        //Validação de CEP
        public static bool ValidaCEP(string cep)
        {
            Boolean ok = true;
            if (cep.Length != 8)
            {
                ok = false;
            }

            switch (cep)
            {
                case "11111111":
                case "00000000":
                case "22222222":
                case "33333333":
                case "44444444":
                case "55555555":
                case "66666666":
                case "77777777":
                case "88888888":
                case "99999999":
                    ok = false;
                    break;
            }

            if (cep == "")
            {
                ok = true;
            }
            return ok;
        }

        //Remove os caracteres especiais da string
        public static String RemCaracteresEsp(String input, bool remEspacos = false)
        {
            var trim = input.Trim();

            if (remEspacos)
                return Regex.Replace(trim, "[^0-9a-zA-Z]+", "");
            return Regex.Replace(trim, "[^0-9a-zA-Z ]+", "");
        }

        //Remove um parte da string recebida
        public static String RemSubstring(String input, int index)
        {
            var substring = input.Remove(index);
            return substring;
        }

        //Remove os acentos de uma string
        public static string RemoverAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        //Geracao de Hash
        public static class GeraMD5
        {
            public static string GeraHash(string palavra)
            {
                MD5 md5Hash = MD5.Create();
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(palavra));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }

            public static bool VerificaHash(string palavra, string hashBanco)
            {
                string hashRecebido = GeraHash(palavra);

                StringComparer comparacao = StringComparer.OrdinalIgnoreCase;

                if (0 == comparacao.Compare(hashRecebido, hashBanco))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Criptografia XOR
        public static class XOREncryption
        {
            public static string DefaultKey { get; set; } = "HzIhND8cVi7qPLchVq6E1U737mTGlPKAnZs4v689798dasdaX8B46hY0VLVt" +
                                                            "XhsmsgRmakFLdCWBxQhgtyhghgvjkhvkjbkljhoq6L2e54J1gysFjGzJGU" +
                                                            "5Ot5WmYXDErnAuI9wARyLYVlNlDpdoFk3z9dNM4hxR1nc444DDsJvpVzPkgmYkC";

            public static string Encrypt(string palavra, string key = "")
            {
                if (String.IsNullOrEmpty(key))
                {
                    key = DefaultKey;
                }

                string sbOut = String.Empty;
                for (int i = 0; i < palavra.Length; i++)
                {
                    sbOut += String.Format("{0:00}", palavra[i] ^ key[i % key.Length]);
                }

                return sbOut;
            }

            public static string Decrypt(string palavra, string key = "")
            {
                if (String.IsNullOrEmpty(key))
                {
                    key = DefaultKey;
                }

                string sbOut = String.Empty;
                for (int i = 0; i < palavra.Length; i += 2)
                {
                    byte code = Convert.ToByte(palavra.Substring(i, 2));
                    sbOut += (char)(code ^ key[(i / 2) % key.Length]);
                }

                return sbOut;
            }
        }
    }
}