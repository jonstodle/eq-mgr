module internal EquipmentManager.UpdateEquipment.InternalTypes

open EquipmentManagement.Common
open EquipmentManagement.UpdateEquipment

type ValidatedEquipmentUpdate =
    { EquipmentId: EquipmentId
      Name: String200 option
      Details: String1000 option
      Rate: Rate option
      RateStartDay: RateStartDay option
      RentDurationLimit: RentDurationLimit option }
    
type ValidateEquipmentId =
    EquipmentId -> bool

type ValidateEquipmentUpdate =
    ValidateEquipmentId -> UnvalidatedEquipmentUpdate -> Result<ValidatedEquipmentUpdate, ValidationError>
    
type LoadEquipment =
    EquipmentId -> Equipment
    
type UpdatedEquipment =
    { EquipmentId: EquipmentId
      Name: String200
      Details: String1000
      Rate: Rate
      RateStartDay: RateStartDay
      RentDurationLimit: RentDurationLimit }

type OptionallyUpdateEquipment =
    LoadEquipment -> ValidatedEquipmentUpdate -> UpdatedEquipment
    
type CreateEvents =
    UpdatedEquipment -> EquipmentUpdated
