using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SubTitle
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SousTitre subtitle;
        bool isPlaying;
        bool firsthit;
        BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();

            subtitle = new SousTitre();
            worker = new BackgroundWorker();
            isPlaying = false;
            firsthit = true;

        }

        

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (subtitle.m_i < subtitle.m_sousTitres.Count)
            {
                int progressPercentage = Convert.ToInt32(((double)subtitle.m_i / subtitle.m_sousTitres.Count) * 100);

                if (isPlaying)
                {
                    int i = subtitle.DisplaySubtitle().Result;
                    (sender as BackgroundWorker).ReportProgress(progressPercentage, i);
                }
            }
            
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            subtitles.Text = subtitle.m_sousTitres[(int)e.UserState].m_texte;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {}

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Player != null) && (Player.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                Player.Play();
                play.Content = "Pause";
            }
            else
            {
                isPlaying = false;
                Player.Pause();
                play.Content = "Play";
            }

            if (firsthit && subtitle.m_read)
            {
                worker.WorkerReportsProgress = true;
                worker.DoWork += worker_DoWork;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
                firsthit = false;
            }
        }

        private void openVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp4;*.avi;*.mpeg)|*.mp4;*.avi;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                Player.Source = new Uri(openFileDialog.FileName);
        }

        private void openSubtitle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Subtitle files (*.srt)|*.srt;|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                subtitle.Read(openFileDialog.FileName);
        }
    }

    struct Format
    {
        public Format(string l_texte, TimeSpan l_begin, TimeSpan l_end)
        {

            m_texte = l_texte;
            m_begin = l_begin;
            m_end = l_end;
        }

        public string m_texte;
        public TimeSpan m_begin;
        public TimeSpan m_end;
    }

    class SousTitre
    {
        public SousTitre()
        {
            m_sousTitres = new ObservableCollection<Format>();
            m_i = 0;
            m_read = false;
        }

        public void Read(string path)
        {
            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] tab = file.ReadLine().Split(' ');
                        TimeSpan begin = TimeSpan.Parse(tab[0]);
                        TimeSpan end = TimeSpan.Parse(tab[2]);
                        string texte = "";

                        while ((line = file.ReadLine()) != "") { texte += '\n' + line; }

                        m_sousTitres.Add(new Format(texte, begin, end));
                    }
                    m_read = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async public Task<int> DisplaySubtitle()
        {
            if (m_i == 0)
            {
                await Task.Delay((int)m_sousTitres[m_i].m_begin.TotalMilliseconds);

                int i = m_i;
                ++m_i;
                return i;
            }
            else
            {
                await Task.Delay((int)m_sousTitres[m_i - 1].m_end.TotalMilliseconds - (int)m_sousTitres[m_i - 1].m_begin.TotalMilliseconds);
                await Task.Delay((int)m_sousTitres[m_i].m_begin.TotalMilliseconds - (int)m_sousTitres[m_i - 1].m_end.TotalMilliseconds);

                int i = m_i;
                ++m_i;
                return i;
            }
        }

        public ObservableCollection<Format> m_sousTitres;
        public int m_i;
        public bool m_read;
    }

}
