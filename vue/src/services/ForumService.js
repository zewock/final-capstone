import axios from 'axios';

export default {

    getForums() {
      return axios.get('/ForumsList')
    },
    getForumId(id){
      return axios.get(`/ForumsList/${id}`)      
    },
    create(newForum) {
      return axios.post(`/CreateForum`, newForum)
    },
    deleteForum(forumId) {
      return axios.put(`/DeleteForum`, forumId)
    }
  }
