using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpSample.Services;

internal class FtpService
{
    public SftpClient? ConnectToSftp(string host, string username, string password)
    {
        try
        {
            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();
                return sftp;
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public void DownloadFile(SftpClient sftpClient, string remoteDirectory, string localDirectory, string fileName, string fileExtension)
    {
        try
        {
            var files = sftpClient.ListDirectory(remoteDirectory);

            foreach (var file in files)
            {
                string remoteFileName = file.Name;
                if (remoteFileName == fileName)
                {

                    using (Stream file1 = File.OpenWrite(localDirectory + fileName + fileExtension))
                    {
                        sftpClient.DownloadFile(remoteDirectory + fileName + fileExtension, file1);
                    }
                }
            }
        }
        catch (Exception e)
        {

            throw;
        }

    }
    public void DisconnectFromSftp(SftpClient sftpClient)
    {
        try
        {
            sftpClient.Disconnect();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
