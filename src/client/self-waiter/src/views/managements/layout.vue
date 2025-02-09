<template>
    <div>
        <v-card>
            <v-layout>
                <v-navigation-drawer elevation="24" v-model="drawer" :rail="rail" permanent @click="rail = false">
                    <v-list-item
                        prepend-avatar="https://raw.githubusercontent.com/tahapek5454/Self-Waiter/refs/heads/main/docs/sw-icon-small-foto-circle.png"
                        title="SELF WAITER" nav>
                        <template v-slot:append>
                            <v-btn icon="mdi-chevron-left" variant="text" @click.stop="rail = !rail"></v-btn>
                        </template>
                    </v-list-item>

                    <v-divider></v-divider>

                    <v-list density="compact" nav>
                        <v-list-item prepend-icon="mdi-home-city" title="Ana Sayfa" value="home"></v-list-item>
                        <v-list-item prepend-icon="mdi-cart-arrow-down" title="Siparişler" value="orders"></v-list-item>
                        <v-list-item prepend-icon="mdi-poll" title="Analizler" value="analyze"></v-list-item>
                        <v-list-item prepend-icon="mdi-home-group" title="Şube İşlemleri" value="dealer"></v-list-item>
                    </v-list>


                    <template v-slot:append>
                        <v-list density="compact" nav>
                            <v-list-item prepend-icon="mdi-exit-to-app" title="Çıkış Yap" value="exit"></v-list-item>
                        </v-list>
                    </template>

                </v-navigation-drawer>

                
                <!-- <div
                    :class="`${dealerMenu ? 'tw-w-[30rem]' : 'tw-w-20'} tw-z-[1004] tw-h-20 tw-fixed tw-top-0 tw-right-0 tw-m-4 tw-rounded-bl-3xl tw-bg-white tw-shadow-lg  tw-flex  tw-justify-around tw-items-center`">
                    <div v-if="dealerMenu" class="tw-w-80">
                        <v-combobox label="Şubeler"
                        :items="['California', 'Colorado', 'Florida', 'Georgia', 'Texas', 'Wyoming']"
                        variant="underlined"></v-combobox>
                    </div>
                    
                    <span class="material-symbols-outlined tw-cursor-pointer !tw-text-3xl" @click="dealerMenu = !dealerMenu">
                        menu
                    </span>
                </div> -->


                <div
          :class="[
            dealerMenu ? 'tw-w-[18rem] md:tw-w-[28rem]' : 'tw-w-16',
            'tw-z-[1004] tw-h-[4.1rem] md:tw-h-20 tw-fixed tw-top-0 tw-right-0 tw-m-4 tw-rounded-bl-3xl tw-bg-white tw-shadow-lg tw-flex  tw-items-center tw-transition-all tw-duration-300 tw-ease-in-out'
          ]">
          
          <!-- Combobox kısmı -->
          <transition name="slide-fade">
            <div v-if="dealerMenu" class="tw-w-[14rem] md:tw-w-80 tw-ml-4">
              <v-combobox 
                label="Şubeler"
                :items="['California', 'Colorado', 'Florida', 'Georgia', 'Texas', 'Wyoming']"
                variant="underlined"></v-combobox>
            </div>
          </transition>
          
          <span class="material-symbols-outlined tw-cursor-pointer tw-right-0 tw-absolute tw-mr-4 md:!tw-text-3xl" @click="dealerMenu = !dealerMenu">
            menu
          </span>
        </div>

                <v-main>
                    <div class="tw-h-screen tw-flex tw-items-center tw-justify-center tw-bg-[#EEEEEE]">
                        <router-view></router-view>
                    </div>
                    <div class="tw-h-screen tw-flex tw-items-center tw-justify-center tw-bg-orange-500">
                        Hello World
                    </div>
                </v-main>


            </v-layout>
        </v-card>

    </div>
</template>


<script setup lang="ts">
import { ref } from 'vue';
const drawer = ref(true);
const rail = ref(true);
const dealerMenu = ref(false);


</script>

<style scoped>
.slide-fade-enter-active, .slide-fade-leave-active {
  transition: all 0.1s ease;
}
.slide-fade-enter-from, .slide-fade-leave-to {
  opacity: 0;
  transform: translateX(-20px);
}
</style>