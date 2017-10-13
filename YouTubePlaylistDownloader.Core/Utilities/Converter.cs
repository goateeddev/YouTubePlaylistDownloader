using System;
using System.Diagnostics;
using System.IO;

namespace YouTubePlaylistDownloader.Core.Utilities
{
    public static class Converter
    {
        public static bool ConvertMP4ToMP3(string path, string filename)
        {
            string input_file = Path.Combine(path, filename + ".mp4");
            string output_name = Path.Combine(path, filename + ".mp3");
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                cmd.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                cmd.StartInfo.WorkingDirectory = @"";
                cmd.StartInfo.Arguments = "/C ffmpeg -i \"" + input_file + "\" -vn -b:a 320k -c:a libmp3lame \"" + output_name + "\"";
                cmd.Start();
                cmd.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return false;
        }
    }
}
