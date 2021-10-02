using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Result
{
    public class ApplicationCreateResult : ApplicationResult
    {
        public string Id { get; set; }

        public ApplicationCreateResult(string id, bool successo, IEnumerable<string> mensagens) : base(successo, mensagens)
        {
            this.Id = id;
        }
    }
}
