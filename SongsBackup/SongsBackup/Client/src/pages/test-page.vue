<script setup lang="ts">
  // import { axiosService } from "@/Services/AxiosService.ts";
  function uploadFiles(){
    
  }
</script>

<template>
  
  <div class="header">
    <h1>This is a page for testing new functionality</h1>
  </div>
  <div class="body">
    <input type="file" id="fileSelector" multiple>
    <button @click="uploadFiles">Upload</button>
  </div>
</template>

<style scoped>

</style>