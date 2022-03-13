using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class CoinAlreadyExistsException : Exception
    {
        public CoinAlreadyExistsException()
        {
        }

        public CoinAlreadyExistsException(string? message) : base(message)
        {
        }

        public CoinAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CoinAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}