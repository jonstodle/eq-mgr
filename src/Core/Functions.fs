module Core.Functions

open System
open Core.Types

let logIn username password =
    // Get the user
    // Validate password
    // Return user
    {
        Id = Guid.NewGuid()
        Name = "Tom Baker"
        PhoneNumber = "9988776655"
        Password = "This is a password"
        Role = UserRole.User
    }

let getEquipment =
    // Get equpiment
    [
        {
            Id = Guid.NewGuid()
            Name = "Nail gun, small pneumatic"
            Details = ""
            Rate = 5000
            RateStartDay = RateStartDay.Second
            RentDurationLimit = 4
        }
        {
            Id = Guid.NewGuid()
            Name = "Wood splitter, petrol"
            Details = ""
            Rate = 20000
            RateStartDay = RateStartDay.First
            RentDurationLimit = 2
        }
    ]
    
let importUsers usersCsv =
    // Parse CSV
    // Add Users
    Ok
    
let addEquipment equipment =
    // Store equipment
    {
        Id = Guid.NewGuid()
        Name = "Nail gun, small pneumatic"
        Details = ""
        Rate = 5000
        RateStartDay = RateStartDay.Second
        RentDurationLimit = 4
    }
    
let removeEquipment equipmentId =
    // Remove equpiment with id
    Ok
    
let getCurrentRentingReport =
    // Get reservations active today
    [
        {
            Id = Guid.NewGuid()
            Reserver = {
                Id = Guid.NewGuid()
                Name = "Tom Baker"
                PhoneNumber = "9988776655"
                Password = "This is a password"
                Role = UserRole.User
            }
            Equipment = {
                Id = Guid.NewGuid()
                Name = "Wood splitter, petrol"
                Details = ""
                Rate = 20000
                RateStartDay = RateStartDay.First
                RentDurationLimit = 2
            }
            StartDate = DateTime.Now.AddDays(-1.0)
            EndDate = DateTime.Now.AddDays(1.0)
            Comment = "I'll have it back by 5PM"
            IsSettled = false
        }
    ]
    
let getCreditDueReport =
    // Get past unsettled reservations
    [
        {
            Id = Guid.NewGuid()
            Reserver = {
                Id = Guid.NewGuid()
                Name = "Tom Baker"
                PhoneNumber = "9988776655"
                Password = "This is a password"
                Role = UserRole.User
            }
            Equipment = {
                Id = Guid.NewGuid()
                Name = "Wood splitter, petrol"
                Details = ""
                Rate = 20000
                RateStartDay = RateStartDay.First
                RentDurationLimit = 2
            }
            StartDate = DateTime.Now.AddDays(-3.0)
            EndDate = DateTime.Now.AddDays(-2.0)
            Comment = "I'll have it back by 5PM"
            IsSettled = false
        }
    ]
    
