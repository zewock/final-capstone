<template>
  <div>
    <div class="post-card" :class="{ 'root-post': isRootPost }">
      <button
        id="post-header"
        class="card-header button"
        @click="toggleReplies"
      >
        <h1 class="card-header-title">
          {{ reply.title }}
          Comments: {{ reply.replies.length }}
        </h1>
        <p class="replies" v-if="!isRootPost"></p>
      </button>
      <PostControls
        @createPost="
          togglePostVisibility(true);
          retrievePosts(reply);
        "
        v-if="$store.state.posts == true"
      />
      <PostForm
        v-show="visiblePostForm"
        @cancelForm="togglePostVisibility(false)"
      />
      <div class="card-content replies">
        <div class="content">
          <p>@{{ reply.username }}</p>
          {{ reply.content }}
          <img v-if="reply.image" :src="reply.image" alt="Reply image" />
          <br />
          <br />
          <time>{{ formatDateTime(reply.createDate) }}</time>
        </div>
      </div>
      <footer class="card-footer">
        <a class="card-footer-item">Like | {{ reply.upVotes }}</a>
        <a class="card-footer-item">Dislike | {{ reply.downVotes }}</a>
        <a class="card-footer-item">Favorite</a>
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
  },
  created() {},
};
</script>

<style scoped>
.root-post {
  margin-bottom: 10px;
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
.card-content {
  background-color: #faf3e3;
  color: #1a4d2e;
}
</style>