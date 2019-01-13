using System;
using Faker;
using TripStyle.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace TripStyle.Data
{
    class Program
    {
        static string[] ProductCategory = new string[] { "Skirt", "Dress", "Suit", "Underwear", "Swimsuit", "Shirt", "Coat" };
        static string[] ProductName = new string[] { 
            "Apron",
            "Baldric",
            "Basque",
            "Bathing suit",
            "Bathrobe",
            "Blouse",
            "Body",
            "Body Stocking",
            "Bodysuit",
            "Cardigan ",
            "Chaps",
            "Chausses",
            "Chador",
            "Coat",
            "Coatee",
            "Codpiece",
            "Cummerbund",
            "Dolman" 
         };
        static string[] ProductDescription = new string[] { 
            "a protective or sometimes decorative or ceremonial garment worn over the front of the body and tied around the waist",
            "a wide silk sash or leather belt worn over the right shoulder to the left hip for carrying a sword, etc",
            "a short extension below the waist to the bodice of a woman's jacket, etc",
            "a garment worn for bathing, esp an old-fashioned one that covers much of the body",
            "a dressing gown",
            "a woman's shirtlike garment made of cotton, nylon, etc",
            "a woman's close-fitting one-piece garment for the torso",
            "a one-piece undergarment for women, usually of nylon, covering the torso",
            "a one-piece undergarment for a baby",
            "a knitted jacket or sweater with buttons up the front",
            "leather overalls without a seat, worn by cowboys",
            "a tight-fitting medieval garment covering the feet and legs, usually made of chain mail",
            "a large shawl or veil worn by Muslim or Hindu women that covers them from head to foot",
            "an outdoor garment with sleeves, covering the body from the shoulder to waist, knee, or foot",
            "a short coat, esp for a baby",
            "a bag covering the male genitals, attached to hose or breeches by laces, etc, worn in the 15th and 16th centuries",
            "a wide sash, worn with a dinner jacket" 
        };

        static Random random = new Random();
        
        static void Main(string[] args)
        {
            var roles = CreateRoles();
            var categories = CreateCategories();
            var users = CreateUsers(100);
            var products = CreateProducts(200, categories);

            using (var db = new TripStyleContext())
            {
                db.Roles.AddRange(roles);
                db.Categories.AddRange(categories);
                db.Users.AddRange(users);
                db.Products.AddRange(products);
                db.SaveChanges();
            }

        }

        public static List<Role> CreateRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                    Name = "Customer",
                    Description = "The Customer is King"

                },
                new Role
                {
                    Name = "Admin",
                    Description = "The Admin is King"

                }
            };
        }

        public static List<Category> CreateCategories()
        {
            return ProductCategory.Select(category => new Category
            {
                Name = category
            }).ToList();
        }

        public static List<User> CreateUsers(int NumberOfUsers)
        {
            string[] Gender = { "Male", "Female" };
            
            return Enumerable.Range(0, NumberOfUsers).Select(_ => {
                string Firstname = Faker.Name.First();
                string Lastname = Faker.Name.Last();
                string Email = Faker.Internet.Email(Firstname);

                return new User
                {
                    Firstname = Firstname,
                    Lastname = Lastname,
                    Gender = Gender[random.Next(0, Gender.Length)],
                    Email = Email,
                    Phonenumber = random.Next(600000000, 699999999).ToString(),
                    Password = random.Next(1000, 100000).ToString(),
                    Street = Faker.Address.StreetName(),
                    City = Faker.Address.City(),
                    Country = Faker.Address.Country(),
                    RoleId = 1
                };
            }).ToList();
        }

        public static List<Product> CreateProducts(int NumberOfProducts, List<Category> categories)
        {
            string[] Size = { "XS", "S", "M", "L", "XL" };
            string[] Make = { "Nike", "Meindl", "Atomic", "Protest" };
            string[] Color = { "Black", "Purple", "White", "Yellow", "Pink", "Blue", "Red", "Greendotne" };
            string[] Region = { "Europe", "Asia", "Africa", "North America", "South America", "Ocania" };
            string[] Season = { "Summer", "Winter", "Autumn", "Spring" };

            return Enumerable.Range(0, NumberOfProducts).Select(_ => {
                return new Product
                {
                    Name = ProductName[random.Next(0, ProductName.Length)],
                    Make = Make[random.Next(0, Make.Length)],
                    Price = random.Next(5, 200),
                    Size = Size[random.Next(0, Size.Length)],
                    Color = Color[random.Next(0, Color.Length)],
                    Region = Region[random.Next(0, Region.Length)],
                    Season = Season[random.Next(0, Season.Length)],
                    Category = categories[random.Next(0, categories.Count -1)],
                    Image = "https://picsum.photos/500?image=" + random.Next(0,500),
                    Stock = 10
                };
            }).ToList();
        }
    }
}
