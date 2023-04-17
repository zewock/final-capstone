using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Capstone.Models;
using Capstone.DAO;
using Capstone.Security;
using Capstone.Models.IncomingDTOs;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Net;
using Capstone.Models.OutgoingDTOs;
using Newtonsoft.Json;
using Capstone.Models.IntermediaryModles;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    { 
        private readonly IUserDao userDao;
        private readonly IForumDao forumDao;


        public ForumController(IForumDao _forumDao, IUserDao _userDoa)
        {
            forumDao = _forumDao;
            userDao = _userDoa;
        }

        [HttpGet("/ForumsList")]
        public ActionResult<ForumListDTO> getForums()
        {   
                int tokenUserId;
            string tokenUserName;
            string tokenUserRole;
            

            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
                tokenUserName = userDao.GetUser(User.Identity.Name).Username;
                tokenUserRole = userDao.GetUser(User.Identity.Name).Role;
            }
            catch (Exception)
            {
                tokenUserId = 0;
                tokenUserName = "";
                tokenUserRole = "";
            }
            try
            {
                ForumListDTO forumListDTO = forumDao.getAllForums(tokenUserRole, tokenUserName, tokenUserId);
                List<ForumModAndUsername> forumMods = forumDao.GetAllForumMods();
                List<ForumFavoriteAndUsername> forumFavorites = forumDao.GetAllForumFavorites();

                int modListIterator = 0;
                int favoriteListIterator = 0;
                for (int i = 0; i < forumListDTO.ForumArray.Length; i++)
                {
                    List<Forum_ModsArray> indvidualForumModList = new List<Forum_ModsArray>();
                    while (modListIterator < forumMods.Count && 
                        forumMods[modListIterator].forumId == forumListDTO.ForumArray[i].ForumID)
                    {
                        Forum_ModsArray forum_ModsArray = new Forum_ModsArray();
                        forum_ModsArray.Username = forumMods[modListIterator].username;
                        forum_ModsArray.UserID = forumMods[modListIterator].userId;
                        indvidualForumModList.Add(forum_ModsArray);
                        modListIterator++;
                    }
                    forumListDTO.ForumArray[i].Forums_ModsArrays = indvidualForumModList.ToArray();

                    List<Forums_FavoritesArray> indvidualForumFavoriteList = new List<Forums_FavoritesArray>();
                    while (favoriteListIterator < forumFavorites.Count &&
                        forumFavorites[favoriteListIterator].forumId == forumListDTO.ForumArray[i].ForumID)
                    {
                        Forums_FavoritesArray forums_FavoritesArray = new Forums_FavoritesArray();
                        forums_FavoritesArray.Username = forumFavorites[favoriteListIterator].username;
                        forums_FavoritesArray.UserID = forumFavorites[favoriteListIterator].userId;
                        indvidualForumFavoriteList.Add(forums_FavoritesArray);
                        favoriteListIterator++;
                    }
                    forumListDTO.ForumArray[i].Forums_FavoritesArrays = indvidualForumFavoriteList.ToArray();
                }
                forumListDTO.UserRole = tokenUserRole;


                //return Ok(json);
                return Ok(forumListDTO);

            }
            catch
            {
                return StatusCode(500, "Unable to get forums");
            }

        }

        [HttpPost("/CreateForum")]
        public ActionResult CreateForum(CreateForumDTO createForumDTO)
        {
            int tokenUserId;
            try 
            { 
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId; 
            }
            catch(Exception)
            {
                return StatusCode(401, "You need to be logged in to create a forum");
            }


            Forum forum = new Forum();
            forum.ownerId = tokenUserId;
            forum.title = createForumDTO.ForumTitle;
            forum.topic = createForumDTO.ForumTopic;
            forum.description = createForumDTO.ForumDescription;
            forum.isVisible = true;

            int forumID = forumDao.CreateForum(forum);

            ForumPost forumPost = new ForumPost();
            forumPost.Header = createForumDTO.PostHeader;
            forumPost.PostContent = createForumDTO.PostContent;
            forumPost.ImageURL = createForumDTO.PostImageURL;
            forumPost.UserID = tokenUserId;
            forumPost.ForumID = forumID;
            forumPost.IsVisable = true;
            forumPost.ParentPostID = null;

            forumDao.PostToForum(forumPost);


            return StatusCode(200, "New Forum Created");
        }
        /*
        [HttpPost("/ChangeFavoriteForumState")]

        public ActionResult ToggleForumFavorite(ChangeFavoritveForumStateDTO favoriteForum)
        {
            int tokenUserId; 
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch(Exception)
            {
                return StatusCode(401, "You need to be logged in to complete this action");
            }
            try
            {
                int favoriteForumId = favoriteForum.ForumId;
                forumDao.ToggleForumFavorites(tokenUserId, favoriteForumId);
            }
            catch(Exception)
            {
                return StatusCode(400, "Bad Request");
            }

            return StatusCode(200);
        }
        */
    }
}
