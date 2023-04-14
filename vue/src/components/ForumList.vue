<template>
  <body class="mainBody">
      <section class="box" v-bind:key="form" v-show="form" >
          <h1 style="">New Forum Form</h1>

<div class="field">
  <label class="label">Username</label>
  <div class="control has-icons-left has-icons-right">
    <input class="input is-success" type="text" placeholder="Text input" value="bulma">
    <span class="icon is-small is-left">
      <i class="fas fa-user"></i>
    </span>
    <span class="icon is-small is-right">
      <i class="fas fa-check"></i>
    </span>
  </div>
</div>

<div class="field">
  <label class="label">Forum Name</label>
  <div class="control has-icons-left has-icons-right">
    <input class="input is-success" type="text" placeholder="Text input" value="bulma">
    <span class="icon is-small is-left">
      <i class="fa-sharp fa-light fa-input-text"></i>
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
      <select>
        <option>Select dropdown</option>
        <option>With options</option>
      </select>
    </div>
  </div>
</div>

<div class="field">
  <label class="label">Description</label>
  <div class="control">
    <textarea class="textarea" placeholder="Textarea"></textarea>
  </div>
</div>

<div class="field is-grouped">
  <div class="control">
    <button class="button" v-bind="form" @click="form = false" >Submit</button>
  </div>
  <div class="control">
    <button class="button" v-bind="form" @click="form = false" >Cancel</button>
  </div>
</div>
      </section>
      
        <div class="card">
  <header class="card-header">
    <section class="card-header-title">
       <button class="button" v-bind="form" @click="form = true">New Forum</button>
       <div class="dropdown is-hoverable">
  <div class="dropdown-trigger">
    <button class="button" aria-haspopup="true" aria-controls="dropdown-menu4">
      <span>Options</span>
      <span class="icon is-small">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
  </div>
  <div class="dropdown-menu" id="dropdown-menu4" role="menu">
    <div class="dropdown-content">
      <div class="dropdown-item">
          <button class="button">Moderators</button>
          <button class="button">Users</button>
          <button class="button">Delete</button>
      </div>
    </div>
  </div>
</div>
    </section>
  </header>
</div>
    <div class="card">
  <header class="card-header" v-for="forum in forums" :key="forum.ForumID">
    <section class="card-header-title"  >
      {{forum.Topic}}
     <span><time>{{forum.CreateDate}} </time></span>
     <div class="dropdown is-hoverable">
  <div class="dropdown-trigger">
     <button class="button" aria-haspopup="true" aria-controls="dropdown-menu4">
      <span>Options</span>
      <span class="icon is-small">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
     <div class="dropdown-menu" id="dropdown-menu5" role="menu">
    <div class="dropdown-content">
      <div class="dropdown-item">
          <button class="button">Moderators</button>
          <button class="button">Users</button>
          <button class="button">Delete</button>
      </div>
    </div>
  </div>
   </div>
  </div>
  </section>
  </header>
</div>
</body>
</template>

<script>
import ForumService from '../services/ForumService';

export default {
  name: 'forumList',
  data() {
    return {
        forums: [],
        form: false
    };
  },
  methods:{
    ViewForum(id){
      this.$router.push(`/forum/${id}`)
    },
    
  },
  created() {
    ForumService.getForums().then((response) => {
       let parsedResponse = JSON.parse(response.data.value);
       const forumArray = parsedResponse.ForumArray;
       this.forums.push(...forumArray);
    });
  }
};
</script>

<style scope>
.mainBody {
  grid-area: mainBody;
  position: sticky;
  overflow: auto;
  height: 87vh;
  background-color: #FAF3E3;
  padding: 15px;
  border-radius:10px;
}
.card-header-title{
  color:#1A4D2E;
  justify-content: space-between;
}
.card{
  background-color: #FF9F29;
  padding: 15px;
  margin-bottom: 10px;
  color: #000000;
}
body::-webkit-scrollbar {
  width: 0px;               /* width of the entire scrollbar */
}
.button {
    background-color: #1A4D2E;
    color: #FAF3E3;
}
.dropdown-menu {
    display: none;
    left: 0;
    min-width: 6rem;
    padding-top: 4px;
    position: absolute;
    top: 100%;
    z-index: 20;
}

.box {
    height: 100%;
    background-color: #FF9F29;
    z-index: 20;
}
</style>