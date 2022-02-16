using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paul_Andreea_Proiect.Models;

namespace Paul_Andreea_Proiect.Data
{
    public class DBInitializer
    {
        public static void Initialize(AutoTraderContext context)
        {
            context.Database.EnsureCreated();
            if (context.Cars.Any())
            {
                return; 
            }
            var cars = new Car[]
            {
             new Car{PostTitle = "Mercedes C Class 2.2 CDi Elegance", Brand="Mercedes-Benz", Model="C-Class", Price=Decimal.Parse("8900")},
             new Car{PostTitle = "Audi A5 3.0 quatro", Brand="Audi",Model="A5", Price=Decimal.Parse("12500")},
             new Car{PostTitle = "Volkswagen Passat CC",Brand="Volkswagen",Model="Passat", Price=Decimal.Parse("5600")},
             new Car{PostTitle="Dacia Duster 4x4 1, 5 DCI", Brand = "Dacia",Model="Duster", Price=Decimal.Parse("6250")},
             new Car{PostTitle="Audi A4 ,1.9 TDI", Brand="Audi",Model="A4", Price=Decimal.Parse("11490")},
             new Car{PostTitle="Bmw 520 Xdrive 2016",Brand="BMW",Model="Seria 5", Price=Decimal.Parse("18900")}
            };
            foreach (Car c in cars)
            {
                context.Cars.Add(c);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {
                 new Customer{CustomerID=100,Name="Popescu Mihaela",BirthDate=DateTime.Parse("1989-09-01")},
                 new Customer{CustomerID=101,Name="Mihailescu Vasile",BirthDate=DateTime.Parse("1969-07-08") },
                 new Customer{CustomerID=102,Name="Silaghi Alexandru",BirthDate=DateTime.Parse("1992-02-18")},
                 new Customer{CustomerID=103,Name="Tat Cristina",BirthDate=DateTime.Parse("1983-11-02")},
                 new Customer{CustomerID=104,Name="Oltean Teofil",BirthDate=DateTime.Parse("1981-10-12")}

                };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
             new Order{CarID=1,CustomerID=104},
             new Order{CarID=3,CustomerID=102},
             new Order{CarID=4,CustomerID=103},
             new Order{CarID=2,CustomerID=101},
             new Order{CarID=5,CustomerID=100 }
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();

            var sellers = new Seller[]
            {
                new Seller{SellerName="Paul Andreea",Adress="Str. Aviatorilor, nr. 40, Bucuresti"},
                new Seller{SellerName="Pop Bianca-Maria",Adress="Str. Plopilor, nr. 35, Ploiesti"},
                new Seller{SellerName="Berinde Ionel",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
                new Seller{SellerName="Anton Dan",Adress="Str. Salcamului, nr. 17, Iasi"},
                new Seller{SellerName="Mois Florin",Adress="Str. Mihai Eminescu, nr.13, Cluj-Napoca"},
            };
            foreach (Seller s in sellers)
            {
                context.Sellers.Add(s);
            }
            context.SaveChanges();
            var soldcars = new SoldCar[]
            {
                new SoldCar { CarID = cars.Single(c => c.PostTitle == "Audi A5 3.0 quatro" ).ID, SellerID = sellers.Single(i => i.SellerName == "Paul Andreea").ID},
                new SoldCar { CarID = cars.Single(c => c.PostTitle == "Bmw 520 Xdrive 2016" ).ID, SellerID = sellers.Single(i => i.SellerName == "Pop Bianca-Maria").ID},
                new SoldCar { CarID = cars.Single(c => c.PostTitle == "Volkswagen Passat CC" ).ID, SellerID = sellers.Single(i => i.SellerName == "Berinde Ionel").ID},
                new SoldCar { CarID = cars.Single(c => c.PostTitle == "Mercedes C Class 2.2 CDi Elegance" ).ID, SellerID = sellers.Single(i => i.SellerName == "Anton Dan").ID},
                new SoldCar { CarID = cars.Single(c => c.PostTitle == "Dacia Duster 4x4 1, 5 DCI" ).ID, SellerID = sellers.Single(i => i.SellerName == "Mois Florin").ID}

            };
            foreach (SoldCar sc in soldcars)
            {
                context.SoldCars.Add(sc);
            }
            context.SaveChanges();
        }
    }
       
}
