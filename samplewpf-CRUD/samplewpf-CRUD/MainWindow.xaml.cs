using System;
using System.IO;
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

namespace samplewpf_CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class item
        {
            public string user { get; set; }
            public string pass { get; set; }            
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        sampleDBEntities db = new sampleDBEntities();
        tlogin tbl = new tlogin();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string msg;

            if (txt_username.Text == "" || txt_password.Password =="")
            {
                msg = "Campos estan vacios";
            }
            else
            {
                tbl.username = txt_username.Text;
                tbl.password = txt_password.Password;
                db.tlogins.Add(tbl);
                db.SaveChanges();
                msg = "Data ha sido guardado.";
            }

            MessageBox.Show(msg.ToString());
            
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            bool flag = db.tlogins.Where(
                    x => x.username == txt_username.Text &&
                    x.password == txt_password.Password
                ).Any();

            string msg = "";

            if (!flag)
            {
                msg = "¡Login Exitoso....!";
            }
            else
            {
                msg = "¡Algo ocurrio mal, Intenta de nuevo....!";
            }

            MessageBox.Show(msg);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            btn_search.IsEnabled = false;
            txt_id.IsEnabled = false;

            var data = from x in db.tlogins select x;
            if (data.ToList().Count>0)
            {
                DGrid.ItemsSource = data.ToList();
            }
            else
            {
                MessageBox.Show("¡No se encontraron datos...!");
            }
            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            btn_search.IsEnabled = true;
            txt_id.IsEnabled = true;
            DGrid.ItemsSource = null;

        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {

            //int id = int.Parse(txt_id.Text);
            //bool flag = db.tlogins.Where(x => x.id == id).Any();            
            //if (flag)            
            //{
                
            //    tbl = db.tlogins.Where(x => x.id== id).First();
            //    txt_username.Text = tbl.username;
            //    txt_password.Password = tbl.password;
            //}
            //else
            //{
            //    MessageBox.Show("¡ID no valida, intenta de nuevo...!");
            //}


            int id = int.Parse(txt_id.Text);            
            var data = from x in db.tlogins where x.id == id  select x;
            if (data.ToList().Count > 0)
            {
                DGrid.ItemsSource = data.ToList();
                txt_id.Text = "";
            }
            else
            {
                MessageBox.Show("¡ID no valida, intenta de nuevo...!");
            }
        }

        private void DGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ////if (DGrid.SelectedItem == null) return;
            
            ////System.Data.DataRowView dv = DGrid.SelectedItem as System.Data.DataRowView;
            ////txt_username.Text = dv.Row[1].ToString();
            ////txt_password.Password = dv.Row[2].ToString();

            //if (DGrid.SelectedItem == null)
            //    return;
            //System.Data.DataTable dt = DGrid.SelectedItem as System.Data.DataTable;
            ////System.Data.DataRow dr1 = dr.Row;

            ////txtName.Text = Convert.ToString(dr1.ItemArray[1]);     
        }

    }
}
