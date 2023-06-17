using System;
using System.ComponentModel;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Serwis;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.Json";
        private BindingList<ToDoModel> _toDoData;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _toDoData = _fileIOService.ToDoList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
                
            }

            dgList.ItemsSource = _toDoData;

            _toDoData.ListChanged += _toDoData_ListChanged;
        }

        private void _toDoData_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();

                }
            };
        }
    }
}
