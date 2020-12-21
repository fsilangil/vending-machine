import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {

    appUrl: 'https://localhost:44325/api',
    //'http://localhost:64120/api'
    products: []

  },
  mutations: {

    SET_PRODUCTS(state, products) {
      state.products = products
    }

  },
  actions: {
  
    GET_PRODUCTS({ commit}) {

      return axios.get(this.state.appUrl+'/product/getallproducts').then((response) => {

        const result = response.data
        console.log(result)
        commit('SET_PRODUCTS', result)

      })

    }
  
  },
  getters: {
    getAllProducts(state) {
      return state.products
    }
  },
  modules: {
  }
})
