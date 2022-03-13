using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class InvestorAlreadyExistsException : Exception
    {
        public InvestorAlreadyExistsException()
        {
        }

        public InvestorAlreadyExistsException(string? message) : base(message)
        {
        }

        public InvestorAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvestorAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}