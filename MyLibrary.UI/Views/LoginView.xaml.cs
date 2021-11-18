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
using MyLibrary.UI.DAL;
using Common.Lib.Authentication;

namespace MyLibrary.UI.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 
    public MainWindow  Parent { get; set; } 
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            var result = Login(TbEmail.Text, PbPassword.Password);
            if (result)
            {
                var main = new MainWindow();


            }
        }
        public bool Login (string email, string password)
        { 
            using (var repo = new LibrarianRepository())
            {
                var admin = repo
                    .GetAll()  
                    .FirstOrDefault ()
            }
        
        
        }
    }
}
