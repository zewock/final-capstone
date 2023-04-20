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
using Capstone.Services;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    { 
        private readonly IUserDao userDao;
        private readonly IForumDao forumDao;
        private readonly IRandomFactService randomFactService;

        public ForumController(IForumDao _forumDao, IUserDao _userDoa, IRandomFactService _randomFactService)
        {
            forumDao = _forumDao;
            userDao = _userDoa;
            randomFactService = _randomFactService;
        }

        [HttpGet("/ForumsList")]
        public ActionResult<ForumListDTO> getForums()
        {
            try
            {
                int tokenUserId;
                string tokenUserName;
                string tokenUserRole;

                try
                {
                    UserData userData = userDao.GetUserData(User.Identity.Name);
                    tokenUserId = userData.UserID;
                    tokenUserName = userData.Username;
                    tokenUserRole = userData.Userrole;
                    if(userData.RestoreBanTime > DateTime.Now)
                    {
                        return StatusCode(401, "" + userData.Username + " is currently banned until " + userData.RestoreBanTime);
                    }
                }
                catch (Exception)
                {
                    tokenUserId = 0;
                    tokenUserName = "";
                    tokenUserRole = "";
                }
            
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
                forumListDTO.RandomFact = randomFactService.GetRandomFact();

                //return Ok(json);
                return Ok(forumListDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while getting forums");
            }

        }

        [HttpPost("/CreateForum")]
        public ActionResult CreateForum(CreateForumDTO createForumDTO)
        {
            try {
                UserData userData = new UserData();
                try 
                {
                    userData = userDao.GetUserData(User.Identity.Name);
                    if (userData.RestoreBanTime > DateTime.Now)
                    {
                        return StatusCode(401, "" + userData.Username + " is currently banned until " + userData.RestoreBanTime);
                    }
                }
                catch(Exception)
                {
                    return StatusCode(401, "You need to be logged in to create a forum");
                }

                Forum forum = new Forum();
                forum.ownerId = userData.UserID;
                forum.title = createForumDTO.ForumTitle;
                forum.topic = createForumDTO.ForumTopic;
                forum.description = createForumDTO.ForumDescription;
                forum.isVisible = true;

                int forumID = forumDao.CreateForum(forum);
                /*
                ForumPost forumPost = new ForumPost();
                forumPost.Header = createForumDTO.PostHeader;
                forumPost.PostContent = createForumDTO.PostContent;
                forumPost.ImageURL = createForumDTO.PostImageURL;
                forumPost.UserID = userData.UserID;
                forumPost.ForumID = forumID;
                forumPost.IsVisable = true;

                forumDao.PostToForum(forumPost);
                */
                return StatusCode(200, "New Forum Created");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating forum");
            }
        }

        [HttpPut("/DeleteForum")]
        public ActionResult DeleteForum(DeleteForumDTO deleteForumDTO)
        {
            try
            {
                UserData userData = new UserData();
                try
                {
                    userData = userDao.GetUserData(User.Identity.Name);
                    if (userData.RestoreBanTime > DateTime.Now)
                    {
                        return StatusCode(401, "" + userData.Username + " is currently banned until " + userData.RestoreBanTime);
                    }
                }
                catch (Exception)
                {
                    return StatusCode(401, "You need to be logged in to delete a forum");
                }

                int forumOwnerID = forumDao.GetForumOwnerUserID(deleteForumDTO.ForumId);

                if (userData.Userrole == "admin" || forumOwnerID == userData.UserID)
                {
                    forumDao.DeletePost(deleteForumDTO.ForumId);
                    return StatusCode(200, "Forum successfully deleted");
                }
                else
                {
                    return StatusCode(401, "Only admins or the post owner can delete this forum");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting forum");
            }
        }

        [HttpGet("/TopTenPopularPosts")]
        public ActionResult<ForumListDTO> GetTopTenPopularPosts()
        {
            try
            {
                TopTenPopularPostsDTO topTenPopularPostsDTO = new TopTenPopularPostsDTO();
                List<TopTenPopularPostsArray> topTenPopularPostsArray = forumDao.GetTopTenPopularPost();
                topTenPopularPostsDTO.TopTenPopularPostsArray = topTenPopularPostsArray.ToArray();
                return Ok(topTenPopularPostsDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while getting top 10 posts");
            }
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
