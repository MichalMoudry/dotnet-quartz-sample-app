module SampleAppQuartz.Domain.JobModel

open System

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
