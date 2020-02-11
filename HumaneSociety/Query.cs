using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        static HumaneSociertyDataContext db;

        static Query()
        {
            db = new HumaneSociertyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();

            return allStates;
        }

        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }

            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;

            // submit changes
            db.SubmitChanges();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }

        //// TODO Items: ////

        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            // Create ------------------------------------------------------------
            switch (crudOperation)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    break;
            // Delete ------------------------------------------------------------
                case "delete":
                    db.Employees.DeleteOnSubmit(employee);
                    db.SubmitChanges();
                    break;
            // Read ------------------------------------------------------------
                case "read":
                    db.Employees.First(i => i == employee);
                    break;
          
            // Update ------------------------------------------------------------
                case "update":
                    db.Employees.First(i => i == employee);
                    db.SubmitChanges();
                    break;
            }
        }

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            return db.Animals.First(a => a.AnimalId == id);
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            db.Animals.Where(a => a.AnimalId == animalId).FirstOrDefault();
            foreach (KeyValuePair<int, string> pair in updates) 
            {
                switch (pair.Key)
                {
                    case 1:
                        db.Animals.Where(a => a.CategoryId.ToString() == pair.Value);
                        break;
                    case 2:
                        db.Animals.Where(a => a.Name == pair.Value);
                        db.SubmitChanges();
                        break;
                    case 3:
                        db.Animals.Where(a => a.Age.ToString() == pair.Value);
                        db.SubmitChanges();
                        break;
                    case 4:
                        db.Animals.Where(a => a.Demeanor == pair.Value);
                        db.SubmitChanges();
                        break;
                    case 5:
                        db.Animals.Where(a => a.KidFriendly.ToString() == pair.Value);
                        db.SubmitChanges();
                        break;
                    case 6:
                        db.Animals.Where(a => a.PetFriendly.ToString() == pair.Value);
                        db.SubmitChanges();
                        break;
                    case 7:
                        db.Animals.Where(a => a.Weight.ToString() == pair.Value);
                        db.SubmitChanges();
                        break;
                    case 8:
                        Console.WriteLine("Finished");
                        break;
                    default:
                        Console.WriteLine("Not a valid entry");
                        break;
                }
            }
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            var animalIDResult = db.Animals.Where(a => a == animal).FirstOrDefault();
            
            db.SubmitChanges();
        }

        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            var result = db.Animals.ToList();
            //remove what I'm NOT looking for//

            foreach (KeyValuePair<int, string> pair in updates)
            {
                switch (pair.Key)
                {
                    case 1:
                        return db.Animals.Where(a => a.Category.Name == pair.Value);
                    case 2:
                        return db.Animals.Where(a => a.Name == pair.Value);
                    case 3:
                        return db.Animals.Where(a => a.Age.ToString() == pair.Value);
                    case 4:
                        return db.Animals.Where(a => a.Demeanor == pair.Value);
                    case 5:
                        return db.Animals.Where(a => a.KidFriendly.ToString() == pair.Value);
                    case 6:
                        return db.Animals.Where(a => a.PetFriendly.ToString() == pair.Value);
                    case 7:
                        return db.Animals.Where(a => a.KidFriendly.ToString() == pair.Value);
                    case 8:
                        return db.Animals.Where(a => a.Weight.ToString() == pair.Value);
                    case 9:
                        return db.Animals.Where(a => a.AnimalId.ToString() == pair.Value);
                    default:
                        Console.WriteLine("finished");
                        break;
                }
            }
            return 
        }    
         
        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            var result = db.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
            return result.CategoryId;
        }
        
        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            var result = db.DietPlans.Where(d => d.Name == dietPlanName).FirstOrDefault();
            return result.DietPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            var result = db.Adoptions.Where(a => a.Animal == animal).ToList();
            var result2 = db.Adoptions.Where(b => b.Client == client).ToList();
            db.Adoptions.First(i => i == result[0]);
            db.Adoptions.First(j => j == result2[0]);
            db.SubmitChanges();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            var result = db.Adoptions.Where(a => a.ApprovalStatus == "pending");
            return result;
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            var result = db.Adoptions.Where(a => a.ClientId == clientId).ToList();
            var result2 = db.Adoptions.Where(a => a.ClientId == clientId).ToList();
            db.Adoptions.DeleteOnSubmit(result[0]);
            db.Adoptions.DeleteOnSubmit(result2[0]);
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var result = db.Animals.Where(a => a == animal);
            return result;
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            db.Shots.Where(a => a == shotName);
            db.Shots.Where(b => b == animal);

        }
    }
}