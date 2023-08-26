module SampleAppQuartz.Jobs.Triggers

open Quartz
open System

/// Function for creating a new job trigger.
let CreateSimpleTrigger (name, group, startTime, schedule: SimpleScheduleBuilder -> unit) =
    let jobStartTime =
        match startTime with
        | Some(startTime) -> startTime
        | None -> DateTimeOffset.UtcNow
    TriggerBuilder
        .Create()
        .StartAt(jobStartTime)
        .WithSimpleSchedule(schedule)
        .WithIdentity(name, group)
        .Build()

/// A trigger for the hello job.
let HelloJobTrigger() =
    CreateSimpleTrigger(
        "hello-job-trigger",
        "default",
        None,
        fun i ->
            i.WithIntervalInSeconds(5)
                .RepeatForever()
                |> ignore
    )

/// A trigger for the Sqlite insert job.
let SqliteInsertJobTrigger() =
    CreateSimpleTrigger(
        "sqlite-insert-job-trigger",
        "database",
        None,
        fun i ->
            i.WithIntervalInMinutes(1)
                .RepeatForever()
                |> ignore
    )
