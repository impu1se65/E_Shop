using E_Shop.DAL.Identity;
using E_Shop.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace E_Shop.DAL.EF
{
    public class EShopDbContext : IdentityDbContext<Customer>
    {
        public EShopDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new EShopDbInitializer());
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }

    public class EShopDbInitializer : DropCreateDatabaseAlways<EShopDbContext>
    {
        protected override void Seed(EShopDbContext db)
        {
            #region User Initialize

          
                var roleManager = new EShopRoleManager(new RoleStore<Role>(db));
                var userManager = new EShopUserManager(new UserStore<Customer>(db));

                Role role1 = new Role { Name = "administrator" };
                Role role2 = new Role { Name = "moderator" };
                Role role3 = new Role { Name = "user" };
                roleManager.Create(role1);
                roleManager.Create(role2);
                roleManager.Create(role3);
                db.SaveChanges();

                Customer admin = new Customer();
                admin.Email = "admin@mail.com";
                admin.UserName = "admin@mail.com";
                userManager.Create(admin, "admin123");
                userManager.AddToRole(admin.Id, role1.Name);


                Customer moder = new Customer();
                moder.UserName = "moder@mail.com";
                moder.Email = "moder@mail.com";
                userManager.Create(moder, "moder123");
                userManager.AddToRole(moder.Id, role2.Name);

                Customer user = new Customer();
                user.UserName = "user@mail.com";
                user.Email = "user@mail.com";
                userManager.Create(user, "user123");
                userManager.AddToRole(user.Id, role3.Name);
                #endregion
            

            #region Shirts Initialize

            var product1 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name= "Jack & Jones Premium Slim Smart Shirt",
                Category="Shirts",
                Price=60,
                Color= "Grey melange",
                Gender=Gender.Men,
                ProductDetails= "Suitable for work and play. Spread collar. Button placket. Ensure it’s buttoned up for added neatness. Curved hem Slim fit. A narrow cut that sits close to the body.",
                PhotoUrl= "http://images.asos-media.com/products/jack-jones-premium-slim-smart-shirt/7858057-1-greymelange?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product2 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Jack & Jones Premium Slim Smart Shirt",
                Category = "Shirts",
                Price = 60,
                Color = "Grey melange",
                Gender = Gender.Men,
                ProductDetails = "Suitable for work and play. Spread collar. Button placket. Ensure it’s buttoned up for added neatness. Curved hem Slim fit. A narrow cut that sits close to the body.",
                PhotoUrl = "http://images.asos-media.com/products/jack-jones-premium-slim-smart-shirt/7858057-1-greymelange?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product3 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Jack & Jones Premium Slim Smart Shirt",
                Category = "Shirts",
                Price = 60,
                Color = "Grey melange",
                Gender = Gender.Men,
                ProductDetails = "Suitable for work and play. Spread collar. Button placket. Ensure it’s buttoned up for added neatness. Curved hem Slim fit. A narrow cut that sits close to the body.",
                PhotoUrl = "http://images.asos-media.com/products/jack-jones-premium-slim-smart-shirt/7858057-1-greymelange?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            db.Products.Add(product1); db.Products.Add(product2); db.Products.Add(product3);

            var product4 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "JCarhartt WIP Long Sleeve Madison Oxford Shirt In Navy",
                Category = "Shirts",
                Price = 110,
                Color = "Navy",
                Gender = Gender.Men,
                ProductDetails = "Button-down collar. Chest pocket. Carhartt logo detail. Button fastening. Regular cut. Fits you just right",
                PhotoUrl = "http://images.asos-media.com/products/carhartt-wip-long-sleeve-madison-oxford-shirt-in-navy/8761819-1-navy?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product5 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "JCarhartt WIP Long Sleeve Madison Oxford Shirt In Navy",
                Category = "Shirts",
                Price = 110,
                Color = "Navy",
                Gender = Gender.Men,
                ProductDetails = "Button-down collar. Chest pocket. Carhartt logo detail. Button fastening. Regular cut. Fits you just right",
                PhotoUrl = "http://images.asos-media.com/products/carhartt-wip-long-sleeve-madison-oxford-shirt-in-navy/8761819-1-navy?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            var product6 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "JCarhartt WIP Long Sleeve Madison Oxford Shirt In Navy",
                Category = "Shirts",
                Price = 110,
                Color = "Navy",
                Gender = Gender.Men,
                ProductDetails = "Button-down collar. Chest pocket. Carhartt logo detail. Button fastening. Regular cut. Fits you just right",
                PhotoUrl = "http://images.asos-media.com/products/carhartt-wip-long-sleeve-madison-oxford-shirt-in-navy/8761819-1-navy?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product4); db.Products.Add(product5); db.Products.Add(product6);

            var product7 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "ASOS TALL Skinny Denim Western Shirt In Mid Wash",
                Category = "Shirts",
                Price = 40,
                Color = "Mid wash",
                Gender = Gender.Men,
                ProductDetails = "Mid-blue wash. Spread collar. Press-stud fastening. Chest pockets. Ideal for layering. Skinny fit - cut very closely to the body.",
                PhotoUrl = "http://images.asos-media.com/products/asos-tall-skinny-denim-western-shirt-in-mid-wash/8972219-4?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product8 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "ASOS TALL Skinny Denim Western Shirt In Mid Wash",
                Category = "Shirts",
                Price = 40,
                Color = "Mid wash",
                Gender = Gender.Men,
                ProductDetails = "Mid-blue wash. Spread collar. Press-stud fastening. Chest pockets. Ideal for layering. Skinny fit - cut very closely to the body.",
                PhotoUrl = "http://images.asos-media.com/products/asos-tall-skinny-denim-western-shirt-in-mid-wash/8972219-4?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product7); db.Products.Add(product8);

            var product9 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Calvin Klein Skinny Smart Shirt With Stretch In Check",
                Category = "Shirts",
                Price = 113,
                Color = "Granite",
                Gender = Gender.Men,
                ProductDetails = "Breathable woven fabric. Check design. Spread collar. Button placket. Skinny fit - cut very closely to the body. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products/calvin-klein-skinny-smart-shirt-with-stretch-in-check/7870258-1-granite?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            var product10= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Calvin Klein Skinny Smart Shirt With Stretch In Check",
                Category = "Shirts",
                Price = 113,
                Color = "Granite",
                Gender = Gender.Men,
                ProductDetails = "Breathable woven fabric. Check design. Spread collar. Button placket. Skinny fit - cut very closely to the body. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products/calvin-klein-skinny-smart-shirt-with-stretch-in-check/7870258-1-granite?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product9); db.Products.Add(product10);

            var product11= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Polo Ralph Lauren Slim Fit Garment Dyed Shirt Polo Player in Green",
                Category = "Shirts",
                Price = 150,
                Color = "Green",
                Gender = Gender.Men,
                ProductDetails = "Smart/casual personified. Button-down collar. Button placket. Embroidered Polo Player to chest. For that logo life. Slim fit. A narrow cut that sits close to the body.",
                PhotoUrl = "http://images.asos-media.com/products/polo-ralph-lauren-slim-fit-garment-dyed-shirt-polo-player-in-green/8994918-1-green?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product12= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Polo Ralph Lauren Slim Fit Garment Dyed Shirt Polo Player in Green",
                Category = "Shirts",
                Price = 150,
                Color = "Green",
                Gender = Gender.Men,
                ProductDetails = "Smart/casual personified. Button-down collar. Button placket. Embroidered Polo Player to chest. For that logo life. Slim fit. A narrow cut that sits close to the body.",
                PhotoUrl = "http://images.asos-media.com/products/polo-ralph-lauren-slim-fit-garment-dyed-shirt-polo-player-in-green/8994918-1-green?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product11); db.Products.Add(product12);
            #endregion

            #region T-Shirts Initialize
            var product13 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "ASOS TALL NASA Longline Long Sleeve Rugby Polo",
                Category = "T-Shirts",
                Price = 41,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Contrast collar. Button placket. NASA branding to chest. It's all in the details. Fitted cuffs. Fixed hem. Longline cut. Cut longer than standard length.",
                PhotoUrl = "http://images.asos-media.com/products/asos-tall-nasa-longline-long-sleeve-rugby-polo/8936375-1-white?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product14 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "ASOS TALL NASA Longline Long Sleeve Rugby Polo",
                Category = "T-Shirts",
                Price = 41,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Contrast collar. Button placket. NASA branding to chest. It's all in the details. Fitted cuffs. Fixed hem. Longline cut. Cut longer than standard length.",
                PhotoUrl = "http://images.asos-media.com/products/asos-tall-nasa-longline-long-sleeve-rugby-polo/8936375-1-white?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product15 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "ASOS TALL NASA Longline Long Sleeve Rugby Polo",
                Category = "T-Shirts",
                Price = 41,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Contrast collar. Button placket. NASA branding to chest. It's all in the details. Fitted cuffs. Fixed hem. Longline cut. Cut longer than standard length.",
                PhotoUrl = "http://images.asos-media.com/products/asos-tall-nasa-longline-long-sleeve-rugby-polo/8936375-1-white?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product13); db.Products.Add(product14); db.Products.Add(product15);

            var product16= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Billionaire Boys Club T-Shirt With Construction Print",
                Category = "T-Shirts",
                Price = 123,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch cotton. Crew neck. BBC print to chest and back. Short sleeves. Fixed cuffs. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/billionaire-boys-club-t-shirt-with-construction-print/8970330-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product17= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Billionaire Boys Club T-Shirt With Construction Print",
                Category = "T-Shirts",
                Price = 123,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch cotton. Crew neck. BBC print to chest and back. Short sleeves. Fixed cuffs. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/billionaire-boys-club-t-shirt-with-construction-print/8970330-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product18 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Billionaire Boys Club T-Shirt With Construction Print",
                Category = "T-Shirts",
                Price = 123,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch cotton. Crew neck. BBC print to chest and back. Short sleeves. Fixed cuffs. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/billionaire-boys-club-t-shirt-with-construction-print/8970330-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product16); db.Products.Add(product17); db.Products.Add(product18);

            var product19 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Billionaire Boys Club T-Shirt With Collage Print",
                Category = "T-Shirts",
                Price = 143,
                Color = "Grey",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch cotton. Crew neck. Graphic print to chest and back. Short sleeves. Fixed cuffs. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/illionaire-boys-club-t-shirt-with-collage-print/8970350-1-grey?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product20 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Billionaire Boys Club T-Shirt With Collage Print",
                Category = "T-Shirts",
                Price = 143,
                Color = "Grey",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch cotton. Crew neck. Graphic print to chest and back. Short sleeves. Fixed cuffs. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/illionaire-boys-club-t-shirt-with-collage-print/8970350-1-grey?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product19); db.Products.Add(product20);

            var product21 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Fred Perry Slim Fit Colour Block Pique Polo Shirt In White",
                Category = "T-Shirts",
                Price = 102,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Smart/casual personified. Polo colla.r Cos going preppy’s always a good shout. Button placket. Signature twin tipping detail on collar and cuffs.",
                PhotoUrl = "http://images.asos-media.com/products/fred-perry-slim-fit-colour-block-pique-polo-shirt-in-white/8907833-4?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product22= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Fred Perry Slim Fit Colour Block Pique Polo Shirt In Green",
                Category = "T-Shirts",
                Price = 102,
                Color = "Green",
                Gender = Gender.Men,
                ProductDetails = "Smart/casual personified. Polo colla.r Cos going preppy’s always a good shout. Button placket. Signature twin tipping detail on collar and cuffs.",
                PhotoUrl = "http://images.asos-media.com/products/fred-perry-slim-fit-tipped-placket-pique-polo-shirt-in-green/8907851-4?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product23 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "AAPE By A Bathing Ape T-Shirt With Universe Panel Print",
                Category = "T-Shirts",
                Price = 105,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Crew neck. It's classic you. AAPE Universe print. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//aape-by-a-bathing-ape-t-shirt-with-universe-panel-print/8497157-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            var product24 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "AAPE By A Bathing Ape T-Shirt With Universe Panel Print",
                Category = "T-Shirts",
                Price = 105,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Crew neck. It's classic you. AAPE Universe print. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//aape-by-a-bathing-ape-t-shirt-with-universe-panel-print/8497157-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product21); db.Products.Add(product22); db.Products.Add(product23); db.Products.Add(product24);

            var product25 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "ASOS Queen Of The Stone Age Band T-Shirt",
                Category = "T-Shirts",
                Price = 28,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "You can never have too many. Crew neck  Printed band graphic to front. Fixed trims. Regular fit. A standard cut for a classic shape.",
                PhotoUrl = "http://images.asos-media.com/products//asos-queen-of-the-stone-age-band-t-shirt/8949077-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product26= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "ASOS Queen Of The Stone Age Band T-Shirt",
                Category = "T-Shirts",
                Price = 28,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "You can never have too many. Crew neck  Printed band graphic to front. Fixed trims. Regular fit. A standard cut for a classic shape.",
                PhotoUrl = "http://images.asos-media.com/products//asos-queen-of-the-stone-age-band-t-shirt/8949077-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product27 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "ASOS Queen Of The Stone Age Band T-Shirt",
                Category = "T-Shirts",
                Price = 28,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "You can never have too many. Crew neck  Printed band graphic to front. Fixed trims. Regular fit. A standard cut for a classic shape.",
                PhotoUrl = "http://images.asos-media.com/products//asos-queen-of-the-stone-age-band-t-shirt/8949077-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product25); db.Products.Add(product26); db.Products.Add(product27);

            var product28 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Fred Perry T-Shirt with Crew Neck in Steel Marl",
                Category = "T-Shirts",
                Price = 28,
                Color = "Vintage steel marl",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch, marl jersey. Crew neck. Signature wreath logo. Regular fit - true to size. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products//fred-perry-t-shirt-with-crew-neck-in-steel-marl/5056107-1-vintagesteelmarl?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product29 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Fred Perry T-Shirt with Crew Neck in Steel Marl",
                Category = "T-Shirts",
                Price = 28,
                Color = "Vintage steel marl",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch, marl jersey. Crew neck. Signature wreath logo. Regular fit - true to size. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products//fred-perry-t-shirt-with-crew-neck-in-steel-marl/5056107-1-vintagesteelmarl?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product28); db.Products.Add(product29);

            var product30 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Religion Longline T-Shirt",
                Category = "T-Shirts",
                Price = 39,
                Color = "Light grey marl",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch jersey. Crew neck. Embroidered logo. Curved hem. Longline cut. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products//religion-longline-t-shirt/5314463-1-lightgreymarl?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product31 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Religion Longline T-Shirt",
                Category = "T-Shirts",
                Price = 39,
                Color = "Light grey marl",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch jersey. Crew neck. Embroidered logo. Curved hem. Longline cut. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products//religion-longline-t-shirt/5314463-1-lightgreymarl?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product32 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Religion Longline T-Shirt",
                Category = "T-Shirts",
                Price = 39,
                Color = "Light grey marl",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch jersey. Crew neck. Embroidered logo. Curved hem. Longline cut. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products//religion-longline-t-shirt/5314463-1-lightgreymarl?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product30); db.Products.Add(product31); db.Products.Add(product32);

            var product33 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Migos Culture Bleached T-Shirt",
                Category = "T-Shirts",
                Price = 49,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Tie-dye print. Embrace the throwback vibes. Crew neck Repeat 'Culture' print. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products//migos-culture-bleached-t-shirt/8975186-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            var product34 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "New Look T-Shirt With Lace Side In Nude",
                Category = "T-Shirts",
                Price = 20,
                Color = "Light pink",
                Gender = Gender.Men,
                ProductDetails = "Crew neck. Dropped shoulders. Lace-up sides. Raw-cut trims. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products//new-look-t-shirt-with-lace-side-in-nude/8968324-1-lightpink?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            var product35 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Religion Muscle Fit T-Shirt With Skeleton Print",
                Category = "T-Shirts",
                Price = 55,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Another staple to line your wardrobe. Crew neck. It's classic you Religion skeleton print to front. Fixed cuffs. Close-cut muscle fit. Show off the gains.",
                PhotoUrl = "http://images.asos-media.com/products//religion-muscle-fit-t-shirt-with-skeleton-print/9007014-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product36 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Religion Muscle Fit T-Shirt With Skeleton Print",
                Category = "T-Shirts",
                Price = 55,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Another staple to line your wardrobe. Crew neck. It's classic you Religion skeleton print to front. Fixed cuffs. Close-cut muscle fit. Show off the gains.",
                PhotoUrl = "http://images.asos-media.com/products//religion-muscle-fit-t-shirt-with-skeleton-print/9007014-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product37 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Religion Muscle Fit T-Shirt With Skeleton Print",
                Category = "T-Shirts",
                Price = 55,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Another staple to line your wardrobe. Crew neck. It's classic you Religion skeleton print to front. Fixed cuffs. Close-cut muscle fit. Show off the gains.",
                PhotoUrl = "http://images.asos-media.com/products//religion-muscle-fit-t-shirt-with-skeleton-print/9007014-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product33); db.Products.Add(product34); db.Products.Add(product35);
            db.Products.Add(product36); db.Products.Add(product37);
            #endregion

            #region Jumpers and Cardigans Initialize
            var product38 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Asprit Jumper With Garment Dye",
                Category = "Jumpers and Cardigans",
                Price = 71,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Throw this on when the temperature dips. Crew neck. Plain design. A wardrobe staple. Ribbed trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products/esprit-jumper-with-garment-dye/9064939-1-340?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product39 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Asprit Jumper With Garment Dye",
                Category = "Jumpers and Cardigans",
                Price = 71,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Throw this on when the temperature dips. Crew neck. Plain design. A wardrobe staple. Ribbed trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products/esprit-jumper-with-garment-dye/9064939-1-340?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product40= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Asprit Jumper With Garment Dye",
                Category = "Jumpers and Cardigans",
                Price = 71,
                Color = "White",
                Gender = Gender.Men,
                ProductDetails = "Throw this on when the temperature dips. Crew neck. Plain design. A wardrobe staple. Ribbed trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products/esprit-jumper-with-garment-dye/9064939-1-340?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product38); db.Products.Add(product39); db.Products.Add(product40);

            var product41 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Original Penguin Crew Jumper Cotton Small Logo in Navy",
                Category = "Jumpers and Cardigans",
                Price = 111,
                Color = "Dark sapphire",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch knit. Crew neck. Logo detail. Ribbed trims. Regular fit - true to size. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products/esprit-jumper-with-garment-dye/9064939-1-340?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product42 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Original Penguin Crew Jumper Cotton Small Logo in Navy",
                Category = "Jumpers and Cardigans",
                Price = 111,
                Color = "Dark sapphire",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch knit. Crew neck. Logo detail. Ribbed trims. Regular fit - true to size. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products/esprit-jumper-with-garment-dye/9064939-1-340?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product43 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Original Penguin Crew Jumper Cotton Small Logo in Navy",
                Category = "Jumpers and Cardigans",
                Price = 111,
                Color = "Dark sapphire",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch knit. Crew neck. Logo detail. Ribbed trims. Regular fit - true to size. Machine wash. 100% Cotton.",
                PhotoUrl = "http://images.asos-media.com/products/esprit-jumper-with-garment-dye/9064939-1-340?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product41); db.Products.Add(product42); db.Products.Add(product43);

            var product44 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "ASOS Fisherman Rib Jumper In Navy Twist",
                Category = "Jumpers and Cardigans",
                Price = 57,
                Color ="Navy",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch knit. Crew neck. It's classic you. Ribbed trims. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/asos-fisherman-rib-jumper-in-navy-twist/8929549-1-navy?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product45= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "ASOS Fisherman Rib Jumper In Navy Twist",
                Category = "Jumpers and Cardigans",
                Price = 57,
                Color = "Navy",
                Gender = Gender.Men,
                ProductDetails = "Soft-touch knit. Crew neck. It's classic you. Ribbed trims. Regular fit - true to size.",
                PhotoUrl = "http://images.asos-media.com/products/asos-fisherman-rib-jumper-in-navy-twist/8929549-1-navy?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product44); db.Products.Add(product45);

            var product46 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Pull&Bear Hooded Cardigan In Grey",
                Category = "Jumpers and Cardigans",
                Price = 57,
                Color = "Grey",
                Gender = Gender.Men,
                ProductDetails = "Your downtime uniform. Fixed hood. Open front. Side pockets. Fixed trims. Regular fit. A standard cut for a classic shape.",
                PhotoUrl = "http://images.asos-media.com/products/pullbear-hooded-cardigan-in-grey/9264760-1-chinefon?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product47 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Pull&Bear Hooded Cardigan In Grey",
                Category = "Jumpers and Cardigans",
                Price = 57,
                Color = "Grey",
                Gender = Gender.Men,
                ProductDetails = "Your downtime uniform. Fixed hood. Open front. Side pockets. Fixed trims. Regular fit. A standard cut for a classic shape.",
                PhotoUrl = "http://images.asos-media.com/products/pullbear-hooded-cardigan-in-grey/9264760-1-chinefon?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product46); db.Products.Add(product47);

            #endregion

            #region Hoodies and Sweatshirts Initialize
            var product48 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Jack & Jones Core Hoodie",
                Category = "Hoodies and Sweatshirts",
                Price = 53,
                Color = "Light grey melange",
                Gender = Gender.Men,
                ProductDetails = "A wardrobe staple. Drawstring hood. Zip fastening. Side pockets. Fitted trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//jack-jones-core-hoodie/9105772-1-lightgreymelange?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product49 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Jack & Jones Core Hoodie",
                Category = "Hoodies and Sweatshirts",
                Price = 53,
                Color = "Light grey melange",
                Gender = Gender.Men,
                ProductDetails = "A wardrobe staple. Drawstring hood. Zip fastening. Side pockets. Fitted trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//jack-jones-core-hoodie/9105772-1-lightgreymelange?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product50 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Jack & Jones Core Hoodie",
                Category = "Hoodies and Sweatshirts",
                Price = 53,
                Color = "Light grey melange",
                Gender = Gender.Men,
                ProductDetails = "A wardrobe staple. Drawstring hood. Zip fastening. Side pockets. Fitted trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//jack-jones-core-hoodie/9105772-1-lightgreymelange?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product48); db.Products.Add(product49); db.Products.Add(product50);

            var product51 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Carhartt WIP Chase Sweatshirt In Black",
                Category = "Hoodies and Sweatshirts",
                Price = 94,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Crew neck. Raglan sleeves. Small logo print. For keeping your branding low key. Ribbed trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//carhartt-wip-chase-sweatshirt-in-black/8762241-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            var product52= new Product
            {
                Date = DateTimeOffset.Now,
                Size = "M",
                Name = "Carhartt WIP Chase Sweatshirt In Black",
                Category = "Hoodies and Sweatshirts",
                Price = 94,
                Color = "Black",
                Gender = Gender.Men,
                ProductDetails = "Crew neck. Raglan sleeves. Small logo print. For keeping your branding low key. Ribbed trims. Regular cut. Fits you just right.",
                PhotoUrl = "http://images.asos-media.com/products//carhartt-wip-chase-sweatshirt-in-black/8762241-1-black?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product51); db.Products.Add(product52);

            var product53 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "L",
                Name = "Reebok Training Logo Hoodie In Grey CE4766",
                Category = "Hoodies and Sweatshirts",
                Price = 67,
                Color = "Grey",
                Gender = Gender.Men,
                ProductDetails = "The perfect kit for the gym or a lazy Sunday. You decide. Drawstring hood. Signature Reebok logo.",
                PhotoUrl = "http://images.asos-media.com/products/eebok-training-logo-hoodie-in-grey-ce4766/8714929-1-grey?$XXL$&amp;wid=513&amp;fit=constrain",
            };

            var product54 = new Product
            {
                Date = DateTimeOffset.Now,
                Size = "XL",
                Name = "Reebok Training Logo Hoodie In Grey CE4766",
                Category = "Hoodies and Sweatshirts",
                Price = 67,
                Color = "Grey",
                Gender = Gender.Men,
                ProductDetails = "The perfect kit for the gym or a lazy Sunday. You decide. Drawstring hood. Signature Reebok logo.",
                PhotoUrl = "http://images.asos-media.com/products/eebok-training-logo-hoodie-in-grey-ce4766/8714929-1-grey?$XXL$&amp;wid=513&amp;fit=constrain",
            };
            db.Products.Add(product53); db.Products.Add(product54);
            #endregion


             db.SaveChanges();
            base.Seed(db);
        }

    }
}
