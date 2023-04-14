import axios from 'axios'

export default {
    getPost(id){
        return axios.get(`/ForumPosts/${id}`)
    }

}