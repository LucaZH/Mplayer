using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPlayer2._0
{
    public partial class MPlayer : Form
    {
        string[] fichier, chemin;
        public MPlayer()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.uiMode = "none";
            Pause.Hide();
            Play.Show();
            Ajouter.Hide();
            Ouvrir.Show();
            OuvrirPlus.Show();
            allera.Show();
        }
        public void UpdateselectedIndex()
        {
            if (Playlist.SelectedIndex != Genre.SelectedIndex)
            {
                Genre.SetSelected(Playlist.SelectedIndex, true);
                Album.SetSelected(Playlist.SelectedIndex, true);

            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
            Play.Hide();
            Pause.Show();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            Pause.Hide();
            Play.Show();
        }

        private void PlaylisteH_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            IndicMenu.Value = 0;
            Ajouter.Hide();
            Effacer.Hide();
            Ouvrir.Show();
            OuvrirPlus.Show();
            allera.Show();
            PlayListPanel.Hide();
        }

        private void MPlaylist_Click(object sender, EventArgs e)
        {
            IndicMenu.Value = 1;
            PlayListPanel.Show();
            Ajouter.Show();
            Effacer.Show();
        }

        private void Quitter_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void OuvrirPlus_Click(object sender, EventArgs e)
        {
            Playlist.Items.Clear();
            Genre.Items.Clear();
            Album.Items.Clear();
            OpenFileDialog ouvrir = new OpenFileDialog() { Multiselect = true, Filter = "MP4 File|*.mp4|AVI File|*.avi|MP3 File|*.mp3|All File|*.*" };
            if (ouvrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fichier = ouvrir.SafeFileNames;
                chemin = ouvrir.FileNames;
                axWindowsMediaPlayer1.URL = chemin[0];

                for (int i = 0; i < fichier.Length; i++)
                {
                    Playlist.Items.Add(fichier[i]);
                    Genre.Items.Add("Inconnue");
                    Album.Items.Add("Inconnue");
                }
                Playlist.SetSelected(0, true);
                Genre.SetSelected(0, true);
                Album.SetSelected(0, true);
                titrem.Text = Playlist.SelectedItem.ToString();

            }
        }

        private void Ajouter_Click(object sender, EventArgs e)
        {
            OpenFileDialog ouvrir = new OpenFileDialog() { Multiselect = true, Filter = "MP4 File|*.mp4|AVI File|*.avi|MP3 File|*.mp3|All File|*.*" };
            if (ouvrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fichier = ouvrir.SafeFileNames;
                chemin = ouvrir.FileNames;

                for (int i = 0; i < fichier.Length; i++)
                {
                    Playlist.Items.Add(fichier[i]);
                    Genre.Items.Add("Inconnue");
                    Album.Items.Add("Inconnue");
                }

            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                time.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                TrackBarTime.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
        }

        private void TrackBarTime_Scroll(object sender, ScrollEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBarTime.Value;
            timeav.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            timeav.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            

        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (Playlist.SelectedIndex < Playlist.Items.Count - 1 && Genre.SelectedIndex < Genre.Items.Count - 1 && Album.SelectedIndex < Album.Items.Count - 1)
            {
                Playlist.SelectedIndex = Playlist.SelectedIndex + 1;
                titrem.Text = Playlist.SelectedItem.ToString();
                Genre.SelectedIndex = Genre.SelectedIndex + 1;
                Album.SelectedIndex = Album.SelectedIndex + 1;
            }
            UpdateselectedIndex();
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            if (Playlist.SelectedIndex > 0 && Genre.SelectedIndex > 0 && Album.SelectedIndex > 0)
            {
                Playlist.SelectedIndex = Playlist.SelectedIndex - 1;
                Genre.SelectedIndex = Genre.SelectedIndex - 1;
                Album.SelectedIndex = Album.SelectedIndex - 1;
                titrem.Text = Playlist.SelectedItem.ToString();

            }
            UpdateselectedIndex();

        }
        
        private void Playlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = chemin[Playlist.SelectedIndex];
            titrem.Text = Playlist.SelectedItem.ToString();
            UpdateselectedIndex();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                TrackBarTime.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                Pause.Show();
                Play.Hide();

                timeav.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                time.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString.ToString();
            }
            if (TrackBarTime.Value == (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration)
            {
                if (Playlist.SelectedIndex < Playlist.Items.Count - 1)
                {
                    Playlist.SelectedIndex = Playlist.SelectedIndex + 1;
                    titrem.Text = Playlist.SelectedItem.ToString();
                }
            }
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                Pause.Hide();
            }
        }

        private void Effacer_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            Play.Show();
            Pause.Hide();
            Playlist.Items.Clear();
            Genre.Items.Clear();
            Album.Items.Clear();
        }

        private void Vol_Scroll(object sender, ScrollEventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = Vol.Value;
            Lvol.Text = Vol.Value.ToString();
            if (Vol.Value == 0)
            {
                Vollab.Hide();
                Mut.Show();
            }
            if (Vol.Value != 0)
            {
                Vollab.Show();
                Mut.Hide();
            }
        }

        private void Mut_Click(object sender, EventArgs e)
        {
            Vollab.Show();
            Mut.Hide();
            axWindowsMediaPlayer1.settings.volume = Vol.Value;
            Vol.Value = 50;

        }

        private void Vollab_Click(object sender, EventArgs e)
        {
            Vollab.Hide();
            Mut.Show();
            axWindowsMediaPlayer1.settings.volume = Vol.Value;
            Vol.Value = 0;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            Pause.Hide();
            Play.Show();
            TrackBarTime.Value = 0;

        }

        private void Menu_Click(object sender, EventArgs e)
        {
            PannelMenu.Show();
            Play.Size = new Size(55, 35);
            Pause.Size = new Size(55, 35);
            BNext.Size = new Size(55, 35);
            BPrev.Size = new Size(55, 35);
            Stop.Size = new Size(55, 35);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PannelMenu.Hide();
            Play.Size = new Size(55, 35);
            Pause.Size = new Size(55, 35);
            BNext.Size = new Size(55, 35);
            BPrev.Size = new Size(55, 35);
            Stop.Size = new Size(55, 35);


        }

        private void Gototime_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (int)heur.Value * 360 + (int)min.Value * 60 + (int)sec.Value;
        }

        private void hidealler_Click(object sender, EventArgs e)
        {
            gototimepanel.Hide();
        }

        private void allera_Click(object sender, EventArgs e)
        {
            gototimepanel.Show();
        }

        private void Ouvrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog ouvrir = new OpenFileDialog() { Multiselect = false, Filter = "MP4 File|*.mp4|AVI File|*.avi|MP3 File|*.mp3|All File|*.*" };
            Playlist.Items.Clear();
            Genre.Items.Clear();
            Album.Items.Clear();
            if (ouvrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fichier = ouvrir.SafeFileNames;
                chemin = ouvrir.FileNames;
                axWindowsMediaPlayer1.URL = chemin[0];


                for (int i = 0; i < fichier.Length; i++)
                {
                    Playlist.Items.Add(fichier[i]);
                    Genre.Items.Add("Inconnue");
                    Album.Items.Add("Inconnue");

                }
                Playlist.SetSelected(0, true);

                titrem.Text = Playlist.SelectedItem.ToString();

            }
        }

        private void MPlayer_SizeChanged(object sender, EventArgs e)
        {
            Play.Size = new Size(55, 35);
            Pause.Size = new Size(55, 35);
            BNext.Size = new Size(55, 35);
            BPrev.Size = new Size(55, 35);
            Stop.Size = new Size(55, 35);
        }
    }
}
