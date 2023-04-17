<template>
  <body class="mainBody">
    <div class="body-container">
      <div class="card" v-for="forum in forums"
          :key="forum.forumID" >
            
        <header class="card-header">
          <p class="card-header-title">
          
          </p>
          <button class="card-header-icon" aria-label="more options">
            <span class="icon">
             <i class="fa fa-columns"></i>
            </span>
          </button>
        </header>
      </div>
    </div>
  </body>
</template>

<script>
import ForumService from "../services/ForumService";
import PostService from "../services/PostService";
export default {
  name: "mainBody",
  data() {
    return{
      forums: [],
      postsList: [],
    }
  },
  methods: {
     RetrievePosts(forum) {
      PostService.getPost(forum.forumID).then((response) => {
        this.postsList = response.data;

      });
    },
},
     created() {
    ForumService.getForums().then((response) => {
      this.forums = response.data.forumArray;
    this.forums.forEach(forum => {
      this.RetrievePosts(forum)
      
    });
    });
  },
};

</script>

<style scoped>
.mainBody {
  grid-area: mainBody;
  position: sticky;
  overflow: hidden;
  height: 87vh;
  background-color: #faf3e3;
  padding: 15px;
  border-radius: 10px;
}
.card-header-title {
  color: #1a4d2e;
}
.card {
  background-color: #ff9f29;
  padding: 15px;
  margin-bottom: 10px;
  color: #000000;
}
.body-container::-webkit-scrollbar {
  width: 0px; /* width of the entire scrollbar */
}
.body-container {
  height: 100%;
  overflow-y: auto;
  border-radius: 10px;
}
</style>