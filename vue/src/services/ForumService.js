import axios from 'axios';

export default {

    ForumList() {
      return axios.get('/forum')
    },
   
  }