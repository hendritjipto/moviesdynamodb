## 👨🏻‍💻 Sample code for import the movie data 🍿 to DynamoDB
----------
This is a .NET code, please install the neccessary sdk to run this code. The code use the highlevel dotnet library utilising the batchwrite that can only import 25 items at one given time 🥲 😎

Change the table name :

![SCR-20221122-gd9-2](/assets/SCR-20221122-gd9-2.png)

Run this code : 

```dotnet
dotnet restore
dotnet build
dotnet run
```

#### Result 

![SCR-20221122-g9t-2](/assets/SCR-20221122-g9t-2.png)
