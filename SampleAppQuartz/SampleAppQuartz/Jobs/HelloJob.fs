module SampleAppQuartz.Jobs.HelloJob

open System.Threading.Tasks
open Quartz

type HelloJob() =
    interface IJob with
        member this.Execute(context) =
            printfn "Hello!"
            Task.FromResult()
