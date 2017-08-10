namespace System.Web
{
    /// <summary>
    /// 使用 DateTime 算法给每一个文件进行重命名。
    /// </summary>
    public class DateTimePostedFileName : IPostedFileName
    {
        /// <summary>
        /// 初始化 <see cref="DateTimePostedFileName"/> 类的新实例。
        /// </summary>
        public DateTimePostedFileName()
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
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }
    }
}
