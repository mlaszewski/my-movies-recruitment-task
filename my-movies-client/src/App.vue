<template>
  <div id="app">
    <div class="container">
      <h2 class="text-center mt-5 mb-3">My Movies</h2>
      <MovieList/>
    </div>
  </div>
</template>

<script>
import MovieList from "@/components/MovieList.vue";

export default {
  name: 'App',
  components: {MovieList}
}
</script>

<style>
</style>
