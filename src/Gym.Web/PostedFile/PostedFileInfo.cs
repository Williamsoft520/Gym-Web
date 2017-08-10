namespace System.Web
{
    /// <summary>
    /// 表示上传的文件单个信息。
    /// </summary>
    public class PostedFileInfo
    {
        /// <summary>
        /// 获取上传文件的原始名称，而不是上传以后的名称。
        /// </summary>
        /// <remarks>上传以后的名称可能是自动生成的。</remarks>
        public string OriginalFileName { get; internal set; }
        /// <summary>
        /// 获取上传文件的大小，单位是 byte。
        /// </summary>
        public long Size { get; internal set; }
        /// <summary>
        /// 获取文件上传到服务器后的名称，不是上传前的原始名称。当 <see cref="Result"/> 是 <see cref="PostedFileResult.Success"/> 时有效。
        /// </summary>
        public string FileName { get; internal set; }
        /// <summary>
        /// 获取文件的扩展名。
        /// </summary>
        public string Extension { get; internal set; }
        /// <summary>
        /// 获取上传结果。
        /// </summary>
        public PostedFileResult Result { get;internal set; }
    }
}
