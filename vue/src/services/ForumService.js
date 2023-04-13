import axios from 'axios';

export default {

    ForumList() {
      return axios.post('/forum')
    },
    getFourms(){
      return axios.get('/forums')
    }
  }