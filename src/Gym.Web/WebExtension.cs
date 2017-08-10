using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace System.Web
{
    /// <summary>
    /// 提供 Web 操作时常用扩展。
    /// </summary>
    public static class WebExtension
    {
        /// <summary>
        /// 在页面显示一个 alert 弹出框，该弹出框只有一个“确定”按钮。
        /// </summary>
        /// <param name="page"><see cref="Page"/> 扩展实例。</param>
        /// <param name="message">展现在弹出框中的文本。</param>
        public static void Alert(this Page page,string message)
        {
            var script =string.Format("window.alert('{0}');",message);
            page.ClientScript.RegisterStartupScript(page.GetType(), string.Format("alert_{0}", Guid.NewGuid()), script, true);
        }

        /// <summary>
        /// 在页面显示一个 alert 弹出框，该弹出框只有一个“确定”按钮，点击按钮后将重定向到指定的 url 链接。
        /// </summary>
        /// <param name="page"><see cref="Page"/> 扩展实例。</param>
        /// <param name="message">展现在弹出框中的文本。</param>
        /// <param name="url">重定向的链接地址。null 表示当前页面的 url 链接地址。</param>
        public static void Alert(this Page page, string message, string url)
        {
            url = url ?? page.Request.Url.AbsoluteUri;
            var script =string.Format("window.alert('{0}');window.location.href='{1}';",message, url);
            page.ClientScript.RegisterStartupScript(page.GetType(),string.Format("alert_{0}",Guid.NewGuid()), script, true);
        }

        /// <summary>
        /// 获取发起请求的远程主机的 IP 地址。
        /// </summary>
        /// <param name="request"><see cref="HttpRequestBase"/> 的扩展实例。</param>
        /// <returns>符合远程主机的 IP 地址号段。</returns>
        public static string IpAddress(this HttpRequestBase request) => request.ServerVariables[ServerVariables.REMOTE_ADDR];

        /// <summary>
        /// 获取发起请求的远程主机的 IP 地址。
        /// </summary>
        /// <param name="request"><see cref="HttpRequest"/> 的扩展实例。</param>
        /// <returns>符合远程主机的 IP 地址号段。</returns>
        public static string IpAddress(this HttpRequest request) => new HttpRequestWrapper(request).IpAddress();

        /// <summary>
        /// 获取 Request 中的 <see cref="HttpFileCollectionBase" /> 文件集合对话并根据 <see cref="PostedFileOption" /> 的配置全部上传到当前站点指定的目录中。
        /// </summary>
        /// <param name="request"><see cref="HttpRequestBase" /> 文件对象的扩展实例。</param>
        /// <param name="options">文件上传的配置选项。</param>
        /// <returns>
        /// 所有文件上传信息集合。
        /// </returns>
        /// <exception cref="NullReferenceException">request</exception>
        /// <exception cref="PostedFileException">
        /// DirectoryPath
        /// or
        /// 创建上传目录出现异常，详情请查看 Inner Exception。
        /// or
        /// 文件上传发生错误，详情请查看 InnerException。
        /// </exception>
        public static IEnumerable<PostedFileInfo> PostedFiles(this HttpRequestBase request, PostedFileOption options=null)
        {
            if (request == null)
            {
                throw new NullReferenceException(nameof(request));
            }

            if (options == null)
            {
                options = new PostedFileOption();
            }

            if (string.IsNullOrWhiteSpace(options.DirectoryPath))
            {
                throw new PostedFileException($"{nameof(options.DirectoryPath)} 的值不允许是空、null 或空白字符串");
            }


            var serverDirectory = request.MapPath(options.DirectoryPath);
            if (!Directory.Exists(serverDirectory))
            {
                try
                {
                    Directory.CreateDirectory(serverDirectory);
                }
                catch (Exception ex)
                {
                    throw new PostedFileException("创建上传目录出现异常，详情请查看 Inner Exception。", ex);
                }
            }

            if (serverDirectory.Last() != '/' || serverDirectory.Last() != '\\')
            {
                serverDirectory =string.Format("{0}/",serverDirectory);
            }

            var httpPostedFiles = request.Files;
            if (httpPostedFiles == null || httpPostedFiles.Count == 0)
            {
                throw new PostedFileException("没有任何要上传的文件");
            }

            var uploadResultList = new HashSet<PostedFileInfo>();

            foreach (string inputName in httpPostedFiles)
            {
                var file = httpPostedFiles[inputName];
                var fileName = file.FileName;
                var fileExtension = Path.GetExtension(fileName).ToLower();
                var fileSize = file.ContentLength;

                if (fileSize <= 0)
                {
                    uploadResultList.Add(new PostedFileInfo { Extension = fileExtension, OriginalFileName = fileName, Size = fileSize, Result = PostedFileResult.NoFileContent });
                    continue;
                }

                if (options.AllowMinFileSize > 0 && file.ContentLength < options.AllowMinFileSize)
                {
                    uploadResultList.Add(new PostedFileInfo { Extension=fileExtension, OriginalFileName= fileName, Size= fileSize , Result= PostedFileResult.InvalidMinFileSize});
                    continue;
                }

                if (options.AllowMaxFileSize > 0 && file.ContentLength > options.AllowMaxFileSize)
                {
                    uploadResultList.Add(new PostedFileInfo { Extension = fileExtension, OriginalFileName = fileName, Size = fileSize, Result = PostedFileResult.InvalidMaxFileSize });
                    continue;
                }


                if (options.AllowExtentions != null && options.AllowExtentions.Length > 0
                    && !options.AllowExtentions.Select(m => m.ToLower().Replace(".","")).Contains(fileExtension.Replace(".",""))
                    )
                {
                    uploadResultList.Add(new PostedFileInfo { Extension = fileExtension, OriginalFileName = fileName, Size = fileSize, Result = PostedFileResult.InvalidFileExtensions });
                    continue;
                }

                var saveFileName = fileName;
                if (options.PostedFileName!=null)
                {
                    saveFileName = string.Concat(options.PostedFileName.GenerateName(), fileExtension);
                }

                try
                {
                    file.SaveAs(string.Concat(serverDirectory, saveFileName));
                    uploadResultList.Add(new PostedFileInfo
                    {
                        Extension = fileExtension,
                        OriginalFileName = fileName,
                        Size = fileSize,
                        Result = PostedFileResult.Success,
                        FileName = saveFileName
                    });
                }
                catch (Exception ex)
                {
                    throw new PostedFileException("文件上传发生错误，详情请查看 InnerException。", ex);
                }
            }
            return uploadResultList;
        }

        /// <summary>
        /// 获取 Request 中的 <see cref="HttpFileCollection" /> 文件集合对象并根据 <see cref="PostedFileOption" /> 的配置全部上传到当前站点指定的目录中。
        /// </summary>
        /// <param name="request"><see cref="HttpRequest" /> 文件对象的扩展实例。</param>
        /// <param name="options">文件上传的配置选项。</param>
        /// <returns>
        /// 所有文件上传信息集合
        /// </returns>
        /// <exception cref="NullReferenceException">request</exception>
        /// <exception cref="PostedFileException">
        /// DirectoryPath
        /// or
        /// 创建上传目录出现异常，详情请查看 Inner Exception。
        /// or
        /// 文件上传发生错误，详情请查看 InnerException。
        /// </exception>
        public static IEnumerable<PostedFileInfo> PostedFiles(this HttpRequest request,PostedFileOption options=null)
        {
            return new HttpRequestWrapper(request).PostedFiles(options);
        }

        /// <summary>
        /// 获取 Request 中的 <see cref="HttpPostedFileBase" /> 文件集合对话并根据 <see cref="PostedFileOption" /> 的配置上传到当前站点指定的目录中。
        /// 该方法适用于只有一个 type="file" 的 input 控件在一个 form 提交时使用。
        /// </summary>
        /// <param name="request"><see cref="HttpRequestBase" /> 文件对象的扩展实例。</param>
        /// <param name="options">文件上传的配置选项。</param>
        /// <returns>
        /// 一个文件上传的信息。
        /// </returns>
        /// <exception cref="System.Web.PostedFileException">若一个 form 中出现多个 type=""file"" 并且带有 name 值的 input 控件时，在 form 提交时将出现该异常。该方法仅限于一个 form 中只能有一个 type=""file"" 并且带有 name 值的 input 控件。多个控件请使用 PostedFiles 方法。</exception>
        /// <exception cref="NullReferenceException">request</exception>
        /// <exception cref="PostedFileException">DirectoryPath
        /// or
        /// 创建上传目录出现异常，详情请查看 Inner Exception。
        /// or
        /// 文件上传发生错误，详情请查看 InnerException。</exception>
        public static PostedFileInfo PostedFile(this HttpRequestBase request,PostedFileOption options = null)
        {
            try
            {
                return request.PostedFiles(options).Single();
            }
            catch (InvalidOperationException ex)
            {
                throw new PostedFileException(@"若一个 form 中出现多个 type=""file"" 并且带有 name 值的 input 控件时，
在 form 提交时将引发该异常。该方法仅限于一个 form 中只能有一个 type=""file"" 并且带有 name 值的 input 控件。
多个控件请调用 PostedFiles 方法。", ex);
            }
        }

        /// <summary>
        /// 获取 Request 中的 <see cref="HttpPostedFile" /> 文件对象并根据 <see cref="PostedFileOption" /> 的配置上传到当前站点指定的目录中。
        /// </summary>
        /// <param name="request"><see cref="HttpRequest" /> 文件对象的扩展实例。</param>
        /// <param name="options">文件上传的配置选项。</param>
        /// <returns>
        /// 一个文件上传的信息。
        /// </returns>
        /// <exception cref="System.Web.PostedFileException">若一个 form 中出现多个 type=""file"" 并且带有 name 值的 input 控件时，在 form 提交时将出现该异常。该方法仅限于一个 form 中只能有一个 type=""file"" 并且带有 name 值的 input 控件。多个控件请使用 PostedFiles 方法。</exception>
        /// <exception cref="NullReferenceException">request</exception>
        /// <exception cref="PostedFileException">DirectoryPath
        /// or
        /// 创建上传目录出现异常，详情请查看 Inner Exception。
        /// or
        /// 文件上传发生错误，详情请查看 InnerException。</exception>
        public static PostedFileInfo PostedFile(this HttpRequest request, PostedFileOption options = null)
        {
            try
            {
                return new HttpRequestWrapper(request).PostedFiles(options).Single();
            }
            catch (InvalidOperationException ex)
            {
                throw new PostedFileException(@"若一个 form 中出现多个 type=""file"" 并且带有 name 值的 input 控件时，
在 form 提交时将引发该异常。该方法仅限于一个 form 中只能有一个 type=""file"" 并且带有 name 值的 input 控件。
多个控件请调用 PostedFiles 方法。", ex);
            }
        }
    }
}
