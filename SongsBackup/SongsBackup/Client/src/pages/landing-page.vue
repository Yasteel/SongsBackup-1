<script setup lang="ts">
  import navBar from '../components/nav-bar.vue';
  import { ref, computed } from 'vue';
  import { axiosService } from "@/Services/AxiosService.ts";
  
  const fileInput = ref(null);
  const selectedFiles = ref<File[]>([]);
  
  const showSecond = function(){
      document.querySelector('#second').scrollIntoView({behavior: "smooth"});
  }
  
  function openFile(){
    document.getElementById('file-select').click();
  }
  
  function handleFileChange(e: Event){
    const target = e.target as HTMLInputElement;
    if(target.files){
      selectedFiles.value = Array.from(target.files);
    }
  }

  const uploadFiles = async () => {
    if (!selectedFiles.value.length) return;

    const formData = new FormData();
    selectedFiles.value.forEach(file => formData.append('files', file));

    try {
      const response = await fetch('/api/upload/upload-files', {
        method: 'POST',
        body: formData,
      });

      const result = await response.json();
      console.log('Upload successful:', result);
      alert('Files uploaded successfully!');
    } catch (error) {
      console.error('Error uploading files:', error);
      alert('Failed to upload files. Please try again.');
    } finally {
      selectedFiles.value = [];
    }
  };
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
            <input @change="handleFileChange" type="file" id="file-select" ref="fileInput" multiple/>
            <button @click="openFile" id="file-select-button"><i class="fa-solid fa-upload"></i> Select your Files</button>
            <button @click="uploadFiles">Upload files</button>
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