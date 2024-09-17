<template>
  <div class="navbar">
    <h2>Songs Backup</h2>
    <div class="circle">
      <img :src="image" alt="" v-if="image != null" width="36" height="36">
      <div class="username">
        <p>Logged in as: {{ displayName }}</p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
   defineProps({
    displayName: String,
    image: String
  });
</script>

<style scoped>
  .navbar{
    width: 100%;
    height: 4rem;
    padding: 0rem 2rem;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    color: white;
  }
  
  .navbar h2 {
    text-transform: uppercase;
    font-family: Helvetica, sans-serif;
  }
  
  .navbar .circle{
    width: 36px;
    aspect-ratio: 1;
    border-radius: 50%;
    outline: 2px solid #21D754;
    background: #21D754;
    display: grid;
    place-items: center;
    position: relative;
  }
  
  .navbar .circle img{
    width: 36px;
    height: 36px;
    border-radius: 50%;
  }
  
  .navbar .circle img:hover ~.username{
    display: flex;
    opacity: 1;
  }
  
  .navbar .circle .username{
    width: 200px;
    position: absolute;
    top: 160%;
    right: 10%;
    display: none;
    justify-content: center;
    align-items: center;
    opacity: 0;
    padding: 1rem;
    border: 1px solid #21D754;
    background: #000;
    
    transition: all ease-in-out 1500ms;
  }

  .navbar .circle .username::after{
    content: '';
    width: 15px;
    aspect-ratio: 1;
    background: #000;
    border-left: 1px solid #21D754;
    border-top: 1px solid #21D754;
    position: absolute;
    top: -8.5px;
    right: 10px;
    transform: rotate(45deg);
  }
  
</style>