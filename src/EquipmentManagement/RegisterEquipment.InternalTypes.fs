module internal EqipmentManagement.RegisterEqipment.InternalTypes

open EquipmentManagement.Common
open EquipmentManagement.RegisterEquipment

type ValidatedEquipment =
    { Id: EqipmentId
      Name: String200
      Details: String1000
      Rate: Rate
      RateStartDay: RateStartDay
      RentDurationLimit: RentDurationLimit }

type ValidateEquipment = UnvalidatedEquipment -> Result<ValidatedEquipment, ValidationError>

type CreateEvents = ValidatedEquipment -> EquipmentRegistered
