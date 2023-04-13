<template>
<body class="mainBody">
    <div class="card">
  <header class="card-header">
    <p class="card-header-title">
      Post 1
    </p>
    <button class="card-header-icon" aria-label="more options">
      <span class="icon">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
  </header>
  <div class="card-content">
    <div class="content">
      <p>@username</p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec iaculis mauris.
      <a href="#">@bulmaio</a>. <a href="#">#css</a> <a href="#">#responsive</a>
      <br>
      <time datetime="2016-1-1">11:09 PM - 1 Jan 2016</time>
    </div>
  </div>
  <footer class="card-footer">
    <a href="#" class="card-footer-item">Like</a>
    <a href="#" class="card-footer-item">Dislike</a>
    <a href="#" class="card-footer-item">Save</a>
  </footer>
</div>
<div class="card">
  <header class="card-header">
    <p class="card-header-title">
      Post 2
    </p>
    <button class="card-header-icon" aria-label="more options">
      <span class="icon">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
  </header>
  <div class="card-content">
    <div class="content">
      <p>@username</p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec iaculis mauris.
      <a href="#">@bulmaio</a>. <a href="#">#css</a> <a href="#">#responsive</a>
      <br>
      <time datetime="2016-1-1">11:09 PM - 1 Jan 2016</time>
    </div>
  </div>
  <footer class="card-footer">
    <a href="#" class="card-footer-item">Like</a>
    <a href="#" class="card-footer-item">Dislike</a>
    <a href="#" class="card-footer-item">Save</a>
  </footer>
</div>
<div class="card">
  <header class="card-header">
    <p class="card-header-title">
      Post 3
    </p>
    <button class="card-header-icon" aria-label="more options">
      <span class="icon">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
  </header>
  <div class="card-content">
    <div class="content">
      <p>@username</p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec iaculis mauris.
      <a href="#">@bulmaio</a>. <a href="#">#css</a> <a href="#">#responsive</a>
      <br>
      <time datetime="2016-1-1">11:09 PM - 1 Jan 2016</time>
    </div>
  </div>
  <footer class="card-footer">
    <a href="#" class="card-footer-item">Like</a>
    <a href="#" class="card-footer-item">Dislike</a>
    <a href="#" class="card-footer-item">Save</a>
  </footer>
</div>
<div class="card">
  <header class="card-header">
    <p class="card-header-title">
      Post 4
    </p>
    <button class="card-header-icon" aria-label="more options">
      <span class="icon">
        <i class="fas fa-angle-down" aria-hidden="true"></i>
      </span>
    </button>
  </header>
  <div class="card-content">
    <div class="content">
      <p>@username</p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec iaculis mauris.
      <a href="#">@bulmaio</a>. <a href="#">#css</a> <a href="#">#responsive</a>
      <br>
      <time datetime="2016-1-1">11:09 PM - 1 Jan 2016</time>
    </div>
  </div>
  <footer class="card-footer">
    <a href="#" class="card-footer-item">Like</a>
    <a href="#" class="card-footer-item">Dislike</a>
    <a href="#" class="card-footer-item">Save</a>
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