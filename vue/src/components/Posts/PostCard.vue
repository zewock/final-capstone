<template>
<div>
  <div class="post-card">
  <button id="post-header" class="card-header button" @click="toggleReplies">
    <h1 class="card-header-title">
      {{ post.title }}
    </h1>
    <p class="replies">View {{ post.replies.length }} Comments</p>
  </button>
  <div class="card-content">
    <div class="content">
      <p>@{{ post.username }}</p>
      {{ post.content }}
      <br />
      <br />
      <time>{{ formatDateTime(post.createDate) }}</time>
    </div>
  </div>
  <footer class="card-footer">
    <a class="card-footer-item">Like | {{ post.upVotes }}</a>
    <a class="card-footer-item">Dislike | {{ post.downVotes }}</a>
    <a class="card-footer-item">Favorite</a>
  </footer>
  </div>
  <div v-if="showReplies" class="replies-container">
  <article v-for="(reply, index) in post.replies" :key="index" class="media">
    <div class="media-content">
      <p><strong>@{{ reply.username }}</strong>: {{ reply.content }}</p>
    </div>
  </article>
</div>
</div>
</template>

<script>
export default {
  name: "PostCard",
  data(){
    return{
    showReplies: false,
    }
  },
  props: {
    post: Object,
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
    }
  },
};
</script>

<style scoped>
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
