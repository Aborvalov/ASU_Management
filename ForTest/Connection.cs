
using System.Configuration;

namespace ForTest
{
    internal class Connection
    {
        public static string ConStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
    }
}
