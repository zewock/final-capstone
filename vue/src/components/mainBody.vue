<template>
  <body class="mainBody">
    <div class="body-container" v-if="postList.length === 0">
      Active Forums
<ForumCard
  v-for="forum in topActiveForums"
  :key="forum.forumID"
  :forum="forum"
  :format-date="formatDate"
/>
      <h1>Popular Posts</h1>
      <PostCard
        v-for="posts in topPosts"
        :key="posts.postId"
        :post="posts"
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
import ForumCard from "../components/Forums/ForumCard.vue";
export default {
  components: { PostCard, ForumCard },
  name: "mainBody",
  data() {
    return {
      forums: [],
      topPosts: [],
      topActiveForums: [],
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
        this.topPosts = response.data.topTenPopularPostsArray;
      });
    },
    getTopActiveForums() {
      this.topActiveForums = this.forums
        .slice()
        .sort(
          (a, b) =>
            new Date(b.mostRecentPostDate) - new Date(a.mostRecentPostDate)
        )
        .slice(0, 5);
    },
  },
  created() {
    ForumService.getForums().then((response) => {
      this.forums = response.data.forumArray;
      this.forums.forEach((forum) => {
        this.RetrievePosts(forum);
      });
      this.getTopActiveForums();
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