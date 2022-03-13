using System.Runtime.Serialization;

namespace TugaExchangeCLI
{
    [Serializable]
    internal class QuantityIsSmallerThanOrEqualToZeroException : Exception
    {
        public QuantityIsSmallerThanOrEqualToZeroException()
        {
        }

        public QuantityIsSmallerThanOrEqualToZeroException(string? message) : base(message)
        {
        }

        public QuantityIsSmallerThanOrEqualToZeroException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QuantityIsSmallerThanOrEqualToZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}