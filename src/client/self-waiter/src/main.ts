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

// App.vue
import App from './App.vue'

const app = createApp(App);

app.use(vuetify)

app.mount('#app')
