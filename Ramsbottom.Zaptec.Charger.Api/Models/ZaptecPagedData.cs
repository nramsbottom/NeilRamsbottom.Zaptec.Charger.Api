using System.Collections.Generic;

namespace NeilRamsbottom.Zaptec.Charger.Api.Models
{
    public class ZaptecPagedData<T>
    {
        public int Pages { get; set; }
        public List<T> Data { get; set; }
        public string Message { get; set; }
    }
}
