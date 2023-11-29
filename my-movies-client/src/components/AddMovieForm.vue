<template>
  <form @submit="handleSubmit">
    <div :class="{ error: v$.movie.title.$error }" class="mb-3">
      <label class="form-label" for="title">Title</label>
      <input
          id="title"
          v-model="movie.title"
          class="form-control"
          placeholder="Movie title"
          type="text"
          @blur="v$.movie.title.$touch"
      >
      <div v-for="error of v$.movie.title.$errors" :key="error.$uid">
        <p class="error error--message">*{{ error.$message }}</p>
      </div>
    </div>

    <div :class="{ error: v$.movie.director.$error }" class="mb-3">
      <label class="form-label" for="director">Director</label>
      <input
          id="director"
          v-model="movie.director"
          class="form-control"
          placeholder="Director (optional)"
          type="text"
          @blur="v$.movie.director.$touch"
      >
      <div v-for="error of v$.movie.director.$errors" :key="error.$uid">
        <p class="error error--message">*{{ error.$message }}</p>
      </div>
    </div>

    <div :class="{ error: v$.movie.yearOfRelease.$error }" class="mb-3">
      <label class="form-label" for="yearOfRelease">Year of release</label>
      <input
          id="yearOfRelease"
          v-model="movie.yearOfRelease"
          class="form-control"
          max="2200"
          min="1900"
          placeholder="1900-2200"
          type="number"
          @blur="v$.movie.yearOfRelease.$touch"
      >
      <div v-for="error of v$.movie.yearOfRelease.$errors" :key="error.$uid">
        <p class="error error--message">*{{ error.$message }}</p>
      </div>
    </div>

    <div class="modal-footer">
      <button class="btn btn-secondary" @click="closeModal">Close</button>
      <button class="btn btn-primary" type="submit">
        Add Movie
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
import {maxLength, maxValue, minValue, required} from "@vuelidate/validators";

export default {
  name: 'AddMovieForm',
  setup() {
    return {v$: useVuelidate()}
  },
  data() {
    return {
      movie: {
        title: '',
        director: '',
        yearOfRelease: ''
      },
      isLoading: false
    }
  },
  validations() {
    return {
      movie: {
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
      await axios.post('/api/Movie', JSON.stringify(this.movie), {
        headers: {
          'content-type': 'text/json'
        }
      })
          .then(response => {
            Swal.fire({
              icon: 'success',
              title: 'Movie added successfully!',
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