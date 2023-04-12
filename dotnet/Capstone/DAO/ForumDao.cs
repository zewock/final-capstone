
using Capstone.Models;
using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System;
using System.Data.SqlClient;
using Capstone.Models.DatabaseModles;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public class ForumDao : IForumDao
    {
        private readonly string connectionString; 
        public ForumDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int CreateForum(Forum forum)
        {
            return 1;
        }



        /*
        public Forum getForumById(int userId)
        {   Forum forum = new Forum();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {

            }
        }
        

        //GetAllForums() - Retrieves all forums.
        public List<Forum> getAllForums()
        {

        }
        //GetFavoritedForumsByUserId(int userId) - Retrieves favorited forums for a specific user by ID.
        public List<Forum> getFavoritedForumsByUserId(int userID)
        {

        }
        //CreateForum(Forum forum) - Creates a new forum.
        public ActionResult<Forum> createForum()
        {

        }
        //UpdateForum(Forum forum) - Updates an existing forum.
        public ActionResult<Forum> updateForum(int forumId)
        {

        }
        //DeleteForum(int forumId) - Deletes a forum by ID.
        public ActionResult deleteForum(int forumId)
        {

        }
        */

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

        
    