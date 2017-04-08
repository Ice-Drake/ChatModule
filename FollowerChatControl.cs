using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ChatModule
{
    public class FollowerChatControl : UserControl, IChatSource
    {
        public string SourceName
        {
            get
            {
                return "Follower";
            }
        }

        public IList<UserAccount> Accounts
        {
            get
            {
                return accounts.Values;
            }
        }

        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                if (TextColorChanged != null)
                    TextColorChanged(this, new PropertyChangedEventArgs("TextColor"));
            }
        }

        public bool Muted { get; private set; }

        public UserAccount MasterAccount { get; private set; }

        public event IncomingMessageEventHandler IncomingMessage;
        public event PropertyChangedEventHandler TextColorChanged;

        private SortedList<string, UserAccount> accounts;
        private Color textColor;
        private DataTable statusTable;
        private BindingSource statusTableBS;
        private Timer timer;

        #region Component Designer variables
        
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView chatLog;
        private System.Windows.Forms.GroupBox attendeeBox;
        private System.Windows.Forms.Button muteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn chatNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chatMessageColumn;
        private System.Windows.Forms.ComboBox statusComboBox;
        private DataGridViewImageColumn userStatusColumn;
        private DataGridViewTextBoxColumn userNameColumn;
        private System.Windows.Forms.DataGridView userStatusGridView;

        #endregion

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatLog = new System.Windows.Forms.DataGridView();
            this.chatNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chatMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attendeeBox = new System.Windows.Forms.GroupBox();
            this.userStatusGridView = new System.Windows.Forms.DataGridView();
            this.muteButton = new System.Windows.Forms.Button();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.userStatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.userNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chatLog)).BeginInit();
            this.attendeeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userStatusGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // chatLog
            // 
            this.chatLog.AllowUserToAddRows = false;
            this.chatLog.AllowUserToDeleteRows = false;
            this.chatLog.AllowUserToResizeColumns = false;
            this.chatLog.AllowUserToResizeRows = false;
            this.chatLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.chatLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.chatLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chatLog.ColumnHeadersVisible = false;
            this.chatLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chatNameColumn,
            this.chatMessageColumn});
            this.chatLog.GridColor = System.Drawing.SystemColors.Window;
            this.chatLog.Location = new System.Drawing.Point(3, 3);
            this.chatLog.Name = "chatLog";
            this.chatLog.ReadOnly = true;
            this.chatLog.RowHeadersVisible = false;
            this.chatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.chatLog.Size = new System.Drawing.Size(252, 307);
            this.chatLog.TabIndex = 4;
            this.chatLog.SelectionChanged += new System.EventHandler(chatLog_SelectionChanged);
            // 
            // chatNameColumn
            // 
            this.chatNameColumn.HeaderText = "Name";
            this.chatNameColumn.Name = "chatNameColumn";
            this.chatNameColumn.ReadOnly = true;
            this.chatNameColumn.Width = 75;
            // 
            // chatMessageColumn
            // 
            this.chatMessageColumn.HeaderText = "Message";
            this.chatMessageColumn.Name = "chatMessageColumn";
            this.chatMessageColumn.ReadOnly = true;
            this.chatMessageColumn.Width = 175;
            // 
            // attendeeBox
            // 
            this.attendeeBox.Controls.Add(this.userStatusGridView);
            this.attendeeBox.Location = new System.Drawing.Point(261, 3);
            this.attendeeBox.Name = "attendeeBox";
            this.attendeeBox.Size = new System.Drawing.Size(135, 278);
            this.attendeeBox.TabIndex = 5;
            this.attendeeBox.TabStop = false;
            this.attendeeBox.Text = "Attendees";
            // 
            // userStatusGridView
            // 
            this.userStatusGridView.AllowUserToAddRows = false;
            this.userStatusGridView.AllowUserToDeleteRows = false;
            this.userStatusGridView.AllowUserToResizeColumns = false;
            this.userStatusGridView.AllowUserToResizeRows = false;
            this.userStatusGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.userStatusGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.userStatusGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userStatusGridView.ColumnHeadersVisible = false;
            this.userStatusGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userStatusColumn,
            this.userNameColumn});
            this.userStatusGridView.GridColor = System.Drawing.SystemColors.Window;
            this.userStatusGridView.Location = new System.Drawing.Point(6, 19);
            this.userStatusGridView.MultiSelect = false;
            this.userStatusGridView.Name = "userStatusGridView";
            this.userStatusGridView.ReadOnly = true;
            this.userStatusGridView.RowHeadersVisible = false;
            this.userStatusGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.userStatusGridView.Size = new System.Drawing.Size(123, 253);
            this.userStatusGridView.TabIndex = 0;
            // 
            // muteButton
            // 
            this.muteButton.Location = new System.Drawing.Point(341, 284);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(55, 23);
            this.muteButton.TabIndex = 6;
            this.muteButton.Text = "Mute";
            this.muteButton.UseVisualStyleBackColor = true;
            this.muteButton.Click += new System.EventHandler(this.muteButton_Click);
            // 
            // statusComboBox
            // 
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "Available",
            "Away",
            "Busy"});
            this.statusComboBox.Location = new System.Drawing.Point(261, 284);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(74, 21);
            this.statusComboBox.TabIndex = 7;
            this.statusComboBox.SelectedIndexChanged += new System.EventHandler(this.statusComboBox_SelectedIndexChanged);
            // 
            // userStatusColumn
            // 
            this.userStatusColumn.DataPropertyName = "Status";
            this.userStatusColumn.HeaderText = "Status";
            this.userStatusColumn.Name = "userStatusColumn";
            this.userStatusColumn.ReadOnly = true;
            this.userStatusColumn.Width = 20;
            // 
            // userNameColumn
            // 
            this.userNameColumn.DataPropertyName = "Name";
            this.userNameColumn.HeaderText = "Name";
            this.userNameColumn.Name = "userNameColumn";
            this.userNameColumn.ReadOnly = true;
            // 
            // FollowerChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.muteButton);
            this.Controls.Add(this.chatLog);
            this.Controls.Add(this.attendeeBox);
            this.Name = "FollowerChatControl";
            this.Size = new System.Drawing.Size(400, 313);
            ((System.ComponentModel.ISupportInitialize)(this.chatLog)).EndInit();
            this.attendeeBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userStatusGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public FollowerChatControl()
        {
            InitializeComponent();

            statusTable = new DataTable();
            statusTable.TableName = "Status Table";
            statusTable.Columns.Add("Status", typeof(Bitmap));
            statusTable.Columns.Add("Name", typeof(string));
            userStatusGridView.DataSource = statusTable;

            statusTableBS = new BindingSource();
            statusTableBS.DataSource = statusTable;

            accounts = new SortedList<string, UserAccount>();

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void addUser(UserAccount account)
        {
            if (!accounts.ContainsKey(account.Username) && account.Owner != null && account.Owner is IChatBot)
            {
                //Add chatbot status to the table
                DataRow row = statusTable.NewRow();
                switch (account.Status)
                {
                    case ChatStatus.Available:
                        row["Status"] = global::ChatModule.Properties.Resources.Available;
                        break;
                    case ChatStatus.Busy:
                        row["Status"] = global::ChatModule.Properties.Resources.Busy;
                        break;
                    case ChatStatus.Idle:
                        row["Status"] = global::ChatModule.Properties.Resources.Away;
                        break;
                    default:
                        row["Status"] = global::ChatModule.Properties.Resources.Offline;
                        break;
                }
                row["Name"] = account.Username;
                statusTable.Rows.Add(row);

                account.StatusChanged += new PropertyChangedEventHandler(chatBot_StatusChanged);

                ((IChatBot)account.Owner).join(this);
            }

            accounts.Add(account.Username, account);
        }

        public UserAccount createUser(string username)
        {
            return new UserAccount(SourceName, username);
        }

        public UserAccount findUser(string username)
        {
            if (accounts.ContainsKey(username))
                return accounts[username];
            else
                return null;
        }

        public void send(string message, UserAccount sender)
        {
            lock (chatLog)
            {
                int row = chatLog.Rows.Add();
                chatLog.Rows[row].Cells["chatNameColumn"].Value = sender.Username;
                chatLog.Rows[row].Cells["chatMessageColumn"].Value = message;

                //Autoscroll down
                int displayed = chatLog.DisplayedRowCount(true);
                if (chatLog.FirstDisplayedScrollingRowIndex >= 0 && displayed < chatLog.RowCount)
                    chatLog.FirstDisplayedScrollingRowIndex = chatLog.RowCount - displayed;
            }

            //Determine who the message should be directed to
            UserAccount target = null;
            string selectedName = userStatusGridView.SelectedRows[0].Cells[1].Value.ToString();
            
            //Split message into words
            string[] messagePieces = message.Split(new char [] {',', ' '});

            //Check if the message is directed to a chatbot
            foreach (UserAccount account in Accounts)
            {
                if (account.Owner != null && account.Owner is IChatBot)
                {
                    IChatBot bot = (IChatBot)account.Owner;

                    ///Check if the message contains a follower name
                    if (messagePieces.Length > 1)
                    {
                        //Check if the name is in the beginning of the message
                        if (messagePieces[0].ToUpper().IndexOf(bot.Profile.Name.ToUpper()) > -1)
                        {
                            target = account;
                            message = message.Substring(messagePieces[0].Length);
                            break;
                        }
                        //Check if the username is in the beginning of the message
                        else if (messagePieces[0].ToUpper().IndexOf(account.Username.ToUpper()) > -1)
                        {
                            target = account;
                            message = message.Substring(messagePieces[0].Length);
                            break;
                        }
                        //Check if the name is in the end of the message
                        else if (messagePieces[messagePieces.Length - 1].ToUpper().IndexOf(bot.Profile.Name.ToUpper()) > -1)
                        {
                            target = account;
                            message = message.Substring(0, message.Length - messagePieces[messagePieces.Length - 1].Length);
                            break;
                        }
                        //Check if the username is in the end of the message
                        else if (messagePieces[messagePieces.Length - 1].ToUpper().IndexOf(account.Username.ToUpper()) > -1)
                        {
                            target = account;
                            message = message.Substring(0, message.Length - messagePieces[messagePieces.Length - 1].Length);
                            break;
                        }
                    }

                    //Direct the message to follower selected given that the message might not contain a follower name
                    if (target == null && account.Username.Equals(selectedName) && account.Status == ChatStatus.Available)
                    {
                        target = account;
                    }
                }
            }

            if (target != null && target.Owner != null)
            {
                //Target must be a chat bot
                ((IChatBot)target.Owner).receive(new ChatMessage(this, sender, target, new TextMessage(message)));
            }

            UserAccount lastListener = null;

            //Direct all followers who is available to listen to the message
            foreach (UserAccount account in Accounts)
            {
                if (account.Owner != null && account.Owner is IFollower && target != account && account.Status == ChatStatus.Available)
                {
                    lastListener = account;
                    IFollower follower = (IFollower)account.Owner;
                    follower.listen(new ChatMessage(this, sender, account, new TextMessage(message)));
                }
            }

            //Select the (last) follower in current conversation
            if (target != null && target.Owner != null)
                userStatusGridView.Rows[statusTableBS.Find("Name", target.Username)].Selected = true;
            else if (lastListener != null)
                userStatusGridView.Rows[statusTableBS.Find("Name", lastListener.Username)].Selected = true;
        }

        public bool setup(UserAccount account)
        {
            if (!SourceName.Equals(account.SourceName))
                return false;
            MasterAccount = account;
            return true;
        }

        public void finalize()
        {
            statusComboBox.SelectedIndex = 0;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (UserAccount account in Accounts)
            {
                if (account.Owner != null && account.Owner is IChatBot)
                {
                    IChatBot bot = (IChatBot)account.Owner;
                    lock (bot.OutgoingMessage)
                    {
                        if (bot.OutgoingMessage.Count > 0)
                        {
                            ChatMessage message = bot.OutgoingMessage.Dequeue();

                            if (!message.Hidden)
                            {
                                lock (chatLog)
                                {
                                    int row = chatLog.Rows.Add();
                                    if (message.Sender.Owner != null)
                                        chatLog.Rows[row].DefaultCellStyle.ForeColor = message.Sender.Owner.FontColor;
                                    chatLog.Rows[row].Cells["chatNameColumn"].Value = message.Sender.Username;

                                    if (message.Recipient.Owner is IChatBot && message.Recipient.Status != ChatStatus.Available)
                                        chatLog.Rows[row].Cells["chatMessageColumn"].Value = message.Content.Message.Insert(message.Content.Message.Length - 1, ", " + message.Recipient.Owner.retrieve(this)[0].Username);
                                    else
                                        chatLog.Rows[row].Cells["chatMessageColumn"].Value = message.Content.Message;

                                    //Autoscroll down
                                    int displayed = chatLog.DisplayedRowCount(true);
                                    if (chatLog.FirstDisplayedScrollingRowIndex >= 0 && displayed < chatLog.RowCount)
                                        chatLog.FirstDisplayedScrollingRowIndex = chatLog.RowCount - displayed;
                                }
                            }

                            UserAccount recipient = message.Recipient;
                            if (recipient.Owner != null && recipient.Owner is IChatBot)
                            {
                                ((IChatBot)recipient.Owner).receive(message);
                            }

                            if (IncomingMessage != null)
                            {
                                if (message.Sender.Owner != null)
                                    IncomingMessage(new ChatSourceMessage(message.Source, message.Sender.Owner.Profile.Name, message.Content.Message));
                            }
                        }
                    }
                }
            }
        }

        private void chatBot_StatusChanged(object sender, PropertyChangedEventArgs e)
        {
            UserAccount account = (UserAccount)sender;

            if (account.Owner != null && account.Owner is IChatBot)
            {
                DataRow row = statusTable.Rows[statusTableBS.Find("Name", account.Username)];

                switch (account.Status)
                {
                    case ChatStatus.Available:
                        row["Status"] = global::ChatModule.Properties.Resources.Available;
                        break;
                    case ChatStatus.Busy:
                        row["Status"] = global::ChatModule.Properties.Resources.Busy;
                        break;
                    case ChatStatus.Idle:
                        row["Status"] = global::ChatModule.Properties.Resources.Away;
                        break;
                    default:
                        row["Status"] = global::ChatModule.Properties.Resources.Offline;
                        break;
                }
            }
        }

        private void chatLog_SelectionChanged(object sender, EventArgs e)
        {
            chatLog.ClearSelection();
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            Muted = !Muted;
            if (Muted)
                muteButton.Text = "Unmute";
            else
                muteButton.Text = "Mute";
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(statusComboBox.SelectedIndex)
            {
                case 1:
                    MasterAccount.Status = ChatStatus.Idle;
                    break;
                case 2:
                    MasterAccount.Status = ChatStatus.Busy;
                    break;
                default:
                    MasterAccount.Status = ChatStatus.Available;
                    break;
            }
        }
    }
}
