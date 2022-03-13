using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class InvestorCouldNotBeFoundException : Exception
    {
        public InvestorCouldNotBeFoundException()
        {
        }

        public InvestorCouldNotBeFoundException(string? message) : base(message)
        {
        }

        public InvestorCouldNotBeFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvestorCouldNotBeFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}