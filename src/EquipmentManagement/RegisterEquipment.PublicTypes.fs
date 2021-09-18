namespace EquipmentManagement.RegisterEquipment

open EquipmentManagement.Common

type UnvalidatedEquipment =
    { Name: string
      Details: string
      Rate: int
      RateStartDay: int
      RentDurationLimit: int }

type EquipmentRegistered =
    { Id: EquipmentId
      Name: String200
      Details: String1000
      Rate: Rate
      RateStartDay: RateStartDay
      RentDurationLimit: RentDurationLimit }

type ValidationError = ValidationError of string

type RegisterEquipmentError = Validation of ValidationError

type RegisterEquipment = UnvalidatedEquipment -> Result<EquipmentRegistered, RegisterEquipmentError>
