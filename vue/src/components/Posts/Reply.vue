<template>
  <div>
    <div class="post-card" :class="{ 'root-post': isRootPost }">
      <button
        id="post-header"
        class="card-header button"
        @click="toggleReplies"
      >
        <h1 class="card-header-title">
          <div>{{ reply.title }}</div>
          <div v-if="reply.replies"> Comments: {{ reply.replies.length }}</div>
        </h1>
        <p class="replies" v-if="!isRootPost"></p>
      </button>
      <div v-if="$store.state.token != ''">
      <PostControls @createPost="togglePostVisibility(true); retrievePosts(reply)" v-if="$store.state.posts == true" :reply="reply" />
      <PostForm v-show="visiblePostForm" @cancelForm="togglePostVisibility(false)" />
      </div>
      <div class="card-content replies">
        <div class="content">
          <p>@{{ reply.username }}</p>
          
          <img v-if="reply.image" :src="reply.image" alt="Reply image" />
          <br />
          {{ reply.content }}
          <br/>
          <br/>
          <time>{{ formatDateTime(reply.createDate) }}</time>
        </div>
      </div>
      <footer class="card-footer" v-if="isRootPost">
        <a class="card-footer-item" @click="upvotePost">Like | {{ upVotes }}</a>
        <a class="card-footer-item" @click="downvotePost"
          >Dislike | {{ downVotes }}</a
        >
      </footer>
    </div>
    <div v-if="showReplies" class="replies-container">
     
      <Reply
        v-for="(nestedReply, index) in reply.replies"
        :key="index"
        :reply="nestedReply"
        :isRootPost="false"
      />
    </div>
  </div>
</template>

<script>
import PostControls from "../NewPostForm/PostControls.vue";
import PostForm from "../NewPostForm/PostForm.vue";
export default {
  name: "Reply",
  components: {
    PostControls,
    PostForm,
  },
  props: {
    reply: Object,
    isRootPost: {
      type: Boolean,
      default: false,
    },
    localVotes: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      showReplies: false,
      visiblePostForm: false,
    };
  },
  methods: {
    formatDateTime(dateString) {
      try {
        const date = new Date(dateString);
        if (isNaN(date)) {
          throw new Error("Invalid date");
        }
        const year = date.getFullYear();
        const month = ("0" + (date.getMonth() + 1)).slice(-2);
        const day = ("0" + date.getDate()).slice(-2);
        const hours = date.getHours();
        const minutes = ("0" + date.getMinutes()).slice(-2);
        const ampm = hours >= 12 ? "PM" : "AM";
        const formattedHours = hours % 12 || 12;
        return `${month}-${day}-${year} ${formattedHours}:${minutes} ${ampm}`;
      } catch (error) {
        console.error(`Error formatting date: ${error.message}`);
        return "Invalid date";
      }
    },
    toggleReplies() {
      this.showReplies = !this.showReplies;
    },
    retrievePosts(reply) {
      this.$store.dispatch("selectPost", reply);
    },
    togglePostVisibility(Bool) {
      this.visiblePostForm = Bool;
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
  computed: {
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
  created() {
    console.log("Reply received:", this.reply);
  },
};
</script>

<style scoped>
.root-post {
  margin-bottom: 10px;
}
.card-header{
  width: 100%;
  border-radius: 10px;
  border-width: 0;
}
.card-header-title{
  display: flex;
  justify-content: space-between;
  padding-left: 0;
}
.card-header button{
  display: flex;
  justify-content: space-between;
}
.media {
  background-color: #e0e0e0;
  padding: 10px;
  margin-bottom: 5px;
  border-radius: 5px;
}
.replies {
  color: #1a4d2e;
}
.post-card {
  background-color: #ff9f29;
  border:2px,#ff9f29;
  border-radius: 10px;
  margin-bottom: 10px;
}
.card-footer a {
  color: #1a4d2e;
}
.card-footer a:hover {
  background-color: #faf3e3;
}
.card-footer a:first-child:hover {
  border-bottom-left-radius: 10px;
}
.card-footer a:last-child:hover {
  border-bottom-right-radius: 10px;
}
.card-content{
  background-color: #faf3e3;
  color: #1a4d2e;
  border: #ff9f29;
}
</style>