<script setup lang="ts">
import { spinnerStore } from './SpinnerState';
</script>



<template>
    <div v-if="spinnerStore.isLoading" class="tw-z-[51] tw-fixed tw-bg-slate-800 tw-bg-opacity-60  tw-h-screen tw-w-screen tw-flex tw-items-center tw-justify-center">
        <div class="tw-flex tw-items-center">
          <svg class="tw-animate-spin tw-h-16 tw-w-16 md:tw-h-32 md:tw-w-32 tw-text-white" xmlns="http://www.w3.org/2000/svg" fill="none"
               viewBox="0 0 24 24">
            <circle class="tw-opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
            <path class="tw-opacity-75" fill="currentColor"
                  d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z">
            </path>
          </svg>
        </div>
    </div>
</template>


