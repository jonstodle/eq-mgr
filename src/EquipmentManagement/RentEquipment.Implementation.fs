module internal EquipmentManagement.RegisterEquipment.Implementation

open EquipmentManagement.Common
open EquipmentManagement.RegisterEquipment.InternalTypes
open EquipmentManagement.RentEquipment

let failIfInvalid result =
    match result with
    | Ok value -> value
    | Error (ValidationError msg) -> failwith msg

let validateRentRequest: ValidateRentRequest =
    fun verifyUserId verifyEquipmentId unvalidatedRentRequest ->
        let reservationId = ReservationId.generate ()

        let verifyUserId userId =
            if verifyUserId userId then
                Ok(userId)
            else
                Error("User ID not found")

        let createUserId userId =
            userId
            |> UserId.create
            |> Result.mapError ValidationError

        let userId =
            unvalidatedRentRequest.UserId
            |> verifyUserId
            |> Result.mapError ValidationError
            |> Result.map string
            |> Result.bind createUserId
            |> failIfInvalid

        let verifyEquipmentId equipmentId =
            if verifyEquipmentId equipmentId then
                Ok(equipmentId)
            else
                Error("Equipment ID not found")

        let createEquipmentId equipmentId =
            equipmentId
            |> EquipmentId.create
            |> Result.mapError ValidationError

        let equipmentId =
            unvalidatedRentRequest.EquipmentId
            |> verifyEquipmentId
            |> Result.mapError ValidationError
            |> Result.map string
            |> Result.bind createEquipmentId
            |> failIfInvalid

        let startDate =
            unvalidatedRentRequest.StartDate
            |> StartDate.create
            |> Result.mapError ValidationError
            |> failIfInvalid

        let endDate =
            unvalidatedRentRequest.EndDate
            |> EndDate.create startDate
            |> Result.mapError ValidationError
            |> failIfInvalid

        let validatedRentRequest: ValidatedRentRequest =
            { ReservationId = reservationId
              UserId = userId
              EquipmentId = equipmentId
              StartDate = startDate
              EndDate = endDate }

        Ok(validatedRentRequest)

let confirmRentPeriod: ConfirmRentPeriod =
    fun getEquipmentRentDurationLimit verifyTimeSlot validatedRentRequest ->
        let validateRentDuration startDate endDate limit =
            let startDate = startDate |> StartDate.value

            let endDate = endDate |> EndDate.value

            let rentPeriod = endDate - startDate

            rentPeriod.Days <= limit

        let rentDurationLimitNotExceeded =
            validatedRentRequest.EquipmentId
            |> getEquipmentRentDurationLimit
            |> validateRentDuration validatedRentRequest.StartDate validatedRentRequest.EndDate

        let timeSlotAvailable =
            verifyTimeSlot validatedRentRequest.StartDate validatedRentRequest.EndDate

        match (rentDurationLimitNotExceeded, timeSlotAvailable) with
        | true, true ->
            let reservation: Reservation =
                { ReservationId = validatedRentRequest.ReservationId
                  UserId = validatedRentRequest.UserId
                  EquipmentId = validatedRentRequest.EquipmentId
                  StartDate = validatedRentRequest.StartDate
                  EndDate = validatedRentRequest.EndDate }

            Ok(reservation)
        | false, _ -> Error(RentDurationExceedsLimit)
        | _, false -> Error(TimeSlotNotAvailable)

let createEvents: CreateEvents =
    fun reservation ->
        let reservationMade: ReservationMade =
            { ReservationId = reservation.ReservationId
              UserId = reservation.UserId
              EquipmentId = reservation.EquipmentId
              StartDate = reservation.StartDate
              EndDate = reservation.EndDate }

        reservationMade

let rentEquipment verifyUserId verifyEquipmentId getEquipmentRentDurationLimit verifyTimeSlot : RentEquipment =
    fun unvalidatedRentRequest ->
        unvalidatedRentRequest
        |> validateRentRequest verifyUserId verifyEquipmentId
        |> Result.mapError Validation
        |> Result.bind
            (fun verifiedRentRequest ->
                verifiedRentRequest
                |> confirmRentPeriod getEquipmentRentDurationLimit verifyTimeSlot
                |> Result.mapError Reservation)
        |> Result.map createEvents
