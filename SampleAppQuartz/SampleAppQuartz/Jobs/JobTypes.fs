module SampleAppQuartz.Jobs.JobTypes

open System
open System.Threading.Tasks
open Quartz
open SampleAppQuartz.Domain

/// An abstract class representing an entity in the system.
[<AbstractClass>]
type Entity() =
    let id: Guid = Guid.NewGuid()
    member this.Id = id

type ISampleJob =
    inherit IJob
    abstract member Type: JobModel.JobType
    abstract member Name: string

/// A simple job that says 'hello!'.
type HelloJob() =
    inherit Entity()
    interface ISampleJob with
        member this.Execute _ =
            printfn "Hello!"
            Task.FromResult()
        member this.Type = JobModel.JobType.HelloJob
        member this.Name = nameof(this)

type SqliteInsertJob() =
    inherit Entity()
    interface ISampleJob with
        member this.Execute _ =
            Task.FromResult()
        member this.Type = JobModel.JobType.SqliteInsertJob
        member this.Name = nameof(this)

type SqliteReadJob() =
    inherit Entity()
    interface ISampleJob with
        member this.Execute _ =
            Task.FromResult()
        member this.Type = JobModel.JobType.SqliteReadJob
        member this.Name = nameof(this)
