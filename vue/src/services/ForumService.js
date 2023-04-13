import axios from 'axios';

export default {

    getForums() {
      return axios.get('/ForumsList')
    },
    getForumId(id){
      return axios.get(`/ForumsList/${id}`)      
    }
    get
  }