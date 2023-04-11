<template>
        <aside class="menu">
            <p class="menu-label">
                General
            </p>
        <ul class="menu-list">
            <li><a>Dashboard</a></li>
            <li><a>Customers</a></li>
        </ul>
        <p class="menu-label">
            Administration
        </p>
        <ul class="menu-list">
            <li><a>Team Settings</a></li>
            <li><a>Invitations</a></li>
            <li><a>Cloud Storage Environment Settings</a></li>
            <li><a>Authentication</a></li>
        </ul>
        <p class="menu-label">
            Transactions
        </p>
        <ul class="menu-list">
            <li><a>Payments</a></li>
            <li><a>Transfers</a></li>
            <li><a>Balance</a></li>
        </ul>
        </aside>
</template>


<script>

export default {
  name: 'sidebarLeft',
data() {
  return {
    
  }
}
};
</script>

<style>
.menu {
  grid-area: menu;
  background-color:lavender;
  position: static;
  display:inline-block;
  vertical-align: text-bottom;
  overflow-y: visible;
  top: 0;
  bottom: 0;
  padding-top: 100px;
}

</style>