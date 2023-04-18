import axios from 'axios'

export default {
    getPost(forumId){
        return axios.get(`/ForumPosts/${forumId}`)
    },
    create(newPost){
        return axios.post('/PostToForum', newPost)
    }
}