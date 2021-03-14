using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using WindowsDesktop;

namespace IbAutoScreenshot
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use Task Scheduler to run it periodly

            Capture();

            //MessageBox.Show("Pause");
        }

        static void Capture()
        {
            /*
            try
            {
                var c = VirtualDesktop.Current;
                //MessageBox.Show(VirtualDesktop.Current.Id.ToString());
            }
            catch (System.NotSupportedException)
            {
                MessageBox.Show("NotSupported");
            }
            */

            var infos = new DirectoryInfo("Screenshots").GetFileSystemInfos();
            if (infos.Length >= 10)
            {
                var fileInfo = infos.OrderBy(fi => fi.CreationTime).First();
                fileInfo.Delete();
                //MessageBox.Show(fileInfo.FullName);
            }

            Bitmap bmp = new Bitmap(SystemInformation.VirtualScreen.Width,
                SystemInformation.VirtualScreen.Height,
                PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(SystemInformation.VirtualScreen.X,
                SystemInformation.VirtualScreen.Y,
                0,
                0,
                SystemInformation.VirtualScreen.Size,
                CopyPixelOperation.SourceCopy);
            bmp.Save($"Screenshots\\{DateTime.Now.ToString("yyyy-MM-dd HH..mm")}.png", ImageFormat.Png);
        }
    }
}
