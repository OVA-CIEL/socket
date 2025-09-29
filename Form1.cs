using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Socket SSockUDP;
        private IPEndPoint localEP;
        private IPEndPoint remoteEP;
        private EndPoint senderEP;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SSockUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress LocalIP =IPAddress.Parse(textBox1.Text);
            int LocalPort = int.Parse(textBox3.Text);
            localEP = new IPEndPoint(LocalIP, LocalPort);

            SSockUDP.Bind(localEP);

            SSockUDP.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SSockUDP.Close();
            SSockUDP = null;
            textBox6.AppendText("Socket Close.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] msg = Encoding.ASCII.GetBytes(textBox5.Text);

            IPAddress destIP = IPAddress.Parse(textBox2.Text);
            int destPort = int.Parse(textBox4.Text);
            remoteEP = new IPEndPoint(destIP, destPort);

            SSockUDP.SendTo(msg, remoteEP);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1024];
            senderEP = new IPEndPoint(IPAddress.Any, 0);
            int len = SSockUDP.ReceiveFrom(buffer,ref senderEP);
            string received = Encoding.ASCII.GetString(buffer, 0, len);
            textBox6.AppendText("Message :" + received);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }
    }
}
