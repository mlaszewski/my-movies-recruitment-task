<template>
  <tr class="align-middle">
    <td class="text-center">{{ movie.id }}</td>
    <td class="col-5">{{ movie.title }}</td>
    <td class="col-4">{{ movie.director }}</td>
    <td class="text-center">{{ movie.yearOfRelease }}</td>
    <td class="text-center col-2">
      <button class="btn btn-primary m-1" @click="handleEdit">Edit</button>
      <button class="btn btn-danger m-1" @click="handleDelete">Delete</button>
    </td>
  </tr>
</template>

<script>
import Swal from "sweetalert2";
import {deleteMovie} from "@/assets/ServiceAPI";

export default {
  name: 'MovieListItem',
  props: {
    movie: {
      type: Object,
      required: true
    }
  },
  methods: {
    async handleDelete() {
      await Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then(async (result) => {
        if (result.isConfirmed) {
          await deleteMovie(this.movie.id)
              .then(response => {
                Swal.fire({
                  icon: 'success',
                  title: 'Movie deleted successfully!',
                  showConfirmButton: false,
                  toast: true,
                  timer: 1500
                })
                this.$emit('setList', response.data);
                return response
              }).catch(error => {
                Swal.fire({
                  icon: 'error',
                  title: 'An error occured!',
                  showConfirmButton: false,
                  toast: true,
                  timer: 1500
                })
                return error
              });
        }
      });
    },
    handleEdit() {
      this.$emit('edit', this.movie);
    }
  }
}
</script>

<style scoped>

</style>