<template>
  <div class="__container">
      
    <div v-for="(layoutItem, index) in allProducts" :key="index" class="md-layout" style="padding-right: 15px">

        <div v-for="item in layoutItem" :key="item.id" class="md-layout-item md-size-20">

            <md-card :id="item.id" class="__cards">
                <md-card-header class="__card-header ">
                    <md-card-header-text>
                        <div class="md-title">{{ item.name }}</div>                        
                        <div class="md-subhead">Price: ₱{{item.price}}</div>
                        <div class="md-subhead">Remaining: {{item.quantity}}</div>
                    </md-card-header-text>

                    <md-card-media>
                        <img src="../assets/image/image-not-available.jpg" alt="product">
                    </md-card-media>
                </md-card-header>

                <md-card-actions>  
                    
                    <md-button class="md-icon-button" @click="onToggleQuantity(false, item)">
                        <md-icon>remove</md-icon>
                    </md-button>
                   
                    <input v-model="items.quantity[item.id]" class="__quantity" type="text">

                    <md-button class="md-icon-button" @click="onToggleQuantity(true, item)">
                        <md-icon>add</md-icon>
                    </md-button>
                                    
                    <md-button @click="onAddToCartButtonClick(item)" class="__btnaddcart">Add To Cart</md-button>
                </md-card-actions>
            </md-card>
        </div>
        
    </div>


    <v-snackbar
        v-model="error.Show"
        :vertical="vertical"
        :top="vertical"
        >
        {{ error.Message }}

        <template v-slot:action="{ attrs }">
            <v-btn
            color="indigo"
            text
            v-bind="attrs"
            @click="error.Show = false"
            >
            Close
            </v-btn>
        </template>
    </v-snackbar>

  </div>
  
</template>

<script>
 import axios from 'axios'

export default {
    props: {
        allProducts: {
            type: Array,
            default() {
                return []
            }
        }
    },
 
    data() {
        return {                                
            items: {
                quantity: []

            },
            purchases: {

            },
            error: {
                Show: false,
                Message: ''
            },   
            vertical: true              
        }
    },            
    methods: {
        onToggleQuantity(isAdd, item) {
           
            if(!isAdd && (this.items.quantity[item.id] == undefined || this.items.quantity[item.id] <= 0)) return
            
            let itemQuantity = this.items.quantity[item.id] == undefined ? 0 : this.items.quantity[item.id]
            
            if(isAdd && item.quantity < itemQuantity + 1) return                
            const newQuantity = isAdd ? itemQuantity + 1 : itemQuantity - 1             
            this.$set(this.items.quantity,item.id,newQuantity)           

        },
        getAllProductsByAccountID(){
            this.$emit('getAllProductsByAccountID');
        },
        onAddToCartButtonClick(item) {
            
            if(this.items.quantity[item.id] == undefined || this.items.quantity[item.id] <= 0) {
                this.displayMessage('Please select an item first before adding it to your cart.')
                return
            }
            
            var totalAmount = this.items.quantity[item.id] * item.price
            
             const param = {
                AccountID: localStorage.getItem('id'),
                ProductID: item.id,
                Quantity: this.items.quantity[item.id],
                IsCheckOut: false,
                Amount: totalAmount
            }

            axios.post(this.$store.state.appUrl+'/purchase/addpurchase', param).then((result) => {
             if (result.data.isSuccess){
                 this.displayMessage('Product added to cart')
                this.getAllProductsByAccountID()
             }
        });

        },
        displayMessage(message) {
            this.error.Message = message
            this.error.Show = true
        },
        
    }
}
</script>

<style scoped>

.__container {

    height: 100%;
    width: 100%;

}

.md-layout {
    height: 270px;
    margin-top: 10px;    
}

.__cards {
    height: 270px;
    padding: 10px;
}

.__card-header {
    height: 195px;
    border: thin solid gainsboro;
}

.md-layout-item {
    padding: 0px 15px 0 15px;
}

.__quantity {
    text-align: center;
    width: 50px;
}

.__btnaddcart {
    background-color: #C96210;
    color: white !important;
}

.md-subhead {
    opacity: .90 !important;
}


</style>