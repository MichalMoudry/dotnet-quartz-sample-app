namespace SampleAppQuartz

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Quartz
open SampleAppQuartz.Database
open SampleAppQuartz.Jobs

module Program =
    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                fun _ services ->
                (
                    services
                        .AddQuartz() // fun i -> i.UseJobFactory<JobTypes.Factory>()
                        .AddQuartzHostedService(fun options -> options.WaitForJobsToComplete <- true)
                        |> ignore
                )
            )

    [<EntryPoint>]
    let main args =
        let builder = createHostBuilder(args).Build()
        let conn = Database.GetConnection()
        conn |> Database.SafeInit

        Database.GetInitializer(conn).InitJobResults()
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let schedulerFactory =
            builder
                .Services
                .GetRequiredService<ISchedulerFactory>()
        schedulerFactory.GetScheduler()
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> Scheduler.ScheduleJobs
            |> Async.RunSynchronously

        builder.Run()

        0 // exit code