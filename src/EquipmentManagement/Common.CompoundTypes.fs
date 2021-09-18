namespace EquipmentManagement.Common

open System

type Equipment = {
    Id: Guid
    Name: String200
    Details: String1000
    Rate: Rate
    RateStartDay: RateStartDay
    RentDurationLimit: RentDurationLimit
}

type Reservation = {
    Id: Guid
    Reserver: UserId
    Equipment: Equipment
    StartDate: StartDate
    EndDate: EndDate
    Comment: String1000
    IsSettled: bool
}
