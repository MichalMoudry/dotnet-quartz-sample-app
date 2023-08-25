namespace SampleAppQuartz

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Quartz
open SampleAppQuartz.Jobs.HelloJob

module Program =
    (*let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(fun hostContext services ->
                services.AddHostedService<Worker>() |> ignore)*)

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                fun ctx services ->
                (
                    services
                        .AddQuartz()
                        .AddQuartzHostedService(fun options -> options.WaitForJobsToComplete <- true)
                        |> ignore
                )
            )

    [<EntryPoint>]
    let main args =
        let builder = createHostBuilder(args).Build()
        
        let schedulerFactory =
            builder.Services
                .GetRequiredService<ISchedulerFactory>()
        let scheduler =
            schedulerFactory.GetScheduler()
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let helloJob =
            JobBuilder
                .Create<HelloJob>()
                .WithIdentity("hello-job", "default")
                .Build()
        let defaultTrigger =
            TriggerBuilder
                .Create()
                .WithIdentity("default-trigger", "default")
                .StartNow()
                .WithSimpleSchedule(fun i ->
                    i.WithIntervalInSeconds(5).RepeatForever() |> ignore
                )
                .Build()
        
        scheduler.ScheduleJob(helloJob, defaultTrigger)
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore
        builder.Run()

        0 // exit code