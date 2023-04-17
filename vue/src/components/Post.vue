<template>
  <div>
    <button id="post-header" class="card-header button" @click="RetrieveReply(post)">
      <h1 class="card-header-title">
        {{ post.title }}
      </h1>
    </button>
    <div class="card-content">
      <div class="content">
        <p>@{{ post.authorUserName }}</p>
        {{ post.content }}
        <br />
        <br />
        <time>{{ formatDateTime(post.createDate) }}</time>
      </div>
    </div>
    <footer class="card-footer">
      <a href="#" class="card-footer-item">Like | {{ post.upVotes }}</a>
      <a href="#" class="card-footer-item">Dislike | {{ post.downVotes }}</a>
      <a href="#" class="card-footer-item">Favorite</a>
    </footer>
  </div>
</template>

<script>
export default {
    name: "PostContent",
    props: ['post'],
    methods:{
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
    RetrieveReply(post) {
      if (post.postId > post.ParentPostId) {
        if (!this.replyList[post.ParentPostId]) {
          this.replyList[post.ParentPostId] = [];
        }
        post.replies.forEach((reply) => {
          this.replyList[post.ParentPostId].push(reply);
        });
      }
      this.postsList = post.replies;
      if (post.replies.length > 0) {
        this.replyList = this.replyList[post.replies[0].ParentPostId];
      }
    },
    }
};
</script>

<style>
.mainBody {
  grid-area: mainBody;
  position: sticky;
  overflow: hidden;
  height: 87vh;
  background-color: #faf3e3;
  padding: 15px;
  border-radius: 10px;
}
#in-forum-title #forum-title {
  margin-bottom: 10px;
}
#in-forum-title .card-header {
  background-color: #ff9f29;
  margin-bottom: 0;
  border-radius: 10px;
  position: sticky;
  height: auto;
  align-items: center;
  width: 100%;
}

#in-forum-title .card-header h1 {
  display: inline-flex;
  color: #1a4d2e;
  font-size: larger;
  font-weight: bold;
}
#in-forum-title .card-header p {
  width: 75%;
  float: right;
  font-size: smaller;
  text-align: right;
}

.card-header {
  background-color: #ff9f29;
  margin-bottom: 10px;
  border-radius: 10px;
  border-color: transparent;
  height: auto;
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
.card-header-title {
  color: #1a4d2e;
  justify-content: space-between;
  display: flex;
  align-items: center;
}
.post-card {
  background-color: #ff9f29;
  border-radius: 10px;
  margin-bottom: 10px;
}
.card {
  background-color: #1a4d2e;
  padding: 15px;
  margin-bottom: 10px;
  color: #000000;
  border-radius: 10px;
  z-index: 0;
}
.cards-container::-webkit-scrollbar {
  width: 0px; /* width of the entire scrollbar */
}
.button {
  background-color: #1a4d2e;
  color: #faf3e3;
}
.dropdown-menu {
  display: none;
  left: 0;
  right: 1px;
  min-width: 6rem;
  padding-top: 4px;
  position: absolute;
  top: 100%;
}
.dropdown-item {
  right: 10px;
  align-items: center;
}
.dropdown-content {
  z-index: 1;
}
.box {
  height: 100%;
  background-color: #ff9f29;
  z-index: 20;
}
.date {
  color: black;
  padding-right: 10px;
}
.card-header:last-child {
  margin-bottom: 0;
}
.cards-container {
  height: 100%;
  overflow-y: auto;
  border-radius: 10px;
}
</style>