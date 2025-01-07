<script setup lang="ts">
  import navBar from '../components/nav-bar.vue';
  import { onMounted, ref } from 'vue';
  
  // Todo: Add id to SpotifyResponse interface
  // Todo: Create function to fetch user playlists and display them
  // Todo: Conditionally display add to playlist button based on which select is selected
  // Todo: Add functionality to select all songs
  // Todo: Make Tabs functional and display songs that do not have search results in the second tab
  // Todo: Add functionality to add songs to playlist
  // Todo: Add functionality to create playlist
  
  interface SpotifyResponse{
    title: string;
    artist: string;
    album: string;
    albumArt: string;
  }
  
  const songs = ref<SpotifyResponse[]>([]);
  
  onMounted(async () => {
    const result = await getSongs();
    if(result){
      songs.value = result.songs;
    }
  });
  
  const getSongs = async () => {
    try {
      const response = await fetch('/api/songs/get-songs');
      if(response.ok){
        return await response.json();
      }
    } catch (error) {
      console.error(error);
      return null;
    }
  };
</script>

<template>
  <div class="main">
    <navBar />
    <div class="container">
      <div class="tabs">
        <div class="tab-links">
          <button class="tab-link">Recognised Songs</button>
          <button class="tab-link">Unrecognised Songs</button>
        </div>
        <div class="tab-content details">
          <div class="details">
            <div class="toolbar">
              <div class="select-all">
                <i class="fa-regular fa-square-check"></i> select all
              </div>
              <div class="add-multiple-to-playlist">
                <button class="btn-add-multiple-to-playlist">Add to Playlist</button>
              </div>
            </div>
            <div class="song-list">
              <div class="song" v-for="song in songs">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img :src="song.albumArt" alt="bad habits">
                </div>
                <div class="info">
                  <p class="title">{{ song.title }}</p>
                  <p>{{ song.artist }}</p>
                  <p>{{ song.album }}</p>
                  <button class="add-to-playlist">Add to Playlist</button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="tab-content"></div>
      </div>
      
      <div class="playlists">
        <div class="playlist-header">
          <p>Select Playlist</p>
          <button class="create-playlist">Create Playlist</button>
<!--          <button><i class="fa-solid fa-plus"></i></button>-->
        </div>
        <div class="playlist-content">
          <p>Old School tracks</p>
        </div>
        <div class="playlist-content">
          <p>Guitar Learning</p>
        </div>
        <div class="playlist-content">
          <p>Throwbacks</p>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
  .main{
    background: #0f1111;
    font-family: "Helvetica Neue", "Segoe UI", helvetica, verdana, sans-serif;
  }

  .container {  
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    grid-template-rows: 1fr 1fr 1fr 1fr;
    gap: 1rem;
    grid-auto-flow: row;
    grid-template-areas:
    "details details details playlists"
    "details details details playlists"
    ". . . ."
    ". . . .";
  }
  
  .container > div{
    background: #121414;
    color: #D7D3CE;
    padding: 1rem;
  }
  
  .tabs{
    grid-area: details;
  }
  
  .tab-links{
  }
  
  button.tab-link{
    padding: 1rem .5rem;
    border: none;
    background: none;
    color: #D7D3CE;
    cursor: pointer;
  }

  button.tab-link:hover{
    color: #21D754;
  }
  
  .playlists{
    grid-area: playlists;
  }
  
  .song-list{
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 1rem 0;
  }
  
  .toolbar{
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    padding: 1rem;
    border-radius: 1rem;
    background: #0f1111;
  }
  
  .song{
    display: flex;
    align-items: center;
    background: #0f1111;
    padding: 1rem;
    border-radius: 1rem;
    gap: 2rem;
  }
  
  .image img{
    width: 82px;
    height: 82px;
  }

  .select{
    font-size: 1.5rem;
  }

  .select .fa-square-check{
    color: #21D754;
  }
  
  .title{
    font-weight: 700;
  }
  
  button.add-to-playlist, button.btn-add-multiple-to-playlist{
    background: #21D754;
    color: #0f1111;
    padding: .5rem 1rem;
    border: none;
    border-radius: .5rem;
    cursor: pointer;
    margin-top: .5rem;
  }
  
  .playlists{
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }
  
  .playlist-header{
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 1rem;
  }
  
  .playlist-header button{
    background: #21D754;
    color: #0f1111;
    padding: .5rem 1rem;
    border: none;
    border-radius: .5rem;
    cursor: pointer;
  }
  
  .playlist-content{
    padding: 1rem;
    border-radius: 1rem;
    background: #0f1111;
    cursor: pointer;
  }
</style>