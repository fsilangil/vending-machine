<template>
    <div class="__container">
        <div class="navbar">            
            <md-toolbar class="md-primary navbar-wrapper">
                
                <div class="__mright">
                   <v-btn     
                    @click="toggleProductModal"              
                    color="blue-grey darken-1"
                    class="ma-2 white--text"                  
                    >
                    Add Product
                    <v-icon
                        right
                        dark
                    >
                        mdi-cloud-upload
                    </v-icon>
                    </v-btn>
                </div>   

                 <div class="__mright">
                   Logged As: <span class="__email">{{ account.Email }}</span>
                </div>

                 <div class="__mright">
                   Account Balance: ₱<span class="__balance">{{ account.Balance | formatCurrency }}</span>
                </div>
                
                <div class="__mright">
                    <md-badge :md-content="cartItems">
                        <md-button class="md-icon-button" @click="toggleAddCartModal">
                        <md-icon>shopping_cart</md-icon>
                        </md-button>
                    </md-badge>
                </div>                
               
                <div>                   
                    <md-button class="md-icon-button" @click="logout">
                    <md-icon title="logout">exit_to_app</md-icon>
                    </md-button>
                    
                </div>

                <h3 class="md-title"></h3>
            </md-toolbar>

            <ProductModal @getAllProducts="getAllProducts" @toggleProductModal="toggleProductModal" :showProductModal="showProductModal" />
            <AddCartModal v-if="showAddCartModal" @toggleAddCartModal="toggleAddCartModal" :showAddCartModal="showAddCartModal" :productsByAccountId="allProductsByAccountID" @initializeData="initializeData" :allSummaryOrders="allSummaryOrders"/>

        </div>
        
        <div class="__main">
            <Products :allProducts="productProp" @addToCart="addToCart" @getAllProductsByAccountID="getAllProductsByAccountID"/>
        </div>

        

    </div>
</template>

<script>

import Products from './products'
import ProductModal from './product-modal'
import AddCartModal from './addcart-modal'
import axios from 'axios'

export default {

    components: {
        Products,
        ProductModal,
        AddCartModal
    },    
    data() {
        return {
            accountBalance: 0,
            emailAddress: '',
            cartItems: 0,
            account: {
                Id: 0,
                Email: '',
                Balance: 0,
                Guestid: 0
            },
            showProductModal: false,
            showAddCartModal: false,
            productProp: [],
            allProductsByAccountID: [],
            allSummaryOrders: []
        }
    },
    computed: {
        products() {
            return this.$store.getters.getAllProducts
        }
    },
    created() {
       this.initializeData()
    },    
    methods: {
        initializeData(isToggle){
            this.account.Guestid = localStorage.getItem('id')
            this.getAccountDetails()
            this.getAllProducts(isToggle)
        },
        addToCart() {
            
            this.cartItems = this.cartItems + 1
        },
        getAccountDetails() {
            
            axios.get(this.$store.state.appUrl + '/account/getbyid?id='+this.account.Guestid).then((response) => {

                const result = response.data
                
                this.account.Email = result.guest.emailAddress
                this.account.Balance = result.balance

            })
        },
        toggleProductModal() {
            this.showProductModal = !this.showProductModal
        },
        async toggleAddCartModal() {
            this.showAddCartModal = !this.showAddCartModal
        },
        renderProducts() {
            
            const _products =  this.products.length / 5 
            let layout = 0
            let _layoutItems = []

            const productLayout = _products                     
            if(productLayout > 0) {
                layout = productLayout > 1 ? Math.trunc(productLayout) + 1 : Math.trunc(1)
            } else {
                layout = _products
            }

            let prodcts = this.products            
            for (let index = 0; index < layout; index++) {                               
                const productFiltered = prodcts.filter((obj,idx) => idx < 5)             
                _layoutItems.push(productFiltered)    
                prodcts.splice(index,5)
            }                        
            
            this.productProp = _layoutItems    
        },
        async getAllProducts(isToggle) {
            await this.$store.dispatch('GET_PRODUCTS').then(() => {
                this.getAllProductsByAccountID(isToggle)
                this.getAllSummaryOrder(isToggle)
                this.renderProducts()
            })            
        },
        async getAllProductsByAccountID(isToggleAddCartModal) {
             this.account.Id = localStorage.getItem('id');
             return axios.get(this.$store.state.appUrl+'/purchase/getallpurchase?accountID='+this.account.Id).then((result) => {                 
                    this.cartItems = result.data.length
                    this.allProductsByAccountID = result.data
                    if(isToggleAddCartModal != undefined || isToggleAddCartModal == true) {
                        this.showAddCartModal = !this.showAddCartModal
                    }
            });      
        },
        async getAllSummaryOrder(isToggleAddCartModal) {
             this.account.Id = localStorage.getItem('id');
             return axios.get(this.$store.state.appUrl+'/purchase/getallsummaryorder?accountID='+this.account.Id).then((result) => {                 
                    this.allSummaryOrders = result.data
                    if(isToggleAddCartModal != undefined || isToggleAddCartModal == true) {
                        this.showAddCartModal = !this.showAddCartModal
                    }
            });      
        },
        logout() {
            localStorage.removeItem('id')
            this.$router.push('/')
        }
    },
    filters: {
        formatCurrency(value) {
            return parseFloat(value).toFixed(2)
        }
    }

}
</script>

<style scoped>

.__container {

    width: 100%;
    height: 100%;
    
}

.__main {
    margin-top: 7px;
    width: 100%;
    height: 100%;
}

.navbar-wrapper {
    display: flex;
    justify-content: flex-end;
    /* flex-direction: row-reverse; */
}

.__email,
.__balance {
    color: #7cfc00;
}

.__mright {
    margin-right: 20px;
}

.addprduct {
    display: flex;
    justify-content: center;
    align-items: center;
}

</style>
