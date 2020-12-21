import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';
import '@mdi/font/css/materialdesignicons.css'

Vue.use(Vuetify);

export default new Vuetify({

    //Icon links reference: https://materialdesignicons.com/
    icons: {
        iconfont: 'mdi',
      },
});
