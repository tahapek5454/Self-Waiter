<script setup lang="ts">
import { spinnerStore } from './components/spinners/SpinnerState';
import Spinner from './components/spinners/Spinner.vue';
</script>

<template>
  <div>
    <Spinner />
    <div class="tw-flex tw-h-screen tw-justify-center tw-items-center">
      <v-btn @click="spinnerStore.showLoading()">
        Button
      </v-btn>
    </div>
  </div>

</template>
