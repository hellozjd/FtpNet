/*****************************************************
 * zhaojd    ftp上传工具     2020年9月1日
 ****************************************************/
using System;

namespace FtpNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"必传参数为：ip(示例：192.168.xx.xx)、port(示例：21)、userid(账号，示例：xxx)、password(密码，示例：xxx)、localPath(本机要上传的目录的父目录，示例：C:\Users\Administrator\Desktop\)、fileName(要上传的目录名或者文件名，示例：xxx)");
            Console.WriteLine("选传参数为:savePath(服务端存储路径，多层级目录用/区分，示例：zjd1/zjd2/zjd3)");
            if (args.Length < 6)
            {
                Console.WriteLine("参数缺失，请检查！");
                return;
            }
            string ip = args[0];
            string port = args[1];
            var user = args[2];
            var password = args[3];
            //本机要上传的目录的父目录
            string localPath = args[4];
            //要上传的目录名或者文件名
            string fileName = args[5];
            string savePath = args.Length == 7 ? args[6] : "";  //多层级目录用/区分
            //FTP地址
            string ftpPath = $"Ftp://{ip}:{port}/";
            if (!string.IsNullOrEmpty(savePath))
            {
                ftpPath += savePath + "/";
                //先对服务端文件存储目录预处理
                var sarr = savePath.Split('/');
                var temppath = @"Ftp://" + ip + ":" + port.ToString() + "/";
                foreach (var item in sarr)
                {
                    if (!FTPHelper.CheckDirectoryExist(temppath, item, user, password))
                    {
                        FTPHelper.MakeDir(temppath, item, user, password);
                    }
                    temppath += item + "/";
                }
            }
            FTPHelper.UploadDirectory(localPath, ftpPath, fileName, user, password);
            Console.WriteLine("上传成功！");
        }
    }
}
