// Libraries used in this class:
using System;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace YouTubePlaylistDownloader
{
    public class DownloadClass1
    {
        private byte[] downloadedDataStream;
        //private Video_Downloader_DLL.VideoDownloader downloader = new Video_Downloader_DLL.VideoDownloader();

        //TextBox OutLink, LinkText;
        SaveFileDialog saveDiag1 = new SaveFileDialog();

        private void GetFileName(string title, string url)
        {
            downloadData(url);

            //Get the last part of the url, ie the file name
            if (downloadedDataStream != null && downloadedDataStream.Length != 0)
            {
                string ytdata = title;
                string urlName = url;
                if (urlName.EndsWith("/"))
                    urlName = urlName.Substring(0, urlName.Length - 1); //Chop off the last '/'

                urlName = urlName.Substring(urlName.LastIndexOf('/') + 1);

                saveDiag1.FileName = ytdata + ".flv";
            }
        }

        private void downloadData(string url)
        {
            downloadedDataStream = new byte[0];
            try
            {
                //Get a data stream from the url
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();
                //Download in chuncks
                byte[] buffer = new byte[1024];
                //Get Total Size
                int dataLength = (int)response.ContentLength;

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedDataStream = memStream.ToArray();

                //Clean up
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                //May not be connected to the internet
                //Or the URL might not exist
                MessageBox.Show("There was an error accessing the URL.");
            }
            WriteBytesToFile();
        }

        private void WriteBytesToFile()
        {
            if (downloadedDataStream != null && downloadedDataStream.Length != 0)
            {
                if (saveDiag1.ShowDialog() == DialogResult.OK)
                {
                    //Write the bytes to a file
                    FileStream newFile = new FileStream(saveDiag1.FileName, FileMode.Create);
                    newFile.Write(downloadedDataStream, 0, downloadedDataStream.Length);
                    newFile.Close();

                    MessageBox.Show("Saved the file Successfully");
                }
            }
            else MessageBox.Show("No File was Downloaded !");
        }
    }
}
