import { createApp } from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.css';
import axios from 'axios';
import VueAxios from 'vue-axios'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

const app = createApp(App);

axios.defaults.baseURL = process.env.VUE_APP_BASEURL;

app.use(VueSweetalert2);
app.use(VueAxios, axios);
app.mount('#app');
