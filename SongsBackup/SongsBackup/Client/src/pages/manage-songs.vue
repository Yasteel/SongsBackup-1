<script setup lang="ts">
  import { onMounted, ref } from 'vue';
  import navBar from '../components/nav-bar.vue';
  import createPlaylist from './create-playlist.vue';
  import alertModal from './alert-modal.vue'

  interface SpotifySongResponse{
    title: string;
    artist: string;
    album: string;
    albumArt: string;
    uri: string;
    isSelected: boolean;
  }
  
  interface SpotifyPlaylistResponse{
    name: string;
    id: string;
    uri: string; 
  }
  
  const props = defineProps({
    userName: String,
    userImage: String
  });
  
  const songs = ref<SpotifySongResponse[]>([]);
  const playlists = ref<SpotifyPlaylistResponse[]>([]);
  const selectedPlaylist = ref<string>('');
  const allSongsSelected = ref<boolean>(false);
  const createPlaylistRef = ref(null);
  const alertRef = ref(null);
  
  onMounted(async () => {
    const songResult = await getSongs();
    const playlistResult = await getPlaylists();
    
    if(songResult){
      songs.value = songResult.songs;
    }
    
    if(playlistResult){
      playlists.value = playlistResult.playlists;
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
  
  const getPlaylists = async () => {
    try{
      const response = await fetch('/api/songs/get-playlists');
      if(response.ok){
        return await response.json();
      }
    }catch (error) {
      console.error(error);
      return null;
    }
  };
  
  const selectPlaylist = (e: Event) => {
    document.querySelector('.playlist-content.selected')?.classList.remove('selected');
    (e.target as HTMLElement).classList.add('selected');
    selectedPlaylist.value = (e.target as HTMLElement).id;
  }
  
  const selectSong = (uri: string) => {
    const index = songs.value.findIndex((song) => song.uri == uri);
    songs.value[index].isSelected = !songs.value[index].isSelected;

    const selectedSongs = songs.value.filter((song) => song.isSelected);
    allSongsSelected.value = songs.value.length === selectedSongs.length;
  }
  
  const selectAllSongs = () => {
    let selectSongs: boolean = !allSongsSelected.value;

    songs.value.forEach((song) => {
      song.isSelected = selectSongs;
    });
    
    allSongsSelected.value = !allSongsSelected.value;
  }
  
  const addSongsToPlaylist = () => {
    const selectedSongs = songs.value.filter((song) => song.isSelected).map( song => song.uri);
    
    if(selectedPlaylist.value && selectedSongs.length > 0){
      fetch('/api/songs/add-to-playlist', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          uris: selectedSongs,
          playlistId: selectedPlaylist.value
        })
      });
    }else{
      alertRef.value?.showAlert();
    }
  }
  
  const showCreateModal = () => {
    createPlaylistRef.value?.showModal();
  }

</script>

<template>
  <div class="main">
    <navBar :user-name="props.userName" :user-image="props.userImage"/>
    
    <div class="container">
      <div class="tabs">
        <div class="tab-links">
          <button class="tab-link">Recognised Songs</button>
          <button class="tab-link">Unrecognised Songs</button>
        </div>
        <div class="tab-content details">
          <div class="details">
            <div class="toolbar">
              <div class="select-all" :class="{selected: allSongsSelected}">
                <p class="btn-select-all" @click="selectAllSongs"><i :class="allSongsSelected ? 'fa-regular fa-square-check' : 'fa-regular fa-square'"></i> select all</p>
              </div>
              <div class="add-multiple-to-playlist">
                <button class="btn-add-to-playlist" @click="addSongsToPlaylist">Add to Playlist</button>
              </div>
            </div>
            <div class="song-list">
              <div class="song" v-for="song in songs" :id="song.uri">
                <div class="select-song">
                  <i :class="song.isSelected ? 'fa-regular fa-check-square' : 'fa-regular fa-square'" @click="selectSong(song.uri)"></i>
                </div>
                <div class="image">
                  <img :src="song.albumArt" alt="bad habits">
                </div>
                <div class="info">
                  <p class="title">{{ song.title }}</p>
                  <p>{{ song.artist }}</p>
                  <p>{{ song.album }}</p>
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
          <button class="create-playlist" @click="showCreateModal">Create Playlist</button>
        </div>
        <button class="playlist-content" v-for="playlist in playlists" :id="playlist.id" @click="selectPlaylist">
          {{ playlist.name }}
        </button>
      </div>
    </div>
  </div>
  <createPlaylist ref="createPlaylistRef" />
  <alert-modal  ref="alertRef"/>
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
  
  button.tab-link{
    padding: 1rem .5rem;
    border: none;
    background: none;
    color: #D7D3CE;
    cursor: pointer;
  }
  
  button:hover{
    filter: brightness(1.2);
  }

  button.tab-link:hover{
    color: #21D754;
  }
  
  
  .playlists{
    grid-area: playlists;
  }
  
  .toolbar{
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    padding: .6rem 1rem;
    border-radius: 1rem;
    background: #0f1111;

    .btn-select-all{
      cursor: pointer;
    }

    .selected {
      color: #21D754;
    }

    button.btn-add-to-playlist{
      background: #21D754;
      color: #0f1111;
      padding: .5rem 1rem;
      border: none;
      border-radius: .5rem;
      cursor: pointer;
      margin-top: .5rem;
    }
  }

  .song-list{
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 1rem 0;

    .song{
      display: flex;
      align-items: center;
      background: #0f1111;
      padding: 1rem;
      border-radius: 1rem;
      gap: 2rem;

      .image img{
        width: 82px;
        height: 82px;
      }

      .title{
        font-weight: 700;
      }

      .select-song{
        font-size: 1.5rem;
      }

      .select-song .fa-check-square{
        color: #21D754;
        cursor: pointer;
      }
    }
  }
  
  .playlists{
    display: flex;
    flex-direction: column;
    gap: 1rem;

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
      border: none;
      outline: none;
      background: #0f1111;
      cursor: pointer;
      text-align: center;
      color: #D7D3CE;
    }

    .playlist-content.selected{
      background: #21D754;
      color: #0f1111;
    }
  }
  
  
</style>