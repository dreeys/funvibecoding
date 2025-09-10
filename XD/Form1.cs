using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;

namespace XD
{
    public partial class Form1 : Form
    {
        private int lovePoints = 0;
        private Panel pagePanel;
        private Button btnEarnPage;
        private Button btnSpendPage;
        private Button btnExit;
        private PictureBox kittyPic;

        // Controls for Earn Page
        private Label lblPointsEarn;

        // Controls for Spend Page
        private Label lblPointsSpend;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
            ShowEarnPage();
        }

        private void SetupUI()
        {
            this.Text = "LovePoints 💜 Hello Kitty Edition";
            this.Size = new Size(820, 620);
            this.BackColor = Color.MediumPurple;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Navigation buttons
            btnEarnPage = CreateNavButton("Earn Points", new Point(40, 20));
            btnEarnPage.Click += (s, e) => ShowEarnPage();
            this.Controls.Add(btnEarnPage);

            btnSpendPage = CreateNavButton("Spend Points", new Point(180, 20));
            btnSpendPage.Click += (s, e) => ShowSpendPage();
            this.Controls.Add(btnSpendPage);

            btnExit = CreateNavButton("Exit", new Point(320, 20));
            btnExit.Click += (s, e) => { this.Close(); };
            this.Controls.Add(btnExit);

            // Hello Kitty couple image at the top center (always visible)
            kittyPic = new PictureBox();
            kittyPic.Image = LoadImage("kitty_couple1.png"); // <-- uses your actual filename
            kittyPic.SizeMode = PictureBoxSizeMode.Zoom;
            kittyPic.Location = new Point((this.ClientSize.Width - 200) / 2, 70);
            kittyPic.Size = new Size(200, 120);
            kittyPic.Anchor = AnchorStyles.Top;
            this.Controls.Add(kittyPic);

            // Main page panel
            pagePanel = new Panel();
            pagePanel.Location = new Point(40, 200);
            pagePanel.Size = new Size(720, 370);
            pagePanel.BackColor = Color.Lavender;
            pagePanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pagePanel);
        }

        private Button CreateNavButton(string text, Point location)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.BackColor = Color.Purple;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Size = new Size(120, 38);
            btn.Location = location;
            btn.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 20, 20));
            return btn;
        }

        // --- PAGE 1: EARN POINTS ---
        private void ShowEarnPage()
        {
            pagePanel.Controls.Clear();

            var lblTitle = new Label();
            lblTitle.Text = "Earn LovePoints by doing cute things 💜";
            lblTitle.Font = new Font("Segoe Script", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.Purple;
            lblTitle.Location = new Point(30, 10);
            lblTitle.AutoSize = true;
            pagePanel.Controls.Add(lblTitle);

            lblPointsEarn = new Label();
            lblPointsEarn.Text = $"Your LovePoints: {lovePoints}";
            lblPointsEarn.Font = new Font("Comic Sans MS", 14, FontStyle.Bold);
            lblPointsEarn.ForeColor = Color.DarkViolet;
            lblPointsEarn.Location = new Point(30, 50);
            lblPointsEarn.AutoSize = true;
            pagePanel.Controls.Add(lblPointsEarn);

            // Scrollable panel for tasks
            var earnListPanel = new Panel();
            earnListPanel.Location = new Point(30, 90);
            earnListPanel.Size = new Size(660, 260);
            earnListPanel.AutoScroll = true;
            earnListPanel.BackColor = Color.Thistle;
            pagePanel.Controls.Add(earnListPanel);

            // List of tasks to earn points
            var earnTasks = new (string, int)[]
            {
                ("Send a sweet text 💌", 2),
                ("Give a hug 🤗", 1),
                ("Make breakfast for us 🍳", 3),
                ("Say 'I love you' 💖", 1),
                ("Write a love note ✍️", 2),
                ("Surprise me with a treat 🍫", 2),
                ("Give a massage 💆‍♀️", 3),
                ("Draw something for me 🎨", 2),
                ("Share a funny meme 😂", 1),
                ("Plan a date night 🌙", 4),
                ("Help with chores 🧹", 2),
                ("Take a selfie together 📸", 1),
                ("Watch a movie together 🎬", 2),
                ("Go for a walk with me 🚶‍♀️", 2),
                ("Dance together 💃", 2),
                ("Make me laugh 😄", 1),
                ("Sing a song for me 🎤", 2),
                ("Play a game together 🎲", 2),
                ("Cook dinner together 🍝", 3),
                ("Tell me a secret 🤫", 1),
                ("Make a playlist for us 🎧", 2),
                ("Plan a future trip ✈️", 3),
                ("Read a poem to me 📖", 2),
                ("Make a handmade gift 🎁", 4),
                ("Do something silly together 🤪", 1),
                ("Give a flower 🌹", 2),
                ("Share a childhood story 🧸", 1),
                ("Help me relax 🛁", 2)
            };

            int y = 10;
            foreach (var (task, points) in earnTasks)
            {
                var btn = new Button();
                btn.Text = $"{task} (+{points} LP)";
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btn.BackColor = Color.MediumPurple;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(620, 35);
                btn.Location = new Point(10, y);
                btn.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 15, 15));
                btn.Click += (s, e) =>
                {
                    lovePoints += points;
                    lblPointsEarn.Text = $"Your LovePoints: {lovePoints}";
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show($"You earned {points} LovePoints!\nTask: {task}", "So sweet!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                earnListPanel.Controls.Add(btn);
                y += 45;
            }
        }

        // --- PAGE 2: SPEND POINTS ---
        private void ShowSpendPage()
        {
            pagePanel.Controls.Clear();

            var lblTitle = new Label();
            lblTitle.Text = "Spend your LovePoints on cute rewards 💜";
            lblTitle.Font = new Font("Segoe Script", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.Purple;
            lblTitle.Location = new Point(30, 10);
            lblTitle.AutoSize = true;
            pagePanel.Controls.Add(lblTitle);

            lblPointsSpend = new Label();
            lblPointsSpend.Text = $"Your LovePoints: {lovePoints}";
            lblPointsSpend.Font = new Font("Comic Sans MS", 14, FontStyle.Bold);
            lblPointsSpend.ForeColor = Color.DarkViolet;
            lblPointsSpend.Location = new Point(30, 50);
            lblPointsSpend.AutoSize = true;
            pagePanel.Controls.Add(lblPointsSpend);

            // Scrollable panel for rewards
            var spendListPanel = new Panel();
            spendListPanel.Location = new Point(30, 90);
            spendListPanel.Size = new Size(660, 260);
            spendListPanel.AutoScroll = true;
            spendListPanel.BackColor = Color.Thistle;
            pagePanel.Controls.Add(spendListPanel);

            // List of rewards to spend points
            var spendRewards = new (string, int, string)[]
            {
                ("Get a handwritten love letter 💌", 10, "You get a special love letter!"),
                ("Movie night of your choice 🎬", 8, "You pick the movie!"),
                ("Home-cooked dinner 🍝", 12, "I'll cook your favorite meal!"),
                ("Breakfast in bed 🍳", 10, "Breakfast will be served!"),
                ("Massage session 💆‍♀️", 15, "Relax and enjoy a massage!"),
                ("Surprise treat 🍫", 7, "A sweet treat is coming!"),
                ("Date night planned by me 🌙", 20, "A surprise date night!"),
                ("A bouquet of flowers 🌹", 14, "Beautiful flowers for you!"),
                ("A playlist made just for you 🎧", 6, "Enjoy your custom playlist!"),
                ("A day without chores 🧹", 18, "You get a day off from chores!"),
                ("A silly dance performance 💃", 5, "Get ready to laugh!"),
                ("A poem written for you 📖", 9, "A poem from my heart!"),
                ("A photo session together 📸", 8, "Let's take cute photos!"),
                ("A spa night at home 🛁", 16, "Relax with a spa night!"),
                ("A game night 🎲", 7, "Let's play your favorite games!"),
                ("A secret revealed 🤫", 4, "I'll share a secret!"),
                ("A day trip planned for you ✈️", 25, "Adventure awaits!"),
                ("A handmade gift 🎁", 20, "A unique gift just for you!"),
                ("A playlist swap 🎧", 6, "Let's exchange playlists!"),
                ("A breakfast date at a café ☕", 13, "Let's go out for breakfast!"),
                ("A bucket list planning session 📝", 8, "Let's dream together!"),
                ("A scrapbook made together 📔", 15, "Memories to cherish!"),
                ("A chocolate box 🍫", 10, "Sweet chocolates for you!"),
                ("A flower delivery 🌷", 12, "Flowers delivered to you!"),
                ("A day of pampering 🛁", 18, "You deserve it!"),
                ("A love song performance 🎤", 7, "I'll sing for you!"),
                ("A picnic in the park 🧺", 14, "Let's enjoy the outdoors!"),
                ("A day of fun activities 🤪", 10, "Let's have fun together!"),
                ("A custom meme made for you 😂", 5, "Get ready to laugh!"),
                ("A future trip planned together 🌍", 30, "Let's plan our next adventure!")
            };

            int y = 10;
            foreach (var (reward, cost, msg) in spendRewards)
            {
                var btn = new Button();
                btn.Text = $"{reward} ({cost} LP)";
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btn.BackColor = Color.MediumPurple;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(620, 35);
                btn.Location = new Point(10, y);
                btn.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 15, 15));
                btn.Click += (s, e) =>
                {
                    if (lovePoints >= cost)
                    {
                        lovePoints -= cost;
                        lblPointsSpend.Text = $"Your LovePoints: {lovePoints}";
                        SystemSounds.Exclamation.Play();
                        MessageBox.Show(msg, "Reward!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        SystemSounds.Hand.Play();
                        MessageBox.Show("Not enough LovePoints!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };
                spendListPanel.Controls.Add(btn);
                y += 45;
            }
        }

        // Helper to load images safely
        private Image LoadImage(string filename)
        {
            try
            {
                return Image.FromFile(filename);
            }
            catch
            {
                // If image not found, return a blank bitmap
                Bitmap bmp = new Bitmap(1, 1);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                }
                return bmp;
            }
        }

        // Native method for rounded buttons
        internal static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }
    }
}