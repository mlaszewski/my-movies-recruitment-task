<template>
  <div class="card">
    <div class="card-header">
      <button class="btn btn-primary mx-1" @click="toggleAddModal">
        Add new movie
      </button>
      <button class="btn btn-outline-primary mx-1" @click="fetchExternalMovieList">
        Fetch movies
      </button>
    </div>
    <div class="card-body">
      <table class="table table-bordered table-responsive">
        <thead>
        <tr class="align-middle text-center">
          <th>Id</th>
          <th>Title</th>
          <th>Director</th>
          <th>Year</th>
          <th>Actions</th>
        </tr>
        </thead>
        <tbody>
        <tr v-if="isLoading">
          <td class="text-center" colspan="5">
            <div class="spinner-border text-primary" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </td>
        </tr>
        <template v-if="!isLoading && movies.length > 0">
          <MovieListItem
              v-for="movie in movies"
              :key="movie.id"
              :movie="movie"
              @edit="toggleEditModal"
              @setList="setMovies"/>
        </template>
        <tr v-if="!isLoading && movies.length === 0">
          <td class="text-center" colspan="5">No movies found.</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>

  <teleport v-if="showAddModal" to=".modals">
    <UIModal
        title="Add new movie"
        @closeModal="toggleAddModal"
    >
      <AddMovieForm
          @closeModal="toggleAddModal"
          @setList="setMovies"
      />
    </UIModal>
  </teleport>

  <teleport v-if="showEditModal" to=".modals">
    <UIModal
        title="Edit movie"
        @closeModal="toggleEditModal"
    >
      <EditMovieForm
          :movie="selectedMovie"
          @closeModal="toggleEditModal"
          @setList="setMovies"
      />
    </UIModal>
  </teleport>
</template>

<script>
import Swal from "sweetalert2";
import MovieListItem from "@/components/MovieListItem.vue";
import UIModal from "@/components/Modal.vue";
import AddMovieForm from "@/components/AddMovieForm.vue";
import EditMovieForm from "@/components/EditMovieForm.vue";
import {addMovie, getExternalMovieList, getMovieList} from "@/assets/ServiceAPI";

export default {
  name: 'MovieList',
  components: {EditMovieForm, AddMovieForm, MovieListItem, UIModal},
  data() {
    return {
      movies: [],
      isLoading: false,
      showEditModal: false,
      showAddModal: false,
      selectedMovie: null
    }
  },
  mounted() {
    this.fetchMovieList();
  },
  methods: {
    setMovies(movies) {
      this.movies = movies;
    },
    async fetchMovieList() {
      this.isLoading = true;
      await getMovieList()
          .then(response => {
            this.setMovies(response.data);
            return response;
          })
          .catch(error => {
            console.log(error);
            let errorMessage = 'An unexpected error occured.';
            if (error.response.status === 404)
              errorMessage = 'No movies found.';
            else if (error.response.status === 500)
              errorMessage = 'An error occured while fetching movies.';
            else if (error.response.status === 401)
              errorMessage = 'You are not authorized to view this page.';

            Swal.fire({
              icon: 'error',
              title: errorMessage,
              showConfirmButton: false,
              position: "bottom-end",
              toast: true,
              timer: 3500
            })
            return error
          })
          .finally(() => {
            this.isLoading = false;
          });
    },
    async fetchExternalMovieList() {
      this.isLoading = true;
      await getExternalMovieList()
          .then(async response => {
            for (const movie of response.data) {
              const {title, director, year} = movie;
              if(this.movies.findIndex(item => item.title === title) === -1) {
                await addMovie({
                  "title": title,
                  "director": director,
                  "yearOfRelease": year
                })
              }
            }
            await this.fetchMovieList();
            return response;
          }).catch(error => {
            Swal.fire({
              icon: 'error',
              title: "There was an error fetching movies.",
              showConfirmButton: false,
              position: "bottom-end",
              toast: true,
              timer: 3500
            })
            return error;
          }).finally(() => {
            this.isLoading = false;
          })
    },
    toggleEditModal(movie) {
      this.selectedMovie = movie;
      this.showEditModal = !this.showEditModal;
    },
    toggleAddModal() {
      this.showAddModal = !this.showAddModal;
    }
  },
}
</script>

<style>

</style>