using System;

namespace NeilRamsbottom.Zaptec.Charger.Api
{
    public class ZaptecApiException : Exception
    {
        public ZaptecApiException(string message) : base(message)
        {
        }

        public ZaptecApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
