<template>
  <section class="box">
      <h1 style="">New Forum Form</h1>
      <div class="field">
        <label class="label">Forum Name</label>
        <div class="control has-icons-left has-icons-right">
          <input
            class="input is-success"
            type="text"
            placeholder="Text input"
            value="bulma"
            v-model="newForum.title"
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
        <label class="label">Topic</label>
        <div class="control">
          <div class="select">
            <select v-model="newForum.Topic">
              <option>Gaming</option>
              <option>Sports</option>
              <option>Tech</option>
              <option>Television</option>
              <option>Spongebob</option>
            </select>
          </div>
        </div>
      </div>

      <div class="field">
        <label class="label">Description</label>
        <div class="control">
          <textarea
            class="textarea"
            placeholder="Textarea"
            v-model="newForum.description"
          ></textarea>
        </div>
      </div>

      <div class="field is-grouped">
        <div class="control">
          <button
            class="button"
            @click="
              SaveForum();
              refreshForum();
            "
          >
            Submit
          </button>
        </div>
        <div class="control">
          <button class="button" @click="onFormCancel">Cancel</button>
        </div>
      </div>
  </section>
</template>

<script>
import ForumService from "@/services/ForumService"
export default {
  name: "ForumForm",
data() {
  return {
    newForum: {
      image: "",
      topic: "",
      title: "",
      description: "",
    },
      visible: false,
  }
},
    methods: {
    SaveForum() {
    ForumService.create(this.newForum).then((response) => {
      if (response.status === 201) {
        alert("Forum Created");
        this.forums.push(response.data);
        this.form = false;
        }
      })
    },
      refreshForum() {
      this.$nextTick(() => {
      this.$router.go();
    });
  },
    onFormCancel() {
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
