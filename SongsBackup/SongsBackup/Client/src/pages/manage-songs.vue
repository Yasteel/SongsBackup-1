<script setup lang="ts">
  import navBar from '../components/nav-bar.vue';
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
            <div class="song-list">
              <div class="song">
                <div class="select">
                  <i class="fa-regular fa-square-check"></i>
                </div>
                <div class="image"></div>
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
                <div class="image"></div>
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
                <div class="image"></div>
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
        <p>Playlists</p>
        
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
    padding: 1rem;
  }
  
  .song{
    display: flex;
    align-items: center;
    background: #0f1111;
    padding: 1rem;
    border-radius: 1rem;
  }

  .select{
    font-size: 1.5rem;
    margin-right: 1rem;
  }

  .select .fa-square-check{
    color: #21D754;
  }
  
  .title{
    font-weight: 700;
  }
  
  button.add-to-playlist{
    background: #21D754;
    color: #D7D3CE;
    padding: .5rem 1rem;
    border: none;
    border-radius: .5rem;
    cursor: pointer;
    margin-top: .5rem;
    font-weight: 750;
  }
  
  .song > div{
    //outline: 1px solid yellow
  }
</style>