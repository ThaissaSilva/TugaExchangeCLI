using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class CoinNameAlreadyExistsException : Exception
    {
        public CoinNameAlreadyExistsException()
        {
        }

        public CoinNameAlreadyExistsException(string? message) : base(message)
        {
        }

        public CoinNameAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CoinNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}