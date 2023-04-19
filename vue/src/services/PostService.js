import axios from 'axios'

export default {
    getPost(forumId){
        return axios.get(`/ForumPosts/${forumId}`)
    },
    createPost(newPost){
        return axios.post('/PostToForum', newPost)
    },
    searchPosts(keyword){
        return axios.get(`/Posts/${keyword}`)
    },
    upvote(){
        return axios.put('/ChangeUpvoteState')
    },
    downvote(){
        return axios.put('/ChangeDownvoteState')
    },
    deletePost(Ids) {
        return axios.put(`/DeletePost`, Ids)
    }
}