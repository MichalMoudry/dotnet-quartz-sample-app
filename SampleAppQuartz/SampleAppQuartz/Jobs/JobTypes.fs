module SampleAppQuartz.Jobs.JobTypes

open System.Threading.Tasks
open Quartz
open SampleAppQuartz.Domain.Models

type ISampleJob =
    inherit IJob
    abstract member Type: JobType
    abstract member Name: string

/// A simple job that says 'hello!'.
type HelloJob() =
    interface ISampleJob with
        member _.Execute _ =
            printfn "Hello!"
            Task.FromResult()
        member _.Type = JobType.HelloJob
        member this.Name = nameof(this)

type SqliteInsertJob() =
    interface ISampleJob with
        member this.Execute _ =
            Task.FromResult()
        member _.Type = JobType.SqliteInsertJob
        member this.Name = nameof(this)

type SqliteReadJob() =
    interface ISampleJob with
        member this.Execute _ =
            Task.FromResult()
        member _.Type = JobType.SqliteReadJob
        member this.Name = nameof(this)

/// Method for simplifying job building.
let BuildJob<'T when 'T :> IJob> name group =
    JobBuilder
        .Create<'T>()
        .WithIdentity(name, group)
        .Build()
