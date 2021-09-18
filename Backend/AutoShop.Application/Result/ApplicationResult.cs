using System.Collections.Generic;

namespace AutoShop.Application.Result
{
    public class ApplicationResult
    {
        public bool Sucesso { get; set; }
        public IEnumerable<string> Mensagens { get; set; }

        private ApplicationResult(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public ApplicationResult(bool successo, IEnumerable<string> mensagens) : this(successo)
        {
            Mensagens = mensagens ?? new List<string>();
        }
    }
}
