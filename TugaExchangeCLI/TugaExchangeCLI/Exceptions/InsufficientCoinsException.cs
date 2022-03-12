using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class InsufficientCoinsException : Exception
    {
        public InsufficientCoinsException()
        {
        }

        public InsufficientCoinsException(string? message) : base(message)
        {
        }

        public InsufficientCoinsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficientCoinsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}