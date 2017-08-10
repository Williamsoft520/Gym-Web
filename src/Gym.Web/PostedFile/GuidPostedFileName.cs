namespace System.Web
{
    /// <summary>
    /// 使用 Guid 算法给每一个文件进行重命名。
    /// </summary>
    public class GuidPostedFileName : IPostedFileName
    {
        /// <summary>
        /// 初始化 <see cref="GuidPostedFileName"/> 类的新实例。
        /// </summary>
        public GuidPostedFileName()
        {

        }
        /// <summary>
        /// 生成名称的算法。
        /// </summary>
        /// <returns>
        /// 文件名，不带后缀。
        /// </returns>
        public string GenerateName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
