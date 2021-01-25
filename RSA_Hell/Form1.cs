using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace RSA_Hell
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		Form2 form2;

		private void button1_Click(object sender, EventArgs e)
		{
			if ((textBox1.Text.All(char.IsDigit)) & (textBox3.Text.All(char.IsDigit)))
			{
				if ((textBox1.Text.Length > 0) && (textBox3.Text.Length > 0))
				{
					long p = Convert.ToInt64(textBox1.Text);
					long q = Convert.ToInt64(textBox3.Text);

					if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
					{
						string s = "";

						StreamReader sr = new StreamReader("in.txt");

						while (!sr.EndOfStream)
						{
							s += sr.ReadLine();
						}

						sr.Close();

						s = s.ToUpper();

						long n = p * q;
						long m = (p - 1) * (q - 1);

						long d = Calculate_d(m);
						long e_ = Calculate_e(d, m);

						/*
						string dataVal = s + "/" + e_.ToString() + "/" + n.ToString();

						const string ip = "127.0.0.1";
						const int port = 8080;

						var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
						var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

						var mess = Encoding.UTF8.GetBytes(dataVal);

						form2.RunServer(1);

						Thread.Sleep(TimeSpan.FromSeconds(0.5));

						tcpSocket.Connect(tcpEndPoint);
						tcpSocket.Send(mess);

						tcpSocket.Shutdown(SocketShutdown.Both);
						tcpSocket.Close();
						*/

						List<string> result = Form2.ServerRSA.RSA_Endoce(s, e_, n);

						StreamWriter sw = new StreamWriter("out1.txt");
						foreach (string item in result)
							sw.WriteLine(item);
						sw.Close();

						textBox2.Text = d.ToString();
						textBox4.Text = n.ToString();

						Process.Start("out1.txt");
					}
					else { MessageBox.Show("p или q - не простые числа!"); }
				}
				else { MessageBox.Show("Введите p и q!"); }
			}
			else { MessageBox.Show("Введите p и q!"); }
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if ((textBox2.Text.All(char.IsDigit)) & (textBox4.Text.All(char.IsDigit)))
			{
				if ((textBox2.Text.Length > 0) && (textBox4.Text.Length > 0))
				{
					long d = Convert.ToInt64(textBox2.Text);
					long n = Convert.ToInt64(textBox4.Text);

					List<string> inpu = new List<string>();

					StreamReader stream = new StreamReader("out1.txt");

					while (!stream.EndOfStream)
					{
						inpu.Add(stream.ReadLine());
					}
					stream.Close();

					Form2.ServerRSA.input = inpu;

					//++++++++++++++++++++++++++++++++++++

					string result = Form2.ServerRSA.RSA_Dedoce(d, n);

					StreamWriter sw = new StreamWriter("out2.txt");
					sw.WriteLine(result);
					sw.Close();

					Process.Start("out2.txt");
				}
				else { MessageBox.Show("Введите секретный ключ!"); }
			}
			else { MessageBox.Show("Введите секретный ключ!"); }
		}

		bool IsTheNumberSimple(long n)
		{
			if (n < 2)
				return false;

			if (n == 2)
				return true;

			for (long i = 2; i < n; i++)
				if (n % i == 0)
					return false;

			return true;
		}

		long Calculate_d(long m)
		{
			long d = m - 1;

			for (long i = 2; i <= m; i++)
				if ((m % i == 0) && (d % i == 0))
				{
					d--;
					i = 1;
				}

			return d;
		}

		long Calculate_e(long d, long m)
		{
			long e = 10;

			while (true)
			{
				if ((e * d) % m == 1)
					break;
				else
					e++;
			}

			return e;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if ((textBox5.Text.All(char.IsDigit)) & (textBox6.Text.All(char.IsDigit)) & (textBox7.Text.All(char.IsDigit)) & (textBox8.Text.All(char.IsDigit)))
			{
				Diffi_Hellman diffi_HellmanB3 = new Diffi_Hellman(int.Parse(textBox5.Text), int.Parse(textBox6.Text), int.Parse(textBox7.Text), int.Parse(textBox8.Text), 'H');
			}
			else { MessageBox.Show("Введите числа!"); }
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if ((textBox7.Text.All(char.IsDigit)) & (textBox8.Text.All(char.IsDigit)) & (textBox5.Text.All(char.IsDigit)))
			{
				Diffi_Hellman diffi_HellmanB4 = new Diffi_Hellman(int.Parse(textBox7.Text), int.Parse(textBox8.Text), int.Parse(textBox5.Text));
			}
			else { MessageBox.Show("Введите числа!"); }
		}

		internal void TextBoxClearAB() { textBox7.Text = ""; textBox8.Text = ""; }

		private void button5_Click(object sender, EventArgs e)
		{
			if ((textBox8.Text.All(char.IsDigit)) & (textBox7.Text.All(char.IsDigit)) & (textBox5.Text.All(char.IsDigit)))
			{
				Diffi_Hellman diffi_HellmanB5 = new Diffi_Hellman(int.Parse(textBox8.Text), int.Parse(textBox7.Text), int.Parse(textBox5.Text), true);
			}
			else { MessageBox.Show("Введите числа!"); };
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			form2 = new Form2();
			form2.Show();
		}

        private void button6_Click(object sender, EventArgs e)
        {
			const string ip = "127.0.0.1";
			const int port = 8080;

			var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
			var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			var data = Encoding.UTF8.GetBytes(textBox9.Text);

			tcpSocket.Connect(tcpEndPoint);
			tcpSocket.Send(data);

			var answer = new StringBuilder();
			var buff = new byte[256];
			var size = 0;

			do
			{
				size = tcpSocket.Receive(buff);
				answer.Append(Encoding.UTF8.GetString(buff, 0, size));
			}
			while (tcpSocket.Available > 0);

			MessageBox.Show(answer.ToString());

			tcpSocket.Shutdown(SocketShutdown.Both);
			tcpSocket.Close();
		}
    }

    class Diffi_Hellman
    {
		private int a = 0;
		private int b = 0;
		private int q = 0;
		private int p = 0;
		private bool but_controll = false;
		private Form1 form1;

		public Diffi_Hellman(int _q, int _p, int _a, int _b, char _but_controll)
        {
			if (!but_controll)
			{
				this.q = _q;
				this.p = _p;
				this.a = _a;
				this.b = _b;

				this.form1 = new Form1();

				if (_but_controll == 'H') this.but_controll = true;

				MessageBox.Show("A = " + Form2.ServerHelman.RetA(q, a, p) + "\n" + "B = " + Form2.ServerHelman.RetB(q, b, p));

				form1.TextBoxClearAB();
			}
		}

		public Diffi_Hellman(int _a, int _b, int _p)
		{
			this.b = _b;
			this.a = _a;
			this.p = _p;

			MessageBox.Show("Key1 = " + Form2.ServerHelman.RetKeyB(a, b, p));
		}

		public Diffi_Hellman(int _b, int _a, int _p, bool _formal)
		{
			this.b = _b;
			this.a = _a;
			this.p = _p;

			MessageBox.Show("Key2 = " + Form2.ServerHelman.RetKeyA(a, b, p));
		}
	}
}