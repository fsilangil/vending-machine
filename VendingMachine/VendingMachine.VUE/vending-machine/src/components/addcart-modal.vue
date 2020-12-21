<template>
    <div>

        <v-row justify="center">
                <v-dialog
                v-model="showAddCartModal"
                persistent
                max-width="980px"
                style="height: 650px;"
                >                
                <v-card>
                    <v-card-title>
                    <span class="headline">My Order Details</span>
                    </v-card-title>
                    <v-card-text>
                   
                        <v-tabs
                            v-model="tab"
                            background-color="#448aff"
                            centered
                            dark
                            icons-and-text
                            >
                            <v-tabs-slider></v-tabs-slider>

                            <v-tab @click="onTabClick(true)" href="#tab-1">
                                ON MY CART
                                <v-icon>mdi-cart-variant</v-icon>
                            </v-tab>

                            <v-tab @click="onTabClick(true)" href="#tab-2">
                                ORDER HISTORY
                                <v-icon>mdi-history</v-icon>
                            </v-tab>
                           
                            </v-tabs>
                            <v-tabs-items v-model="tab">
                            <v-tab-item
                                :key="1"
                                :value="'tab-' + 1"
                            ><v-card flat>
                                <v-card-text>
                                   <v-simple-table v-if="IsTabCartIsActive">
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                            Product Name
                                            </th>
                                            <th class="text-left">
                                            Product Price
                                            </th>
                                             <th class="text-left">
                                            Quantity
                                            </th>
                                            <th class="text-left">
                                            Total Price
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                           v-for="item in productsByAccountId"
                                            :key="item.id"
                                        >
                                            <td>{{ item.product.name }}</td>
                                            <td>{{ item.product.price }}</td>
                                            <td>{{ item.quantity}} </td>
                                            <td>{{ item.amount}} </td>
                                        </tr>
                                        </tbody>
                                    </v-simple-table>
                                    <div class="__total">TOTAL AMOUNT: {{totalAmount}}</div>
                                </v-card-text>
                             </v-card>
                            </v-tab-item>
                             <v-tab-item
                                :key="2"
                                :value="'tab-' + 2"
                            ><v-card flat>
                                <v-card-text>
                                   <v-simple-table v-if="IsTabCartIsActive">
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                            Product Name
                                            </th>
                                            <th class="text-left">
                                            Product Price
                                            </th>
                                             <th class="text-left">
                                            Quantity
                                            </th>
                                            <th class="text-left">
                                            Total Price
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                           v-for="item in allSummaryOrders"
                                            :key="item.id"
                                        >
                                            <td>{{ item.product.name }}</td>
                                            <td>{{ item.product.price }}</td>
                                            <td>{{ item.quantity}} </td>
                                            <td>{{ item.amount}} </td>
                                        </tr>
                                        </tbody>
                                    </v-simple-table>
                                     <div class="__total">TOTAL AMOUNT: {{summaryAmount}}</div>
                                </v-card-text>
                             </v-card>
                            </v-tab-item>
                            </v-tabs-items>                    
                            </v-card-text>
                            <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn
                                color="red darken-1"
                                text
                                @click="toggleAddCartModal"
                            >
                                Close
                            </v-btn>     
                             <v-btn
                                color="#C96210"
                                text
                                @click="checkoutProduct"
                            >
                                CheckOut
                            </v-btn>                             
                            </v-card-actions>
                </v-card>
                </v-dialog>
            </v-row>
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
        showAddCartModal: {
            type: Boolean,
            default: false
        },
        productsByAccountId: {
            type: Array,
            default() {
                return []
            }
        },
        allSummaryOrders:{
            type: Array,
            default() {
                return []
            }
        }    
    },
   
    watch: {
        showAddCartModal(newValue) {
            if(newValue) {
                this.onTabClick(true)
            }
        }
    },
    data() {
        return {
            tab: null,
            text: 'Tab 1',
            products: [],
            IsTabCartIsActive: true,
            error: {
                Show: false,
                Message: ''
            },   
            vertical: true
        }
    },
    computed: {
        totalAmount() {
            return this.productsByAccountId.reduce((acc, item) => acc + item.amount, 0)
        },
        summaryAmount(){
            return this.allSummaryOrders.reduce((acc, item) => acc + item.amount, 0)
        }
    },
    methods: {
        toggleAddCartModal() {
            this.$emit('toggleAddCartModal')
        },
        onTabClick(isCartActive) {
            this.text = isCartActive ? 'Items' : 'Summary'
            this.IsTabCartIsActive = isCartActive ? true : false
        },
        displayMessage(message) {
            this.error.Message = message
            this.error.Show = true

                var vm = this;
                setTimeout(function(){
                     vm.$emit('initializeData', true)
                 }, 1000);

        },
        checkoutProduct()
        {
            axios.put(this.$store.state.appUrl+'/purchase/checkoutproduct', this.productsByAccountId).then((response) => {
            if(response.data.isSuccess) {      
                this.displayMessage('Checkout successful', true)
            }
            else{
                this.displayMessage(response.data.message)
            }
        });
        }
    }
}
</script>

<style scoped>

.v-dialog {
    overflow-y: hidden !important;
}

.v-window-item {
    height: 600px !important;
    margin-top: 10px;
    overflow-y: auto;
}

.v-tab--active {
    color: white !important;
}

.__btnCheckout{
    background-color: #C96210;
    color: white !important;
}

.__total{
    color: green;
    font-weight: bold;
    text-align: right;
}

</style>