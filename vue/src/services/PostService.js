import axios from 'axios'

export default {
    getPost(forumId) {
        return axios.get(`/ForumPosts/${forumId}`)
    },
    createPost(newPost) {
        return axios.post('/PostToForum', newPost)
    },
    searchPosts(keyword) {
        return axios.get(`/Posts/${keyword}`)
    },
    upvote(payload) {
        return axios.put("/ChangeUpvoteState", payload);
    },
    downvote(payload) {
        return axios.put("/ChangeDownvoteState", payload);
    },
    deletePost(Ids) {
        return axios.put(`/DeletePost`, Ids)
    },
    top10posts(){
        return axios.get('/TopTenPopularPosts')
    },
    randomFact(){
        return axios.get('/RandomFact')
    }
}