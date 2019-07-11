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

namespace Assignment2
{ //Arashdeep Wander 991495450
    public partial class Form1 : Form
    {
        private SqlConnection con = new SqlConnection();

        private string conString = "Server=DESKTOP-VIEIQBS\\SQLEXPRESS; Database=CSharpClass2;"
               + "User=wandeara; Password=Gabziscool1";
        private SqlCommand cmd;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void RefreshAll()
        {
            con.ConnectionString = conString;
            cmd = con.CreateCommand();

            try
            {
                string query = "Select * from PersonInfo;";

                cmd.CommandText = query;
                con.Open();

                SqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(read);

                dataGridView1.DataSource = dt;
                cmbSelect.DisplayMember = "personName";
                cmbSelect.ValueMember = "personID";
                cmbSelect.DataSource = dt;

                read.Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                string caption = "Error!";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void AddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string grade = txtGrade.Text;
            string comment = txtComment.Text;

            decimal mark;

            if ((name == "") || (course == "") || (grade == "") || (comment == ""))
            {
                MessageBox.Show("Fields can not be Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!decimal.TryParse(grade, out mark))
            {
                MessageBox.Show("Enter Proper Grade", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    con.ConnectionString = conString;
                    cmd = con.CreateCommand();

                    string query = " Insert into PersonInfo values('" + name + "', '" + course + "', '" + grade + "', '" + comment + "');";

                    cmd.CommandText = query;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    string message = ex.Message.ToString();
                    string caption = "Error!";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                    RefreshAll();
                }
            }
        }

        private void DeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            con.ConnectionString = conString;
            cmd = con.CreateCommand();

            int pID = Convert.ToInt32(cmbSelect.SelectedValue);

            try
            {
                string query = "Delete from PersonInfo where personID = " + pID;

                cmd.CommandText = query;
                con.Open();
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                string caption = "Error!";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                RefreshAll();
            }
        }

        private void UpdateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string grade = txtGrade.Text;
            string comment = txtComment.Text;

            decimal mark;

            if ((name == "") || (course == "") || (grade == "") || (comment == ""))
            {
                MessageBox.Show("Fields can not be Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!decimal.TryParse(grade, out mark))
            {
                MessageBox.Show("Enter Proper Grade", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    con.ConnectionString = conString;
                    cmd = con.CreateCommand();

                    int pID = Convert.ToInt32(cmbSelect.SelectedValue);

                    string query = "Update PersonInfo Set personName='" + name + "', Course_Name='" + course + "' , " +
                    "Course_Grade='" + grade + "', Comments='" + comment + "' where personID= " + pID;

                    cmd.CommandText = query;
                    con.Open();
                    cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    string message = ex.Message.ToString();
                    string caption = "Error!";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                    RefreshAll();
                }
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbSelect.SelectedIndex = 0;
            txtName.Text = "";
            txtCourse.Text = "";
            txtGrade.Text = "";
            txtComment.Text = "";
        }

        private void CmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (!cb.Focused)
            {
                return;
            }

            int pID = Convert.ToInt32(cmbSelect.SelectedValue);

            string query1 = "Select personName from PersonInfo where personID=" + pID;
            string query2 = "Select Course_Name from PersonInfo where personID=" + pID;
            string query3 = "Select Course_grade from PersonInfo where personID=" + pID;
            string query4 = "Select Comments from PersonInfo where personID=" + pID;

            if (con.State == ConnectionState.Open)
            {

                cmd.CommandText = query1;
                txtName.Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = query2;
                txtCourse.Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = query3;
                txtGrade.Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = query4;
                txtComment.Text = cmd.ExecuteScalar().ToString();

                cmd.Dispose();
                con.Close();
            }
            else
            {
                con.Open();

                cmd.CommandText = query1;
                txtName.Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = query2;
                txtCourse.Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = query3;
                txtGrade.Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = query4;
                txtComment.Text = cmd.ExecuteScalar().ToString();

                cmd.Dispose();
                con.Close();
            }
        }
    }
}
