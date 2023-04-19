<template>
  <body class="mainBody">
    <div class="body-container" v-if="postList.length === 0">
      <div  class="card" v-for="forum in forums"
          :key="forum.forumID" > 
      </div>
    </div>
          <PostCard
      v-for="post in postList"
      :key="post.postId"
      :post="post"/>
  </body>
</template>

<script>
import ForumService from "../services/ForumService";
import PostService from "../services/PostService";
import PostCard from '../components/Posts/PostCard.vue'
export default {
  components: {PostCard},
  name: "mainBody",
  data() {
    return{
      forums: [],
    }
  },
  computed:{
    postList(){
      return this.$store.state.postsList;
    },
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
  overflow: auto;
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