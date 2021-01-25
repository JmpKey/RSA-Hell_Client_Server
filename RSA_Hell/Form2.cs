using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;

namespace RSA_Hell
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		/*
		internal void RunRSA(int _param)
		{
			const string ip = "127.0.0.1";
			const int port = 8080;

			var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
			var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			tcpSocket.Bind(tcpEndPoint);
			tcpSocket.Listen(3);

			var data = new StringBuilder();
			var listener = tcpSocket.Accept();
			var buff = new byte[256];
			var size = 0;
			
			do
			{
				size = listener.Receive(buff);
				data.Append(Encoding.UTF8.GetString(buff, 0, size));
			}
			while (listener.Available > 0);

			listener.Shutdown(SocketShutdown.Both);
			listener.Close();
			
			if (_param == 1)
			{
				string retString = data.ToString();
				string[] reData = retString.Split(new char[] { '/' });

				Server.s = reData[0];
				Server.e = int.Parse(reData[1]);
				Server.n = int.Parse(reData[2]);
			}
		}
		*/

		/*
		internal void RunHelman()
		{

		}
		*/

		internal struct ServerRSA
		{
			private static char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ', 'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
			/*
			internal static string s;
			internal static int e;
			internal static int n;
			*/
			internal static List<string> input;

			internal static List<string> RSA_Endoce(string s, long e, long n)
			{
				List<string> result = new List<string>();

				BigInteger bi;

				for (int i = 0; i < s.Length; i++)
				{
					int index = Array.IndexOf(characters, s[i]);

					bi = new BigInteger(index);
					bi = BigInteger.Pow(bi, (int)e);

					BigInteger n_ = new BigInteger((int)n);

					bi = bi % n_;

					result.Add(bi.ToString());
				}

				return result;
			}

			internal static string RSA_Dedoce(long d, long n)
			{
				string result = "";

				BigInteger bi;

				foreach (string item in input)
				{
					bi = new BigInteger(Convert.ToDouble(item));
					bi = BigInteger.Pow(bi, (int)d);

					BigInteger n_ = new BigInteger((int)n);

					bi = bi % n_;

					int index = Convert.ToInt32(bi.ToString());

					result += characters[index].ToString();
				}

				return result;
			}
		}

		internal struct ServerHelman
		{
			internal static int RetA(int _q, int _a, int _p)
			{
				int j = (int)Math.Pow(_q, _a) % _p;
				WriteInfo("A = " + _a.ToString() + " Q = " + _q.ToString() + " P = " + _p.ToString());
				return j;
			}

			internal static int RetB(int _q, int _b, int _p)
			{
				int j = (int)Math.Pow(_q, _b) % _p;
				WriteInfo("Q = " + _q.ToString() + " B = " + _b.ToString() + " P = " + _p.ToString());
				return j;
			}

			internal static int RetKeyA(int _a, int _b, int _p)
			{
				int tmp = (int)(Math.Pow(_a, _b) + 1) % _p;
				WriteInfo("A = " + _a.ToString() + " B = " + _b.ToString() + " P = " + _p.ToString());
				return tmp;
			}

			internal static int RetKeyB(int _a, int _b, int _p)
			{
				int tmp = (int)Math.Pow(_b, _a) % _p;
				WriteInfo("A = " + _a.ToString() + " B = " + _b.ToString() + " P = " + _p.ToString());
				return tmp;
			}
		}

		static internal void WriteInfo(string _data)
        {
			listBox1.Text = "";
			listBox1.Items.Add(_data);
        }
	}
}