using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initium.Models
{
    public class HomeModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class ClientModel
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
    }
    public class CountDownModel
    {
        public int IdCountDown { get; set; }
        public int IdClient { get; set; }
        public string Name { get; set; }
        public DateTime DtBegin { get; set; }
        public DateTime DtEnd { get; set; }
        public int SecondsRemaining { get; set; }
    }
}
