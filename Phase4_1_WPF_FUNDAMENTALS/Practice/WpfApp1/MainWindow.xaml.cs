using System.ComponentModel;
using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
//using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;
using System.Drawing;
using System.Reflection.Metadata;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Automation;
using System.Windows.Input;
using System.Windows.Forms.Design;





namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        private bool isDarkTheme = false;

        private const string notesPath = "Notes";
        private Forms.NotifyIcon trayIcon;
        private string currentFilePath = "";



        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
            ThemeToggle.IsChecked = isDarkTheme;
        }

        private void InitializeTrayIcon()
        {
            trayIcon = new Forms.NotifyIcon();

            trayIcon.Icon = new Icon("icons8-ico-64.ico"); // make sure file exists
            trayIcon.Visible = true;
            trayIcon.Text = "Quick Notes - Click to open";
            trayIcon.MouseClick += trayIcon_MouseClick;
            trayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
            LoadNotesList();


            System.Windows.Forms.ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();

            menu.Items.Add("View Data", null, (s, e) => ShowWindow());
            menu.Items.Add("Minimize", null, (s, e) => MinimizeApplication());
            menu.Items.Add("Save Notes", null, (s, e) => SaveCurrentNotes());
            menu.Items.Add("New Quick Notes", null, (s, e) => SaveQuickNots());
            //menu.Items.Add("View Favorites", null);
            menu.Items.Add("Search Notes", null, (s, e) => SearchNotes());
            menu.Items.Add("Settings", null, (s, e) => Settings(null,null));
            menu.Items.Add("Exit", null, (s, e) => ExitApplication());
            menu.Items.Add("Delete", null, (s, e) => DeleteNotes());
            menu.Items.Add("Edit Note", null, (s, e) => EditSelectedNote());

            trayIcon.ContextMenuStrip = menu;

        }


        private void TrayIcon(object sender, EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.Hide();
            }
            else
            {
                ShowWindow();
            }
        }

        private void ShowWindow()
        {

            var notes = LoadNotes();

            NotesListView.ItemsSource = notes;

            NotesListView.Visibility = Visibility.Visible;

            NotesTextBox.Visibility = Visibility.Collapsed;
            NotesListBox.Visibility = Visibility.Collapsed;
            TitleBox.Visibility = Visibility.Collapsed;
            TitleTextBox.Visibility = Visibility.Collapsed;


            this.Show();

        }

        private void ExitApplication()
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveQuickNots()
        {




            NotesListView.Visibility = Visibility.Collapsed;

            NotesTextBox.Visibility = Visibility.Visible;
            NotesListBox.Visibility = Visibility.Visible;
            TitleBox.Visibility = Visibility.Visible;
            TitleTextBox.Visibility = Visibility.Visible;
        }



        private void SaveCurrentNotes()
        {


            string title = TitleTextBox.Text;
            string content = NotesTextBox.Text;

            if (string.IsNullOrWhiteSpace(title))
            {
                System.Windows.MessageBox.Show("Please enter a title");
                return;
            }

            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notes");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string timeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


            string filePath = System.IO.Path.Combine(folderPath, title + ".txt");

            string noteWithTimeStamp = $" {content}";


            File.WriteAllText(filePath, noteWithTimeStamp);
            System.Windows.MessageBox.Show("Note saved successfully");


            ShowNotification("Successful", "Note saved successfully", ToolTipIcon.Info);



            if (!NotesListBox.Items.Contains(title))
            {
                LoadNotesList();
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            On_Click_Close(e);
            On_Click_Save(e);
        }

        private void On_Click_Close(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }


        private void On_Click_Save(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveCurrentNotes();
            }
        }


        private void MinimizeApplication()
        {
            this.WindowState = WindowState.Minimized;
        }

        private void trayIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Hide();
                }
                else
                {
                    ShowWindow();
                }
            }
        }

        private void SearchNotes()
        {
            string searchText = Interaction.InputBox("Enter note title to search", "Search Notes", "");


            if (string.IsNullOrWhiteSpace(searchText))
                return;


            searchText = searchText.ToLower();

            string folderPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Notes"
            );

            if (!Directory.Exists(folderPath))
                return;

            var files = Directory.GetFiles(folderPath, "*.txt");

            NotesListBox.Items.Clear();

            foreach (var file in files)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file);

                //  Partial Search Logic
                if (fileName.ToLower().Contains(searchText))
                {
                    NotesListBox.Items.Add(fileName);
                }
            }
        }

        private void ShowNotification(string title, string message, ToolTipIcon icon)
        {
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = message;
            trayIcon.BalloonTipIcon = icon;

            trayIcon.ShowBalloonTip(3000);
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void LoadNotesList()
        {
            string folderPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Notes"
            );

            if (!Directory.Exists(folderPath))
            {
                return;
            }

            NotesListBox.Items.Clear();

            var files = Directory.GetFiles(folderPath, "*.txt");


            string favoritesPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "favorites.txt"
            );

            List<string> favorites = new List<string>();

            if (File.Exists(favoritesPath))
            {
                favorites = File.ReadAllLines(favoritesPath).ToList();
            }


            foreach (var file in files)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file);

                if (favorites.Contains(fileName))
                {
                    NotesListBox.Items.Add("⭐ " + fileName);
                }
                else
                {
                    NotesListBox.Items.Add(fileName);
                }
            }
        }// character eclipse


        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (NotesListBox.SelectedItem == null)
                return;


            string selectedNote = NotesListBox.SelectedItem as string;

            string folderPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Notes"
            );

            string filePath = System.IO.Path.Combine(folderPath, selectedNote + ".txt");

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);


                NotesTextBox.Clear();

                NotesTextBox.Text = content;

                currentFilePath = filePath;
            }
        }

        private void DeleteNotes()
        {

            if (NotesListBox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Please select a note to delete");
                return;
            }


            string selectedNote = NotesListBox.SelectedItem.ToString();


            string folderPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Notes"
            );

            string filePath = System.IO.Path.Combine(folderPath, selectedNote + ".txt");

            var result = System.Windows.MessageBox.Show(
                $"Are you sure you want to delete '{selectedNote}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath); // ⭐ actual delete

                    System.Windows.MessageBox.Show("Note deleted successfully");

                    LoadNotesList();

                    TitleTextBox.Clear();
                    NotesTextBox.Clear();
                }
                else
                {
                    System.Windows.MessageBox.Show("File not found");
                }
            }
        }

        private void TitleTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //TitleTextBox.Clear();
                NotesTextBox.Clear();
            }
        }

        private void EditSelectedNote()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                System.Windows.MessageBox.Show("Please select a note to edit");
                return;
            }

            string updatedContent = NotesTextBox.Text;

            File.WriteAllText(currentFilePath, updatedContent);

            System.Windows.MessageBox.Show("Notes edited successfully");

            LoadNotesList();
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Please select a note");
                return;
            }

            string selectedNote = NotesListBox.SelectedItem.ToString().Replace("*", "").Trim();

            string favoritesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favorites.txt");

            List<string> favorites = new List<string>();

            if (File.Exists(favoritesPath))
            {
                favorites = File.ReadAllLines(favoritesPath).ToList();
            }

            if (favorites.Contains(selectedNote))
            {
                System.Windows.MessageBox.Show("Already in favorites");
                return;
            }

            favorites.Add(selectedNote);

            File.WriteAllLines(favoritesPath, favorites);

            System.Windows.MessageBox.Show("Added to Favorites");
            //System.Windows.MessageBox.Show(NotesListBox.SelectedItem.GetType().ToString()); 

            LoadNotesList();            
        }


        //private void RemovefromFavorites(object sender, RoutedEventArgs e)
        //{
        //    if (NotesListBox.SelectedItem == null)
        //        return;

        //    string selectedNote = NotesListBox.SelectedItem as string;

        //    if (string.IsNullOrEmpty(selectedNote))
        //        return;

        //    string favFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favorites");

        //    string cleanFile = selectedNote.Replace("*", "");

        //    string filePath = System.IO.Path.Combine(favFolder, cleanFile);

        //    if(File.Exists(filePath))
        //    {

        //        File.Delete(filePath);
        //        System.Windows.MessageBox.Show("Removed from Favorites");

        //    }
        //    else
        //    {
        //        System.Windows.MessageBox.Show("File not found in Favorites");
        //    }
        //}



        //private void RemovefromFavorites(object sender, RoutedEventArgs e)
        //{
        //    if (NotesListBox.SelectedItem == null)
        //    {
        //        System.Windows.MessageBox.Show("Please select a note");
        //        return;
        //    }




        //    string selectedNote = NotesListBox.SelectedItem.ToString();

        //    NotesListBox.Items.Remove("⭐ " + fileName);

        //    // FIX: proper cleaning (same as Add)
        //    string cleanName = selectedNote.Replace("*", "6666666666").Trim().ToString();

        //    string favoritesPath = System.IO.Path.Combine(
        //        AppDomain.CurrentDomain.BaseDirectory,
        //        "favorites.txt"
        //    );

        //    if (File.Exists(favoritesPath))
        //    {
        //        var favorites = File.ReadAllLines(favoritesPath).ToList();

        //        if (favorites.Contains(cleanName))
        //        {
        //            favorites.Remove(cleanName);
        //            File.WriteAllLines(favoritesPath, favorites);

        //            System.Windows.MessageBox.Show("Removed from Favorites");
        //        }
        //        else
        //        {
        //            System.Windows.MessageBox.Show("Note not found in favorites");
        //        }
        //    }

        //    LoadNotesList();
        //}


        private void RemovefromFavorites(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem == null)
                return;

            string selectedNote = NotesListBox.SelectedItem.ToString();




            // Remove star
            string cleanName = selectedNote
            .Replace("*", "")
            .Replace("⭐", "")
            .Replace("★", "")
            .Trim();ToString();
           // NotesListView..Replace("*", " ").Trim();

            // Add extension back
            string fileName = cleanName + ".txt";

            string favFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favorites");
            string filePath = System.IO.Path.Combine(favFolder, fileName);

            string favoritesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favorites.txt");

            NotesListBox.Items.Remove("⭐ " + fileName);

            // Delete from favorites folder
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove from favorites.txt
            if (File.Exists(favoritesPath))
            {
                var favorites = File.ReadAllLines(favoritesPath).ToList();
                favorites.Remove(cleanName);
                File.WriteAllLines(favoritesPath, favorites);
            }

            System.Windows.MessageBox.Show("Removed from Favorites");

            LoadNotesList();
        }











        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTheme("DarkTheme.xaml");
            isDarkTheme = true;
        }

        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyTheme("LightTheme.xaml");
            isDarkTheme = false;
        }


        private void ApplyTheme(string themeFile)
        {
            var dict = new ResourceDictionary();
            dict.Source = new Uri(themeFile, UriKind.Relative);

            var dictionaries = System.Windows.Application.Current.Resources.MergedDictionaries;

            if (dictionaries.Count > 0)
            {
                dictionaries.RemoveAt(0);
            }

            dictionaries.Insert(0, dict);
        }


        public string GetPreview(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return text.Length <= maxLength
                ? text
                : text.Substring(0, maxLength) + "...";
        }

        public List<NotesData> LoadNotes()
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notes");
            var notesList = new List<NotesData>();
            var files = Directory.GetFiles(folderPath, "*.txt");

            foreach (var file in files)
            {
                string content = File.ReadAllText(file);
                notesList.Add(new NotesData
                {
                    FileName = System.IO.Path.GetFileName(file),
                    Preview = GetPreview(content, 20),
                    Date = File.GetLastWriteTime(file).ToString("dd MM yyyy hh:mm:ss")
                }
                );
            }
            return notesList;
        }


        //public List<NotesData> LoadFavorites()
        //{
        //    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favorites");


        //    var favoritesList = new List<NotesData>();

        //    if (!Directory.Exists(folderPath))
        //        return favoritesList;

        //    var files = Directory.GetFiles(folderPath, "*.txt");

        //    foreach (var file in files)
        //    {
        //        string content = File.ReadAllText(file);

        //        favoritesList.Add(new NotesData
        //        {
        //            FileName = System.IO.Path.GetFileName(file),
        //            Preview = GetPreview(content, 20),
        //            Date = File.GetLastWriteTime(file).ToString("dd MMM yyyy hh:mm tt")
        //        });
        //    }

        //    return favoritesList;


        private void Settings(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var settingsWindow = new Settings();
                settingsWindow.ShowDialog();
            });
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                PlaceholderText.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Collapsed;
            }
        }

        //private void SaveButton(object sender, RoutedEventArgs e)
        //{
        //    SaveCurrentNotes;
        //}

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                SaveCurrentNotes();
            }
        }




    }
}





