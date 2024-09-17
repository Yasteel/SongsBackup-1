<script setup lang="ts">
  import navBar from '../components/nav-bar.vue';
  import { ref } from 'vue'
  import { axiosService } from "@/Services/AxiosService.ts";
  
  const imageSrc = ref('/images/landing-page-img.jpg');
  
  const showSecond = function(){
      document.querySelector('#second').scrollIntoView({behavior: "smooth"});
  }
  
  const openFile = function(){
    document.getElementById('file-select').click();
  }
</script>

<template>
  <div class="main">
    <navBar />
    <div class="content">
      <div class="text-content">
        <div class="first" id="first">
          <h2>Backup Your Local Music to Spotify with Ease</h2>
          <p>Welcome to our music backup solution! Effortlessly safeguard your cherished local songs by transferring them to a Spotify playlist. Our web app allows you to:</p>
          <ul>
            <li>Select Your Local Files: Easily choose the songs you want to back up from your device.</li>
            <li>Read Metadata: Our app reads available metadata from your local files to identify the tracks.</li>
            <li>Search Spotify: Automatically search for matching songs on Spotify using the extracted metadata.</li>
            <li>Create Playlists: Seamlessly add the found tracks to a Spotify playlist, preserving your music collection online</li>
          </ul>
          <p>Say goodbye to the fear of losing your favorite tunes. Start backing up your local music to Spotify today!</p>
          <button class="get-started" @click="showSecond">get started</button>
        </div>
        <div class="second" id="second">
          <div class="file-container">
            <input type="file" id="file-select" webkitdirectory directory multiple/>
            <button @click="openFile" id="file-select-button"><i class="fa-solid fa-upload"></i> Select your Files</button>
          </div>          
        </div>
      </div>
      <div class="image-content">
      </div>
    </div>
  </div>
</template>

<style>
  :root{
    --background: #141817;
    --text: #fff;
    --sub-text: #adadad;
    --primary: #21D754;
  }
  
  *{
    font-family: Helvetica;
  }
  
  
  html{
    background: var(--background);
  }
  
  .content{
    width: 100%;
    height: calc(100vh - 4rem);
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }
  
  .text-content, .image-content{
    width: 50%;
    height: 100%;
    overflow: hidden;
  }
  
  .first, .second{
    height: 100%;
  }
  
  .second{
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  
  .second input{
    display: none;
  }
  
  
  h2, p{
    color: var(--text);
  }
  
  ul li{
    color: var(--sub-text);
  }
  
  .text-content{
    height: 100%;
    padding: 0 15rem;
  }
  
  .text-content :is(h2){
    margin: 3rem 0;
  }

  .text-content :is(ul){
    padding: 2rem 0;
  }
  
  .text-content ul li{
    margin: 1rem;
  }
    
  .text-content ul li::marker{
    color: var(--primary);
  }
  
  button{
    background: var(--primary);
    color: #000;
    text-transform: uppercase;
    padding: 1.5rem 3rem;
    margin: 5rem 0;
    border-radius: .5rem;
    cursor: pointer;
  }
  
  .image-content{
    background-image: url('/Images/landing-page-img.jpg');
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
  }
  
  .image-content img{
    width: 100%;
    height: 100%;
  }
</style>