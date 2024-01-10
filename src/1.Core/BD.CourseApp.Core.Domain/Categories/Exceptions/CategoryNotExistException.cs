using System.Runtime.Serialization;

namespace BD.CourseApp.Core.Domain.Categories.Exceptions
{
    [Serializable]
    public class CategoryNotExistException : Exception
    {
        public CategoryNotExistException()
        {
        }

        public CategoryNotExistException(string? message) : base(message)
        {
        }

        public CategoryNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CategoryNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}