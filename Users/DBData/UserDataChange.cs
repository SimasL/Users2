using System.Collections.Generic;
using System.Data.SqlClient;

namespace Users.DBData
{
    public class UserDataChange
    {
        SqlConnection m_DBConnection;

        public void NewUser(string name, List<string> mail, List<string> phone)
        {
            ConnectToDB();
            m_DBConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = m_DBConnection;
            command.CommandText = "INSERT INTO [User] (name) VALUES ('" + name + "') ;SELECT CAST(scope_identity() as int)";
            int userUID = (int)command.ExecuteScalar();
            OldUser(userUID, mail, phone, command);
        }

        public void OldUser(int nameUID, List<string> mails, List<string> phones, SqlCommand command = null)
        {
            if(command == null)
            {
                ConnectToDB();
                m_DBConnection.Open();
                command = new SqlCommand();
            }
            foreach (string mail in mails)
            {
                command.Connection = m_DBConnection;
                command.CommandText = "INSERT INTO [Email] (USERKEY, email) VALUES ("+ nameUID + ",'" + mail + "')";
                command.ExecuteNonQuery();
            }
            foreach (string phone in phones)
            {
                command.Connection = m_DBConnection;
                command.CommandText = "INSERT INTO [Phone] (USERKEY, phone) VALUES (" + nameUID + ",'" + phone + "')";
                command.ExecuteNonQuery();
            }

        }


        private void ConnectToDB()
        {
            m_DBConnection = new SqlConnection("user id= " + Properties.User +
                                       ";password=" + Properties.Password + ";server=" + Properties.DBName +
                                       ";database=UserDB; ");

        }
    }
}