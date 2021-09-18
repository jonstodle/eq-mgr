module internal EqipmentManagement.RegisterEqipment.Implementation

open EqipmentManagement.RegisterEqipment.InternalTypes
open EquipmentManagement.Common
open EquipmentManagement.RegisterEquipment

let failIfInvalid result =
    match result with
    | Ok value -> value
    | Error (ValidationError msg) -> failwith msg

let validateEqipment: ValidateEquipment =
    fun unvalidatedEquipment ->
        let id = EqipmentId.create ()

        let name =
            String200.create unvalidatedEquipment.Name
            |> Result.mapError ValidationError
            |> failIfInvalid

        let details =
            String1000.create unvalidatedEquipment.Details
            |> Result.mapError ValidationError
            |> failIfInvalid

        let rate =
            Rate.create unvalidatedEquipment.Rate
            |> Result.mapError ValidationError
            |> failIfInvalid

        let rateStartDay =
            RateStartDay.create unvalidatedEquipment.Rate
            |> Result.mapError ValidationError
            |> failIfInvalid

        let rentDurationLimit =
            RentDurationLimit.create unvalidatedEquipment.RentDurationLimit
            |> Result.mapError ValidationError
            |> failIfInvalid

        let validatedEquipment: ValidatedEquipment =
            { Id = id
              Name = name
              Details = details
              Rate = rate
              RateStartDay = rateStartDay
              RentDurationLimit = rentDurationLimit }

        Ok(validatedEquipment)

let createEvents: CreateEvents =
    fun validatedEquipment ->
        let equipmentRegistered: EquipmentRegistered =
            { Id = validatedEquipment.Id
              Name = validatedEquipment.Name
              Details = validatedEquipment.Details
              Rate = validatedEquipment.Rate
              RateStartDay = validatedEquipment.RateStartDay
              RentDurationLimit = validatedEquipment.RentDurationLimit }

        equipmentRegistered

let registerEquipment: RegisterEquipment =
    fun unvalidatedEquipment ->
        unvalidatedEquipment
        |> validateEqipment
        |> Result.mapError Validation
        |> Result.map createEvents
