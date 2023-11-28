import { createApp } from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.css';
import axios from 'axios';
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

axios.defaults.baseURL = process.env.VUE_APP_BASEURL;

const app = createApp(App);

app.use(VueSweetalert2);

app.mount('#app');
