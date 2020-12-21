<template>
    <div class="__container">
        <div></div>
        <div class="__login">
            <div class="wrapper">
                <div class="__title __border">
                    <span class="__title-caption">Welcome To Vending Machine</span>
                </div>
                <div class="__controls __border">
                    <div v-if="!isSignup" class="login-section">
                        <md-field>
                        <label>Enter your email address</label>
                        <md-input @keyup.enter="onLoginClick" v-model="user.Email" md-counter="100"></md-input>                     
                    </md-field>
                    <div class="__btn-login">
                        <div @click="onSignupClick" class="signup-link">
                            <span><u>No account yet? Click here</u></span> 
                        </div>
                        <div class="btnlogin">
                            <md-button @click="onLoginClick" class="md-raised md-primary">LOGIN</md-button>
                        </div>                        
                    </div>                    
                    
                    </div>
                    <div v-else class="signup-section">
                        
                    <md-field>
                        <label>Enter your email address</label>
                        <md-input v-model="user.Email" md-counter="100"></md-input>                     
                    </md-field>

                    <md-field>
                        <label>Enter Amount</label>
                        <span class="md-prefix">₱</span>
                        <md-input v-model="user.Balance"></md-input>
                    </md-field>
                    
                    <div class="signup-btn">
                        <md-button class="md-raised md-accent" @click="onSignupClick">BACK TO LOGIN</md-button>
                        <md-button @click="onSignUpClick" class="md-raised md-primary">SIGN UP</md-button>
                    </div>                    

                    </div>
                    
                </div>
            </div>
        
            
            <v-overlay :value="overlay">
                <v-progress-circular
                    indeterminate
                    size="64"
                ></v-progress-circular>
            </v-overlay>

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

        <div></div>

    </div>   

</template>

<script>

import axios from 'axios'

export default {

    data() {
        return {
            regular: null,
            isSignup: false,
            balance: 0.00,
            indeterminate: 5,
            overlay: false,
            vertical: true,
            user: {
                Email: '',
                Balance: 0
            },
            error: {
                Show: false,
                Message: ''
            }            

        }
    },
    methods: {
        onSignupClick() {
            this.isSignup = !this.isSignup
            this.user.Email = ''
        },
        displayMessage(message) {
            this.error.Message = message
            this.error.Show = true
        },
        isValidEmail(email) {
            const re = /\S+@\S+\.\S+/
            return re.test(email)
        },
        onLoginClick() {

            if(!this.isSignup && this.user.Email == '') {
                this.displayMessage('Email Address is required.')                
                return
            }
            this.loginAuthentication(this.user.Email)
            
        },
        onSignUpClick() {
            
            if(this.isSignup && this.user.Email == '') {
                this.displayMessage('Email Address is required.')                
                return
            }

            if(!this.isValidEmail(this.user.Email)) {
                this.displayMessage('Invalid email address.')                
                return
            }
            
            if(this.isSignup && this.user.Balance <= 0) {
                this.displayMessage('Amount cannot be zero.')                
                return
            }
            

            if(this.isSignup && !isFinite(this.user.Balance) && !isNaN(parseFloat(this.user.Balance))) {
                this.displayMessage('Invalid amount.')                
                return
            }

            this.signUp(this.user.Email, this.user.Balance)
        },
        loginAuthentication(emailAddress) {


            this.overlay = !this.overlay
            const param = {
                Email: emailAddress
            }

            axios.post(this.$store.state.appUrl+'/guest/login', param).then((result) => {
                
                this.overlay = !this.overlay

                if(result.data.guestID > 0) {      
                    
                    localStorage.setItem('id',result.data.guestID )
                    this.$router.push('/shop')
                } else {
                    this.displayMessage('Email address not exist.')
                }
            });
        },
        signUp(email, balance) {

            const param = {
                EmailAddress: email
            }

            axios.post(this.$store.state.appUrl+'/guest/addguest?balance='+balance, param).then((result) => {

                if(result.data.isSuccess) {
                    this.displayMessage('Account successfuly created')
                    this.isSignup = !this.isSignup
                } else {
                    this.displayMessage(result.data.message)
                }

            });

        }
    }

}
</script>

<style scoped>

.main {
    height: 100%;
    width: 100%;
}

.__container {
    height: 100%;
    width: 100%;
    display: grid;
    grid-template-rows: 15% auto 45%;
}

.__login {
    padding: 0% 36% 0% 36%;
}

.wrapper {
    display: grid;
    grid-template-rows: 20% auto;
    height: 100%;
    width: 100%;    
}

.__title {
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: #448aff;
}

.__title > .__title-caption {
    font-size: 2rem;
    font-weight: 600;
    color: white;
}

.__border {
    border: thin solid gainsboro;
}

.__controls {
    padding: 0px 40px 0px 40px;
}

.login-section,
.signup-section {
    margin-top: 35px;
    height: 80%;
    width: 100%;
}

.__btn-login {
    display: grid;
    grid-template-columns: 40% auto;
}

.signup-link {
    display: flex;
    justify-content: center;
    align-items: center;
    color: red;
}

.signup-link:hover {
    cursor: pointer;
}

.btnlogin,
.signup-btn {
    display: flex;
    justify-content: flex-end;
}

.loader {
    background: #e9e9e9;
    display: none;
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    opacity: 0.5;
    height: 100%;
    width: 100%;
}

</style>