module SampleAppQuartz.Jobs.Scheduler

open Quartz

let ScheduleJobs (scheduler: IScheduler) =
    scheduler.ScheduleJob(
        JobTypes.BuildJob<JobTypes.HelloJob> "hello-job" "default",
        Triggers.HelloJobTrigger
    ) |> Async.AwaitTask |> Async.RunSynchronously |> ignore
    ()