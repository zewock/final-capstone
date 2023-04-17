using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Capstone.Models.IntermediaryModles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostDao postDao;
        private readonly IUserDao userDao;


        public PostController(IPostDao _postDao, IUserDao userDao)
        {
            postDao = _postDao;
            this.userDao = userDao;
        }

        [HttpGet("/ForumPosts/{forumId}")]


        public ActionResult<List<ForumPostWithVotesAndUserName>> GetPostsByForumId(int forumId)
        {
            // need to pass in the user id to get information on whether the user has upvoted or downvoted posts
            // as well as has the user favorited this forum/ as a mod of this forum
            try
            {
                var posts = postDao.GetPostsByForumId(forumId);
                return Ok(posts);
            }
            catch (Exception)
            {

                return StatusCode(500, new { message = "An error occurred while fetching the posts." });
            }
        }

        [HttpGet("/PostById/{postId}")]

        public ActionResult<List<ForumPostWithVotesAndUserName>> GetPostsByPostId(int postId)
        {
            try
            {
                var posts = postDao.GetPostsByForumId(postId);
                return Ok(posts);
            }
            catch (Exception)
            {

                return StatusCode(500, new { message = "An error occurred while fetching the posts." });
            }
        }

        [HttpGet("/Posts/{keyword}")]
        public ActionResult<List<ForumPostWithVotesAndUserName>> SearchPosts(string keyword)
        {
            try
            {
                var posts = postDao.SearchPostsForKeyword(keyword);
                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while fetching the posts." });
            }
        }

        [HttpPost("/PostToForum")]
        public ActionResult PostToForum(PostToForumDTO postToForumDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged in to post to a forum");
            }

            try
            {
                postDao.PostToForum(postToForumDTO, tokenUserId);
                return StatusCode(200, "Post successfully added");
            }
            catch (Exception)
            {
                return StatusCode(500, "Post was unable to be added");
            }
        }

        [HttpPut("/DeletePost")]
        public ActionResult DeletePost(DeletePostDTO deletePostDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged in to delete a post");
            }

            string tokenUserRole = userDao.GetUserRoleById(tokenUserId);
            int postOwnerID = postDao.GetUserIDByPostID(deletePostDTO.PostID);
            List<int> modsID = postDao.GetModsIDsByForumID(deletePostDTO.FormID);

            if (postOwnerID == tokenUserId || tokenUserRole == "admin" || modsID.Contains(tokenUserId))
            {
                postDao.DeletePost(deletePostDTO.PostID);
                return StatusCode(200, "Post successfully deleted");
            }
            else
            {
                return StatusCode(401, "Only a admins, mods, or the post owner can delete a post");
            }
        }

        [HttpPost("/AddMod")]
        public ActionResult AddMod (AddRemoveModDTO addRemoveModDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged to add a mod");
            }

            string tokenUserRole = userDao.GetUserRoleById(tokenUserId);
            int isUserOwnerOfForum = postDao.IsUserOwnerOfForum(tokenUserId, addRemoveModDTO.formID);

            if (tokenUserRole == "admin" || isUserOwnerOfForum == 1)
            {
                if (userDao.IsUsernameInDatabase(addRemoveModDTO.username) != 1)
                {
                    return StatusCode(401, "" + addRemoveModDTO.username + " is not in the database");
                }
                else
                {
                    if(postDao.IsUserModOfForum(addRemoveModDTO.username, addRemoveModDTO.formID) == 1)
                    {
                        return StatusCode(401, "" + addRemoveModDTO.username + " is already in the database as a mod of this forum");
                    }
                    else
                    {
                        int modUserID = userDao.GetUserIDByUsername(addRemoveModDTO.username);
                        postDao.AddMod(modUserID, addRemoveModDTO.formID);
                        return StatusCode(200, "Mod successfully added");
                    }
                }
            }
            else
            {
                return StatusCode(401, "Only an admin or the owner of the forum can promote mods");
            }

        }

        [HttpPost("/RemoveMod")]
        public ActionResult RemoveMod(AddRemoveModDTO addRemoveModDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged to remove a mod");
            }

            string tokenUserRole = userDao.GetUserRoleById(tokenUserId);
            int isUserOwnerOfForum = postDao.IsUserOwnerOfForum(tokenUserId, addRemoveModDTO.formID);

            if (tokenUserRole == "admin" || isUserOwnerOfForum == 1)
            {
                if (userDao.IsUsernameInDatabase(addRemoveModDTO.username) != 1)
                {
                    return StatusCode(401, "" + addRemoveModDTO.username + " is not in the database");
                }
                else
                {
                    if (postDao.IsUserModOfForum(addRemoveModDTO.username, addRemoveModDTO.formID) <= 0)
                    {
                        return StatusCode(401, "" + addRemoveModDTO.username + " is not a mod of this forum");
                    }
                    else
                    {
                        int modUserID = userDao.GetUserIDByUsername(addRemoveModDTO.username);
                        postDao.RemoveMod(modUserID, addRemoveModDTO.formID);
                        return StatusCode(200, "Mod successfully removed");
                    }
                }
            }
            else
            {
                return StatusCode(401, "Only an admin or the owner of the forum can demote mods");
            }
        }

        [HttpPost("/ChangeFavoriteState")]
        public ActionResult ChangeFavoriteState(ChangeFavoritveForumStateDTO changeFavoritveForumStateDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged to favorite a page");
            }
            
            if(postDao.doseForumExist(changeFavoritveForumStateDTO.ForumId) >= 0)
            {
                return StatusCode(401, "Forum dose not exist");
            }

            if (postDao.isForumFavorited(tokenUserId, changeFavoritveForumStateDTO.ForumId) == 1)
            {
                postDao.RemoveFavorite(tokenUserId, changeFavoritveForumStateDTO.ForumId);
                return StatusCode(200, "Forum successfully unfavorited");
            }
            else
            {
                postDao.AddFavoriteForum(tokenUserId, changeFavoritveForumStateDTO.ForumId);
                return StatusCode(200, "Forum successfully favorited");
            }
        }

        [HttpPut("/ChangeUpvoteState")]
        public ActionResult ChangeUpvoteState(ChangeUpvoteDownvoteStateDTO changeUpvoteDownvoteStateDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged to upvote or downvote");
            } 

            if (postDao.doseForumExist(changeUpvoteDownvoteStateDTO.PostID) >= 0)
            {
                return StatusCode(401, "No such post exists");
            }
            IsUpvotedDownVoted isUpvotedDownVoted = new IsUpvotedDownVoted();
            isUpvotedDownVoted.postID = -1;
            isUpvotedDownVoted = postDao.GetPostsUpvotesDownvotes
                (tokenUserId, changeUpvoteDownvoteStateDTO.PostID, isUpvotedDownVoted);
            if(isUpvotedDownVoted.postID == -1)
            {
                //add new object
            }
            else if (isUpvotedDownVoted.isUpvoted)
            {
                //remove object
            }
            else
            {
                //change states of curret object
            }

            return StatusCode(200, "Post successfully upvoted");
        }



        /* [HttpPost("/PostToForum")]


         public IActionResult PostToForum(PostToForumDTO PostToForumDTO)
         {
             int tokenUserId;
             try
             {
                 tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
             }
             catch (Exception)
             {
                 return StatusCode(401, "You need to be logged in to post to a forum");
             }

             ForumPost forumPost = new ForumPost();
             forumPost.isVisible = true;
             forumPost.image = PostToForumDTO.Image;
             forumPost.content = PostToForumDTO.Content;
             forumPost.forumId = PostToForumDTO.ForumID;
             forumPost.postId = tokenUserId;


             return StatusCode(201, "Forum post successfully added to database ");
         }*/
    }
}