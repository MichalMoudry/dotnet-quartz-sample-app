module SampleAppQuartz.Jobs.Scheduler

open Quartz

/// A method for scheduling all the sample jobs.
let ScheduleJobs (scheduler: IScheduler) =
    async {
        scheduler.ScheduleJob(
            JobTypes.BuildJob<JobTypes.HelloJob> "hello-job" "default",
            Triggers.HelloJobTrigger()
        ) |> Async.AwaitTask |> ignore

        scheduler.ScheduleJob(
            JobTypes.BuildJob<JobTypes.SqliteInsertJob> "sqlite-insert-job" "database",
            Triggers.SqliteInsertJobTrigger()
        ) |> Async.AwaitTask |> ignore
    }