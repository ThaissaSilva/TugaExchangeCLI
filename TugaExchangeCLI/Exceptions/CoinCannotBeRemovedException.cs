using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class CoinCannotBeRemovedException : Exception
    {
        public CoinCannotBeRemovedException()
        {
        }

        public CoinCannotBeRemovedException(string? message) : base(message)
        {
        }

        public CoinCannotBeRemovedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CoinCannotBeRemovedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}