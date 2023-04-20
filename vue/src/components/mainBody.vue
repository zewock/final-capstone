<template>
  <body class="mainBody">
    <div class="body-container" v-if="postList.length === 0">
      <!-- <div class="card" v-for="forum in forums" :key="forum.forumID"></div> -->
      <PostCard
        class="card"
        v-for="post in topPosts"
        :key="post.postId"
        :post="post"
      />
    </div>
    <div v-if="$store.state.keyword != null">
      <PostCard v-for="post in postList" :key="post.postId" :post="post" />
    </div>
  </body>
</template>

<script>
import ForumService from "../services/ForumService";
import PostService from "../services/PostService";
import PostCard from "../components/Posts/PostCard.vue";
export default {
  components: { PostCard },
  name: "mainBody",
  data() {
    return {
      forums: [],
      topPosts: [],
    };
  },
  computed: {
    postList() {
      return this.$store.state.postsList;
    },
    upVotes() {
      const post = this.$store.state.postsList.find(
        (post) => post.postId === this.reply.postId
      );
      return post ? post.upVotes : this.reply.upVotes;
    },
    downVotes() {
      const post = this.$store.state.postsList.find(
        (post) => post.postId === this.reply.postId
      );
      return post ? post.downVotes : this.reply.downVotes;
    },
  },
  methods: {
    RetrievePosts(forum) {
      PostService.getPost(forum.forumID).then((response) => {
        this.postsList = response.data;
      });
    },
    Top10Posts() {
      PostService.top10posts().then((response) => {
        console.log(
          "This is what we're looking at " +
            response.data.topTenPopularPostsArray
        );
        this.topPosts = response.data.topTenPopularPostsArray;
        console.log("Top Posts: " + this.topPosts);
      });
    },
    upvotePost() {
      this.$store.dispatch("UPVOTE_POST", {
        postId: this.reply.postId,
        forumId: this.reply.forumId,
      });
    },
    downvotePost() {
      this.$store.dispatch("DOWNVOTE_POST", {
        postId: this.reply.postId,
        forumId: this.reply.forumId,
      });
    },
  },
  created() {
    ForumService.getForums().then((response) => {
      this.forums = response.data.forumArray;
      this.forums.forEach((forum) => {
        this.RetrievePosts(forum);
      });
    });
    this.Top10Posts();
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