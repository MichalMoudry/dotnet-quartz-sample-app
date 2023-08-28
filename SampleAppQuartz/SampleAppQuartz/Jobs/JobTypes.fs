module SampleAppQuartz.Jobs.JobTypes

open System.Threading.Tasks
open Quartz
open SampleAppQuartz.Database

type ISampleJob =
    inherit IJob
    abstract member Type: Model.JobType
    abstract member Name: string

type IDbSampleJob =
    inherit ISampleJob

/// A simple job that says 'hello!'.
type HelloJob() =
    interface ISampleJob with
        member _.Execute _ =
            printfn "Hello!"
            Task.FromResult()
        member _.Type = Model.JobType.HelloJob
        member this.Name = nameof(this)

type SqliteInsertJob() =
    interface IDbSampleJob with
        member this.Execute _ =
            Task.FromResult()
        member _.Type = Model.JobType.SqliteInsertJob
        member this.Name = nameof(this)

type SqliteReadJob() =
    interface IDbSampleJob with
        member this.Execute _ =
            Task.FromResult()
        member _.Type = Model.JobType.SqliteReadJob
        member this.Name = nameof(this)

/// Method for simplifying job building.
let BuildJob<'T when 'T :> IJob> name group =
    JobBuilder
        .Create<'T>()
        .WithIdentity(name, group)
        .Build()

/// Method for building a job that has access to DB.
let BuildJobWithDbContext<'T when 'T :> IDbSampleJob> name group conn =
    JobBuilder
        .Create<'T>()
        .WithIdentity(name, group)
        .Build()
