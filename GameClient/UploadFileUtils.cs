using Aliyun.OSS;

namespace GameClient
{
    public class UploadFileUtils
    {
        /// <summary>
        /// 上传文件到阿里云
        /// </summary>
        /// <param name="filePathName">上传文件名称（带扩展名和文件夹）</param>
        /// <param name="fileStream">文件流</param>
        /// <returns></returns>
        public static string UploadFile(string filePathName, Stream fileStream, out Exception error)
        {
            error = null;
            try
            {
                var client = new OssClient("oss-cn-hangzhou.aliyuncs.com", "", "");
                client.PutObject("steam-dd373", filePathName, fileStream);

                #region 获取上传文件地址
                string fileUrl = $"{filePathName}";
                #endregion

                return fileUrl;
            }
            catch (Exception ex)
            {
                error = ex;
                return null;
            }
        }
    }
}
