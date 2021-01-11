
namespace sj_jha_twitter_app
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.emoji1 = new System.Windows.Forms.PictureBox();
            this.emoji2 = new System.Windows.Forms.PictureBox();
            this.emoji4 = new System.Windows.Forms.PictureBox();
            this.emoji3 = new System.Windows.Forms.PictureBox();
            this.emoji6 = new System.Windows.Forms.PictureBox();
            this.emoji5 = new System.Windows.Forms.PictureBox();
            this.emoji8 = new System.Windows.Forms.PictureBox();
            this.emoji7 = new System.Windows.Forms.PictureBox();
            this.emoji10 = new System.Windows.Forms.PictureBox();
            this.emoji9 = new System.Windows.Forms.PictureBox();
            this.topDomainsTextBox = new System.Windows.Forms.TextBox();
            this.topHashtagsTextBox = new System.Windows.Forms.TextBox();
            this.photoUrlPercentLabel = new System.Windows.Forms.Label();
            this.urlPercentLabel = new System.Windows.Forms.Label();
            this.emojiPercentLabel = new System.Windows.Forms.Label();
            this.tweetsPerSecLabel = new System.Windows.Forms.Label();
            this.tweetCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.emoji1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji9)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(463, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "&Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Location = new System.Drawing.Point(463, 70);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "E&xit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Location = new System.Drawing.Point(463, 41);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Sto&p";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tweet Count:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tweets/Sec:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 27);
            this.label3.TabIndex = 6;
            this.label3.Text = "URL %:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 27);
            this.label4.TabIndex = 5;
            this.label4.Text = "Emoji %:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(275, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "Top Linked Domains";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 27);
            this.label6.TabIndex = 7;
            this.label6.Text = "Photo URL %:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(35, 303);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 27);
            this.label7.TabIndex = 10;
            this.label7.Text = "Top Hashtags";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 218);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 27);
            this.label8.TabIndex = 9;
            this.label8.Text = "Top Emojis:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // emoji1
            // 
            this.emoji1.Location = new System.Drawing.Point(249, 185);
            this.emoji1.Name = "emoji1";
            this.emoji1.Size = new System.Drawing.Size(48, 48);
            this.emoji1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji1.TabIndex = 11;
            this.emoji1.TabStop = false;
            // 
            // emoji2
            // 
            this.emoji2.Location = new System.Drawing.Point(306, 185);
            this.emoji2.Name = "emoji2";
            this.emoji2.Size = new System.Drawing.Size(48, 48);
            this.emoji2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji2.TabIndex = 12;
            this.emoji2.TabStop = false;
            // 
            // emoji4
            // 
            this.emoji4.Location = new System.Drawing.Point(420, 185);
            this.emoji4.Name = "emoji4";
            this.emoji4.Size = new System.Drawing.Size(48, 48);
            this.emoji4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji4.TabIndex = 14;
            this.emoji4.TabStop = false;
            // 
            // emoji3
            // 
            this.emoji3.Location = new System.Drawing.Point(363, 185);
            this.emoji3.Name = "emoji3";
            this.emoji3.Size = new System.Drawing.Size(48, 48);
            this.emoji3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji3.TabIndex = 13;
            this.emoji3.TabStop = false;
            // 
            // emoji6
            // 
            this.emoji6.Location = new System.Drawing.Point(249, 239);
            this.emoji6.Name = "emoji6";
            this.emoji6.Size = new System.Drawing.Size(48, 48);
            this.emoji6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji6.TabIndex = 16;
            this.emoji6.TabStop = false;
            // 
            // emoji5
            // 
            this.emoji5.Location = new System.Drawing.Point(477, 185);
            this.emoji5.Name = "emoji5";
            this.emoji5.Size = new System.Drawing.Size(48, 48);
            this.emoji5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji5.TabIndex = 15;
            this.emoji5.TabStop = false;
            // 
            // emoji8
            // 
            this.emoji8.Location = new System.Drawing.Point(363, 239);
            this.emoji8.Name = "emoji8";
            this.emoji8.Size = new System.Drawing.Size(48, 48);
            this.emoji8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji8.TabIndex = 18;
            this.emoji8.TabStop = false;
            // 
            // emoji7
            // 
            this.emoji7.Location = new System.Drawing.Point(306, 239);
            this.emoji7.Name = "emoji7";
            this.emoji7.Size = new System.Drawing.Size(48, 48);
            this.emoji7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji7.TabIndex = 17;
            this.emoji7.TabStop = false;
            // 
            // emoji10
            // 
            this.emoji10.Location = new System.Drawing.Point(477, 239);
            this.emoji10.Name = "emoji10";
            this.emoji10.Size = new System.Drawing.Size(48, 48);
            this.emoji10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji10.TabIndex = 20;
            this.emoji10.TabStop = false;
            // 
            // emoji9
            // 
            this.emoji9.Location = new System.Drawing.Point(420, 239);
            this.emoji9.Name = "emoji9";
            this.emoji9.Size = new System.Drawing.Size(48, 48);
            this.emoji9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji9.TabIndex = 19;
            this.emoji9.TabStop = false;
            // 
            // topDomainsTextBox
            // 
            this.topDomainsTextBox.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topDomainsTextBox.Location = new System.Drawing.Point(279, 333);
            this.topDomainsTextBox.Multiline = true;
            this.topDomainsTextBox.Name = "topDomainsTextBox";
            this.topDomainsTextBox.ReadOnly = true;
            this.topDomainsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.topDomainsTextBox.Size = new System.Drawing.Size(261, 186);
            this.topDomainsTextBox.TabIndex = 21;
            // 
            // topHashtagsTextBox
            // 
            this.topHashtagsTextBox.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topHashtagsTextBox.Location = new System.Drawing.Point(12, 333);
            this.topHashtagsTextBox.Multiline = true;
            this.topHashtagsTextBox.Name = "topHashtagsTextBox";
            this.topHashtagsTextBox.ReadOnly = true;
            this.topHashtagsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.topHashtagsTextBox.Size = new System.Drawing.Size(261, 186);
            this.topHashtagsTextBox.TabIndex = 22;
            // 
            // photoUrlPercentLabel
            // 
            this.photoUrlPercentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.photoUrlPercentLabel.Location = new System.Drawing.Point(226, 151);
            this.photoUrlPercentLabel.Name = "photoUrlPercentLabel";
            this.photoUrlPercentLabel.Size = new System.Drawing.Size(220, 27);
            this.photoUrlPercentLabel.TabIndex = 27;
            this.photoUrlPercentLabel.Text = "0";
            this.photoUrlPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // urlPercentLabel
            // 
            this.urlPercentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlPercentLabel.Location = new System.Drawing.Point(226, 118);
            this.urlPercentLabel.Name = "urlPercentLabel";
            this.urlPercentLabel.Size = new System.Drawing.Size(220, 27);
            this.urlPercentLabel.TabIndex = 26;
            this.urlPercentLabel.Text = "0";
            this.urlPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // emojiPercentLabel
            // 
            this.emojiPercentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emojiPercentLabel.Location = new System.Drawing.Point(226, 85);
            this.emojiPercentLabel.Name = "emojiPercentLabel";
            this.emojiPercentLabel.Size = new System.Drawing.Size(220, 27);
            this.emojiPercentLabel.TabIndex = 25;
            this.emojiPercentLabel.Text = "0";
            this.emojiPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tweetsPerSecLabel
            // 
            this.tweetsPerSecLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweetsPerSecLabel.Location = new System.Drawing.Point(226, 52);
            this.tweetsPerSecLabel.Name = "tweetsPerSecLabel";
            this.tweetsPerSecLabel.Size = new System.Drawing.Size(220, 27);
            this.tweetsPerSecLabel.TabIndex = 24;
            this.tweetsPerSecLabel.Text = "0";
            this.tweetsPerSecLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tweetCountLabel
            // 
            this.tweetCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweetCountLabel.Location = new System.Drawing.Point(226, 19);
            this.tweetCountLabel.Name = "tweetCountLabel";
            this.tweetCountLabel.Size = new System.Drawing.Size(220, 27);
            this.tweetCountLabel.TabIndex = 23;
            this.tweetCountLabel.Text = "0";
            this.tweetCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(552, 531);
            this.Controls.Add(this.photoUrlPercentLabel);
            this.Controls.Add(this.urlPercentLabel);
            this.Controls.Add(this.emojiPercentLabel);
            this.Controls.Add(this.tweetsPerSecLabel);
            this.Controls.Add(this.tweetCountLabel);
            this.Controls.Add(this.topHashtagsTextBox);
            this.Controls.Add(this.topDomainsTextBox);
            this.Controls.Add(this.emoji10);
            this.Controls.Add(this.emoji9);
            this.Controls.Add(this.emoji8);
            this.Controls.Add(this.emoji7);
            this.Controls.Add(this.emoji6);
            this.Controls.Add(this.emoji5);
            this.Controls.Add(this.emoji4);
            this.Controls.Add(this.emoji3);
            this.Controls.Add(this.emoji2);
            this.Controls.Add(this.emoji1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Twitter Stats";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.emoji1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox emoji1;
        private System.Windows.Forms.PictureBox emoji2;
        private System.Windows.Forms.PictureBox emoji4;
        private System.Windows.Forms.PictureBox emoji3;
        private System.Windows.Forms.PictureBox emoji6;
        private System.Windows.Forms.PictureBox emoji5;
        private System.Windows.Forms.PictureBox emoji8;
        private System.Windows.Forms.PictureBox emoji7;
        private System.Windows.Forms.PictureBox emoji10;
        private System.Windows.Forms.PictureBox emoji9;
        private System.Windows.Forms.TextBox topDomainsTextBox;
        private System.Windows.Forms.TextBox topHashtagsTextBox;
        private System.Windows.Forms.Label photoUrlPercentLabel;
        private System.Windows.Forms.Label urlPercentLabel;
        private System.Windows.Forms.Label emojiPercentLabel;
        private System.Windows.Forms.Label tweetsPerSecLabel;
        private System.Windows.Forms.Label tweetCountLabel;
    }
}

