# Vending Machine
This an e-commerce web prototype refers specifically to the transaction of goods and services.
Here is the sample loom video for the demo of the application 
https://www.loom.com/share/9b69c2e963604a32bb6d6b31acb4361b

# Technologies
Asp.net core framework 5.0
Vue.js 
Material Design
Vuetify
EntityFramework
XUnit

#VUE JS 
**Install the dependencies packages
--npm instal
**Run the application
--npm run serve

#Run tests
dotnet test
XUnit
TODO: Integration tests 

# API
**GET:** https://localhost:44325/api/account/getallaccounts
**Response body:**
[
   {
      "emailAddress":"fsilangil@gmail.com",
      "id":1,
      "dateCreated":"2020-12-18T16:04:35.8662433"
   }
]

**GET:** https://localhost:44325/api/guest/getallguests
**Response body:**
[
   {
      "guestID":1,
      "balance":785,
      "guest":{
         "emailAddress":"fsilangil@gmail.com",
         "id":1,
         "dateCreated":"2020-12-18T16:04:35.8662433"
      },
      "id":1,
      "dateCreated":"2020-12-18T16:04:36.0595978"
   }
]

**POST:** https://localhost:44325/api/guest/login
**Request Body:**

{
    "email":"fsilangil@gmail.com"
}
**Response Body:**
{
    "guest": {
        "emailAddress": "fsilangil@gmail.com",
        "id": 1,
        "dateCreated": "2020-12-18T16:04:35.8662433"
    },
    "lazyLoader": {},
    "guestID": 1,
    "balance": 785,
    "id": 1,
    "dateCreated": "2020-12-18T16:04:36.0595978"
}


**POST:** https://localhost:44325/api/guest/addguest?balance=1500
**Request Body:**
{
    "emailaddress":"fsilangil@yahoomail.com"
}
**Response Body:**
{
    "isSuccess": true,
    "message": "Account created"
}


**GET:** https://localhost:44325/api/product/getallproducts
**Request Body:**
{
    "emailaddress":"fsilangil@yahoomail.com"
}
**Response Body:**
[
    {
        "name": "Iphone 3",
        "price": 25,
        "quantity": 1,
        "id": 1004,
        "dateCreated": "2020-12-21T22:33:11.8443807"
    },
    {
        "name": "Iphone 4",
        "price": 45,
        "quantity": 2,
        "id": 1005,
        "dateCreated": "2020-12-21T22:33:29.5380891"
    },
    {
        "name": "Iphone 11",
        "price": 450,
        "quantity": 45,
        "id": 1006,
        "dateCreated": "2020-12-22T01:04:53.9467651"
    },
    {
        "name": "Iphone 16",
        "price": 130,
        "quantity": 10,
        "id": 1007,
        "dateCreated": "2020-12-22T05:29:23.2311213"
    },
    {
        "name": "Iphone 12",
        "price": 23,
        "quantity": 1,
        "id": 1008,
        "dateCreated": "2020-12-22T05:29:36.4482205"
    },
    {
        "name": "iphone 13",
        "price": 43,
        "quantity": 9,
        "id": 1009,
        "dateCreated": "2020-12-22T05:29:47.0320696"
    }
]



**GET:** https://localhost:44325/api/guest/getallguests
**Response body:**
[
   {
      "guestID":1,
      "balance":785,
      "guest":{
         "emailAddress":"fsilangil@gmail.com",
         "id":1,
         "dateCreated":"2020-12-18T16:04:35.8662433"
      },
      "id":1,
      "dateCreated":"2020-12-18T16:04:36.0595978"
   }
]


**POST:** https://localhost:44325/api/guest/addguest?balance=1500
**Request Body:**
{
    "name":"Iphone IX",
    "price":"",
    "quantity":3
}
**Response Body:**
{
  1
}

**GET:** https://localhost:44325/api/guest/getallguests
**Response body:**
[
   {
      "guestID":1,
      "balance":785,
      "guest":{
         "emailAddress":"fsilangil@gmail.com",
         "id":1,
         "dateCreated":"2020-12-18T16:04:35.8662433"
      },
      "id":1,
      "dateCreated":"2020-12-18T16:04:36.0595978"
   }
]

**GET:** https://localhost:44325/api/guest/getallpurchase?=1
**GET:** https://localhost:44325/api/purchase/getallsummaryorder?=1
**POST:** https://localhost:44325/api/purchase/addpurchase
**PUT:** https://localhost:44325/api/purchase/checkoutproduct
# Author
Ferdinand Erwin Silangil
fsilangil@gmail.com
