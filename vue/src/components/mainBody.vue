<template>
  <body class="mainBody">
    <div class="body-container">
      <div class="card" v-for="post in posts"
          :key="post.postID">
        <header class="card-header">
          <p class="card-header-title">
            {{ post.Header }}
          </p>
          <button class="card-header-icon" aria-label="more options">
            <span class="icon">
              <i class="fas fa-angle-down" aria-hidden="true"></i>
            </span>
          </button>
        </header>
        <div class="card-content">
          <div class="content">
            <p>@{{ ownerUsername }}</p>
            {{ post.Content }}
            <br />
            <time>{{ post.CreateDate }}</time>
          </div>
        </div>
        <footer class="card-footer">
          <a href="#" class="card-footer-item">{{ totalNumUpvotes }} | Like</a>
          <a href="#" class="card-footer-item"
            >{{ totalNumDownvotes }} | Dislike</a
          >
        </footer>
      </div>
    </div>
  </body>
</template>

<script>
export default {
  name: "mainBody",
  data() {
    return{
      posts: []
    }
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
}
</style>