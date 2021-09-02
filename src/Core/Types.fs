module Core.Types

open System

type UserRole =
    | User
    | Administrator

type User = {
    Id: Guid
    Name: string
    PhoneNumber: string
    Password: string
    Role: UserRole
}

type RateStartDay =
    | First
    | Second

type Equipment = {
    Id: Guid
    Name: string
    Details: string
    Rate: int
    RateStartDay: RateStartDay
    RentDurationLimit: int
}

type Reservation = {
    Id: Guid
    Reserver: User
    Equipment: Equipment
    StartDate: DateTime
    EndDate: DateTime
    Comment: string
    IsSettled: bool
}
