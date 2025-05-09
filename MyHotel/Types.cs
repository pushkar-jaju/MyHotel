using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyHotel
{
    public partial class Types : Form
    {
        public Types()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pushkar\OneDrive\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string Query = "select * from TypeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TypesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void InsertCategories()
        {
            if (TypeNameTb.Text == "" || CostTb.Text == "" )
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TYpeTbl(TypeName,TypeCost) values (@TN,@TC)", Con);
                    cmd.Parameters.AddWithValue("@TN", TypeNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", CostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Inserted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void EditCategories()
        {
            if (TypeNameTb.Text == "" || CostTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update TYpeTbl set TypeName=@TN,TypeCost=@TC where TypeNum = @TKey", Con);
                    cmd.Parameters.AddWithValue("@TN", TypeNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCategories();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }
        int Key = 0;
        private void TypesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TypeNameTb.Text = TypesDGV.SelectedRows[0].Cells[1].Value.ToString();
            CostTb.Text = TypesDGV.SelectedRows[0].Cells[2].Value.ToString();
           
            if (TypeNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TypesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditCategories();
        }
        private void DeleteCategories()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Category!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from TYpeTbl where Typenum = @TKey", Con);
                    cmd.Parameters.AddWithValue("@TKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteCategories();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show(); 
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
