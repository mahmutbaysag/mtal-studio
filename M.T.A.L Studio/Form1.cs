using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.ScreenCapture;
using System.IO;
using System.Diagnostics;

namespace M.T.A.L_Studio
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private ScreenCaptureJob scr;
        int kapanis_sayaci = 0;
        string[] dosyalar = System.IO.Directory.GetFiles("C:\\M.T.A.L Studio");
        public Form1()
        {
            InitializeComponent();
            
            {
                
            }



            
            for (int j = 0; j < dosyalar.Length; j++)
            {
                //klasörler dizisinin i. elemanı listboxa ekle
                listBox1.Items.Add(dosyalar[j]);
            }
            metroComboBox1.SelectedItem = 72;
            if(metroComboBox1.SelectedIndex==1)
            {
                scr.ScreenCaptureVideoProfile.FrameRate = 60;
            }
            if(metroComboBox1.SelectedIndex==2)
            {
                scr.ScreenCaptureVideoProfile.FrameRate = 30;
            }
            if(metroComboBox1.SelectedIndex==3)
            {
                scr.ScreenCaptureVideoProfile.FrameRate = 15;
            }
            
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            StartRecord();
            metroButton1.Visible = false;
            metroButton2.Visible = true; 
            kapanis_sayaci += 1;
        }

        void StartRecord()
        {
            scr = new ScreenCaptureJob();

            Rectangle captureRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            scr.CaptureRectangle = captureRect;
            scr.ShowFlashingBoundary = true;
            scr.ShowCountdown = true;
            scr.CaptureMouseCursor = true;
            scr.ScreenCaptureVideoProfile.FrameRate = 72;
            scr.AddAudioDeviceSource(AudioDevices());
            scr.OutputPath = @"C:\M.T.A.L Studio";
            scr.Start();

        }
        EncoderDevice AudioDevices()
        {
            EncoderDevice foundDevice = null;
            Collection<EncoderDevice> audioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);
            try
            {
                foundDevice = audioDevices.First();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ses Aygıtı Bulunamadı" + audioDevices[0].Name + ex.Message);
            }
            return foundDevice;

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (scr.Status == RecordStatus.Running)
            {
                scr.Stop();
                metroButton1.Visible = true;
                metroButton2.Visible = false;
                DialogResult msj;
                msj = MetroFramework.MetroMessageBox.Show(this, "Kayıt Başarılı", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Clear();
                string[] dosyalar = System.IO.Directory.GetFiles("C:\\M.T.A.L Studio");
                for (int j = 0; j < dosyalar.Length; j++)
                {
                    //klasörler dizisinin i. elemanı listboxa ekle
                    listBox1.Items.Add(dosyalar[j]);
                }


            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapanis_sayaci >= 1)
            {
                if (scr.Status == RecordStatus.Running)
                    scr.Stop();

                scr.Dispose();
            }
            else
                System.Environment.Exit(1);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroButton2.Visible = false;
            metroButton1.Style = MetroFramework.MetroColorStyle.Magenta;
            
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            StartRecord();
            metroButton1.Visible = false;
            metroButton2.Visible = true;
            kapanis_sayaci += 1;
            süre();
            metroButton5.Visible = true;
            
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            if (scr.Status == RecordStatus.Running)
            {
                scr.Stop();
                timer1.Stop();
                metroLabel1.Text = "00"; metroLabel2.Text = "00"; metroLabel3.Text = "00";
                sayac = 0; dks = 0; ss = 0;
                metroButton1.Visible = true;
                metroButton2.Visible = false;
                metroButton5.Visible = false;
                metroButton6.Visible = false;
                DialogResult msj;
                msj = MetroFramework.MetroMessageBox.Show(this, "Kayıt Başarılı", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Clear();
                string[] dosyalar = System.IO.Directory.GetFiles("C:\\M.T.A.L Studio");
                for (int j = 0; j < dosyalar.Length; j++)
                {
                    //klasörler dizisinin i. elemanı listboxa ekle
                    listBox1.Items.Add(dosyalar[j]);
                }
            }
        }

        private void MetroLabel1_Click(object sender, EventArgs e)
        {

        }
        int sayac;
        void süre()
        {
            timer1.Start();
            
        }
        int dks; int ss;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            sayac = sayac + 1;
            if(sayac == 60) { dks += 1;  metroLabel2.Text = dks.ToString(); sayac = 0; }
            if(dks == 60) { ss += 1; metroLabel1.Text = ss.ToString(); dks = 0; }
            metroLabel3.Text = sayac.ToString();
        }

        
        
        private void MetroButton3_Click(object sender, EventArgs e)
        {
            sayac++;
            if(sayac==1)
            {
                this.Height = 538;
            }
            if(sayac==2)
            {
                this.Height = 136;
                sayac = 0;
            }
            
        }

        private void MetroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            sayac++;
            if (sayac == 1)
            {
                this.Height = 538;
                pictureBox3.BackgroundImage = Properties.Resources._254151;
            }
            if (sayac == 2)
            {
                this.Height = 142;
                pictureBox3.BackgroundImage = Properties.Resources._25415;
                sayac = 0;
            }
        }

        

        private void MetroButton3_Click_2(object sender, EventArgs e)
        {
            Directory.Delete("C:\\M.T.A.L Studio",true);
            Directory.CreateDirectory("C:\\M.T.A.L Studio");
            listBox1.Items.Clear();
            string[] dosyalar = System.IO.Directory.GetFiles("C:\\M.T.A.L Studio");
            for (int j = 0; j < dosyalar.Length; j++)
            {
                //klasörler dizisinin i. elemanı listboxa ekle
                listBox1.Items.Add(dosyalar[j]);
            }
        }

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\M.T.A.L Studio");
        }

        private void MetroLabel7_Click(object sender, EventArgs e)
        {

        }

        

        private void MetroButton5_Click_1(object sender, EventArgs e)
        {
            scr.Pause();
            timer1.Stop();
            metroButton5.Visible = false;
            metroButton6.Visible = true;
            metroButton2.Visible = false;
        }

        private void MetroButton6_Click_1(object sender, EventArgs e)
        {
            scr.Resume();
            timer1.Start();
            metroButton2.Visible = true;
            metroButton5.Visible = true;
            metroButton6.Visible = false;
        }
    }
}
