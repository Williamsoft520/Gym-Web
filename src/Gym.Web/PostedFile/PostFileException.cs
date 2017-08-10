using System.Runtime.Serialization;

namespace System.Web
{
    /// <summary>
    /// 尝试从客户端上传文件时的异常。
    /// </summary>
    public class PostedFileException : Exception
    {
        /// <summary>
        /// 初始化 <see cref="PostedFileException"/> 类的新实例。
        /// </summary>
        public PostedFileException()
        {
        }

        /// <summary>
        /// 使用自定义异常消息初始化 <see cref="PostedFileException"/> 类的新实例。
        /// </summary>
        /// <param name="message">自定义异常消息。</param>
        public PostedFileException(string message) : base(message)
        {
        }

        /// <summary>
        /// 使用自定义异常消息和一个内部异常初始化 <see cref="PostedFileException"/> 类的新实例。
        /// </summary>
        /// <param name="message">自定义异常消息。</param>
        /// <param name="innerException">引发该异常的内部异常。</param>
        public PostedFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostedFileException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected PostedFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
