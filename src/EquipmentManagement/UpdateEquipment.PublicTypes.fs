namespace EquipmentManagement.UpdateEquipment

open System
open EquipmentManagement.Common

type UnvalidatedEquipmentUpdate =
    { EquipmentId: Guid
      Name: string option
      Details: string option
      Rate: int option
      RateStartDay: int option
      RentDurationLimit: int option }

type EquipmentUpdated =
    { EquipmentId: EquipmentId
      Name: String200
      Details: String1000
      Rate: Rate
      RateStartDay: RateStartDay
      RentDurationLimit: RentDurationLimit }

type ValidationError = ValidationError of string

type UpdateEquipmentError = Validation of ValidationError

type UpdateEquipment =
    UnvalidatedEquipmentUpdate -> Result<EquipmentUpdated, UpdateEquipmentError>
