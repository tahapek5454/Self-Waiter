<script setup lang="ts">
import { spinnerStore } from './components/spinners/SpinnerState';
import Spinner from './components/spinners/Spinner.vue';
import { toast } from 'vue3-toastify';

const notify = () => {
  toast.info('Hello world!');
  toast.success('Hello world!');
  toast.error('Hello world!');
}

const loading = () => {
  spinnerStore.showLoading();
  setTimeout(() => {
    spinnerStore.hideLoading();
  }, 3000);
}
</script>

<template>
  <div>
    <Spinner />
    <div class="tw-flex tw-flex-col tw-space-y-4 tw-h-screen tw-justify-center tw-items-center">
      <v-btn @click="loading">
        Loading...
      </v-btn>

      <v-btn @click="notify">
        Toast
      </v-btn>
    </div>
  </div>

</template>
