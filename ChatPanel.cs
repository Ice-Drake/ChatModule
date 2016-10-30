using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using PluginSDK;

namespace ChatModule
{
    [PluginAttribute(AuthorName = "Que Trac", PluginName = "Chat Module", PluginVersion = "1.0")]
    public class ChatPanel : Form, IModule, IPanelPlugin
    {
        public event FormClosingEventHandler PanelClosing;

        public string PanelName
        {
            get
            {
                return "Chat";
            }
        }

        private ChatManager chatController;
        private FollowerChatControl followerChatControl;
        private Timer timer;

        #region Component Designer variables

        private IContainer components = null;
        private Button sendButton;
        private ComboBox chatComboBox;
        private TabControl tabControl;
        private TabPage tabPage1;
        private DataGridView allDataGridView;
        private DataGridViewTextBoxColumn allNameColumn;
        private DataGridViewTextBoxColumn allMessageColumn;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private TextBox textBox;

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
            this.sendButton = new System.Windows.Forms.Button();
            this.chatComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.allDataGridView = new System.Windows.Forms.DataGridView();
            this.allNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.allDataGridView)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(382, 360);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(43, 23);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // chatComboBox
            // 
            this.chatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chatComboBox.FormattingEnabled = true;
            this.chatComboBox.Location = new System.Drawing.Point(12, 362);
            this.chatComboBox.Name = "chatComboBox";
            this.chatComboBox.Size = new System.Drawing.Size(100, 21);
            this.chatComboBox.TabIndex = 6;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(413, 345);
            this.tabControl.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.allDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(405, 319);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ALL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // allDataGridView
            // 
            this.allDataGridView.AllowUserToAddRows = false;
            this.allDataGridView.AllowUserToDeleteRows = false;
            this.allDataGridView.AllowUserToResizeColumns = false;
            this.allDataGridView.AllowUserToResizeRows = false;
            this.allDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.allDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.allDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.allDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.allDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.allDataGridView.ColumnHeadersVisible = false;
            this.allDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.allNameColumn,
            this.allMessageColumn});
            this.allDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.allDataGridView.GridColor = System.Drawing.SystemColors.Window;
            this.allDataGridView.Location = new System.Drawing.Point(6, 6);
            this.allDataGridView.Name = "allDataGridView";
            this.allDataGridView.ReadOnly = true;
            this.allDataGridView.RowHeadersVisible = false;
            this.allDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.allDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.allDataGridView.Size = new System.Drawing.Size(393, 307);
            this.allDataGridView.TabIndex = 0;
            this.allDataGridView.SelectionChanged += new System.EventHandler(allDataGridView_SelectionChanged);
            // 
            // allNameColumn
            // 
            this.allNameColumn.HeaderText = "Name";
            this.allNameColumn.Name = "NameColumn";
            this.allNameColumn.ReadOnly = true;
            // 
            // allMessageColumn
            // 
            this.allMessageColumn.HeaderText = "Message";
            this.allMessageColumn.Name = "MessageColumn";
            this.allMessageColumn.ReadOnly = true;
            this.allMessageColumn.Width = 290;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 388);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(437, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Image = global::ChatModule.Properties.Resources.Available;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel.Text = "Available";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(118, 362);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(258, 20);
            this.textBox.TabIndex = 11;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
            // 
            // ChatPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(437, 410);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.chatComboBox);
            this.Controls.Add(this.sendButton);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ChatPanel_FormClosing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChatPanel";
            this.Text = "Chat Panel";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.allDataGridView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ChatPanel()
        {
            InitializeComponent();
            chatController = new ChatManager();
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            sendButton.Enabled = false;

            followerChatControl = new FollowerChatControl();
            chatController.addSource(followerChatControl);
            FollowerChatUser mainUser = new FollowerChatUser("User");
            UserAccount mainAccount = followerChatControl.createUser("Boss");
            mainAccount.Owner = mainUser;
            followerChatControl.addUser(mainAccount);
            mainUser.add(mainAccount);
            chatController.addUser(mainUser);
            chatController.changeMainUser(mainUser);

            TabPage tabPage = new TabPage();
            tabPage.SuspendLayout();
            tabPage.Controls.Add(followerChatControl);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(405, 319);
            tabPage.Text = followerChatControl.SourceName.ToUpper();
            tabPage.UseVisualStyleBackColor = true;
            tabControl.Controls.Add(tabPage);

            chatComboBox.Items.Add(followerChatControl.SourceName.ToUpper());
            chatComboBox.SelectedIndex = 0;
        }

        public void activate()
        {
            timer.Start();
            sendButton.Enabled = true;

            foreach (IChatUser chatUser in chatController.ChatUsers)
            {
                if (chatUser is IFollower)
                    ((IFollower)chatUser).start();
            }
        }

        public void attachPlugin(object plugin)
        {
            if (plugin is IChatFormPlugin || plugin is IChatWPFPlugin)
            {
                chatController.addSource((IChatSource)plugin);
            }
            else if (plugin is IFollowerPlugin)
            {
                IFollowerPlugin follower = (IFollowerPlugin)plugin;
                follower.loadDatabase();
                UserAccount followerAccount = followerChatControl.createUser(follower.Profile.Name);
                followerAccount.Owner = follower;
                follower.add(followerAccount);
                followerChatControl.addUser(followerAccount);
                chatController.addUser(follower);
            }
        }

        public bool checkValidPlugin(Type type)
        {
            if ((typeof(IChatFormPlugin)).IsAssignableFrom(type) || (typeof(IChatWPFPlugin)).IsAssignableFrom(type) || (typeof(IFollowerPlugin)).IsAssignableFrom(type))
                return true;
            else
                return false;
        }

        public void showPanel()
        {
            Show();
        }

        public void hidePanel()
        {
            Hide();
        }

        private void addChatSource(IChatSource newSource)
        {
            if(chatController.addSource(newSource))
            {
                chatComboBox.Items.Add(newSource.SourceName.ToUpper());

                if (newSource is IChatFormPlugin)
                {
                    TabPage tabPage = new TabPage();
                    tabPage.SuspendLayout();
                    tabPage.Controls.Add((IChatFormPlugin)newSource);
                    tabPage.Location = new System.Drawing.Point(4, 22);
                    tabPage.Padding = new System.Windows.Forms.Padding(3);
                    tabPage.Size = new System.Drawing.Size(405, 319);
                    tabPage.Text = newSource.SourceName.ToUpper();
                    tabPage.UseVisualStyleBackColor = true;

                    tabControl.Controls.Add(tabPage);

                    if (newSource is IChatFormPlugin)
                        ((IChatFormPlugin)newSource).StatusChanged += new PropertyChangedEventHandler(newSource_StatusChanged);
                }
                else //if (newSource is IChatWPFPlugin)
                {
                    
                }
            }
        }

        private void sendMessage()
        {
            if (sendButton.Enabled)
            {
                string name;
                if (chatController.MainUser.Profile != null)
                    name = chatController.MainUser.Profile.Name;
                else
                    name = "User";
                IChatSource source = chatController.getSource(chatComboBox.SelectedItem.ToString());
                int row = allDataGridView.Rows.Add();
                allDataGridView.Rows[row].DefaultCellStyle.ForeColor = source.TextColor;
                allDataGridView.Rows[row].Cells["NameColumn"].Value = name;
                allDataGridView.Rows[row].Cells["MessageColumn"].Value = textBox.Text;

                string username;
                IList<UserAccount> accounts = chatController.MainUser.retrieve(source);
                if (accounts != null && accounts.Count > 0)
                    username = accounts[0].Username;
                else
                {
                    //Need to randomize username for other user
                    username = "Master";
                }
                chatController.send(new ChatSourceMessage(source, username, textBox.Text));
                textBox.Text = "";

                //Autoscroll down
                int displayed = allDataGridView.DisplayedRowCount(true);
                if (allDataGridView.FirstDisplayedScrollingRowIndex >= 0 && displayed < allDataGridView.RowCount)
                    allDataGridView.FirstDisplayedScrollingRowIndex = allDataGridView.RowCount - displayed;
            }
        }

        private void allDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            allDataGridView.ClearSelection();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ChatSourceMessage[] msgs;
            
            lock(chatController.NewMessages)
            {
                msgs = chatController.NewMessages.ToArray();
                chatController.NewMessages.Clear();
            }

            if (msgs.Length > 0)
            {
                foreach (ChatSourceMessage msg in msgs)
                {
                    int row = allDataGridView.Rows.Add();
                    allDataGridView.Rows[row].DefaultCellStyle.ForeColor = msg.Source.TextColor;
                    allDataGridView.Rows[row].Cells["NameColumn"].Value = msg.Sender;
                    allDataGridView.Rows[row].Cells["MessageColumn"].Value = msg.Message;
                }

                //Autoscroll down
                int displayed = allDataGridView.DisplayedRowCount(true);
                if (allDataGridView.FirstDisplayedScrollingRowIndex >= 0 && displayed < allDataGridView.RowCount)
                    allDataGridView.FirstDisplayedScrollingRowIndex = allDataGridView.RowCount - displayed;

                /*if (!MenuItem.Checked)
                {
                    MenuItem.Checked = true;
                    Show();
                }*/
            }
        }

        private void newSource_StatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IChatFormPlugin)
            {
                IChatFormPlugin source = (IChatFormPlugin)sender;
                switch(source.Status)
                {
                    case SourceStatus.Offline:
                        toolStripStatusLabel.Image = ChatModule.Properties.Resources.Offline;
                        toolStripStatusLabel.Text = "Offline";
                        break;
                    default:
                        toolStripStatusLabel.Image = ChatModule.Properties.Resources.Available;
                        toolStripStatusLabel.Text = "Online";
                        break;
                }
            }
            else if (sender is IChatWPFPlugin)
            {
                IChatWPFPlugin source = (IChatWPFPlugin)sender;
                switch (source.Status)
                {
                    case SourceStatus.Offline:
                        toolStripStatusLabel.Image = ChatModule.Properties.Resources.Offline;
                        toolStripStatusLabel.Text = "Offline";
                        break;
                    default:
                        toolStripStatusLabel.Image = ChatModule.Properties.Resources.Available;
                        toolStripStatusLabel.Text = "Online";
                        break;
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                sendMessage();
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            sendMessage();
        }

        private void ChatPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            if (PanelClosing != null)
                PanelClosing(this, e);
        }
    }
}
