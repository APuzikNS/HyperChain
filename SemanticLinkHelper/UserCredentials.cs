using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticLinkHelper
{
    class UserCredentials
    {
        string m_User;
        string m_sPassword;
        
        public string User
        {
            get { return m_User; }
            set { m_User = value; }
        }

        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }
    }
}
