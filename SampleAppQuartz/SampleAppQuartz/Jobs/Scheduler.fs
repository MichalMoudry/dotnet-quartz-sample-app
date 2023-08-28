module SampleAppQuartz.Jobs.Scheduler

open System.Data
open Quartz

/// A method for scheduling all the sample jobs.
let ScheduleJobs (scheduler: IScheduler, conn: IDbConnection) =
    async {
        scheduler.ScheduleJob(
            JobTypes.BuildJob<JobTypes.HelloJob> "hello-job" "default",
            Triggers.HelloJobTrigger()
        ) |> Async.AwaitTask |> ignore

        scheduler.ScheduleJob(
            JobTypes.BuildJobWithDbContext<JobTypes.SqliteInsertJob>
                "sqlite-insert-job" "database" conn,
            Triggers.SqliteInsertJobTrigger()
        ) |> Async.AwaitTask |> ignore
    }