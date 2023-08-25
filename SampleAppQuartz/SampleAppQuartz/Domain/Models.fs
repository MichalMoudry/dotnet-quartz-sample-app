module SampleAppQuartz.Domain.Models

open System

/// An abstract class representing an entity in the system.
[<AbstractClass>]
type Entity() =
    let id: Guid = Guid.NewGuid()
    member _.Id = id

/// An enumeration representing job's type.
type JobType =
    | HelloJob = 0
    | SqliteInsertJob = 1
    | SqliteReadJob = 2

/// A type representing a result of a specific job.
type JobResult = {
    Id: Guid
    JobName: string
    JobType: JobType
    StartDate: DateTime
    FinishDate: DateTime
}
