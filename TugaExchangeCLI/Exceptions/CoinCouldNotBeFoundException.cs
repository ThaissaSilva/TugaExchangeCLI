using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class CoinCouldNotBeFoundException : Exception
    {
        public CoinCouldNotBeFoundException()
        {
        }

        public CoinCouldNotBeFoundException(string? message) : base(message)
        {
        }

        public CoinCouldNotBeFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CoinCouldNotBeFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}