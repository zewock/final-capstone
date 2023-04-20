using System.Collections.Generic;
namespace Capstone.Models.OutgoingDTOs
{
    public class TopTenPopularPostsDTO
    {
        public TopTenPopularPostsArray[] TopTenPopularPostsArray { get; set; }

        public TopTenPopularPostsDTO()
        {

        }

        public TopTenPopularPostsDTO (List <TopTenPopularPostsArray> topTenPopularPostsArrays)
        {
            TopTenPopularPostsArray = topTenPopularPostsArrays.ToArray();
        }
    }
}
