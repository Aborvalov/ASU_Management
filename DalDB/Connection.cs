using System.Configuration;

namespace DalDB
{
    internal class Connection
    {
        public static string ConStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
    }
}
