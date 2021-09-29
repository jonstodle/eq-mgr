module internal EquipmentManager.UpdateEquipment.Implementation

open EquipmentManagement.Common
open EquipmentManagement.UpdateEquipment
open EquipmentManager.UpdateEquipment.InternalTypes

let okIfTrue f (errorMessage: string) value =
    if (f value) then
        Ok(value)
    else
        Error(errorMessage)

let failIfInvalid result =
    match result with
    | Ok value -> value
    | Error (ValidationError msg) -> failwith msg

let validateEquipmentUpdate: ValidateEquipmentUpdate =
    fun validateEquipmentId unvalidatedEquipmentUpdate ->
        let validateEquipmentId equipmentId =
            equipmentId
            |> okIfTrue validateEquipmentId "Invalid Equipment ID"
            |> Result.mapError ValidationError

        let equipmentId =
            unvalidatedEquipmentUpdate.EquipmentId
            |> string
            |> EquipmentId.create
            |> Result.mapError ValidationError
            |> Result.bind validateEquipmentId
            |> failIfInvalid

        let validateOption validateFn value =
            match value with
            | Some v -> v |> validateFn |> Result.map Some
            | None -> None |> Ok

        let name =
            unvalidatedEquipmentUpdate.Name
            |> validateOption String200.create
            |> Result.mapError ValidationError
            |> failIfInvalid

        let details =
            unvalidatedEquipmentUpdate.Details
            |> validateOption String1000.create
            |> Result.mapError ValidationError
            |> failIfInvalid

        let rate =
            unvalidatedEquipmentUpdate.Rate
            |> validateOption Rate.create
            |> Result.mapError ValidationError
            |> failIfInvalid

        let rateStartDay =
            unvalidatedEquipmentUpdate.RateStartDay
            |> validateOption RateStartDay.create
            |> Result.mapError ValidationError
            |> failIfInvalid

        let rentDurationLimit =
            unvalidatedEquipmentUpdate.RentDurationLimit
            |> validateOption RentDurationLimit.create
            |> Result.mapError ValidationError
            |> failIfInvalid

        let validatedEquipmentUpdate: ValidatedEquipmentUpdate =
            { EquipmentId = equipmentId
              Name = name
              Details = details
              Rate = rate
              RateStartDay = rateStartDay
              RentDurationLimit = rentDurationLimit }

        Ok(validatedEquipmentUpdate)

let optionallyUpdateEquipment: OptionallyUpdateEquipment =
    fun loadEquipment validatedEquipmentUpdate ->
        let existingEquipment =
            validatedEquipmentUpdate.EquipmentId
            |> loadEquipment

        let updatedEquipment: UpdatedEquipment =
            { EquipmentId = validatedEquipmentUpdate.EquipmentId
              Name =
                  validatedEquipmentUpdate.Name
                  |> Option.defaultValue existingEquipment.Name
              Details =
                  validatedEquipmentUpdate.Details
                  |> Option.defaultValue existingEquipment.Details
              Rate =
                  validatedEquipmentUpdate.Rate
                  |> Option.defaultValue existingEquipment.Rate
              RateStartDay =
                  validatedEquipmentUpdate.RateStartDay
                  |> Option.defaultValue existingEquipment.RateStartDay
              RentDurationLimit =
                  validatedEquipmentUpdate.RentDurationLimit
                  |> Option.defaultValue existingEquipment.RentDurationLimit }

        updatedEquipment

let createEvents: CreateEvents =
    fun updatedEquipment ->
        let equipmentUpdated: EquipmentUpdated =
            { EquipmentId = updatedEquipment.EquipmentId
              Name = updatedEquipment.Name
              Details = updatedEquipment.Details
              Rate = updatedEquipment.Rate
              RateStartDay = updatedEquipment.RateStartDay
              RentDurationLimit = updatedEquipment.RentDurationLimit }

        equipmentUpdated

let updateEquipment validateEquipmentId loadEquipment : UpdateEquipment =
    fun unvalidatedEquipmentUpdate ->
        unvalidatedEquipmentUpdate
        |> validateEquipmentUpdate validateEquipmentId
        |> Result.mapError Validation
        |> Result.map
            (fun validatedEquipment ->
                validatedEquipment
                |> optionallyUpdateEquipment loadEquipment)
        |> Result.map createEvents
