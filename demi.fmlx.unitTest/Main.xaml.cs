using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using demi.fmlx.library.Implement;
using demi.fmlx.library.Interface;

namespace demi.fmlx.unitTest
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private IFileManagerLibrary _fileManagerLibrary = new FileManagerLibrary();

        public Main()
        {
            _fileManagerLibrary.Initialize();
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string strName = txtName.Text;
            string strContent = txtContent.Text;
            int intType = !string.IsNullOrEmpty(txtType.Text) ? Convert.ToInt32(txtType.Text) : 0;

            if (!string.IsNullOrEmpty(strName) && !string.IsNullOrEmpty(strContent) && intType > 0)
            {
                _fileManagerLibrary.Register(strName, strContent, intType);
                MessageLbl.Content = "ADD ITEM COMPLETED";
            }
            else
            {
                MessageLbl.Content = "ADD ITEM FAILED";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string strName = txtName.Text;

            if (!string.IsNullOrEmpty(strName))
            {
                _fileManagerLibrary.Deregister(strName);
                MessageLbl.Content = "DELETE ITEM COMPLETED";
            }
            else
            {
                MessageLbl.Content = "ADD ITEM FAILED";
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {           
            string strFindElement = string.Empty;
            strFindElement = _fileManagerLibrary.Retrieve(txtName.Text);
            Console.WriteLine(strFindElement);
            MessageLbl.Content = strFindElement;
        }

        private void btnGetType_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = _fileManagerLibrary.GetType(txtName.Text);
            Console.WriteLine(result);
            MessageLbl.Content = result;
        }
    }
}
