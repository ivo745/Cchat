using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using CefSharp;
using CefSharp.WinForms;

namespace Cchat
{
    public partial class Form1 : Form
    {
        private const string MSG_DISCONNECT = "Disconnected from server.";
        private const string MSG_CONNECT = "Connected to server";
        private const string MSG_ERROR_SEND_DATA = "Send failed!";
        private const string MSG_PREFIX_SENDER = "me: ";
        private const string MSG_PREFIX_RECEIVER = "sender: ";
        //private static string MSG_IMAGE_DECODE_TIME = "Decoded image in: {0} ms.";
        //private static string MSG_ERROR_IMG_SIZE = "Image is too large.";
        //private static int MAX_IMG_SIZE = 1000000;

        private TcpClient client;
        private TcpListener server;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private BinaryReader binaryReader;
        private BinaryWriter binaryWriter;
        private static string text_to_send;
        private static Image image_to_send;

        internal static class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, UIntPtr wParam, IntPtr lParam);
        }

        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;

        private ChromiumWebBrowser browser;
        private CefSettings cefSettings;

        // Create browser instance
        public void InitBrowser()
        {
            cefSettings = new CefSettings();
            Cef.Initialize(cefSettings);
            browser = new ChromiumWebBrowser(@"C:\Users\Ivo\Documents\Cchat\game.html");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.None;
            browser.Location = new Point(12, 150);
            browser.Size = new Size(250, 250);
        }

        // Create application instance
        public Form1()
        {
            InitializeComponent();
            //InitBrowser();

            CchatLog.CreateLog();
            PrintLog(20);
            ScrollToBottom(chatBox);
            startServerIPBox.Text = CchatConnectionInfo.GetServerIP();
            startServerPortBox.Text = CchatConnectionInfo.GetPort();
            connectServerPortBox.Text = CchatConnectionInfo.GetPort();
        }

        // Scroll textbox to bottom
        private static void ScrollToBottom(TextBox tb)
        {
            NativeMethods.SendMessage(tb.Handle, WM_VSCROLL, (UIntPtr)SB_BOTTOM, IntPtr.Zero);
        }

        // Print text stored in log file to chat window
        private void PrintLog(int lines)
        {
            // Store all lins of the log file
            string[] log = CchatLog.ReadFromLog();
            // Set counter by certain amount of lines before end of log
            int lineCounter = log.Length - lines - 1;
            // 
            for (int i = 0; i < log.Length; i++)
            {
                lineCounter++;
                if (lineCounter < log.Length)
                {
                    chatBox.AppendText(log.ElementAt(lineCounter) + "\r\n");
                }
            }
        }

        // Print image to picturebox
        private void PrintImage(Image image)
        {
            // Clean up memory of old image
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            pictureBox1.Image = image;
        }

        // Print text written in textbox to chat window
        private void PrintText(string text)
        {
            // Exclude commands from displaying
            if (text.StartsWith("/", StringComparison.Ordinal))
                return;

            this.Invoke((MethodInvoker)delegate
            {
                chatBox.AppendText(text + "\r\n");
            });
        }

        // Disconnent server or client
        private void Disconnect()
        {
            streamWriter.WriteLine("/disconnect");
            PrintText(MSG_DISCONNECT);
            if (server != null)
                server.Stop();
            client.Close();
            client = null;
            dataSender.CancelAsync();
            dataReceiver.Dispose();
            dataSender.Dispose();
        }

        // Button to start server
        private void button_start_Click(object sender, EventArgs e)
        {
            try
            {
                server = new TcpListener(IPAddress.Any, int.Parse(startServerPortBox.Text, CultureInfo.InvariantCulture));
                server.Start();
                client = server.AcceptTcpClient();
                streamReader = new StreamReader(client.GetStream());
                streamWriter = new StreamWriter(client.GetStream());
                binaryReader = new BinaryReader(client.GetStream());
                binaryWriter = new BinaryWriter(client.GetStream());
                streamWriter.AutoFlush = true;
                dataReceiver.RunWorkerAsync();
                dataSender.WorkerSupportsCancellation = true;
            }
            catch (FormatException x)
            {
                PrintText(x.ToString());
            }
        }

        // button to connect client to server 
        private void button_connect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                IPEndPoint IP_End = new IPEndPoint(IPAddress.Parse(connectServerIPBox.Text), int.Parse(connectServerPortBox.Text, CultureInfo.InvariantCulture));
                client.Connect(IP_End);
                if (client.Connected)
                {
                    PrintText(MSG_CONNECT);
                    streamReader = new StreamReader(client.GetStream());
                    streamWriter = new StreamWriter(client.GetStream());
                    binaryReader = new BinaryReader(client.GetStream());
                    binaryWriter = new BinaryWriter(client.GetStream());
                    streamWriter.AutoFlush = true;
                    dataReceiver.RunWorkerAsync();
                    dataSender.WorkerSupportsCancellation = true;
                }
            }
            catch (FormatException x)
            {
                PrintText(x.ToString());
            }
        }

        // Button to disconnect server or client
        private void button_disconnect_Click(object sender, EventArgs e)
        {
            if (client == null)
                return;

            Disconnect();
        }

        private void InterpetData(string data)
        {
            // Check what type of data we have
            dynamic type = DataType(data);
            if (type == typeof(int))
            {
                // Retrieve buffer length
                int length = int.Parse(data);
                // Retrieve bytes from data stream
                byte[] bytes = binaryReader.ReadBytes(length);
                // Insert and convert bytes back to Image
                CchatImage.ConvertBytesToImage(bytes);
                // Retrieve the Image
                PrintImage(CchatImage.GetImage());
            }
            else if (type == typeof(string))
            {
                if (data.Contains("sender:"))
                {
                    PrintText(data);
                    //CchatLog.WriteToLog(MSG_PREFIX_RECEIVER + data);
                }

                if (data == "/disconnect")
                {
                    Disconnect();
                    return;
                }
            }
        }

        private static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = double.TryParse(Convert.ToString(Expression, NumberFormatInfo.InvariantInfo), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private static Type DataType(object data)
        {
            if (IsNumeric(data))
            {
                return typeof(int);
            }
            else if (data is string)
                return typeof(string);

            return null;
        }

        private void dataReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (client != null && client.Connected)
                {
                    InterpetData(streamReader.ReadLine());
                }
            }
            catch (IOException x)
            {
                PrintText(x.ToString());
            }
        }

        private void dataSender_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client != null && client.Connected)
            {
                if (image_to_send != null)
                {
                    // Convert Image to bytes
                    CchatImage.ConvertImageToBytes(image_to_send);
                    // Store length of the byte array of the Image
                    string imageLength = "" + CchatImage.GetImageData().Length;
                    // Send the length
                    streamWriter.WriteLine(imageLength);
                    // Send the Image
                    binaryWriter.Write(CchatImage.GetImageData(), 0, CchatImage.GetImageData().Length);
                    // Show Image to self
                    PrintImage(image_to_send);
                    // Clean up old image to send
                    image_to_send = null;
                    // Wait before sending next
                    Thread.Sleep(1000);
                }

                if (text_to_send != null)
                {
                    // Send text written in the textbox
                    streamWriter.WriteLine(MSG_PREFIX_RECEIVER + text_to_send);
                    // Show text written to self
                    PrintText(MSG_PREFIX_SENDER + text_to_send);
                    // Store text written in textbox to log
                    CchatLog.WriteToLog(MSG_PREFIX_SENDER + text_to_send);
                    // Clean up old text to send
                    text_to_send = null;
                }
            }
            else
            {
                PrintText(MSG_ERROR_SEND_DATA);
                return;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    text_to_send = textBox1.Text;
                    if (!dataSender.IsBusy)
                        dataSender.RunWorkerAsync();
                }
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "png files (*.png)|*.png";
            openFileDialog1.FilterIndex = 2;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream myStream = openFileDialog1.OpenFile())
                {
                    string imageDir = openFileDialog1.FileName;
                    image_to_send = new Bitmap(imageDir);
                    if (!dataSender.IsBusy)
                        dataSender.RunWorkerAsync();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            browser.Load(@"C:\Users\Ivo\Documents\Cchat\game.html");
        }
    }
}