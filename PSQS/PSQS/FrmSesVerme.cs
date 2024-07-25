using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PSQS
{
	public partial class DQSS : Form
	{
		public DQSS()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			TxtA.KeyPress += new KeyPressEventHandler(SadeceSayi);
			TxtB.KeyPress += new KeyPressEventHandler(SadeceSayi);
			TxtC.KeyPress += new KeyPressEventHandler(SadeceSayi);
			TxtD.KeyPress += new KeyPressEventHandler(SadeceSayi);
			TxtE.KeyPress += new KeyPressEventHandler(SadeceSayi);
		}

		SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1HLMAF8;Initial Catalog=DB_Secki;Integrated Security=True;TrustServerCertificate=True");

		private void BtnSesver_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(TxtRayonAd.Text) ||
					string.IsNullOrWhiteSpace(TxtA.Text) ||
					string.IsNullOrWhiteSpace(TxtB.Text) ||
					string.IsNullOrWhiteSpace(TxtC.Text) ||
					string.IsNullOrWhiteSpace(TxtD.Text) ||
					string.IsNullOrWhiteSpace(TxtE.Text))
				{
					MessageBox.Show("Bütün xanalari doldurun. Aşağıdaki xanalar eksik olabilir:\n" +
									$"{(string.IsNullOrWhiteSpace(TxtRayonAd.Text) ? "- Rayon Adı\n" : "")}" +
									$"{(string.IsNullOrWhiteSpace(TxtA.Text) ? "- A Partiyası\n" : "")}" +
									$"{(string.IsNullOrWhiteSpace(TxtB.Text) ? "- B Partiyası\n" : "")}" +
									$"{(string.IsNullOrWhiteSpace(TxtC.Text) ? "- C Partiyası\n" : "")}" +
									$"{(string.IsNullOrWhiteSpace(TxtD.Text) ? "- D Partiyası\n" : "")}" +
									$"{(string.IsNullOrWhiteSpace(TxtE.Text) ? "- E Partiyası\n" : "")}");
					return;
				}

				baglanti.Open();

				SqlCommand kontrolKomutu = new SqlCommand("SELECT COUNT(*) FROM TBLRayon WHERE RayonAd = @P1", baglanti);
				kontrolKomutu.Parameters.AddWithValue("@P1", TxtRayonAd.Text);
				int mevcutKayit = (int)kontrolKomutu.ExecuteScalar();

				if (mevcutKayit > 0)
				{
					MessageBox.Show("Bu rayon adı artıq mövcuddur, başqa ad daxil edin.", "Əməliyyat Uğursuz", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					SqlCommand komut = new SqlCommand("INSERT INTO TBLRayon (RayonAd, Apartiya, Bpartiya, Cpartiya, Dpartiya, Epartiya) VALUES (@P1, @P2, @P3, @P4, @P5, @P6)", baglanti);
					komut.Parameters.AddWithValue("@P1", TxtRayonAd.Text);
					komut.Parameters.AddWithValue("@P2", TxtA.Text);
					komut.Parameters.AddWithValue("@P3", TxtB.Text);
					komut.Parameters.AddWithValue("@P4", TxtC.Text);
					komut.Parameters.AddWithValue("@P5", TxtD.Text);
					komut.Parameters.AddWithValue("@P6", TxtE.Text);

					komut.ExecuteNonQuery();
					MessageBox.Show("Ses verme müvəffəqiyyətlə daxil edildi!", "Əməliyyat Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				baglanti.Close();
			}
		}

		private void Btnqrafikler_Click(object sender, EventArgs e)
		{
			FrmQrafikler frmQrafikler = new FrmQrafikler();
			frmQrafikler.Show();
		}

		private void SadeceSayi(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true; 
			}
		}
	}
}
