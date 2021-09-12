namespace EqMgr

open System
    
type UserRole =
    | User
    | Administrator

type User = {
    Id: Guid
    Name: String200
    PhoneNumber: PhoneNumber
    Password: string
    Role: UserRole
}

type RateStartDay =
    | First
    | Second
            
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
    Name: String200
    Details: String1000
    Rate: Rate.Rate
    RateStartDay: RateStartDay
    RentDurationLimit: RentDurationLimit.RentDurationLimit
}

type Reservation = {
    Id: Guid
    Reserver: User
    Equipment: Equipment
    StartDate: StartDate
    EndDate: EndDate
    Comment: String1000
    IsSettled: bool
}
