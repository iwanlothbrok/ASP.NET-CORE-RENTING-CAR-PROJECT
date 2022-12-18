# Renting Cars
A simple ASP.NET Core MVC Web App which I created to exercise what I've learned from the ASP.NET Core course at SoftUni.

## :information_source: How It Works

**Guests**
- Have access to home page for guests, all cars page and details page, also can see statistics. Can log in or register.

**Users**
- Have access to all cars page, details page, become dealer page, statistics which are on home page, and can rent a car. Also can become a dealer.

**Dealers**
- Have access to all cars page, mine cars page, details page, offerts page, rented cars page(only his cars), statistics which are on home page, and can rent a car. He can add new cars and edit or delete cars only from my cars page, can accept offers for his cars, and can rent a car.

**Admin**
- Have access to all cars page, details page, offerts page, rented cars page, and statistics which are on the home page. He can accept cars when they were added, he can accept offers for cars, and he can see all offers which are made.

**When you run the project for the first time sample data will be seeded as well as these test accounts:**

- User 1 -> email: **user1@abv.bg** / password: **User.1**
- User 2 -> email: **user2@abv.bg** / password: **User.2**
- Admin -> email: **admin@abv.bg** / password: **Admin.1**

**[Here](https://imgur.com/a/Io6cSqU) is a screenshot of the project's database diagram**

## :hammer_and_pick: Built With
- ASP.NET Core 6
- Entity Framework Core 6.0.8
- Microsoft SQL Server Express
- ASP.NET Identity System
- AutoMapper
- MVC Areas
- Razor Pages + Partial Views
- Dependency Injection
- Paging with EF Core
- Data Validation, both Client-side, and Server-side
- Data Validation in the Input View Models
- Responsive Design
- Bootstrap
- AdminLTE
- jQuery
- HtmlSanitizer 
- NUnit
- Facebook Authentication 
- Fluent Assertions
- Caching
- AJAX
 
 ## License

This project is licensed under the [MIT License](LICENSE).

___
**This project is made only for educational purposes!**
___
