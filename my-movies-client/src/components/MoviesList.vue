<script>
import axios from "axios";
import Swal from "sweetalert2";

export default {
  name: 'MoviesList',
  data() {
    return {
      movies: []
    }
  },
  created() {
    this.fetchProjectList();
  },
  methods: {
    fetchProjectList() {
      axios.get('/api/Movie')
          .then(response => {
            this.movies = response.data;
            return response
          })
          .catch(error => {
            return error
          });
    },
    handleDelete(id){
      Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then((result) => {
        if (result.isConfirmed) {
          axios.delete(`/api/Movie/${id}`)
              .then( response => {
                Swal.fire({
                  icon: 'success',
                  title: 'Project deleted successfully!',
                  showConfirmButton: false,
                  timer: 1500
                })
                this.fetchProjectList();
                return response
              })
              .catch(error => {
                Swal.fire({
                  icon: 'error',
                  title: 'An Error Occured!',
                  showConfirmButton: false,
                  timer: 1500
                })
                return error
              });
        }
      })
    }
  },
}
</script>

<template>
  <div class="card">
    <div class="card-header">
      <button class="btn btn-primary mx-1">
        Add new movie
      </button>
      <button class="btn btn-outline-primary mx-1">
        Add new movie
      </button>
    </div>
    <div class="card-body">
      <table class="table table-bordered">
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
        <tr v-for="movie in movies" :key="movie.id" class="align-middle">
          <td class="text-center">{{ movie.id }}</td>
          <td>{{ movie.title }}</td>
          <td>{{ movie.director }}</td>
          <td class="text-center">{{ movie.yearOfRelease }}</td>
          <td>
            <button class="btn btn-info mx-1">Edit</button>
            <button class="btn btn-danger mx-1" @click="handleDelete(movie.id)">Delete</button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>

</style>