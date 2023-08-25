module SampleAppQuartz.Jobs.Triggers

open Quartz

/// A trigger for the hello job.
let HelloJobTrigger =
    TriggerBuilder
        .Create()
        .WithIdentity("hello-job-trigger", "default")
        .StartNow()
        .WithSimpleSchedule(fun i ->
            i.WithIntervalInSeconds(5)
                .RepeatForever()
                |> ignore
        )
        .Build()
