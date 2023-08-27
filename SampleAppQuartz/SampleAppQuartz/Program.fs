namespace SampleAppQuartz

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Quartz
open SampleAppQuartz.Database
open SampleAppQuartz.Jobs
open Dapper

module Program =
    open Microsoft.Data.Sqlite
    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                fun _ services ->
                (
                    services
                        .AddQuartz()
                        .AddQuartzHostedService(fun options -> options.WaitForJobsToComplete <- true)
                        |> ignore
                )
            )

    [<EntryPoint>]
    let main args =
        FSharp.SQLite.OptionTypes.register()
        let builder = createHostBuilder(args).Build()
        use connection = new SqliteConnection("Data Source=sample_app.db")
        connection.Open()

        connection.ExecuteAsync(Queries.createTablesQuery())
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore

        let schedulerFactory =
            builder
                .Services
                .GetRequiredService<ISchedulerFactory>()
        let scheduler =
            schedulerFactory.GetScheduler()
            |> Async.AwaitTask
            |> Async.RunSynchronously

        Scheduler.ScheduleJobs(scheduler) |> Async.RunSynchronously
        builder.Run()

        0 // exit code