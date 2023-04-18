<template>
  <section class="box">
      <h1 style="">New Post Form</h1>
      <div class="field">
        <label class="label">Post Name</label>
        <div class="control has-icons-left has-icons-right">
          <input
            class="input is-success"
            type="text"
            placeholder="Text input"
            value="bulma"
            v-model="newPost.title"
          />
          <span class="icon is-small is-left">
            <i class="fas fa-font"></i>
          </span>
          <span class="icon is-small is-right">
            <i class="fas fa-check"></i>
          </span>
        </div>
      </div>
      <div class="field">
        <label class="label">Content</label>
        <div class="control">
          <textarea
            class="textarea"
            placeholder="Textarea"
            v-model="newPost.description"
          ></textarea>
        </div>
      </div>
      <div class="field">
        <label class="label">Image</label>
        <div class="control">
          <textarea
            class="textarea"
            placeholder="Textarea"
            v-model="newPost.image"
          ></textarea>
        </div>
      </div>

      <div class="field is-grouped">
        <div class="control">
          <button
            class="button"
            @click="
              SavePost();
              refreshPost();
            "
          >
            Submit
          </button>
        </div>
        <div class="control">
          <button class="button" @click="onPostCancel">Cancel</button>
        </div>
      </div>
  </section>
</template>

<script>
import PostService from "@/services/PostService"
export default {
  name: "ForumForm",
data() {
  return {
    newPost: {
      image: "",
      topic: "",
      title: "",
      content: "",
    },
      visible: false,
  }
},
    methods: {
    SavePost() {
    PostService.create(this.newPost).then((response) => {
      if (response.status === 201) {
        alert("Post Created");
        this.forums.push(response.data);
        this.form = false;
        }
      })
    },
      refreshPost() {
      this.$nextTick(() => {
      this.$router.go();
    });
  },
    onPostCancel() {
      this.$emit("cancelForm");
    }
  }
};
</script>

<style scoped>
.button {
  background-color: #1a4d2e;
  color: #faf3e3;
}
.box {
  height: 100%;
  background-color: #ff9f29;
  z-index: 20;
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
.replies {
  color: #1a4d2e;
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
  padding-left: 0;
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