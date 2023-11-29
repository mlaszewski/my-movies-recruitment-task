<template>
  <div class="modal fade">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Add new movie</h5>
          <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"
                  @click="closeModal"></button>
        </div>
        <div class="modal-body">
          <slot @closeModal="closeModal"></slot>
        </div>
      </div>
    </div>
    <div class="modal-backdrop" @click="closeModal"></div>
  </div>
</template>

<script>
export default {
  name: 'UIModal',
  props: ['title'],
  methods: {
    closeModal() {
      this.$emit('closeModal');
    }
  }
}
</script>

<style scoped>
.modal {
  display: block;
  opacity: 1 !important;
}

.modal-dialog {
  z-index: 1080;
}

.modal-backdrop {
  opacity: 0.5 !important;
}
</style>
