using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using FreeImageAPI;
using System.IO;
using System.Windows.Threading;

namespace TgaToPng
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       public readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private void ChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择图片文件夹";

            if (dialog.ShowDialog() == true)
            {
                FolderTextBox.Text = dialog.FolderName;
            }
        }

        private void OutFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择服务端文件夹";

            if (dialog.ShowDialog() == true)
            {
                OutFolderTextBox.Text = dialog.FolderName;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            

            var files = Directory.GetFiles(FolderTextBox.Text, "*.tga");
            pb.Maximum = files.Length;
            pb.Value = 0;

                foreach (var file in files)
                {
                    var name = Path.GetFileNameWithoutExtension(file);
                    var newpath = @$"{OutFolderTextBox.Text}\{name}.png";
                    Tga2Png(file, newpath);
                    dispatcher.Invoke(() =>
                    {
                        pb.Value++;
                    });
                    
                }


            


        }

        public void Tga2Png(string oldPath,string newPath)
        {
           var fbmp =   FreeImage.LoadEx(oldPath);
            FreeImage.SaveEx(fbmp, newPath,FREE_IMAGE_FORMAT.FIF_PNG);
        }


    }
}