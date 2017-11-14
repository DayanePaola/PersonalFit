using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Util.Annotation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class Obrigatorio : Attribute
    {
        public string Mensagem { get; set; } = "Erros encontrados: ";
        public List<string> MsgCampos { get; set; } = new List<string>();
        public List<string> MaxErros { get; set; } = new List<string>();
        public List<string> MinErros { get; set; } = new List<string>();

        public string ResultObrigatorio { get; set; }
        public string ResultMax { get; set; }
        public string ResultMin { get; set; }

        public void validar(object parametros)
        {
            var obj = Convert.ChangeType(parametros, parametros.GetType());

            PropertyInfo[] campos = obj.GetType().GetProperties();

            foreach (PropertyInfo item in campos)
            {
                var obrigat = item.GetCustomAttributes(typeof(Obrigatorio), false).ToArray();
                Descricao desc = (Descricao)item.GetCustomAttributes(typeof(Descricao), false).FirstOrDefault();
                TMax maxLength = (TMax)item.GetCustomAttributes(typeof(TMax), false).FirstOrDefault();
                TMin minLength = (TMin)item.GetCustomAttributes(typeof(TMin), false).FirstOrDefault();

                var valor = item.GetValue(obj, null);

                if (obrigat.Count() > 0)
                {
                    if (valor == null)
                    {
                        MsgCampos.Add(desc.descricao);
                    }
                    else if (valor is String)
                    {
                        if ((String)valor == "")
                        {
                            MsgCampos.Add(desc.descricao);
                        }
                    }
                    else if (valor is Int32)
                    {
                        if ((Int32)valor == 0)
                        {
                            MsgCampos.Add(desc.descricao);
                        }
                    }
                }

                if (maxLength != null && valor != null)
                {
                    if (valor.ToString().Count() > maxLength.MaxLength)
                    {
                        MaxErros.Add(desc.descricao + " deve ter no máximo " + maxLength.MaxLength + " caracteres");
                    }
                }

                if (minLength != null && valor != null)
                {
                    if (valor.ToString().Length < minLength.MinLength)
                    {
                        MinErros.Add(desc.descricao + " deve ter no mínimo " + minLength.MinLength + " caracteres");
                    }
                }
            }

            if (MsgCampos.Count > 0)
            {
                ResultObrigatorio = "Informe os campos obrigatórios: ";

                for (int i = 0; i < MsgCampos.Count - 1; i++)
                {
                    ResultObrigatorio += MsgCampos[i] + ", ";
                }

                ResultObrigatorio += MsgCampos[MsgCampos.Count - 1] + "; ";

            }

            if (MaxErros.Count > 0)
            {
                for (int i = 0; i < MaxErros.Count - 1; i++)
                {
                    ResultMax += MaxErros[i] + ", ";
                }

                ResultMax += MaxErros[MaxErros.Count - 1] + "; ";
            }

            if (MinErros.Count > 0)
            {
                for (int i = 0; i < MinErros.Count - 1; i++)
                {
                    ResultMin += MinErros[i] + ", ";
                }

                ResultMin += MinErros[MinErros.Count - 1] + "; ";
            }

            if (MsgCampos.Count > 0 || MaxErros.Count > 0 || MinErros.Count > 0)
            {
                throw new ArgumentNullException(Mensagem + ResultObrigatorio + ResultMax + ResultMin);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class Descricao : Attribute
    {
        public String descricao { get; set; }

        public Descricao(String descricao)
        {
            this.descricao = descricao;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TMax : Attribute
    {
        public int MaxLength { get; set; }
        public string Mensagem { get; set; }

        public TMax(int MaxLength, String Mensagem = "")
        {
            this.MaxLength = MaxLength;
            this.Mensagem = Mensagem;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TMin : Attribute
    {
        public int MinLength { get; set; }
        public string Mensagem { get; set; }

        public TMin(int MinLength, String Mensagem = "")
        {
            this.MinLength = MinLength;
            this.Mensagem = Mensagem;
        }
    }
}