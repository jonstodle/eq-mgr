module internal EquipmentManagement.RegisterEquipment.InternalTypes

open System
open EquipmentManagement.Common
open EquipmentManagement.RentEquipment

type ValidatedRentRequest =
    { ReservationId: ReservationId
      UserId: UserId
      EquipmentId: EquipmentId
      StartDate: StartDate
      EndDate: EndDate }

type Reservation =
    { ReservationId: ReservationId
      UserId: UserId
      EquipmentId: EquipmentId
      StartDate: StartDate
      EndDate: EndDate }

type VerifyUserId = Guid -> bool

type VerifyEquipmentId = Guid -> bool

type ValidateRentRequest =
    VerifyUserId -> VerifyEquipmentId -> UnvalidatedRentRequest -> Result<ValidatedRentRequest, ValidationError>

type GetEquipmentRentDurationLimit = EquipmentId -> int

type VerifyTimeSlot = StartDate -> EndDate -> bool

type ConfirmRentPeriod =
    GetEquipmentRentDurationLimit -> VerifyTimeSlot -> ValidatedRentRequest -> Result<Reservation, ReservationError>

type CreateEvents = Reservation -> ReservationMade
