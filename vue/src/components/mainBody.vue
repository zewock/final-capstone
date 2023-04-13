<template>
<body class="mainBody">
    <div class="card">
  <header class="card-header">
    <p class="card-header-title">
      {{header}}
    </p>
    <button class="card-header-icon" aria-label="more options">
      <span class="icon">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
  </header>
  <div class="card-content">
    <div class="content">
      <p>@{{ownerUsername}}</p>
      {{body}}
      <br>
      <time datetime>{{createDate}}</time>
    </div>
  </div>
  <footer class="card-footer">
    <a href="#" class="card-footer-item">{{totalNumUpvotes}} | Like</a>
    <a href="#" class="card-footer-item">{{totalNumDownvotes}} | Dislike</a>
  </footer>
</div>

</body>
</template>

<script>
export default {
  name: 'mainBody',
data() {
  return {
    
  }
}
};
</script>

<style scoped>
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
</style>