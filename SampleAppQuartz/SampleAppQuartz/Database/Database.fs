module SampleAppQuartz.Database.Database

open Dapper
open System.Data
open System.Threading.Tasks
open Microsoft.Data.Sqlite

let mutable isAlreadyInitialized = false

let GetConnection() =
    new SqliteConnection("Data Source=sample_app.db")
   
let SafeInit(conn: IDbConnection) =
    task {
        if isAlreadyInitialized |> not then
            conn.Open()
            isAlreadyInitialized <- true
            Dapper.FSharp.SQLite.OptionTypes.register()
    }
    |> Async.AwaitTask
    |> Async.RunSynchronously

type IInitializer =
    abstract member InitJobResults: unit -> Task<unit>

let GetInitializer(conn: IDbConnection) =
    {
        new IInitializer with
            member _.InitJobResults () =
                task {
                    Queries.CreateTablesQuery() |> conn.ExecuteAsync |> ignore
                    return ()
                }
    }
