namespace EquipmentManagement.RentEquipment

open System
open EquipmentManagement.Common

type UnvalidatedRentRequest =
    { UserId: Guid
      EquipmentId: Guid
      StartDate: DateTime
      EndDate: DateTime }

type ReservationMade =
    { ReservationId: ReservationId
      UserId: UserId
      EquipmentId: EquipmentId
      StartDate: StartDate
      EndDate: EndDate }

type ValidationError = ValidationError of string

type ReservationError =
    | RentDurationExceedsLimit
    | TimeSlotNotAvailable

type RentEquipmentError =
    | Validation of ValidationError
    | Reservation of ReservationError

type RentEquipment = UnvalidatedRentRequest -> Result<ReservationMade, RentEquipmentError>
