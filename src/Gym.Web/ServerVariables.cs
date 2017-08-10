namespace System.Web
{
    /// <summary>
    /// 表示常用的服务器环境变量 。
    /// </summary>
    public sealed class ServerVariables
    {
        /// <summary>
        /// 客户端发送的所有 HTTP 标题文件。
        /// </summary>
        public const string ALL_HTTP = "ALL_HTTP";
        /// <summary>
        /// 发出请求的远程主机的IP地址。
        /// </summary>
        public const string REMOTE_ADDR = "REMOTE_ADDR";
        /// <summary>
        /// 发出请求的主机名称。
        /// </summary>
        public const string REMOTE_HOST = "REMOTE_HOST";
        /// <summary>
        /// 用户发送的未映射的用户名字符串。该名称是用户实际发送的名称，与服务器上验证过滤器修改过后的名称相对。
        /// </summary>
        public const string REMOTE_USER = "REMOTE_USER";
        /// <summary>
        /// 该方法用于提出请求。相当于用于 HTTP 的 GET、HEAD、POST 等等。
        /// </summary>
        public const string REQUEST_METHOD = "REQUEST_METHOD";
        /// <summary>
        /// 执行脚本的虚拟路径。用于自引用的 URL。
        /// </summary>
        public const string SCRIPT_NAME = "SCRIPT_NAME";
        /// <summary>
        /// 出现在自引用 UAL 中的服务器主机名、DNS 化名或 IP 地址。
        /// </summary>
        public const string SERVER_NAME = "SERVER_NAME";
        /// <summary>
        /// 发送请求的端口号。
        /// </summary>
        public const string SERVER_PORT = "SERVER_PORT";
        /// <summary>
        /// 包含 0 或 1 的字符串。如果安全端口处理了请求，则为 1，否则为 0。
        /// </summary>
        public const string SERVER_PORT_SECURE = "SERVER_PORT_SECURE";
        /// <summary>
        /// 请求信息协议的名称和修订。格式为 protocol/revision 。
        /// </summary>
        public const string SERVER_PROTOCOL = "SERVER_PROTOCOL";
        /// <summary>
        /// 应答请求并运行网关的服务器软件的名称和版本。格式为 name/version 。
        /// </summary>
        public const string SERVER_SOFTWARE = "SERVER_SOFTWARE";
        /// <summary>
        /// 提供 URL 的基本部分。
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// 查询 HTTP 请求中问号（?）后的信息。
        /// </summary>
        public const string QUERY_STRING = "QUERY_STRING";
        /// <summary>
        /// PATH_INFO 转换后的版本，该变量获取路径并进行必要的由虚拟至物理的映射。
        /// </summary>
        public const string PATH_TRANSLATED = "PATH_TRANSLATED";
        /// <summary>
        /// 客户端提供的额外路径信息。可以使用这些虚拟路径和 PATH_INFO 服务器变量访问脚本。如果该信息来自 URL，在到达 CGI 脚本前就已经由服务器解码了。
        /// </summary>
        public const string PATH_INFO = "PATH_INFO";
        /// <summary>
        /// 返回接受请求的服务器地址。如果在绑定多个 IP 地址的多宿主机器上查找请求所使用的地址时，这条变量非常重要。
        /// </summary>
        public const string LOCAL_ADDR = "LOCAL_ADDR";
    }
}
