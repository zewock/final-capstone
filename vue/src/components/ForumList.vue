<template>
  <body class="mainBody">
        <div class="card">
  <header class="card-header">
    <section class="card-header-title">
       <a class="button">New Forum</a>
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
  <header class="card-header">
    <p class="card-header-title">
      Post 1
    </p>
    <button class="card-header-icon" aria-label="more options">
     <span><time datetime="2016-1-1">11:09 PM - 1 Jan 2016</time></span>
    </button>
  </header>
</div>
</body>
</template>

<script>
export default {
  name: 'forumList',
data() {
  return {
    forums: [],
  }
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
</style>