module Core.Functions

open System
open Core.Types

let unwrap result =
    match result with
    | Ok value -> value
    | Error err -> failwith err

let logIn username password =
    // Get the user
    // Validate password
    // Return user
    { Id = Guid.NewGuid()
      Name = String200.create "Tom Baker" |> unwrap
      PhoneNumber = PhoneNumber.create "9988776655" |> unwrap
      Password = "This is a password"
      Role = UserRole.User }

let getEquipment =
    // Get equpiment
    [ { Id = Guid.NewGuid()
        Name =
            String200.create "Nail gun, small pneumatic"
            |> unwrap
        Details = String1000.create "" |> unwrap
        Rate = Rate.create 5000 |> unwrap
        RateStartDay = RateStartDay.Second
        RentDurationLimit = RentDurationLimit.create 4 |> unwrap }
      { Id = Guid.NewGuid()
        Name = String200.create "Wood splitter, petrol" |> unwrap
        Details = String1000.create "" |> unwrap
        Rate = Rate.create 20000 |> unwrap
        RateStartDay = RateStartDay.First
        RentDurationLimit = RentDurationLimit.create 2 |> unwrap } ]

let importUsers usersCsv =
    // Parse CSV
    // Add Users
    Ok

let addEquipment equipment =
    // Store equipment
    { Id = Guid.NewGuid()
      Name =
          String200.create "Nail gun, small pneumatic"
          |> unwrap
      Details = String1000.create "" |> unwrap
      Rate = Rate.create 5000 |> unwrap
      RateStartDay = RateStartDay.Second
      RentDurationLimit = RentDurationLimit.create 4 |> unwrap }

let removeEquipment equipmentId =
    // Remove equpiment with id
    Ok

let getCurrentRentingReport =
    // Get reservations active today
    [ { Id = Guid.NewGuid()
        Reserver =
            { Id = Guid.NewGuid()
              Name = String200.create "Tom Baker" |> unwrap
              PhoneNumber = PhoneNumber.create "9988776655" |> unwrap
              Password = "This is a password"
              Role = UserRole.User }
        Equipment =
            { Id = Guid.NewGuid()
              Name = String200.create "Wood splitter, petrol" |> unwrap
              Details = String1000.create "" |> unwrap
              Rate = Rate.create 20000 |> unwrap
              RateStartDay = RateStartDay.First
              RentDurationLimit = RentDurationLimit.create 2 |> unwrap }
        StartDate =
            StartDate.create (DateTime.Now.AddDays(-1.0))
            |> unwrap
        EndDate =
            EndDate.create (DateTime.Now.AddDays(1.0))
            |> unwrap
        Comment =
            String1000.create "I'll have it back by 5PM"
            |> unwrap
        IsSettled = false } ]

let getCreditDueReport =
    // Get past unsettled reservations
    [ { Id = Guid.NewGuid()
        Reserver =
            { Id = Guid.NewGuid()
              Name = String200.create "Tom Baker" |> unwrap
              PhoneNumber = PhoneNumber.create "9988776655" |> unwrap
              Password = "This is a password"
              Role = UserRole.User }
        Equipment =
            { Id = Guid.NewGuid()
              Name = String200.create "Wood splitter, petrol" |> unwrap
              Details = String1000.create "" |> unwrap
              Rate = Rate.create 20000 |> unwrap
              RateStartDay = RateStartDay.First
              RentDurationLimit = RentDurationLimit.create 2 |> unwrap }
        StartDate =
            StartDate.create (DateTime.Now.AddDays(-3.0))
            |> unwrap
        EndDate =
            EndDate.create (DateTime.Now.AddDays(-2.0))
            |> unwrap
        Comment =
            String1000.create "I'll have it back by 5PM"
            |> unwrap
        IsSettled = false } ]
