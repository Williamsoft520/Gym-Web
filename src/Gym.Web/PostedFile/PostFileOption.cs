namespace System.Web
{
    /// <summary>
    /// 文件上传的选项。
    /// </summary>
    public class PostedFileOption
    {
        /// <summary>
        /// 初始化 <see cref="PostedFileOption"/> 类的新实例。默认上传路径是 “/Upload/”
        /// </summary>
        public PostedFileOption()
        {

        }

        /// <summary>
        /// 获取或设置文件上传的服务器目录路径。若该目录不存在，则会自动创建（请确保有足够的权限创建目录）。
        /// </summary>
        public string DirectoryPath { get; set; } = "~/Upload/";
        /// <summary>
        /// 获取或设置允许上传的文件扩展名。例如 jpg png；null 表示不限制扩展名。
        /// </summary>
        /// <remarks>忽略大小写。</remarks>
        /// <example><code>new []{"jpg","png","gif"};</code></example>
        public string[] AllowExtentions { get; set; }
        /// <summary>
        /// 获取或设置允许上传的文件大小的最大值限制，单位是 byte，0 表示无任何限制。
        /// </summary>
        /// <value>默认 512 * 1024 = 512KB</value>
        public int AllowMaxFileSize { get; set; } = 512 * 1024;
        /// <summary>
        /// 获取或设置允许上传的文件大小的最小限制，单位是 byte，0 表示无任何限制。
        /// </summary>
        public int AllowMinFileSize { get; set; }
        /// <summary>
        /// 获取或设置保存在服务器上的文件名。若为 null，则使用上传的文件名称。
        /// </summary>
        public IPostedFileName PostedFileName { get; set; }
    }

    /// <summary>
    /// 文件上传结果。
    /// </summary>
    public enum PostedFileResult
    {
        /// <summary>
        /// 上传成功。
        /// </summary>
        Success=0,
        /// <summary>
        /// 不合法的文件最小大小限制。
        /// </summary>
        InvalidMinFileSize=1,
        /// <summary>
        /// 不合法的文件最大大小限制。
        /// </summary>
        InvalidMaxFileSize=2,
        /// <summary>
        /// 不合法的文件扩展名。
        /// </summary>
        InvalidFileExtensions=3,
        /// <summary>
        /// 文件不包含任何内容
        /// </summary>
        NoFileContent=4,
    }

}
