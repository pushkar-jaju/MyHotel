using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyHotel
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
            populate();
            getCategories();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pushkar\OneDrive\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string Query = "select * from RoomTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RoomsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditRooms()
        {
            if (RnameTb.Text == "" || RTypeCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update RoomTbl set RName=@RN,RType=@RT,RStatus=@RS where RNum = @RKey", Con);
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RT", RTypeCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@RS", StatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteRooms()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Room!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from RoomTbl where Rnum = @RKey", Con);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void InsertRooms()
        {
            if (RnameTb.Text == "" || RTypeCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            } else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RoomTbl(RName,RType,RStatus) values (@RN,@RT,@RS)", Con);
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RT", RTypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RS", "Available");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Added!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            
        }
        private void getCategories()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from TYpeTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeNum", typeof(int));
            dt.Load(rdr);
            RTypeCb.ValueMember = "TypeNum";
            RTypeCb.DataSource = dt;
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertRooms();
        }
        int Key = 0;
        private void RoomsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RnameTb.Text = RoomsDGV.SelectedRows[0].Cells[1].Value.ToString();
            RTypeCb.Text = RoomsDGV.SelectedRows[0].Cells[2].Value.ToString();
            StatusCb.Text = RoomsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if(RnameTb.Text == "")
            {
                Key = 0;
            } else
            {
                Key = Convert.ToInt32(RoomsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditRooms();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteRooms();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            
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
