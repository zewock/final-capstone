﻿


using System;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumsArray
    {
        public int ForumID { get; set; }

        public string Topic { get; set; }

        public DateTime CreateDate { get; set; }

        public int OwnerID { get; set; }

        public string OwnerUsername { get; set; }

        public int TotalNumUpvotes { get; set; }

        public int TotalNumDownvotes { get; set; }

        public int UpvotesLast24Hours { get; set; }

        public int DownvotesLast24Hours { get; set; }

        public bool IsModerator { get; set; }

        public bool IsFavoriteForum { get; set; }

        public ForumsArray()
        {

        }

        public ForumsArray(int forumID, string topic, DateTime createDate, int ownerID,
            string ownerUsername, int totalNumUpvotes, int totalNumDownvotes, int upvotesLast24Hours,
            bool isModerator, bool isFavoriteForum)
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
        }
    }
}