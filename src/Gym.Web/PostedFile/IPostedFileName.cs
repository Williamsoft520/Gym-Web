namespace System.Web
{
    /// <summary>
    /// 提供文件上传时在服务端生成的文件名称的功能。
    /// </summary>
    public interface IPostedFileName
    {
        /// <summary>
        /// 生成名称的算法。
        /// </summary>
        /// <returns>文件名，不带后缀。</returns>
        string GenerateName();
    }
}
