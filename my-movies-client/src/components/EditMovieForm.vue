<template>
  <form @submit="handleSubmit">
    <div :class="{ error: v$.newMovie.title.$error }" class="mb-3">
      <label class="form-label" for="title">Title</label>
      <input
          id="title"
          v-model="newMovie.title"
          class="form-control"
          placeholder="Movie title"
          type="text"
          @blur="v$.newMovie.title.$touch"
      >
      <div v-for="error of v$.newMovie.title.$errors" :key="error.$uid">
        <p class="error error--message">*{{ error.$message }}</p>
      </div>
    </div>

    <div :class="{ error: v$.newMovie.director.$error }" class="mb-3">
      <label class="form-label" for="director">Director</label>
      <input
          id="director"
          v-model="newMovie.director"
          class="form-control"
          placeholder="Director (optional)"
          type="text"
          @blur="v$.newMovie.director.$touch"
      >
      <div v-for="error of v$.newMovie.director.$errors" :key="error.$uid">
        <p class="error error--message">*{{ error.$message }}</p>
      </div>
    </div>

    <div :class="{ error: v$.newMovie.yearOfRelease.$error }" class="mb-3">
      <label class="form-label" for="yearOfRelease">Year of release</label>
      <input
          id="yearOfRelease" v-model="newMovie.yearOfRelease" class="form-control" max="2200"
          min="1900"
          placeholder="1900-2200"
          step="1"
          type="number"
          @blur="v$.newMovie.yearOfRelease.$touch"
      >
      <div v-for="error of v$.newMovie.yearOfRelease.$errors" :key="error.$uid">
        <p class="error error--message">*{{ error.$message }}</p>
      </div>
    </div>

    <div class="modal-footer">
      <button class="btn btn-secondary" @click="closeModal">Close</button>
      <button class="btn btn-primary" type="submit">
        Save Changes
        <template v-if="isLoading">
          <span aria-hidden="true" class="spinner-border spinner-border-sm" role="status"></span>
          <span class="visually-hidden">Loading...</span>
        </template>
      </button>
    </div>
  </form>
</template>

<script>
import axios from "axios";
import Swal from "sweetalert2";
import {useVuelidate} from "@vuelidate/core";
import {required, minValue, maxValue, maxLength} from "@vuelidate/validators";

export default {
  name: 'EditMovieForm',
  setup() {
    return {v$: useVuelidate()}
  },
  props: ['movie'],
  data() {
    return {
      newMovie: {
        title: this.movie.title,
        director: this.movie.director,
        yearOfRelease: this.movie.yearOfRelease
      },
      isLoading: false
    }
  },
  validations() {
    return {
      newMovie: {
        title: {required, maxLength: maxLength(200)},
        director: {maxLength: maxLength(200)},
        yearOfRelease: {required, minValue: minValue(1900), maxValue: maxValue(2200)}
      }
    }
  },
  methods: {
    async handleSubmit(event) {
      event.preventDefault();
      this.isLoading = true;
      const isFormCorrect = await this.v$.$validate()
      if (!isFormCorrect) {
        this.isLoading = false;
        return;
      }
      await axios.put(`/api/Movie/${this.movie.id}`, JSON.stringify(this.newMovie), {
        headers: {
          'content-type': 'text/json'
        }
      })
          .then(response => {
            Swal.fire({
              icon: 'success',
              title: 'Movie edited successfully!',
              showConfirmButton: false,
              toast: true,
              timer: 1500
            })
            this.$emit('setList', response.data);
            this.$emit('closeModal');
            return response;
          }).catch(error => {
            Swal.fire({
              icon: 'error',
              title: 'An error occured!',
              showConfirmButton: false,
              toast: true,
              timer: 1500
            })
            console.log(error);
            return error;
          }).finally(() => {
            this.isLoading = false;
          });
    },
    closeModal() {
      this.$emit('closeModal');
    }
  }
}
</script>

<style scoped>
.error {
  color: red;
}

.error--message {
  font-size: 0.9em;
  margin-top: 0.25em;
}
</style>