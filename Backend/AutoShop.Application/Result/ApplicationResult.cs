using System.Collections.Generic;

namespace AutoShop.Application.Result
{
    public class ApplicationResult
    {
        public bool Successo { get; set; }
        public IEnumerable<string> Mensagens { get; set; }

        public ApplicationResult(){}

        public ApplicationResult(bool successo, IEnumerable<string> mensagens)
        {
            Successo = successo;
            Mensagens = mensagens ?? new List<string>();
        }
    }
}
