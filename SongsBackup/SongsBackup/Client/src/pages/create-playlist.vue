<script setup lang="ts">
import { ref } from 'vue';

const playlistName = ref('');
const playlistDescription = ref('');

const createPlaylist = () => {
  fetch('/api/songs/create-playlist', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      name: playlistName.value,
      description: playlistDescription.value,
      public: true
    })
  });
};

const isModalVisible = ref(false);

const showModal = () => {
  isModalVisible.value = true;
};

const closeModal = () => {
  isModalVisible.value = false;
};

defineExpose({ showModal });
</script>

<template>
  <div v-if="isModalVisible" class="modal">
    <div class="modal-content">
      <p class="header">Create Playlist</p>
      <form @submit.prevent="createPlaylist">
        <div class="form-group">
          <input type="text" id="playlistName" v-model="playlistName" placeholder="Playlist Name" />
        </div>
        <div class="form-group">
          <textarea id="playlistDescription" v-model="playlistDescription" placeholder="Description"></textarea>
        </div>
        <div class="form-actions">
          <button type="button" class="cancel" @click="closeModal">Cancel</button>
          <button type="button" class="create" @click="createPlaylist">Create</button>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
.modal {
  display: flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(125, 124, 124, 0.5);
  color: #D7D3CE;
  font-family: 'Helvetica';

  .modal-content {
    background: #0f1111;
    padding: 2rem;
    border-radius: 0.5rem;
    width: 430px;
    
    .header{
      font-size: .8rem;
      margin-bottom: 2rem;
    }

    .form-group {
      margin-bottom: 1rem;

      input, textarea{
        border: 1px solid #414141;
        outline: none;
        padding: 1rem;
        background: none;
        width: 100%;
        color: #D7D3CE;
      }
      
      input::placeholder, textarea::placeholder{
        color: #fff;
        font-weight: 200;
      }
      
      #playlistDescription{
        height: 150px;
        resize: none;
      }
    }

    .form-actions {
      display: flex;
      justify-content: space-between;
      gap: 1rem;
      margin-top: 1.5rem;
      
      button{
        padding: 0.6rem 1.2rem;
        flex: 1;
        border: none;
        border-radius: 0.6rem;
        cursor: pointer;
      }
      
      button:hover{
        filter: brightness(1.2);
      }
      
      button.cancel{
        background: #F45B5B;
      }
      
      button.create{
        background: #21D754;
      }
    }
  }
}


</style>