module Core.Types

open System
open Core.Types

module String200 =
    type String200 = private String200 of string
    
    let create (userName: string) =
        if userName.Length < 1 then
            Error("String must be at least 1 character")
        elif userName.Length > 200 then
            Error("String must be 200 characters or less")
        else
            Ok(String200(userName))
            
module PhoneNumber =
    type PhoneNumber = private PhoneNumber of string
    
    let create (phoneNumber: string) =
        if phoneNumber.Length <> 8 then
            Error("Phone number must be 8 digits")
        elif phoneNumber.ToCharArray() |> Array.forall Char.IsDigit |> (fun b -> !b) then
            Error("Phone number can only contain digits")
        else
            Ok(PhoneNumber(phoneNumber))

type UserRole =
    | User
    | Administrator

type User = {
    Id: Guid
    Name: String200.String200
    PhoneNumber: PhoneNumber.PhoneNumber
    Password: string
    Role: UserRole
}

type RateStartDay =
    | First
    | Second
    
module String1000 =
    type String1000 = private String1000 of string
    
    let create (string1000: string) =
        if string1000.Length < 1 then
            Error("String must be at least 1 character")
        elif string1000.Length > 1000 then
            Error("String must be 1000 character or less")
        else
            Ok(String1000(string1000))
            
module Rate =
    type Rate = private Rate of int
    
    let create rate =
        if rate < 0 then
            Error("Rate can not be less than 0")
        else
            Ok(Rate(rate))
            
module RentDurationLimit =
    type RentDurationLimit = private RentDurationLimit of int
    
    let create rentDurationLimit =
        if rentDurationLimit < 1 then
            Error("Rent dureation limit can not be less than 1")
        else
            Ok(RentDurationLimit(rentDurationLimit))

type Equipment = {
    Id: Guid
    Name: String200.String200
    Details: String1000.String1000
    Rate: Rate.Rate
    RateStartDay: RateStartDay
    RentDurationLimit: RentDurationLimit.RentDurationLimit
}

module StartDate =
    type StartDate = private StartDate of DateTime
    
    let create (startDate: DateTime) =
            Ok(StartDate(startDate.Date))
            
module EndDate =
    type EndDate = private EndDate of DateTime
    
    let create (endDate: DateTime) =
        Ok(EndDate(endDate.Date))

type Reservation = {
    Id: Guid
    Reserver: User
    Equipment: Equipment
    StartDate: StartDate.StartDate
    EndDate: EndDate.EndDate
    Comment: String1000.String1000
    IsSettled: bool
}
