<template>
  <div id="app">
    <div class="container">
      <h2 class="text-center mt-5 mb-3">My Movies</h2>
      <MoviesList />
    </div>
  </div>
</template>

<script>
import MoviesList from "@/components/MoviesList.vue";

export default {
  name: 'App',
  components: {MoviesList}
}
</script>

<style>
</style>
