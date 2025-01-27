<script setup lang="ts">
  import { ref } from 'vue';
  
  const isVisible = ref<boolean>(false);
  
  const closeAlert = () => {
    isVisible.value = false;
  };
  
  const showAlert = () => {
    isVisible.value = true;
  };
  
  defineExpose({ showAlert });
  
</script>

<template>
  <div class="modal" v-if="isVisible">
    <div class="modal-content">
      <div class="header">
        <p>Select Playlist</p>
      </div>
      <div class="body">
        <button @click="closeAlert" class="closeAlert">Close</button>
      </div>
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
    
    .body{
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

      button.closeAlert{
        background: #F45B5B;
      }
    }
  }
}

</style>