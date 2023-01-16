# Renting Cars
A simple ASP.NET Core MVC Web App which I created to exercise what I've learned from the ASP.NET Core course at SoftUni.

## :information_source: How It Works

**Guests**
- What can Guests do in the project?
	- They can see the Home page. 	
	- They can see the All Cars page.	
	- They can see the Details page. 	
	- They can see the statistics. 	
	- They can register and log in.	
	
**Users**
- What can Users do in the project?
	- They can register and log in.	
	- They can see the Home page. 	
	- They can see the All Cars page.	
	- They can see the Details page.	
	- They can see the statistics. 	
	- They can rent a car.
	- They can send money for the renting.
	- They can become dealers.
	
**Dealers**
- What can Dealers do in the project?
	- They can register and log in.	
	- They can see the Home page. 	
	- They can see the All Cars page.	
	- They can see the Details page.	
	- They can see the Mine Cars page.
	- They can see the statistics.
	- They can add new cars.
	- They can edit their cars.
	- They can delete their cars.
	- They can rent a car.
	- They can send money for the renting.
	- They can see all offers for their cars.
	- They can see their rented cars.
	- They can accept offers.

**Admin**
- What can Admin do in the project?
	- They can register and log in.	
	- They can see the Home page. 	
	- They can see the All Cars page.	
	- They can see the Details page.	
	- They can see the statistics.
	- They can accept the newest cars added.
	- They can accept when the car is edited.
	- They can rent a car.
	- They can send money for the renting.
	- They can see all offers for renting.
	- They can see all rented cars.
	- They can accept offers.
			
**When you run the project for the first time sample data will be seeded as well as these test accounts:**
- Users
	- User 1 -> email: **user1@abv.bg** / password: **User.1**
	- User 2 -> email: **user2@abv.bg** / password: **User.2**
	- Admin -> email: **admin@abv.bg** / password: **Admin.1**
	
- Dealers
	- LuxCars Auto -> user: User1.
	- Iws Auto -> user: User2.
	
- Cars
	- Lamborghini -> dealer: User 1 / It's public and it's not booked.
	- Mercedes GLE -> dealer: User 2 / It's public and it's not booked.
	
- Categories
	- All seven needed categories.


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
