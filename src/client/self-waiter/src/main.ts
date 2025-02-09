import { createApp } from 'vue'

// tailwind.css
import './style.css'

// material symbols
import 'material-symbols';

// Vuetify
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { aliases, mdi } from 'vuetify/iconsets/mdi'

const vuetify = createVuetify({
    components,
    directives,
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
          mdi,
        },
      },
  })

 // Toast
import Vue3Toastify, { type ToastContainerOptions } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

// App.vue
import App from './App.vue'

// Store
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)

const app = createApp(App);

app.use(vuetify)

app.use(
  Vue3Toastify,
  {
    autoClose: 3000,
    position: 'top-right',
    closeButton: true,
    theme: 'light',
    closeOnClick: true,
    multiple: true,
  } as ToastContainerOptions,
);


app.use(pinia)

app.mount('#app')
