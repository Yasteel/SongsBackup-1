<script setup lang="ts">
  import navBar from '../components/nav-bar.vue';
  import { onMounted, ref } from 'vue';
  
  const songs = ref(null);
  
  onMounted(async () => {
    const result = await getSongs();
    if(result){
      songs.value = result;
    }

    console.log(songs.value);
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
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img src="https://cdn-images.dzcdn.net/images/cover/92e6cfa59e6d58e2956b3a0f35d00ccd/0x1900-000000-80-0-0.jpg" alt="bad habits">
                </div>
                <div class="info">
                  <p class="title">Bad Habits</p>
                  <p>Ed Sheeran feat. Bring me the Horizon</p>
                  <p>Album</p>
                  <button class="add-to-playlist">Add to Playlist</button>
                </div>
              </div>
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0Ps-iJrvB8xF7MGM7EZ7oGzrcDySqwnlrVA&s" alt="Throne">
                </div>
                <div class="info">
                  <p class="title">Throne</p>
                  <p>Bring me the Horizon</p>
                  <p>Album</p>
                  <button class="add-to-playlist">Add to Playlist</button>
                </div>
              </div>
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img src="https://images.genius.com/03348fda2bf03706cef6c61790f30b7a.300x300x1.png" alt="line in the sand">
                </div>
                <div class="info">
                  <p class="title">A line in the sand</p>
                  <p>Linkin Park</p>
                  <p>The Hunting Party</p>
                  <button class="add-to-playlist">Add to Playlist</button>
                </div>
              </div>
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img src="https://cdn-images.dzcdn.net/images/cover/92e6cfa59e6d58e2956b3a0f35d00ccd/0x1900-000000-80-0-0.jpg" alt="bad habits">
                </div>
                <div class="info">
                  <p class="title">Bad Habits</p>
                  <p>Ed Sheeran feat. Bring me the Horizon</p>
                  <p>Album</p>
                  <button class="add-to-playlist">Add to Playlist</button>
                </div>
              </div>
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0Ps-iJrvB8xF7MGM7EZ7oGzrcDySqwnlrVA&s" alt="Throne">
                </div>
                <div class="info">
                  <p class="title">Throne</p>
                  <p>Bring me the Horizon</p>
                  <p>Album</p>
                  <button class="add-to-playlist">Add to Playlist</button>
                </div>
              </div>
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image">
                  <img src="https://images.genius.com/03348fda2bf03706cef6c61790f30b7a.300x300x1.png" alt="line in the sand">
                </div>
                <div class="info">
                  <p class="title">A line in the sand</p>
                  <p>Linkin Park</p>
                  <p>The Hunting Party</p>
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