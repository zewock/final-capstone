import axios from 'axios'

export default {
    getPost(forumId){
        return axios.get(`/ForumPosts/${forumId}`)
    }

}