using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;




namespace WindowsPhoneUI
{
    public partial class UserDetails : PhoneApplicationPage
    {
        private string languageFile = "languagefile.txt";
        private string nameFile = "namefile.txt";

        public UserDetails()
        {
            InitializeComponent();
        }
        private async void bt_save_Click(object sender, RoutedEventArgs e)
        {
            if (languageBox.Text != "")
            {
                await WriteFile(languageFile, languageBox.Text);
                //await new MessageDialog("Save Successfully!").ShowAsync();
                MessageBox.Show("Save Successfully!", "Save", MessageBoxButton.OK);
                languageBox.Text = "";
            }
            else
            {
                //await new MessageDialog("Cannot be null").ShowAsync();
                MessageBox.Show("Cannot be null", "Error Dialog", MessageBoxButton.OK);
            }
        }


        private async void bt_read_Click(object sender, RoutedEventArgs e)
        {
            //read the content of the file
            string content = await ReadFile(languageFile);
            //await new MessageDialog(content).ShowAsync();
            MessageBox.Show(content, "Read", MessageBoxButton.OK);
        }
        //read the file in the root
        //
        public async Task<string> ReadFile(string fileName)
        {
            //the content that we read is text
            string text;
            try
            {
                //get the root folder of the app
                IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                //get the content based on the file name
                IStorageFile storageFile = await applicationFolder.GetFileAsync(fileName);
                //open the file and get the IO stream
                IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
                //using StreamReader to read the file 
                //convert IRandomAccessStream object to Stream object
                using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
                {
                    text = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                text = "There is no stored information." + e.Message;
            }
            return text;
        }
        public async Task DeleteFile(string filename)
        {
            string text;
            try
            {
                //root
                IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                //file
                IStorageFile storageFile = await applicationFolder.GetFileAsync(filename);
                //delete
                await storageFile.DeleteAsync();
                text = "Delete Successfully";
            }
            catch (Exception exce)
            {
                text = "There is no information to delete." + exce.Message;
            }
            //await new MessageDialog(text).ShowAsync();
            MessageBox.Show(text, "Error Dialog", MessageBoxButton.OK);
        }

        //write into the file in the root folder
        public async Task WriteFile(string fileName, string content)
        {
            IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
            IStorageFile storageFile = await applicationFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            //using the FileIO
            await FileIO.WriteTextAsync(storageFile, content);
        }

        private async void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            //string x = await DeleteFile(languageFile);
            await DeleteFile(languageFile);


        }

        //
        //Name Section
        //

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private async void bt_save_name_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != "")
            {
                await WriteFile(nameFile, NameBox.Text);
                //await new MessageDialog("Save Successfully!").ShowAsync();
                MessageBox.Show("Save Successfully!", "Save", MessageBoxButton.OK);

                NameBox.Text = "";
            }
            else
            {
                //await new MessageDialog("null").ShowAsync();
                MessageBox.Show("Cannot be null!", "Save", MessageBoxButton.OK);
            }
        }

        private async void bt_read_Name_Click(object sender, RoutedEventArgs e)
        {
            //read the content of the file
            string content = await ReadFile(nameFile);
            //await new MessageDialog(content).ShowAsync();
            MessageBox.Show(content, "Read", MessageBoxButton.OK);
        }



        private async void bt_delete_Name_Click(object sender, RoutedEventArgs e)
        {
            await DeleteFile(nameFile);
        }

    }


}
