import axios from 'axios'

export default {
    getPost(forumId){
        return axios.get(`/ForumPosts/${forumId}`)
    },
    createPost(newPost){
        return axios.post('/PostToForum', newPost)
    }
}