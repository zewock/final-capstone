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
                    
                }

                var posts = postDao.GetPostsByForumId(forumId);
                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while getting the posts" });
            }
        }

        [HttpGet("/PostById/{postId}")]

        public ActionResult<List<ForumPostWithVotesAndUserName>> GetPostsByPostId(int postId)
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

                }

                var posts = postDao.GetPostsByForumId(postId);
                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while getting the post" });
            }
        }

        [HttpGet("/Posts/{keyword}")]
        public ActionResult<List<ForumPostWithVotesAndUserName>> SearchPosts(string keyword)
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

                }

                var posts = postDao.SearchPostsForKeyword(keyword);
                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while searching through posts" });
            }
        }

        [HttpPost("/PostToForum")]
        public ActionResult PostToForum(PostToForumDTO postToForumDTO)
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
                    return StatusCode(401, "You need to be logged in to create a post");
                }

                if (!postDao.DoseForumExist(postToForumDTO.ForumID))
                {
                    return StatusCode(401, "Forum dose not exist");
                }

                if (postToForumDTO.ParentPostID != null)
                {
                    if (!postDao.DosePostExist(postToForumDTO.ParentPostID ?? default, postToForumDTO.ForumID))
                    {
                        return StatusCode(401, "Parent post does not exist");
                    }
                }

                postDao.PostToForum(postToForumDTO, userData.UserID);
                return StatusCode(200, "Post successfully added");
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while creating post" });
            }
        }

        [HttpPut("/DeletePost")]
        public ActionResult DeletePost(DeletePostDTO deletePostDTO)
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
                    return StatusCode(401, "You need to be logged in to delete a post");
                }

                int postOwnerID = postDao.GetUserIDByPostID(deletePostDTO.PostID);
                List<int> modsID = postDao.GetModsIDsByForumID(deletePostDTO.FormID);
                int forumOwnerID = postDao.GetForumOwnerUserID(deletePostDTO.FormID);

                if (postOwnerID == userData.UserID || userData.Userrole == "admin"
                    || modsID.Contains(userData.UserID) || forumOwnerID == userData.UserID)
                {
                    List<int> parentPostIDsToDeleteList = new List<int>() { deletePostDTO.PostID };

                    List<IDsAndParentIDsPostsInForum> idsAndParentIDsPostsInForumList = 
                        postDao.GetIDsAndParentIDsInForm(deletePostDTO.FormID, deletePostDTO.PostID);

                    for (int i = 0; i < idsAndParentIDsPostsInForumList.Count; i++)
                    {
                        for (int j = 0; j < parentPostIDsToDeleteList.Count; j++)
                        {
                            if (idsAndParentIDsPostsInForumList[i].ParentPostID == parentPostIDsToDeleteList[j])
                            {
                                for (int k = i + 1; k < idsAndParentIDsPostsInForumList.Count; k++)
                                {
                                    if(idsAndParentIDsPostsInForumList[i].PostID == idsAndParentIDsPostsInForumList[k].ParentPostID)
                                    {
                                        parentPostIDsToDeleteList.Add(idsAndParentIDsPostsInForumList[i].PostID);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    for(int i = 0; i < parentPostIDsToDeleteList.Count; i++)
                    {
                        postDao.DeletePost(parentPostIDsToDeleteList[i]);
                    }

                    //postDao.DeletePost(deletePostDTO.PostID);
                    return StatusCode(200, "Post successfully deleted");
                }
                else
                {
                    return StatusCode(401, "Only a admins, mods, forum owner, or post owner can delete a post");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while deleting post" });
            }
        }

        [HttpPost("/AddMod")]
        public ActionResult AddMod (AddRemoveModDTO addRemoveModDTO)
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
                    return StatusCode(401, "You need to be logged in to add a mod");
                }

                int forumOwnerID = postDao.GetForumOwnerUserID(addRemoveModDTO.formID);

                if (userData.Userrole == "admin" || forumOwnerID == userData.UserID)
                {
                    if (userDao.IsUsernameInDatabase(addRemoveModDTO.username) != 1)
                    {
                        return StatusCode(401, "" + addRemoveModDTO.username + " is not in the database");
                    }
                    else
                    {
                        if (postDao.IsUserModOfForum(addRemoveModDTO.username, addRemoveModDTO.formID) == 1)
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
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while adding mod" });
            }
        }


        [HttpPost("/RemoveMod")]
        public ActionResult RemoveMod(AddRemoveModDTO addRemoveModDTO)
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
                    return StatusCode(401, "You need to be logged in to remove a mod");
                }

                int forumOwnerID = postDao.GetForumOwnerUserID(addRemoveModDTO.formID);

                if (userData.Userrole == "admin" || forumOwnerID == userData.UserID)
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
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while removing mod" });
            }
        }


        [HttpPost("/ChangeFavoriteState")]
        public ActionResult ChangeFavoriteState(ChangeFavoritveForumStateDTO changeFavoritveForumStateDTO)
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
                    return StatusCode(401, "You need to be logged in to favorite a page");
                }

                if (!postDao.DoseForumExist(changeFavoritveForumStateDTO.ForumId))
                {
                    return StatusCode(401, "Forum dose not exist");
                }

                if (postDao.IsForumFavorited(userData.UserID, changeFavoritveForumStateDTO.ForumId) == 1)
                {
                    postDao.RemoveFavorite(userData.UserID, changeFavoritveForumStateDTO.ForumId);
                    return StatusCode(200, "Forum successfully unfavorited");
                }
                else
                {
                    postDao.AddFavoriteForum(userData.UserID, changeFavoritveForumStateDTO.ForumId);
                    return StatusCode(200, "Forum successfully favorited");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while changing favorite state" });
            }
        }


        [HttpPut("/ChangeUpvoteState")]
        public ActionResult ChangeUpvoteState(ChangeUpvoteDownvoteStateDTO changeUpvoteDownvoteStateDTO)
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
                    return StatusCode(401, "You need to be logged in to upvote or downvote");
                }

                if (!postDao.DosePostExist(changeUpvoteDownvoteStateDTO.PostID,
                    changeUpvoteDownvoteStateDTO.ForumID))
                {
                    return StatusCode(401, "No such post exists");
                }

                IsUpvotedDownVoted isUpvotedDownVoted = new IsUpvotedDownVoted();
                isUpvotedDownVoted.postID = -1;
                isUpvotedDownVoted = postDao.GetPostsUpvotesDownvotes
                    (userData.UserID, changeUpvoteDownvoteStateDTO.PostID, isUpvotedDownVoted);
                if (isUpvotedDownVoted.postID == -1)
                {
                    //add new object
                    PostsUpvotesDownvotes postsUpvotesDownvotes = new PostsUpvotesDownvotes();
                    postsUpvotesDownvotes.isDownVoted = false;
                    postsUpvotesDownvotes.isUpVoted = true;
                    postsUpvotesDownvotes.forumId = changeUpvoteDownvoteStateDTO.ForumID;
                    postsUpvotesDownvotes.postId = changeUpvoteDownvoteStateDTO.PostID;
                    postsUpvotesDownvotes.userId = userData.UserID;
                    postDao.CreateUpvoteDownvote(postsUpvotesDownvotes);
                    return StatusCode(200, "Post successfully upvoted");
                }
                else if (isUpvotedDownVoted.isUpvoted)
                {
                    //remove object
                    postDao.DeleteUpvoteDownvote(userData.UserID, changeUpvoteDownvoteStateDTO.PostID);
                    return StatusCode(200, "Post successfully deupvoted");
                }
                else
                {
                    //change states of curret object
                    PostsUpvotesDownvotes postsUpvotesDownvotes = new PostsUpvotesDownvotes();
                    postsUpvotesDownvotes.isDownVoted = false;
                    postsUpvotesDownvotes.isUpVoted = true;
                    postsUpvotesDownvotes.postId = changeUpvoteDownvoteStateDTO.PostID;
                    postsUpvotesDownvotes.userId = userData.UserID;
                    postsUpvotesDownvotes.createDate = DateTime.Now;
                    postDao.UpdateUpvoteDownvote(postsUpvotesDownvotes);
                    return StatusCode(200, "Post successfully switched to upvoted");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while changing upvote state" });
            }
        }

        [HttpPut("/ChangeDownvoteState")]
        public ActionResult ChangeDownvoteState(ChangeUpvoteDownvoteStateDTO changeUpvoteDownvoteStateDTO)
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
                    return StatusCode(401, "You need to be logged in to upvote or downvote");
                }

                if (!postDao.DosePostExist(changeUpvoteDownvoteStateDTO.PostID,
                    changeUpvoteDownvoteStateDTO.ForumID))
                {
                    return StatusCode(401, "No such post exists");
                }

                IsUpvotedDownVoted isUpvotedDownVoted = new IsUpvotedDownVoted();
                isUpvotedDownVoted.postID = -1;
                isUpvotedDownVoted = postDao.GetPostsUpvotesDownvotes
                    (userData.UserID, changeUpvoteDownvoteStateDTO.PostID, isUpvotedDownVoted);
                if (isUpvotedDownVoted.postID == -1)
                {
                    //add new object
                    PostsUpvotesDownvotes postsUpvotesDownvotes = new PostsUpvotesDownvotes();
                    postsUpvotesDownvotes.isDownVoted = true;
                    postsUpvotesDownvotes.isUpVoted = false;
                    postsUpvotesDownvotes.forumId = changeUpvoteDownvoteStateDTO.ForumID;
                    postsUpvotesDownvotes.postId = changeUpvoteDownvoteStateDTO.PostID;
                    postsUpvotesDownvotes.userId = userData.UserID;
                    postDao.CreateUpvoteDownvote(postsUpvotesDownvotes);
                    return StatusCode(200, "Post successfully downvoted");
                }
                else if (isUpvotedDownVoted.isDownvoted)
                {
                    //remove object
                    postDao.DeleteUpvoteDownvote(userData.UserID, changeUpvoteDownvoteStateDTO.PostID);
                    return StatusCode(200, "Post successfully dedownvoted");
                }
                else
                {
                    //change states of curret object
                    PostsUpvotesDownvotes postsUpvotesDownvotes = new PostsUpvotesDownvotes();
                    postsUpvotesDownvotes.isDownVoted = true;
                    postsUpvotesDownvotes.isUpVoted = false;
                    postsUpvotesDownvotes.postId = changeUpvoteDownvoteStateDTO.PostID;
                    postsUpvotesDownvotes.userId = userData.UserID;
                    postsUpvotesDownvotes.createDate = DateTime.Now;
                    postDao.UpdateUpvoteDownvote(postsUpvotesDownvotes);
                    return StatusCode(200, "Post successfully switched to downvoted");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while changing downvote state" });
            }
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