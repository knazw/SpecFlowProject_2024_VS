using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.DataAccess
{
    internal class ApiEndpoints
    {
        public static readonly string pathDataSeed = "/testData/seed";
        public static readonly string pathTransactions = "/transactions/";
        public static readonly string pathLikesTransaction = "/likes/";
        public static readonly string pathNotifications = "/notifications/";
        public static readonly string pathComments = "/comments/";
        public static readonly string pathUsers = "/users";
        public static readonly string pathLogin = "/login";
    }
}
