using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumPostsDTO
    {
        public int ForumID { get; set; }

        public string Topic { get; set; }

        public DateAndTime CreateDate { get; set; }

        public int OwnerID { get; set; }

        public string OwnerUsername { get; set; }

        public bool IsOwner { get; set; }

        public bool IsAnAdminForum { get; set; }

        public bool IsFavoritedForum { get; set; }

        public bool IsModerator { get; set; }

        public string UserRole { get; set; }

        public int TotalNumUpvotes { get; set; }

        public int TotalNumDownvotes { get; set; }

        public int UpvotesLast24Hours { get; set; }

        public int DownvotesLast24Hours { get; set; }

        public PostsArray[] PostsArray { get; set; }

        public Forums_FavoritesArray[] Forums_FavoritesArrays { get; set; }

        public Forum_ModsArray[] Forums_ModsArrays { get; set; }

        public ForumPostsDTO()
        {

        }

        public ForumPostsDTO(int forumID, string topic, DateAndTime createDate, int ownerID,
            string ownerUsername, bool isFavoritedForum, bool isModerator, int totalNumUpvotes,
            int totalNumDownvotes, int upvotesLast24Hours, int downvotesLast24Hours,
            List<PostsArray> forumPostsList, string userRole, bool isOwner, bool isAnAdminForum,
            List<Forums_FavoritesArray> forums_FavoritesArrays, List<Forum_ModsArray> forums_ModsArrays)
        {
            ForumID = forumID;
            Topic = topic;
            CreateDate = createDate;
            OwnerID = ownerID;
            OwnerUsername = ownerUsername;
            IsFavoritedForum = isFavoritedForum;
            IsModerator = isModerator;
            TotalNumUpvotes = totalNumUpvotes;
            TotalNumDownvotes = totalNumDownvotes;
            UpvotesLast24Hours = upvotesLast24Hours;
            DownvotesLast24Hours = downvotesLast24Hours;
            UserRole = userRole;
            IsOwner = isOwner;
            IsAnAdminForum = isAnAdminForum;
            PostsArray = forumPostsList.ToArray();
            Forums_FavoritesArrays = forums_FavoritesArrays.ToArray();
            Forums_ModsArrays = forums_ModsArrays.ToArray();
        }
    }
}
