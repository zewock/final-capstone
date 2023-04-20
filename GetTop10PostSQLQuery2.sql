select * from Users

select * from Forums

select * from Forum_Posts

select (SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id > DATEADD(day, -1, GETDATE()) AND p.is_upvoted = 1) AS upvoteswithin24hours from Post_Upvotes_Downvotes

select SUM((select count(*) from Post_Upvotes_Downvotes p where p.is_upvoted = 1 and p.create_date > DATEADD(day, -1, GETDATE())) - 
(select count(*) from Post_Upvotes_Downvotes p where p.is_downvoted = 1 and p.create_date > DATEADD(day, -1, GETDATE()))) as UpvotesMinusDownVotesLast24Hours , Forum_Posts.post_id
from Forum_Posts
join Post_Upvotes_Downvotes on Forum_Posts.post_id = Post_Upvotes_Downvotes.post_id
group by Forum_Posts.post_id

select count(*) from Post_Upvotes_Downvotes where is_upvoted = 1 and create_date > DATEADD(day, -1, GETDATE())

select (select count(*) from Post_Upvotes_Downvotes p where p.is_upvoted = 1 and p.create_date > DATEADD(day, -1, GETDATE())) as test
from Forum_Posts
join Post_Upvotes_Downvotes on Forum_Posts.post_id = Post_Upvotes_Downvotes.post_id
group by Forum_Posts.post_id

select count(*) as test, sum(test - 1) post_id from Post_Upvotes_Downvotes
group by post_id

select * from Post_Upvotes_Downvotes

select Top 10 ((select count(*) from Post_Upvotes_Downvotes p WHERE p.is_upvoted = 1 and p.create_date > DATEADD(day, -1, GETDATE()) AND p.is_upvoted = 1) -
(select count(*) from Post_Upvotes_Downvotes p WHERE p.is_downvoted= 1 and p.create_date > DATEADD(day, -1, GETDATE()) AND p.is_upvoted = 1)) as UpvotesMinusDownVotesLast24Hours,
Forum_Posts.post_id from Forum_Posts
join Post_Upvotes_Downvotes on Forum_Posts.post_id = Post_Upvotes_Downvotes.post_id
group by Forum_Posts.post_id
order by UpvotesMinusDownVotesLast24Hours




group by Forum_Posts.post_id

