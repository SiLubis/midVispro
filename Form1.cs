using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace mid.vispro
{
    public partial class Form1 : Form
    {
        private MySqlConnection koneksi;
        private string alamat;

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        public Form1()
        {
            alamat = "server=localhost; database=db_mahasiswa; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
            TambahKontrol(); 
        }

        private void TambahKontrol()
        {
            txtUsername = new TextBox { Location = new System.Drawing.Point(100, 50), Width = 200 };
            txtPassword = new TextBox { Location = new System.Drawing.Point(100, 100), Width = 200, PasswordChar = '*' };
            btnLogin = new Button { Location = new System.Drawing.Point(100, 150), Text = "Login" };

            btnLogin.Click += label1_Click;

            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            CekLogin(txtUsername.Text, txtPassword.Text);
        }

        private void CekLogin(string username, string password)
        {
            try
            {
                string query = "SELECT password FROM tbl_pengguna WHERE username = @username";
                using (MySqlCommand perintah = new MySqlCommand(query, koneksi))
                {
                    perintah.Parameters.AddWithValue("@username", username);
                    koneksi.Open();

                    object result = perintah.ExecuteScalar();
                    koneksi.Close();

                    if (result != null)
                    {
                        if (result.ToString() == password)
                        {
                            FrmMain frmMain = new FrmMain();
                            frmMain.Show();
                        }
                        else
                        {
                            MessageBox.Show("Anda salah input password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username tidak ditemukan");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
        }
    }
}
