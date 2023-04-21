<template>
  <body class="mainBody">
    <div class="body-container" v-if="postList.length === 0">
      <div id="fun" class="card">
        <h1 class="card-header-title">Fun Fact:</h1>
        <span>{{ randomFact.text }}</span>
      </div>
      <div class="card-header-title">Active Forums</div>
      <ForumCard
        v-for="forum in topActiveForums"
        :key="forum.forumID"
        :forum="forum"
        :format-date="formatDate"
      />
      <h1 class="card-header-title">Popular Posts</h1>
      <PostCard v-for="posts in topPosts" :key="posts.postId" :post="posts" />
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
      randomFact: {},
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
    getRandomFact() {
      PostService.randomFact().then(
        (response) => (this.randomFact = response.data),
        console.log(this.randomFact)
      );
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
    this.getRandomFact();
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
.card #fun {
  display: inline-flex;
  justify-content: center;
  border: 10px solid #1a4d2e; /* Add this line to add a border */
}
#fun.card{
  border: 5px solid #1a4d2e; /* Add this line to add a border */
}
.card-header-title h1 {
  display: flex;
  justify-content: center;
}
.fact-text {
  display: block;
  margin-top: -20px;
  text-align: center;
}
.card {
  background-color: #ff9f29;
  padding: 15px;
  margin-bottom: 10px;
  border-radius: 10px;
  color: #000000;
}
span{
  display: flex;
  justify-content: center;
}
.card h1 {
  font-weight: 700;
  color: #1a4d2e;
  display: flex;
  justify-content: center;
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