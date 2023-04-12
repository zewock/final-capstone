using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System;
using System.Data.SqlClient;
using Capstone.Models.DatabaseModles;

namespace Capstone.DAO
{
    public class ForumSqlDao
    {
        private readonly string connectionString; 
        public ForumSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //public Forum getForumById()
        //{   Forum forum = new Forum();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {

        //    }
        //}
        //public Forum createNewForum()
        //{

        //}
        //public Forum deleteForum()
        //{
        //}

        public Forum GetTransferFromReader(SqlDataReader reader)
        {
            Forum forum = new Forum(); 

            forum.forumId = Convert.ToInt32(reader["forum_id"]);
            forum.topic = Convert.ToString(reader["topic"]);
            forum.ownerId = Convert.ToInt32(reader["owner_id"]);
            forum.createdDate = Convert.ToDateTime(reader["create_date"]);
            forum.isVisible = Convert.ToBoolean(reader["isVisible"]);
            return forum;

        }
    }
}

        
 