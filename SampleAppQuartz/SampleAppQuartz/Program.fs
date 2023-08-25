namespace SampleAppQuartz

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Quartz
open SampleAppQuartz.Jobs

module Program =
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
        Dapper.FSharp.SQLite.OptionTypes.register()
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
                .Create<JobTypes.HelloJob>()
                .WithIdentity("hello-job", "default")
                .Build()
        
        scheduler.ScheduleJob(helloJob, Triggers.HelloJobTrigger)
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore
        builder.Run()

        0 // exit code