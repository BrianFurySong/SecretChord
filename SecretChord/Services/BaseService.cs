using DbConnector.Adapter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SecretChord.Services
{
    public abstract class BaseService //abstarct so it HAS TO BE inherited
    {
        public DbAdapter Adapter
        {
            get
            {
                //return new DbAdapter(new SqlCommand(), new SqlConnection("Server = 13.64.246.7;Database = C45_LeaseHold; User Id = C45_User; Password = Sabiopass1!")); //to connect to a offsite server
                return new DbAdapter(new SqlCommand(), new SqlConnection("Server = BRIANFURYSONG\\SQLEXPRESS; Database = SecretChord; Trusted_Connection = True;"));

            }
        }
    }
}
