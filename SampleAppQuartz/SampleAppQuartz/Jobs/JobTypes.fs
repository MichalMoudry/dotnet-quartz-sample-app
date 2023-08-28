module SampleAppQuartz.Jobs.JobTypes

open System.Threading.Tasks
open Quartz
open SampleAppQuartz.Database
open System.Data
open System
open Quartz.Spi

type ISampleJob =
    inherit IJob
    abstract member Type: Model.JobType
    abstract member Name: string


/// A custom job factory that has DI capabilities.
type Factory(serviceProvider: IServiceProvider) =
    let _serviceProvider = serviceProvider
    interface IJobFactory with
        member this.NewJob(bundle: TriggerFiredBundle, scheduler: IScheduler) =
            Activator.CreateInstance(bundle.JobDetail.JobType) :?> IJob
        member this.ReturnJob(job: Quartz.IJob) =
            let disposable = job :?> IDisposable
            disposable.Dispose()


/// A simple job that says 'hello!'.
type HelloJob() =
    interface ISampleJob with
        member _.Execute _ =
            printfn "Hello!"
            Task.FromResult()
        member _.Type = Model.JobType.HelloJob
        member this.Name = nameof(this)


type SqliteInsertJob() =
    interface ISampleJob with
        member _.Execute _ =
            Task.FromResult()
        member _.Type = Model.JobType.SqliteInsertJob
        member this.Name = nameof(this)


type SqliteReadJob() =
    interface ISampleJob with
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
