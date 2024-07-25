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
using System.Windows.Forms.DataVisualization.Charting;

namespace PSQS
{
	public partial class FrmQrafikler : Form
	{
		public FrmQrafikler()
		{
			InitializeComponent();
		}

		private void label7_Click(object sender, EventArgs e)
		{

		}

		SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1HLMAF8;Initial Catalog=DB_Secki;Integrated Security=True;TrustServerCertificate=True");


		private void FrmQrafikler_Load(object sender, EventArgs e)
		{
			//Rayon adlarini Comboboxa cekme
			baglanti.Open();
			SqlCommand komut = new SqlCommand("Select RayonAd from TBLRayon", baglanti);
			SqlDataReader reader = komut.ExecuteReader();
			while (reader.Read())
			{
				comboBox1.Items.Add(reader[0]);
			}
			baglanti.Close();

			chart1.GetToolTipText += new EventHandler<ToolTipEventArgs>(chart1_GetToolTipText);


			baglanti.Open();
			SqlCommand sqlCommand = new SqlCommand("Select SUM(Apartiya),SUM(Bpartiya),SUM(Cpartiya),SUM(Dpartiya),SUM(Epartiya) FROM TBLRayon",baglanti);

			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while(sqlDataReader.Read())
			{
				chart1.Series["Partiyalar"].Points.AddXY("A Partiya", sqlDataReader[0]);
				chart1.Series["Partiyalar"].Points.AddXY("B Partiya", sqlDataReader[1]);
				chart1.Series["Partiyalar"].Points.AddXY("C Partiya", sqlDataReader[2]);
				chart1.Series["Partiyalar"].Points.AddXY("D Partiya", sqlDataReader[3]);
				chart1.Series["Partiyalar"].Points.AddXY("E Partiya", sqlDataReader[4]);
			}

			baglanti.Close();
		}

		private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
		{
			if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
			{
				var point = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
				e.Text = string.Format("{0}: {1}", point.AxisLabel, point.YValues[0]);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			baglanti.Open();
			SqlCommand sqlCommand = new SqlCommand("Select * From TBLRayon where RayonAd = @P1", baglanti);
			sqlCommand.Parameters.AddWithValue("@P1",comboBox1.Text);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				progressBar1.Value = int.Parse(sqlDataReader[2].ToString());
				progressBar2.Value = int.Parse(sqlDataReader[3].ToString());
				progressBar3.Value = int.Parse(sqlDataReader[4].ToString());
				progressBar4.Value = int.Parse(sqlDataReader[5].ToString());
				progressBar5.Value = int.Parse(sqlDataReader[6].ToString());

				LblA.Text = sqlDataReader[2].ToString();
				LblB.Text = sqlDataReader[3].ToString();
				LblC.Text = sqlDataReader[4].ToString();
				LblD.Text = sqlDataReader[5].ToString();
				LblE.Text = sqlDataReader[6].ToString();
			}

			baglanti.Close();
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{

		}
	}
}
