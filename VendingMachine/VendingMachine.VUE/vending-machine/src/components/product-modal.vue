<template>
  
    <div>
        <v-row justify="center">
                <v-dialog
                v-model="showProductModal"
                persistent
                max-width="600px"
                >                
                <v-card>
                    <v-card-title>
                    <span class="headline">Add Product</span>
                    </v-card-title>
                    <v-card-text>
                    <v-container>
                        <v-row>
                        
                        <v-col cols="12">
                            <v-text-field
                            label="Product Name*"
                            required
                            counter
                            maxlength="100"
                            v-model="product.Name"
                            :rules="rules"
                            ></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-text-field
                            label="Price*"                            
                            required
                            v-model="product.Price"
                            ></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-text-field
                            label="Quantity*"
                            required
                            v-model="product.Quantity"
                            ></v-text-field>
                        </v-col>
                        </v-row>
                    </v-container>
                    <small style="color:red">*indicates required field</small>
                    </v-card-text>
                    <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        color="blue darken-1"
                        text
                        @click="toggleProductModal"
                    >
                        Cancel
                    </v-btn>
                    <v-btn
                        color="blue darken-1"
                        text
                        @click="onSubmitClick"
                    >
                        Submit
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
        showProductModal: {
            type: Boolean,
            default: false
        },
    },
    data() {
        
        return {
            rules: [v => v.length <= 25 || 'Max 25 characters'],
            product: {
                Name: '',
                Price: 0,
                Quantity: 0
            },
            error: {
                Show: false,
                Message: ''
            },
            vertical: true
        }
    },
    methods: {
        toggleProductModal() {
            this.$emit('toggleProductModal')
        },
        isFormValid() {

            return this.product.Name == '' && this.product.Quantity <= 0 && this.product.Price <= 0 ? false : true

        },
        displayMessage(message) {
            this.error.Message = message
            this.error.Show = true
        },
        onSubmitClick() {
            if(!this.isFormValid()) {
                this.displayMessage('All fields are required')
                return
            }

            this.addProduct()
        },
        addProduct() {
            
            const param = {
                Name: this.product.Name,
                Price: this.product.Price,
                Quantity: this.product.Quantity 
            }

            axios.post(this.$store.state.appUrl+'/product/addaproduct', param).then((response) => {
                
                const result = response.data 

                if(result.isSuccess) {      
                    this.displayMessage('Product successfuly added')
                    this.$emit('getAllProducts')
                    this.toggleProductModal()
                    this.resetForm()
                    
                } else {
                    this.displayMessage('An error occured upon adding the product. Please try again.')
                }

            });

        },
        resetForm() {
            this.product = {
                Name: '',
                Price: 0,
                Quantity: 0
            }
        }
    }

}
</script>

<style>

</style>