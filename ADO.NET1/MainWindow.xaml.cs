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
using Microsoft.Data.SqlClient;

namespace ADO.NET1
{
    
    public partial class MainWindow : Window
    {
        string catname = "";
        public MainWindow()
        {
            InitializeComponent();
            SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-46JAQGOF\\SQLEXPRESS;" +
                "Initial Catalog=Library;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False");

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT C.[Name] FROM Categories AS C", sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        catcmb.Items.Add(reader[i]);
                    }
                }
            }        
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {
            catname = catcmb.Text;
            SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-46JAQGOF\\SQLEXPRESS;" +
                "Initial Catalog=Library;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False");
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"SELECT B.[Name] FROM Books AS B WHERE B.Id_Category = ANY (SELECT C.Id FROM Categories AS C WHERE C.[Name]='{catname}')", sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        bookcmb.Items.Add(reader[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-46JAQGOF\\SQLEXPRESS;" +
                "Initial Catalog=Library;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False"); 
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(txtbox.Text, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();                
            }
        }
    }
}
