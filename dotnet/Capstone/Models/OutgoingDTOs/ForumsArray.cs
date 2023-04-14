


using System;
using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumsArray
    {
        public int ForumID { get; set; }

        public string Topic { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }
        public string OwnerUsername { get; set; }

        public int OwnerID { get; set; }

        public int TotalNumUpvotes { get; set; }

        public int TotalNumDownvotes { get; set; }

        public int UpvotesLast24Hours { get; set; }

        public int DownvotesLast24Hours { get; set; }

        public bool IsModerator { get; set; }

        public bool IsFavoriteForum { get; set; }

        public bool IsOwner { get; set; }

        public bool IsAnAdminForum { get; set; }

        public Forums_FavoritesArray[] Forums_FavoritesArrays { get; set; }

        public Forum_ModsArray[] Forums_ModsArrays { get; set; }

        public ForumsArray()
        {

        }

        public ForumsArray(int forumID, string topic, DateTime createDate, int ownerID,
            string ownerUsername, int totalNumUpvotes, int totalNumDownvotes, int upvotesLast24Hours,
            bool isModerator, bool isFavoriteForum, bool isOwner, List<Forums_FavoritesArray> forums_FavoritesArrays,
            List<Forum_ModsArray> forums_ModsArrays, bool isAnAdminForum)
        {
            ForumID = forumID;
            Topic = topic;
            CreateDate = createDate;
            OwnerID = ownerID;
            OwnerUsername = ownerUsername;
            TotalNumUpvotes = totalNumUpvotes;
            TotalNumDownvotes = totalNumDownvotes;
            UpvotesLast24Hours = upvotesLast24Hours;
            UpvotesLast24Hours = upvotesLast24Hours;
            IsModerator = isModerator;
            IsFavoriteForum = isFavoriteForum;
            IsOwner = isOwner;
            IsAnAdminForum= isAnAdminForum;
            Forums_FavoritesArrays = forums_FavoritesArrays.ToArray();
            Forums_ModsArrays = forums_ModsArrays.ToArray();
            IsAnAdminForum = isAnAdminForum;
        }
    }
}
