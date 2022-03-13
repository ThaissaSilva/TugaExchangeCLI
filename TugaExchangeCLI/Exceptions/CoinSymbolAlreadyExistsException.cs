using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class CoinSymbolAlreadyExistsException : Exception
    {
        public CoinSymbolAlreadyExistsException()
        {
        }

        public CoinSymbolAlreadyExistsException(string? message) : base(message)
        {
        }

        public CoinSymbolAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CoinSymbolAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}