using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Users
{
    public partial class Default : System.Web.UI.Page
    {
        private static List<UserInfoBoxButton> m_InfoControls;
        private static TableCell tc;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Properties.Pageloaded)
            {
                Controller.DBController db = new Controller.DBController();
                string result = db.TestDBStatus();

                Status_Label.Text = result;
                Properties.Pageloaded = true;
                m_InfoControls = new List<UserInfoBoxButton>();
                tc = this.User_TableCell;
            }
            foreach (Control control in m_InfoControls)
            {
                User_TableCell.Controls.Add(control);
                User_TableCell.Controls.Add(new LiteralControl("<br />"));
            }
            //for (int i = 0; i< 5; i++)
            //{
            //    UserInfoRadioButton rad= new UserInfoRadioButton();
            //    rad.UserTableName = tableName.Email;
            //    rad.UserUID = 1;
            //    rad.UserValue = "bla";
            //    User_TableCell.Controls.Add(rad);
            //    User_TableCell.Controls.Add(new LiteralControl("<br />"));
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    ListItem item = new ListItem();
            //    item.Text = "test" + i;
            //    item.Value = (i * 10).ToString();
            //    Users_DropDownList1.Items.Add(item);
            //}
        }

        protected void Connection_Button_Click(object sender, EventArgs e)
        {
            Properties.DBName = DB_TextBox.Text;
            Properties.User = User_TextBox.Text;
            Properties.Password = Pass_TextBox.Value;
            Controller.DBController db = new Controller.DBController();
            string result = db.TestDBStatus();

            Status_Label.Text = result;
        }

        protected void AddChangeUserName_Button_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(UserName_TextBox.Text))
            {
                return;
            }
            UserInfoBoxButton rad = new UserInfoBoxButton();
            if(m_InfoControls.Exists(m => m.UserTableName == tableName.User))
            {
                UserInfoBoxButton bt = m_InfoControls.Find(m => m.UserTableName == tableName.User);
                bt.UserValue = UserName_TextBox.Text;
                bt.Text = UserName_TextBox.Text;
            }
            else
            {
                rad.UserTableName = tableName.User;
                rad.UserUID = -1;
                rad.UserValue = UserName_TextBox.Text;
                rad.Text = "User Name:" + UserName_TextBox.Text;

                m_InfoControls.Add(rad);
            }
        }

        protected void NewMail_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewMail_TextBox.Text))
            {
                return;
            }
            UserInfoBoxButton rad = new UserInfoBoxButton();
            rad.UserTableName = tableName.Email;
            rad.UserUID = -1;
            rad.UserValue = NewMail_TextBox.Text;
            rad.Text = "User mail:" + NewMail_TextBox.Text;
            m_InfoControls.Add(rad);
        }

        protected void NewPhone_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewPhone_TextBox.Text))
            {
                return;
            }
            UserInfoBoxButton rad = new UserInfoBoxButton();
            rad.UserTableName = tableName.Phone;
            rad.UserUID = -1;
            rad.UserValue = NewPhone_TextBox.Text;
            rad.Text = "User phone:" +  NewPhone_TextBox.Text;
            m_InfoControls.Add(rad);
        }

        protected void AddRecord_Button_Click(object sender, EventArgs e)
        {
            if(Properties.UserDBStatus == dbStatus.NeedCreateDB)
            {
                DBData.CreateDB cDB = new DBData.CreateDB();
                cDB.CreateUserDB();
            }
            string UserName = "";
            int newUser = -2;
            List<string> UserMail = new List<string>();
            List<string> UserPhone = new List<string>();
            foreach (var boxControl in User_TableCell.Controls)
            {
                if (boxControl is UserInfoBoxButton)
                {
                    UserInfoBoxButton box = (UserInfoBoxButton)boxControl;
                    if (box.UserTableName == tableName.Email && box.UserUID == -1)
                    {
                        UserMail.Add(box.UserValue);
                    }
                    else if (box.UserTableName == tableName.Phone && box.UserUID == -1)
                    {
                        UserPhone.Add(box.UserValue);
                    }
                    else
                    {
                        UserName = box.UserValue;
                        newUser = box.UserUID;
                    }
                }
            }
            if (newUser == -1)
            {
                DBData.UserDataChange db = new DBData.UserDataChange();
                db.NewUser(UserName, UserMail, UserPhone);
            }
            if (newUser > -1)
            {
                DBData.UserDataChange db = new DBData.UserDataChange();
                db.OldUser(newUser, UserMail, UserPhone);
            }

        }
        
    }
}