namespace EquipmentManagement.Common

open System
open EquipmentManagement.Common

type EqipmentId = private EqipmentId of Guid

type String200 = private String200 of string

type String1000 = private String1000 of string
    
type PhoneNumber = private PhoneNumber of string
    
type Rate = private Rate of int
    
type RentDurationLimit = private RentDurationLimit of int

type RateStartDay =
    | First
    | Second
    
type StartDate = private StartDate of DateTime
    
type EndDate = private EndDate of DateTime

type UserId = private UserId of Guid

module EqipmentId =
    let value (EqipmentId value) = value
    
    
    let create () = EqipmentId (Guid.NewGuid())

module String200 =
    let value (String200 value) = value
    
    let create (string200: string) =
        if String.IsNullOrEmpty(string200) then
            Error("String must be at least 1 character")
        elif string200.Length > 200 then
            Error("String must be 200 characters or less")
        else
            Ok(String200(string200))
    
module String1000 =
    let value (String1000 value) = value
    
    let create (string1000: string) =
        if String.IsNullOrEmpty(string1000) then
            Error("String must be at least 1 character")
        elif string1000.Length > 1000 then
            Error("String must be 1000 character or less")
        else
            Ok(String1000(string1000))
            
module PhoneNumber =
    let value (PhoneNumber value) = value
    
    let create (phoneNumber: string) =
        if phoneNumber.Length <> 8 then
            Error("Phone number must be 8 digits")
        elif phoneNumber.ToCharArray() |> Array.forall Char.IsDigit |> not then
            Error("Phone number can only contain digits")
        else
            Ok(PhoneNumber(phoneNumber))
            
module Rate =
    let value (Rate value) = value
    
    let create rate =
        if rate < 0 then
            Error("Rate can not be less than 0")
        else
            Ok(Rate(rate))
            
module RentDurationLimit =
    let value (RentDurationLimit value) = value
    
    let create rentDurationLimit =
        if rentDurationLimit < 1 then
            Error("Rent dureation limit can not be less than 1")
        else
            Ok(RentDurationLimit(rentDurationLimit))
            
module RateStartDay =
    let create rateStartDay =
        match rateStartDay with
        | 1 -> Ok(RateStartDay.First)
        | 2 -> Ok(RateStartDay.Second)
        | _ -> failwith "Rate start day must be 1 or 2"
            

module StartDate =
    let value (StartDate value) = value
    
    let create (startDate: DateTime) =
            Ok(StartDate(startDate.Date))
            
module EndDate =
    let value (EndDate value) = value
    
    let create (startDate: StartDate) (endDate: DateTime) =
        if endDate < (startDate |> StartDate.value) then
            Error("End date must be the same as or after start date")
        else
            Ok(EndDate(endDate.Date))

module UserId =
    let value (UserId value) = value
    
    let create (userId: string) =
        try
            let parsed = Guid.Parse(userId)
            Ok(UserId parsed)
        with
        | :? FormatException ->  Error("User id must be a valid GUID")
