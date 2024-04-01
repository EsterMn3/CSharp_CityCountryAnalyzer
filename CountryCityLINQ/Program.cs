using System;
using System.Collections.Generic;
using System.Linq;

record City(string Name, long Population); //data structure used to represent a city.
record Country(string Name, double Area, long Population, List<City> Cities);

class Program
{
    static void Main()
    {
        City[] cities = [
            new City("Tokyo", 37_833_000),
            new City("Delhi", 30_290_000),
            new City("Shanghai", 27_110_000),
            new City("São Paulo", 22_043_000),
            new City("Mumbai", 20_412_000),
            new City("Beijing", 20_384_000),
            new City("Cairo", 18_772_000),
            new City("Dhaka", 17_598_000),
            new City("Osaka", 19_281_000),
            new City("New York-Newark", 18_604_000),
            new City("Karachi", 16_094_000),
            new City("Chongqing", 15_872_000),
            new City("Istanbul", 15_029_000),
            new City("Buenos Aires", 15_024_000),
            new City("Kolkata", 14_850_000),
            new City("Lagos", 14_368_000),
            new City("Kinshasa", 14_342_000),
            new City("Manila", 13_923_000),//17
            new City("Rio de Janeiro", 13_374_000),
            new City("Tianjin", 13_215_000),
            new City("Vatican City", 826),
            new City("San Marino", 4_500)
        ];

        Country[] countries = [
            // Country(string Name, double Area, long Population, List<City> Cities);
            new Country ("Vatican City", 0.44, 526, new List<City> { cities[20] }),
            new Country ("Monaco", 2.02, 38_000, [new City("Monte Carlo", 38_000)]),
            new Country ("Nauru", 21, 10_900, [new City("Yaren", 1_100)]),
            new Country ("Tuvalu", 26, 11_600, [new City("Funafuti", 6_200)]),
            new Country ("San Marino", 61, 33_900, new List<City> { cities[21] }),
            new Country ("Liechtenstein", 160, 38_000, [new City("Vaduz", 5_200)]),
            new Country ("Marshall Islands", 181, 58_000, [new City("Majuro", 28_000)]),
            new Country ("Saint Kitts & Nevis", 261, 53_000, [new City("Basseterre", 13_000)])
       ];

        //Query syntax
        IEnumerable<City> queryMajorCities =
            from city in cities
            where city.Population > 100000
            select city;

        Console.WriteLine("\nCities with a population more than 100000: ");

        // Execute the query to produce the results
        foreach (City city in queryMajorCities)
        {
            Console.WriteLine(city);
        }

        // Output:
        // City { Population = 120000 }
        // City { Population = 112000 }
        // City { Population = 150340 }

        // Method-based syntax
        IEnumerable<City> queryMajorCities2 = cities.Where(c => c.Population > 100000);

        IEnumerable<City> largeCitiesList = ( // Define a new IEnumerable collection of City objects named largeCitiesList
            from country in countries // Begin a query that iterates over each Country object in the countries collection
            from city in country.Cities // For each Country object, iterate over each City object in its Cities collection
            where city.Population > 10000 // Filter the cities based on the condition that their population is greater than 10,000
            select city // Select the city that satisfies the condition
         ).ToList();// End of the LINQ query and assignment to largeCitiesList

        Console.WriteLine("\nCities inside the country list that have a population greater then 10000: ");

        foreach (City city in largeCitiesList)
        {
            Console.WriteLine(city);
        }

        IEnumerable<Country> countryAreaQuery =
                from country in countries
                where country.Area > 10 // Filter countries with an area greater than 40000 sq km
                select country;

        Console.WriteLine("\nCountries with an area greater than 10 sq km:");

        if (countryAreaQuery.Any()) // Check if there are any countries in the query result
        {
            foreach (Country country in countryAreaQuery)
            {
                Console.WriteLine($"Country: {country.Name}, Area: {country.Area} sq km, Population: {country.Population}");
            }
        }
        else
        {
            Console.WriteLine("No countries found with an area greater than 40000 sq km.");
        }


        Console.WriteLine("\nGrouping countries based on their names: ");

        var queryCountryGroups =
            from country in countries
            group country by country.Name[0]; //based on the first letter of the country

        foreach (var group in queryCountryGroups)
        {
            Console.WriteLine($"Group letter: {group.Key}"); //letter

            foreach (var country in group)
            {
                Console.WriteLine($"    {country.Name}");
            }

            Console.WriteLine(); // Add an empty line between groups for clarity
        }
        Console.WriteLine("Countries:"); // Add an empty line between groups for clarity

        //anther way of using select key word
        var queryNameAndPop =
            from country in countries
            select new
            {
                Name = country.Name,
                Pop = country.Population
            };

        foreach (var country in queryNameAndPop)
        {
            Console.WriteLine(country);
        }

        var joinQuery =
            from city in cities
            join country in countries on city.Name equals country.Name // Join cities and countries where the city name matches the country name
            select new { CityName = city.Name, CountryName = country.Name, Population = city.Population };

        Console.WriteLine("\nJoined City and Country Information:");

        foreach (var result in joinQuery)
        {
            Console.WriteLine($"City: {result.CityName}, Country: {result.CountryName}, Population: {result.Population}");
        }




    }
}
